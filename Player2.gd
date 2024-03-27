extends Area2D

func _ready():
    await get_tree().process_frame
    
    seed(198519345)
    var scene := get_tree().current_scene
    for i in range(10000):
        var node := preload("res://enemy.tscn").instantiate()
        scene.add_child(node)
        node.global_position.x = randf_range(-10000.0, 10000.0)
        node.global_position.y = randf_range(-5000.0, 5000.0)

func _process(delta: float) -> void:
    $Label.text = "Area2D check\n10000 entities, 20k x 10k spawn area\n" + str(Engine.get_frames_per_second()) + "fps"
    var checks := 0
    for node in get_overlapping_areas():
        node.do_in_range_logic()
        checks += 1
    $Label.text += "\n" + str(checks) + " in-range checks"
