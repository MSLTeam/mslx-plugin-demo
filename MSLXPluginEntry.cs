using MSLX.SDK;

namespace MSLX.Plugin.Demo;

public class MSLXPluginEntry : IPlugin
{
    public string Id => "mslx-plugin-demo"; 
    public string Name => "MSLX 示例插件";
    public string Description => "这是MSLX的示例插件，简单演示了一些插件可实现的功能，可以前往Github查看具体实现~";
    public string Version => "1.1.0";
    public string Icon => "icon.png";
    public string MinSDKVersion => "1.4.0";
    public string Developer => "luluxiaoyu";
    public string AuthorUrl => "https://mslx.mslmc.cn/plugin-dev/init/start/";
    public string PluginUrl => "https://github.com/MSLTeam/mslx-plugin-demo";

    public async void OnLoad()
    {
        SDK.MSLX.Logger.Info("mslx-plugin-demo 载入成功~");
        SDK.MSLX.Logger.Info("当前存在实例数量：" + SDK.MSLX.Config.Servers.GetServerList().Count.ToString());
        
        // ===== 配置读写示例 =====
        string dataDir = this.Config().GetDataPath();
        SDK.MSLX.Logger.Info("使用的数据目录："+dataDir);
        
        this.Config().WriteConfigKey("author", "xiaoyu");
        this.Config().WriteConfigKey("magicNumber", 1027);
        
        int count = (int?)this.Config().ReadConfigKey("magicNumber") ?? 0;
        SDK.MSLX.Logger.Info("从配置文件读取magicNumber：" + count.ToString());
        
        var allConfig = this.Config().ReadConfig();
        
        // ===== 下载器调用示例 ===== 
        SDK.MSLX.Logger.Info("准备下载文件...");
        string targetPath = Path.Combine(this.Config().GetDataPath(), "server.jar");
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
}