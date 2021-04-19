using System;
using MathClasses;

// --------------------------------------------------------------------------------- //
// The base of every physics based node. Includes a collider and physics calculation //
// --------------------------------------------------------------------------------- //

class PhysicsNode : Node
{
	// ------ //
	// FIELDS //
	// ------ //

	// physics variables //

	protected float delta = 0f;

	protected Vector2 velocity;
	protected Vector2 direction;	//the direction vector applied to velocity
	protected float speed = 0f;		//@Warning: speed can be negative
	protected float max_speed;
	protected float acceleration;
	protected float friction;
	public Collider collider;

	// physics matrices //

	protected Matrix3 oriantation_matrix = new Matrix3(true);
	protected Matrix3 translation_matrix = new Matrix3(true);
	protected Matrix3 previous_transform = new Matrix3(true);

	// linear motion constants //

	public const float ACCELERATION_FAST = 400f;
	public const float ACCELERATION_MED = 200f;
	public const float ACCELERATION_SLOW = 80f;

	public const float FRICTION_HIGH = 100f;
	public const float FRICTION_MED = 80f;
	public const float FRICTION_LOW = 40f;

	public const float MAX_SPEED_HIGH = 300;
	public const float MAX_SPEED_MED = 150;
	public const float MAX_SPEED_LOW = 80;

	// -- // -- // -- // -- //
	//		CONSTRUCTOR     //
	// -- // -- // -- // -- //
	public PhysicsNode(Node _parent) : base(_parent)
	{
		parent = _parent;
		update_physics_variables();
		collider = new CircleCollider(28f);
	}
	
	public PhysicsNode(Node _parent, float _acceleration, float _friction, float _max_speed) : base(_parent)
	{
		parent = _parent;
		update_physics_variables(_acceleration, _friction, _max_speed);
		collider = new CircleCollider(30f);
	}

	// -- // -- // -- // -- // -- //
	// Called every physics frame //
	// -- // -- // -- // -- // -- //
	public virtual void _physics_update(float _delta)
	{
		if (deleted)
			return;

		//update delta
		delta = _delta;

		// -- apply friction -- //
		
		speed -= friction * delta * Math.Sign(speed);
		speed = clamp(speed, -max_speed, max_speed);
		

		// -- update velocity -- //
		direction.Normalize();
		velocity = direction * speed * delta;

		// -- apply velocity -- //

		//update transformation matrices
			//oriantation_matrix.Reset();					WARNING: NOT RESETING THIS
		translation_matrix.SetPosition(velocity);

			//update local matrix
		local_transform = local_transform * oriantation_matrix * translation_matrix;

		previous_transform = local_transform;

		foreach (Node node in get_children())
		{
			if (node is PhysicsNode physicsNode)
				physicsNode._physics_update(_delta);
		}
	}

	// -- // -- // -- // -- //
	//		 METHODS		//
	// -- // -- // -- // -- //

	// update the global transformation and the position of collider of this node //

	public override void _update_global_transform()
	{
		base._update_global_transform();
		if (collider != null)
		{
			collider.set_position(get_global_position());
		}
	}

	// update velocity's direction (NOT VELOCITY ITSELF) and accelerate //
	public virtual void _move(Vector2 _input_direction)
	{
		direction = _input_direction;

		if (speed < max_speed)
			speed += acceleration * delta;
	}

	// get the ref to the collider of this node //

	public Collider get_collider()
	{
		return collider;
	}

	// set acceleration, friction, and max speed //

	protected void update_physics_variables(float _acceleration = ACCELERATION_MED, float _friction = FRICTION_MED, float _max_speed = MAX_SPEED_MED)
	{
		acceleration = _acceleration;
		friction = _friction;
		max_speed = _max_speed;
	}

	// When collision occures, this method is called //

	public virtual void _on_collision(PhysicsNode _other)
	{
		if (Global.IS_DEBUG)
			Console.WriteLine("collision occured");
	}
}

