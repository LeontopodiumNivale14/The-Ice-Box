
using Dalamud.Game.ClientState.Conditions;
using ECommons.DalamudServices;
using ECommons.GameHelpers;

namespace ExamplePlugin.Util;

/**
 * Utility class for common methods used like everywhere
 */
public static class Utils
{
    public static bool NotBusy()
    {
        return Player.Available
               && Player.Object.CastActionId == 0 
               && !IsOccupied() 
               && !Svc.Condition[ConditionFlag.Jumping] 
               && Player.Object.IsTargetable;
    }
}
