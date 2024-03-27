extends Area2D

var enemies : Array[Node] = []

func _ready():
    await get_tree().process_frame
    
    seed(198519345)
    var scene := get_tree().current_scene
    for i in range(10000):
        var node := preload("res://enemyNonArea2D.tscn").instantiate()
        scene.add_child(node)
        node.global_position.x = randf_range(-10000.0, 10000.0)
        node.global_position.y = randf_range(-5000.0, 5000.0)
        enemies.push_back(node)

func _process(delta: float) -> void:
    $Label.text = "Distance check\n10000 entities, 20k x 10k spawn area\n" + str(Engine.get_frames_per_second()) + "fps"
    var checks := 0
    var global_pos := global_position
    var dist := 410.0
    var dist_sq := dist * dist
    for node in enemies:
        if global_pos.distance_squared_to(node.global_position) < dist_sq:
            node.do_in_range_logic()
            checks += 1
    $Label.text += "\n" + str(checks) + " in-range checks"
