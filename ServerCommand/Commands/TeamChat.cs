using CommandSystem;
using PluginAPI.Core;
using RemoteAdmin;
using System;

namespace ServerCommand.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class TeamChat : ICommand
    {
        public string Command { get; } = "cc";
        public string[] Aliases { get; } = new string[] { "cc","teamchat", "cchat" };
        public string Description { get; } = "阵营聊天";
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
                    return true;
                }
                if (!Round.IsRoundStarted)
                {
                    response = "<color=red>回合未开始，不能使用阵营聊天！</color>";
                    return true;
                }
                Log.Info($"[阵营聊天][{player.Nickname}][{player.UserId}]:[{string.Join(" ", arguments)}]");
                foreach (Player player1 in Player.GetPlayers())
                {
                    if (player1.GetTeam(player))
                    {
                        player1.SendBroadcast($"<size=55%>[阵营聊天] {player.PlayerRole()} {player.Nickname} : {string.Join(" ", arguments)}</size>", 3);
                        player1.SendConsoleMessage($"[阵营聊天] {player.PlayerRole()} {player.Nickname} : {string.Join(" ", arguments)}</size>", "green");
                    }
                }
            }
            response = "<color=green>信息已发送!</color>";
            return true;
        }
    }
}
