using CommandSystem;
using PluginAPI.Core;
using RemoteAdmin;
using System;
using System.Linq;

namespace ServerCommand.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class AdminChat : ICommand
    {
        public string Command { get; } = "ac";
        public string[] Aliases { get; } = new string[1] { "ac" };
        public string Description { get; } = "呼叫管理员";
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (sender is PlayerCommandSender playerCommandSender)
            {
                Player player = Player.Get(playerCommandSender.PlayerId);
                if (player == null || !player.IsReady)
                {
                    response = "<color=red>你还没加入服务器！</color>";
                    return false;
                }
                if (arguments.Count < 0 || arguments == null)
                {
                    response = "<color=red>信息不能为空！</color>";
                    return false;
                }
                if (player.IsMuted && Plugin.Config.MutedChat)
                {
                    response = "<color=red>你已被禁言 无法聊天！</color>";
                    return false;
                }
                if (player.RemoteAdminAccess)
                {
                    response = "<color=red>管理不能进行聊天!</color>";
                    return false;
                }
                if (Player.GetPlayers().Where(x => x.RemoteAdminAccess).Count() == 0)
                {
                    Log.Info($"[未响应呼叫][{player.Nickname}][{player.UserId}]:[{string.Join(" ", arguments)}]");
                    response = "<color=red>服务器暂无管理员!</color>";
                    return false;
                }
                foreach (Player AdminPlayer in Player.GetPlayers())
                {
                    if (AdminPlayer.RemoteAdminAccess == true)
                    {
                        AdminPlayer.SendBroadcast($"<size=55%><color=#00FFFF>[管理呼叫]</color> {player.Nickname} : {string.Join(" ", arguments)}</size>", 5);
                    }
                }
                Log.Info($"[响应呼叫][{player.Nickname}][{player.UserId}]:[{string.Join(" ", arguments)}]");
                response = "<color=green>信息已发送!</color>";
                return true;
            }
            response = "<color=green>信息已发送!</color>";
            return true;
        }
    }
}
