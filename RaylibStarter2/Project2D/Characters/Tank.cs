using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathClasses;

class Tank : PhysicsNode
{
	private int rotation_speed = 2;

	// -- // -- // -- // -- //
	//		CONSTRUCTOR
	// -- // -- // -- // -- //
	public Tank(Node _parent) : base(_parent)
	{
		update_physics_variables(ACCELERATION_FAST, FRICTION_HIGH, MAX_SPEED_HIGH);

		texture = Graphics.get_texture_from_path(Graphics.tank_body);

		var turret = new Node(this);
		turret.set_texture(Graphics.get_texture_from_path(Graphics.tank_turret));
	}

	// -- // -- // -- // -- //
	//		 METHODS
	// -- // -- // -- // -- //
	public override void _physics_update(float _delta)
	{
		oriantation_matrix.Reset();
		translation_matrix.Reset();

		//move based on input
		var input = Input.get_primary_input();
		
		//only change direction when input
		if (input != Vector2.ZERO)
			_move(input);

		base._physics_update(_delta);
	}

	public override void _move(Vector2 _input_direction)
	{
		direction = Vector2.UP;

		if (Math.Abs(speed) > 1)
		{
			float rotation_angle = _input_direction.x * rotation_speed;
			oriantation_matrix.SetRotateZ(rotation_angle * delta);
		}

		speed += acceleration * _input_direction.y * delta;
		
	}

	public override void _on_collision(PhysicsNode _other)
	{
		base._on_collision(_other);

		//push apart
		Vector2 new_pos = (_other.get_global_position() - get_global_position()) * velocity.Magnitude();

		set_position(get_local_position() - new_pos);

		speed = 0;
		return;
	}
}
