using PlayerRoles;
using PluginAPI.Core;
using System.Collections.Generic;

namespace ServerCommand
{
    public static class API
    {
        private static List<Team> Foundation = new List<Team>()
        {
            Team.FoundationForces,
            Team.Scientists
        };
        private static List<Team> ChaosInsurgency = new List<Team>()
        {
            Team.ChaosInsurgency,
            Team.ClassD
        };
        private static List<Team> PluginTeam = new List<Team>()
        {
            Team.OtherAlive,
        };
        private static List<Team> DeathTeam = new List<Team>()
        {
            Team.Dead,
        };
        private static List<Team> SCPTeam = new List<Team>()
        {
            Team.SCPs,
        };
        public static bool GetTeam(this Player player, Player player1)
        {
            if (SCPTeam.Contains(player.Team) && SCPTeam.Contains(player1.Team))
                return true;
            if (DeathTeam.Contains(player.Team) && DeathTeam.Contains(player1.Team))
                return true;
            if (ChaosInsurgency.Contains(player.Team) && ChaosInsurgency.Contains(player1.Team))
                return true;
            if (Foundation.Contains(player.Team) && Foundation.Contains(player1.Team))
                return true;
            if (PluginTeam.Contains(player.Team) && PluginTeam.Contains(player1.Team))
                return true;
            return false;
        }
        public static string PlayerRole(this Player player)
        {
            if (player.IsSCP)
            {
                switch (player.Role)
                {
                    case RoleTypeId.Scp049:
                        return "<color=red>[SCP-049]</color>";
                    case RoleTypeId.Scp079:
                        return "<color=red>[SCP-079]</color>";
                    case RoleTypeId.Scp096:
                        return "<color=red>[SCP-096]</color>";
                    case RoleTypeId.Scp106:
                        return "<color=red>[SCP-106]</color>";
                    case RoleTypeId.Scp173:
                        return "<color=red>[SCP-173]</color>";
                    case RoleTypeId.Scp939:
                        return "<color=red>[SCP-939]</color>";
                    case RoleTypeId.Scp3114:
                        return "<color=red>[SCP-3114]</color>";
                    case RoleTypeId.Scp0492:
                        return "<color=red>[SCP-049-2]</color>";
                }
            }
            switch (player.Team)
            {
                case Team.FoundationForces:
                    return "<color=#00FFFF>[机动特遣队]</color>";
                case Team.ChaosInsurgency:
                    return "<color=green>[混沌分裂者]</color>";
                case Team.Scientists:
                    return "<color=yellow>[科学家]</color>";
                case Team.ClassD:
                    return "<color=orange>[D级人员]</color>";
                case Team.Dead:
                    return "<color=white>[观察者]</color>";
                case Team.OtherAlive:
                    return "<color=#FF00FF>[教程角色]</color>";
                case Team.SCPs:
                    return "<color=red>[SCP]</color>";
                default:
                    return "<color=white>[未知]</color>";
            }
        }
    }
}
