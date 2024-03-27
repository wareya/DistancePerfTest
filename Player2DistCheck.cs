using Godot;
using System;

public partial class Player2DistCheck : Node2D
{
    // for the sake of maximal comparability:
    // this is the most similar type to what is returned by GetOverlappingAreas()
    // (GetOverlappingAreas returns a Godot.Collections.Array<Array2D>)
    Godot.Collections.Array<Node2D> Enemies = new Godot.Collections.Array<Node2D>();
    
    Label label;
    async public override void _Ready()
    {
        await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
        
        Godot.GD.Seed(198519345);
        Node scene = GetTree().CurrentScene;
        
        PackedScene enemy_scene = Godot.GD.Load<PackedScene>("res://enemyNonArea2D.tscn");
        foreach(int i in Godot.GD.Range(10000))
        {
            Enemy node = enemy_scene.Instantiate<Enemy>();
            scene.AddChild(node);
            node.GlobalPosition = new Vector2((Godot.GD.Randf() - 0.5f) * 2000.0f, (Godot.GD.Randf() - 0.5f) * 1000.0f);
            Enemies.Add(node);
        }
        
        label = GetNode<Label>("Label");
    }

    public override void _Process(double delta)
    {
        label.Text = "Distance check (C#)\n10000 entities, 2k x 1k spawn area\n" + Engine.GetFramesPerSecond().ToString() + "fps";
        int checks = 0;
        Vector2 global_pos = GlobalPosition;
        float dist = 400.01f;
        float dist_sq = dist * dist;
        foreach(Enemy node in Enemies)
        {
            if (global_pos.DistanceSquaredTo(node.GlobalPosition) < dist_sq)
            {
                node.DoInRangeLogic();
                checks += 1;
            }
        }
        label.Text += "\n" + checks.ToString() + " in-range checks";
    }
}
