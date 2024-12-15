using System.ComponentModel;

namespace ServerCommand
{
    public sealed class Config
    {
        [Description("阴间玩家全局聊天")]
        public bool SpeactorChat {  get; set; } = false;
        [Description("禁言玩家聊天")]
        public bool MutedChat { get; set; } = false;
        [Description("禁言可求助管理")]
        public bool MutedAdminChat { get; set; } = true;
    }
}
