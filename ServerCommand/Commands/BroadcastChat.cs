using CommandSystem;
using PlayerRoles;
using PluginAPI.Core;
using RemoteAdmin;
using System;

namespace ServerCommand.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class BroadcastChat : ICommand
    {
        public string Command { get; } = "bc";
        public string[] Aliases { get; } = new string[] { "bc","globalchat","bchat" };
        public string Description { get; } = "全局聊天";
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
                if (!Round.IsRoundStarted)
                {
                    response = "<color=red>回合未开始，不能使用阵营聊天！</color>";
                    return false;
                }
                if (player.Team == Team.Dead && !Plugin.Config.SpeactorChat)
                {
                    response = "<color=red>你现在不能说话！</color>";
                    return false;
                }
                if (!Round.IsRoundStarted) 
                {
                    Map.Broadcast(3, $"<size=55%>[准备阶段] {player.Nickname} : {string.Join(" ", arguments)}</size>");
                    Log.Info($"[全局聊天][{player.Nickname}][{player.UserId}]:[{string.Join(" ", arguments)}]");
                    response = "<color=green>信息已发送!</color>";
                    return true;
                }
                foreach (Player player1 in Player.GetPlayers())
                {
                    player1.SendConsoleMessage($"[全局聊天] {player.PlayerRole()} {player.Nickname} : {string.Join(" ", arguments)}", "green");
                }
                Map.Broadcast(3, $"<size=55%>{player.PlayerRole()} {player.Nickname} : {string.Join(" ", arguments)}</size>");
                Log.Info($"[全局聊天][{player.Nickname}][{player.UserId}]:[{string.Join(" ", arguments)}]");
                response = "<color=green>信息已发送!</color>";
                return true;
            }
            response = "<color=green>信息已发送!</color>";
            return true;
        }
    }
}
