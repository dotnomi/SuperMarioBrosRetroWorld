using Godot;

public partial class entry_point : AnimatedSprite2D
{
	[Export] private Node2D _player;
	
	public override void _Ready()
	{
		SpriteFrames.ClearAll();
		_player.Visible = true;
	}
}
