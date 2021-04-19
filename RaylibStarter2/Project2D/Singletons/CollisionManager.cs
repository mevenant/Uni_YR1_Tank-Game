using System.Collections.Generic;

// ----------------- //
// Handle collisions //
// ----------------- //

class CollisionManager
{
	List<PhysicsNode> nodes = new List<PhysicsNode>();

	// run collision manager by checking if their colliders overlap //

	public void run()
	{

		foreach (PhysicsNode node1 in nodes)
		{
			foreach (PhysicsNode node2 in nodes)
			{
				if (node1 == node2)
					continue;

				//test collision
				if (node1.get_collider().overlaps(node2.get_collider()))
				{
					node1._on_collision(node2);
				}
			}
		}
	}

	// add a node to collision manager for it to be physically included //

	public void add_node(PhysicsNode _node)
	{
		nodes.Add(_node);
	}

	// clear collision manager //

	public void clear()
    {
		nodes.Clear();
    }
}

