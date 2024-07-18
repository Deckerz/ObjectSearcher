using Dalamud.Game.ClientState.Objects;
using Dalamud.Game;
using Dalamud.IoC;
using Dalamud.Plugin.Services;
using Dalamud.Plugin;
using SamplePlugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fates;
public class Service
{
    [PluginService] public static IDalamudPluginInterface Interface { get; private set; } = null!;
    [PluginService] public static IPluginLog Log { get; private set; } = null!;
    [PluginService] public static IObjectTable ObjectTable { get; private set; } = null!;

    internal static bool IsInitialized = false;
    public static void Init(IDalamudPluginInterface pi)
    {
        if (IsInitialized)
        {
            Log.Debug("Services already initialized, skipping");
        }
        IsInitialized = true;
        try
        {
            pi.Create<Service>();
        }
        catch (Exception ex)
        {
            Log.Error($"Error initalising {nameof(Service)}", ex);
        }
    }
}
