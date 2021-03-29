using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathClasses;

class CollisionManager
{
	List<PhysicsNode> nodes = new List<PhysicsNode>();

	public void run()
	{
		//foreach(PhysicsNode node1 in nodes)
		//{
		//	foreach(PhysicsNode node2 in nodes)
		//	{
		//		if (node1 == node2)
		//			continue;
		//
		//		//test collision
		//		Vector2 difference = node2.get_global_position() - node1.get_global_position();
		//		if (difference.Magnitude() * 0.5f < node1.get_collider().radius)
		//		{
		//			node1._on_collision(node2);
		//		}
		//	}
		//}

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

		//for (int i = 0; i < nodes.Count; ++i)
		//{
		//	for (int j = i + 1; j < nodes.Count; ++j)
		//	{
		//		PhysicsNode node1 = nodes[i];
		//		PhysicsNode node2 = nodes[j];
		//
		//		if (node1 == node2)
		//			continue;
		//
		//		//test collision
		//		Vector2 difference = node2.get_global_position() - node1.get_global_position();
		//		if (difference.Magnitude() < node1.get_collider().radius)
		//		{
		//			node1._on_collision(node2);
		//			node2._on_collision(node1);
		//		}
		//	}
		//}
	}

	public void add_node(PhysicsNode _node)
	{
		nodes.Add(_node);
	}
}

