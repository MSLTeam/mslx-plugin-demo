using MSLX.SDK;

namespace MSLX.Plugin.Demo;

public class MSLXPluginEntry : IPlugin
{
    public string Id => "mslx-plugin-demo"; 
    public string Name => "MSLX 示例插件";
    public string Description => "这是MSLX的示例插件，简单演示了一些插件可实现的功能，可以前往Github查看具体实现~";
    public string Version => "1.0.3";
    public string Icon => "icon.png";
    public string MinSDKVersion => "1.4.0";
    public string Developer => "luluxiaoyu";
    public string AuthorUrl => "https://mslx.mslmc.cn/plugin-dev/init/start/";
    public string PluginUrl => "https://github.com/MSLTeam/mslx-plugin-demo";

    public void OnLoad()
    {
        SDK.MSLX.Logger.Info("mslx-plugin-demo 载入成功~");
        SDK.MSLX.Logger.Info("当前存在实例数量：" + SDK.MSLX.Config.Servers.GetServerList().Count.ToString());
    }

    public void OnUnload() {
        SDK.MSLX.Logger.Info("mslx-plugin-demo 卸载成功~");
    }
}