
using ECommons.Throttlers;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using FFXIVClientStructs.FFXIV.Component.GUI;

namespace ExamplePlugin.Tasks;

public class OpenDutyFinderTask : IBaseTask
{
    public unsafe bool? Run()
    {
        if (TryGetAddonByName<AtkUnitBase>("ContentsFinder", out var addon) && IsAddonReady(addon))
        {
            return true;
        }

        if (EzThrottler.Throttle("OpenContentsFinder", 2000)) { // Throttle to prevent spamming
            AgentContentsFinder.Instance()->AgentInterface.Show();
        }

        return false;
    }
}
