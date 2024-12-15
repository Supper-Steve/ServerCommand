using PluginAPI.Core;
using PluginAPI.Core.Attributes;

namespace ServerCommand
{
    public class Plugin
    {
        [PluginConfig]
        public static Config Config;
        [PluginEntryPoint("ServerCommand", "2.1", "ServerCommand", "史蒂夫")]
        public void Load() 
        {
            Log.Info("加载服务器指令插件! By 史蒂夫");
            Log.Info("如果你在某鱼或者其他平台上买到了这个插件,请立刻退款并举报");
            Log.Info("https://github.com/Supper-Steve/InfAmmo");
        }
    }
}
