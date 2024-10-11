using System;
using System.Timers;
using Dalamud.Game.ClientState.Conditions;
using ECommons.DalamudServices;
using ExamplePlugin.Tasks;

namespace ExamplePlugin.Service;

public class ExampleService : IDisposable
{
    private readonly Timer updateTimer;
    private bool enabled; // private property representing the state of IsEnabled
    
    public bool IsEnabled // Public property which reacts to true/false
    {
        get => enabled;
        set
        {
            enabled = value;
            if (enabled)
            {
                Enable();
            }
            else
            {
                Disable();
            }
        }
    }
    
    /**
     * Constructor for the ExampleService class.
     */
    public ExampleService()
    {
        updateTimer = new Timer();
        updateTimer.Elapsed += OnTimerUpdate;
        updateTimer.Interval = 1000; // You watcher Tickrate in milliseconds
        updateTimer.Enabled = false;
    }

    /**
     * This method is called when the watcher is disabled.
     */
    private void Disable()
    {
        PluginLog.Information("Ending watcher.");
        updateTimer.Enabled = false;
    }

    /**
     * This method is called when the watcher is enabled.
     */
    private void Enable()
    {
        PluginLog.Information("Watcher started.");
        updateTimer.Enabled = true;
    }


    /**
     * This method is called every second by the timer. Its your "Watcher" where you can decide what to do next if ideling.
     */
    private void OnTimerUpdate(object? sender, ElapsedEventArgs e)
    {
        PluginLog.Information("Watcher tick.");
        // Check if execute thread is busy
        if (!TaskManager.IsBusy)
        {
            // execute thread is idle, do something

            if (!Svc.Condition[ConditionFlag.Mounted])
            {
                Enqueue(new MountTask());
                PluginLog.Information("Mounting.");
            }
            else
            {
                Enqueue(new OpenDutyFinderTask());
                PluginLog.Information("Duty");
            }
        }
    }

    public void Dispose()
    {
        
    }
}
