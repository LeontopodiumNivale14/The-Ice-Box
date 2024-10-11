﻿using Dalamud.Game.ClientState.Conditions;
using ECommons.DalamudServices;
using FFXIVClientStructs.FFXIV.Client.Game;

namespace ExamplePlugin.Tasks;

public class MountTask : IBaseTask
{
    public unsafe bool? Run()
    {
        if (Svc.Condition[ConditionFlag.Mounted] && NotBusy()) return true;
        if (!Svc.Condition[ConditionFlag.Casting] && !Svc.Condition[ConditionFlag.Unknown57])
        {
            ActionManager.Instance()->UseAction(ActionType.GeneralAction, 24);
        }

        return false;
    }
}
