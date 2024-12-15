using PluginAPI.Core;
using PluginAPI.Core.Attributes;

namespace ServerCommand
{
    public class Plugin
    {
        [PluginConfig]
        public static Config Config;
        [PluginEntryPoint("ServerCommand","2.1","ServerCommand","史蒂夫")]
        public void Load() => Log.Info("加载服务器指令插件!");
    }
}
