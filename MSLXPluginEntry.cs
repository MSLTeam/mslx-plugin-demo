using MSLX.SDK;

namespace MSLX.Plugin.Demo;

public class MSLXPluginEntry : IPlugin
{
    public string Id => "mslx-plugin-demo"; 
    public string Name => "MSLX 示例插件";
    public string Description => "MSLX 示例插件";
    public string Version => "1.0.2";
    public string MinLoaderVersion => "1.3.8.2";
    public string Developer => "xiaoyu";
    public string AuthorUrl => "https://github.com/MSLTeam";
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