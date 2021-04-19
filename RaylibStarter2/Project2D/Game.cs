using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using Raylib;
using static Raylib.Raylib;
using MathClasses;


namespace Project2D
{
    class Game
    {
        Stopwatch stopwatch = new Stopwatch();

		public static CollisionManager collision_manager = new CollisionManager();

		private long currentTime = 0;
        private long lastTime = 0;
        private float timer = 0;
        private int fps = 1;
        private int frames;
		public enum Modes
		{
			Normal,
			Editor,
			Menu,
		}
		Modes mode = Modes.Menu;

        private float deltaTime = 0.005f;
		Node root;
		UI ui_root;

        public Game(Modes _initial_mode)
        {
			root = new Node(null);
			ui_root = new UI();
			Global.game = this;
			change_mode(_initial_mode);
        }


		// --------------- //
		// INITIALIZE GAME //
		// --------------- //

        public void init()
        {
            stopwatch.Start();
            lastTime = stopwatch.ElapsedMilliseconds;

            if (Stopwatch.IsHighResolution)
            {
                Console.WriteLine("Stopwatch high-resolution frequency: {0} ticks per second", Stopwatch.Frequency);
            }
		}

		// ----------------------- //
		// change mode of the game //

		public void change_mode(Modes _mode)
        {
			mode = _mode;
			
			//clear roots
			root = new Node(null);
			ui_root = new UI();
			collision_manager.clear();

			//create content based on mode
			if (mode == Modes.Normal)
			{
				generate_game_world("../Worlds/created_level.txt");
				var button_size = new Vector2(96, 64);
				Button button_menu = new Button("Back", Global.WINDOW_SIZE - button_size, button_size);
				button_menu._set_parent(ui_root);
				button_menu.set_action(change_to_menu);
			}
			else if (mode == Modes.Editor)
			{
				LevelEditor level_editor = new LevelEditor(GetScreenWidth(), GetScreenHeight());
				level_editor._set_parent(ui_root);
			}
			else if (mode == Modes.Menu)
            {
				MainMenu main_menu = new MainMenu(new Vector2(GetScreenWidth(), GetScreenHeight()));
				main_menu._set_parent(ui_root);
            }
		}

		public void change_to_menu()
        {
			change_mode(Modes.Menu);
        }
		public void change_to_game()
        {
			change_mode(Modes.Normal);
		}
		public void change_to_editor()
        {
			change_mode(Modes.Editor);
        }

		// -------- //
		// end game //
        public void shut_down() { }

		// ----------- //
		// UPDATE GAME //
		// ----------- //

        public void update()
        {
            lastTime = currentTime;
            currentTime = stopwatch.ElapsedMilliseconds;
            deltaTime = (currentTime - lastTime) / 1000.0f;
            timer += deltaTime;
            if (timer >= 1)
            {
                fps = frames;
                frames = 0;
                timer -= 1;
            }
            frames++;

			
			//update
			foreach (Node node in root.get_children())
			{
				if (node is PhysicsNode physics_node) 
				{ 
					//update transformation
					physics_node._physics_update(deltaTime);
				}

				//physics
				node._update_global_transform();
			}

			//update ui
			ui_root._update_global_transform();
			ui_root._update_state();

			collision_manager.run();

		}

		// ----------------- //
		// DRAW GAME CONTENT //
		// ----------------- //

        public void draw()
        {
            BeginDrawing();
			ClearBackground(RLColor.BLACK);

			//Draw FPS
			DrawText(fps.ToString(), 10, 10, 14, RLColor.RED);

			//Draw the level
			root._draw();
			ui_root._draw();

			EndDrawing();
        }

		// ---------------------------------------------------- //
		// generate game world based on the path to a text file //

		private void generate_game_world(string _data_file_path)
		{
			const int	CELL_SIZE = 64;
			const char	CELL_WALL = 'W';
			const char	CELL_TANK = 'T';

			var physics_nodes = new List<PhysicsNode>();

			try
			{
				StreamReader file = new StreamReader(_data_file_path);
				string line;

				int world_width;

				line = file.ReadLine();
				if (line != null)
					world_width = line.Length;  //The width of the world is the length of the first line
				else
				{
					Console.WriteLine("Error: the first line of the file is empty");
					return;
				}

				int current_y = 0;
				while (line != null)
				{
					for (int current_x = 0; current_x < line.Length; ++current_x)
					{
						char current_cell = line[current_x];
						Vector2 current_pos = new Vector2(current_x * CELL_SIZE + CELL_SIZE / 2, current_y * CELL_SIZE + CELL_SIZE / 2);

						if (current_cell == CELL_WALL)
						{
							PhysicsNode node = new PhysicsNode(root);
							node.set_texture(Graphics.texture_wall);
							node.set_position(current_pos);

							//Add to collision manager
							physics_nodes.Add(node);
						}
						if (current_cell == CELL_TANK)
						{
							var tank = new Tank(root);
							tank.set_position(current_pos);
							Console.WriteLine("\n\nTank was created at: \n" + current_pos);
							//Add to collision manager
							physics_nodes.Add(tank);
						}
					}

					++current_y;
					line = file.ReadLine();
				}

				//add nodes to collision_manager
				foreach (PhysicsNode node in physics_nodes)
				{
					collision_manager.add_node(node);
				}

				file.Close();
				file.Dispose();

			} catch(Exception e)
			{
				Console.WriteLine("Exception: " + e.Message);
			}
			
		}

    }
}
