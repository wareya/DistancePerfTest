using Godot;
using System;

public partial class Player2 : Area2D
{
    Label label;
    async public override void _Ready()
    {
        await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
        
        Godot.GD.Seed(198519345);
        Node scene = GetTree().CurrentScene;
        
        PackedScene enemy_scene = Godot.GD.Load<PackedScene>("res://enemy.tscn");
        foreach(int i in Godot.GD.Range(10000))
        {
            Node2D node = enemy_scene.Instantiate<Node2D>();
            scene.AddChild(node);
            node.GlobalPosition = new Vector2((Godot.GD.Randf() - 0.5f) * 2000.0f, (Godot.GD.Randf() - 0.5f) * 1000.0f);
        }
        
        label = GetNode<Label>("Label");
    }

    public override void _Process(double delta)
    {
        label.Text = "Area2D check (C#)\n10000 entities, 2k x 1k spawn area\n" + Engine.GetFramesPerSecond().ToString() + "fps";
        int checks = 0;
        Vector2 global_pos = GlobalPosition;
        float dist = 400.01f;
        float dist_sq = dist * dist;
        foreach(EnemyArea2D node in GetOverlappingAreas())
        {
            node.DoInRangeLogic();
            checks += 1;
        }
        label.Text += "\n" + checks.ToString() + " in-range checks";
    }
}
