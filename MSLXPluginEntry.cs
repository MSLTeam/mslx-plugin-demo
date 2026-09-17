using Microsoft.AspNetCore.Mvc.ApplicationParts;
using MSLX.SDK;
using MSLX.SDK.Interfaces;
using Newtonsoft.Json.Linq;

[assembly: ApplicationPart("MSLX.Plugin.Demo")]

namespace MSLX.Plugin.Demo;

public class MSLXPluginEntry : IPlugin
{
    public static MSLXPluginEntry Instance { get; private set; } = null!;
    public string Id => "mslx-plugin-demo"; 
    public string Name => "MSLX 示例插件";
    public string Description => "这是MSLX的示例插件，简单演示了一些插件可实现的功能，可以前往Github查看具体实现~";
    public string Version => "1.1.0";
    public string Icon => "icon.png";
    public string MinSDKVersion => "1.6.4";
    public string Developer => "luluxiaoyu";
    public string AuthorUrl => "https://mslx.mslmc.cn/plugin-dev/init/start/";
    public string PluginUrl => "https://github.com/MSLTeam/mslx-plugin-demo";

    public async void OnPluginInitialize(IServiceProvider serviceProvider)
    {
        Instance = this;
    }

    public async void OnLoad()
    {
        SDK.MSLX.Logger.Info("mslx-plugin-demo 载入成功~");
        SDK.MSLX.Logger.Info("当前存在实例数量：" + SDK.MSLX.Config.Servers.GetServerList().Count.ToString());

        // ===== 事件钩子示例 (v1.6.4+) =====
        // 1. 监听服务器实例启动完成事件
        SDK.MSLX.Events.OnServerStarted += (sender, e) =>
        {
            SDK.MSLX.Logger.Info($"[事件监听] 实例 [{e.InstanceId}] 启动成功，PID: {e.ProcessId}");
        };

        // 2. 监听服务器实例停止事件
        SDK.MSLX.Events.OnServerStopped += (sender, e) =>
        {
            SDK.MSLX.Logger.Info($"[事件监听] 实例 [{e.InstanceId}] 已停止，退出码: {e.ExitCode}");
        };

        // ===== 配置读写示例 =====
        string dataDir = MSLXPluginEntry.Instance.Config().GetDataPath();
        SDK.MSLX.Logger.Info("使用的数据目录：" + dataDir);

        MSLXPluginEntry.Instance.Config().WriteConfigKey("author", "xiaoyu");
        MSLXPluginEntry.Instance.Config().WriteConfigKey("magicNumber", 1027);

        int count = (int?)MSLXPluginEntry.Instance.Config().ReadConfigKey("magicNumber") ?? 0;
        SDK.MSLX.Logger.Info("从配置文件读取magicNumber：" + count.ToString());

        var allConfig = MSLXPluginEntry.Instance.Config().ReadConfig();


        // get请求示例
        var response = await SDK.MSLX.Http.GetAsync("https://api.mslmc.cn/v3/query/notice?query=id");

        if (response.IsSuccessStatusCode)
        {
            JObject jobj = JObject.Parse(response.Content ?? "{}");
            string content = jobj["data"]?["noticeID"]?.ToString() ?? "";

            SDK.MSLX.Logger.Info($"获取到的MSL公告编号: {content}");
        }

        // post
        /***
        var postResponse = await SDK.MSLX.Http.PostAsync(
            "https://example.cn/post-api",
            PluginHttpContentType.Json,
            new { username = "admin", action = "start" }
        ); ***/


        // ===== 下载器调用示例 ===== 
        SDK.MSLX.Logger.Info("准备下载文件...");
        string targetPath = Path.Combine(MSLXPluginEntry.Instance.Config().GetDataPath(), "server.jar");
        if (File.Exists(targetPath))
        {
            return;
        }

        var result = await SDK.MSLX.Downloader.DownloadFileAsync(
            "https://bmclapi2.bangbang93.com/forge/download?mcversion=26.1.2&version=64.0.8&category=installer&format=jar",
            targetPath,
            (progress, speed) =>
            {
                SDK.MSLX.Logger.Debug($"\r下载中: {progress:0.0}% [{speed}]");
            });

        if (result.Success)
        {
            SDK.MSLX.Logger.Info("下载完成，可以开始搞事情了！");
        }
        else
        {
            SDK.MSLX.Logger.Error($"下载失败: {result.ErrorMessage}");
        }
    }


    public void OnUnload() {
        SDK.MSLX.Logger.Info("mslx-plugin-demo 卸载成功~");
    }

    public void OnRegisterEndpoints(IEndpointRouteBuilder endpoints)
    {
        // 可以在这里注册高级路由
        // endpoints.MapHub<Hub>("/api/hubs/plugins/mslx-plugin-demo/demo");
    }

    public void OnRegisterServices(IServiceCollection services)
    {
        // 可以在这里注册服务
        // services.AddSingleton<DemoManager>();
    }
}