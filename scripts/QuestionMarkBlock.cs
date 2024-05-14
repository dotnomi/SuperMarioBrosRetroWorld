using Godot;

public partial class QuestionMarkBlock : StaticBody2D
{
	[Export] private float _gravity = 2925f;
	[Export] private float _initialBounceSpeed = -240f;
	
	private Timer _bounceTimer;
	private Node2D _node;
	
	private Vector2 _originalPosition;
	private float _ySpeed;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_bounceTimer = GetNode<Timer>("BounceTimer");
		_node = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		
		_originalPosition = _node.Position;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_bounceTimer.IsStopped())
		{
			_node.Position = _originalPosition;
			_ySpeed = 0f;
		}
		else
		{
			_ySpeed += _gravity * (float) delta;
			AddYPosition(_ySpeed * (float) delta);
		}
	}

	private void Bounce()
	{
		_ySpeed = _initialBounceSpeed;
		_bounceTimer.Start();
	}

	private void AddYPosition(float y)
	{
		_node.Position = new Vector2(_node.Position.X, _node.Position.Y + y);
	}

	public void _on_area_2d_area_entered(Area2D area)
	{
		Bounce();
	}
}
