using System;
using System.Linq;
using System.Numerics;
using Dalamud.Interface.Windowing;
using ECommons.GameHelpers;
using Fates;
using ImGuiNET;

namespace SamplePlugin.Windows;

public class MainWindow : Window, IDisposable
{
    private readonly Plugin _plugin;

    private string _filter { get; set; } = string.Empty;
    private bool showNameless = false;

    public MainWindow(Plugin plugin) : base("Object Searcher")
    {
        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(375, 330),
            MaximumSize = new Vector2(float.MaxValue, float.MaxValue)
        };

        _plugin = plugin;
    }

    public void Dispose() { }

    public override unsafe void Draw()
    {
        var currentFilter = _filter;
        if(ImGui.InputText("Filter", ref currentFilter, 100))
        {
            _filter = currentFilter;
        }
        var currentNameless = showNameless;
        if (ImGui.Checkbox("Show Nameless", ref currentNameless))
        {
            showNameless = currentNameless;
        }
        ImGui.NewLine();

        foreach (var obj in Service.ObjectTable.Where(x => x.Name.TextValue.Contains(_filter) && (showNameless || x.Name.TextValue is { Length: > 0 })))
        {
            ImGui.Text(obj.Name.ToString());
            ImGui.SameLine();
            ImGui.Text(Math.Round(Vector3.Distance(Player.Position, obj.Position)).ToString());
            ImGui.SameLine();
            ImGui.Text(obj.DataId.ToString());
        }
    }
}
