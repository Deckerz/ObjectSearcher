using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Interface.Windowing;
using SamplePlugin.Windows;
using ECommons;
using Fates;

namespace SamplePlugin;

public sealed class Plugin : IDalamudPlugin
{
    [PluginService] internal static IDalamudPluginInterface PluginInterface { get; private set; } = null!;

    public readonly WindowSystem WindowSystem = new("Fates");
    private MainWindow MainWindow { get; init; }

    public Plugin()
    {
        ECommonsMain.Init(PluginInterface, this);
        Service.Init(PluginInterface);
        MainWindow = new MainWindow(this);

        WindowSystem.AddWindow(MainWindow);

        PluginInterface.UiBuilder.Draw += DrawUI;
        PluginInterface.UiBuilder.OpenMainUi += ToggleMainUI;
    }

    public void Dispose()
    {
        WindowSystem.RemoveAllWindows();
        MainWindow.Dispose();
    }

    private void DrawUI() => WindowSystem.Draw();
    public void ToggleMainUI() => MainWindow.Toggle();
}
