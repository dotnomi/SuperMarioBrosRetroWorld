extends CharacterBody2D

@export var speed: float
@export var deathtexture : Texture2D
@onready var collision_shape_2d = $CollisionShape2D
@onready var sprite_2d = $Sprite2D

var timer = 1.2
var timerst : bool


func _process(delta):
		if timerst: 
			timer -= delta
		else:
			velocity.x = speed * delta
			move_and_slide()
		if timer <= 0: 
			queue_free()


func _on_wallfinder_body_entered(body):
	speed = -speed


func _on_playerfinder_body_entered(body):
	if body.name =="CharacterBody2D":
		die()
	pass

func die():
	collision_shape_2d.queue_free()
	sprite_2d.texture = deathtexture
	velocity.x = 0
	timerst = true 
