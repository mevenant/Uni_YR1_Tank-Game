using System;
using System.Collections.Generic;
using System.IO;
using MathClasses;
using Raylib;
using static Raylib.Raylib;
class LevelEditor : UI
{
	//CONTAINERS
    Container block_picker; //items to pick from
    Container grid;			//level grid
	Container options_container; //Options such as save, back, etc

    List<Block> available_blocks = new List<Block>();
    int grid_cell_size = 64;
	Vector2 grid_cell_amount;
	LevelEditorCursor cursor;
	string level_file = "../Worlds/created_level.txt";

	//keep track of what grid buttons have been pressed recently
	List<Button> grid_placed_buttons = new List<Button>();

    public LevelEditor(int _width, int _height)
    {

        available_blocks.Add(new Block(Graphics.texture_wall, new Vector2()));
        available_blocks.Add(new Block(Graphics.texture_tank, new Vector2()));
		cursor = new LevelEditorCursor();

		//create main containers
		var block_picker_size = new Vector2(grid_cell_size, _height * 0.5f);
        block_picker = new Container(new Vector2(), block_picker_size);

        var grid_size = new Vector2(_width - block_picker_size.x, _height);
        grid = new Container(new Vector2(block_picker_size.x, 0), grid_size);

		var ui_container_size = block_picker_size;
		options_container = new Container(new Vector2(0, block_picker_size.y), ui_container_size);


		//add main containers to level editor
		block_picker._set_parent(this);
        grid._set_parent(this);
		options_container._set_parent(this);

		//add the items to block picker
		block_picker.set_sort(Container.sort_vertically);
        foreach (Block block in available_blocks)
        {
            var button = new Button(new Vector2(), block.texture);
            button._set_parent(block_picker);
			button.connect(this);
        }

        //manually place grid cells to the grid
        grid_cell_amount = new Vector2(grid_size.x / grid_cell_size, grid_size.y / grid_cell_size); //64px cell size
        for (int j = 0; j < (int)grid_cell_amount.y; ++j)
        {
            for (int i = 0; i < (int)grid_cell_amount.x; ++i)
            {
                var pos = new Vector2(i * grid_cell_size, j * grid_cell_size);
                var button = new Button(pos, Graphics.texture_empty_grid);
                button._set_parent(grid);
				button.connect(this);
			}
        }

		//create UI buttons for options
		Button button_save = new Button("Save", new Vector2(), new Vector2(64, 32));
		Button button_back = new Button("Back", new Vector2(), new Vector2(64, 32));

		button_save._set_parent(options_container);
		button_back._set_parent(options_container);
		options_container.set_sort(Container.sort_vertically);

		button_save.set_action(save);
		button_back.set_action(Global.game.change_to_menu);
	}

	// -------------- //
	// HANDLE SIGNALS //
	// -------------- //

	public override void _recieve_signal(Signal _signal)
    {
        base._recieve_signal(_signal);
        
        if (_signal.source is Button button)
        {
			if (button.get_parent().Equals(grid))		//A Grid button
			{
				if (!grid_placed_buttons.Contains(button))
				{
					if (_signal.message.Equals(Signal.MOUSE_ENTERED))  //if signal is mouse enter and the button is empty
						button.set_texture(cursor.texture);

					else if (_signal.message.Equals(Signal.MOUSE_EXITED)) //if signal is mouse exited and thebutton is empty
						button.set_texture(Graphics.texture_empty_grid);
					
					else if (_signal.message.Equals(Signal.MOUSE_PRESSED))  //place a block
						place_block(button, cursor.texture);
				}
				else
				{
					if (_signal.message.Equals(Signal.MOUSE_PRESSED_SECONDARY))  //pick up a block
						pickup_block(button);
				}
			} 
			else if (button.get_parent().Equals(block_picker))	//A Block picker button
			{
				if (_signal.message.Equals(Signal.MOUSE_PRESSED))
					cursor.texture = button.get_texture();
			}   
        }
    }

	// ---- //
	// SAVE //
	// ---- //
	// Go through each button (they were created in a way that they will be iterated horizontally) and write the content of that button (its texture)

	private void save()
	{
		using (StreamWriter file = new StreamWriter(level_file))
		{
			int horizontal_step = 0;
			var children = grid.get_children();
			foreach (Button button in children)
			{
				if (horizontal_step > grid_cell_amount.x - 1)
				{
					horizontal_step = 0;
					file.WriteLine();
				}

				char block = ' ';

				if (button.get_texture().Equals(Graphics.texture_wall))
					block = 'W';
				else if (button.get_texture().Equals(Graphics.texture_tank))
					block = 'T';

				file.Write(block);

				++horizontal_step;
			}
		}

		if (Global.IS_DEBUG) Console.WriteLine("saved");
	}

	// ----------- //
	// DRAW EDITOR //
	// ----------- //
	//Draw the editor and the mouse cursor for the editor

	public override void _draw()
	{
		base._draw();
		cursor._draw();
	}

	// --------------- //
	// PRIVATE METHODS //
	// --------------- //

	// --------------------------------------- //
	// update the contents of the given button //
	private void place_block(Button _source, Texture2D _texture)
	{
		grid_placed_buttons.Add(_source); //update the button that's just been pressed
		_source.set_texture(_texture);
	}

	// --------------------------------------- //
	// update the contents of the given button //
	private void pickup_block(Button _source)
	{
		grid_placed_buttons.Remove(_source);
		_source.set_texture(Graphics.texture_empty_grid);
	}
}

struct Block
{
    public Texture2D texture;
    public Vector2 position;

    public Block(Texture2D _texture, Vector2 _pos)
    {
        texture = _texture;
        position = _pos;
    }
}

internal class LevelEditorCursor
{
	public Texture2D texture;
	public Vector2 position;

	public void _draw()
	{
		position = new Vector2(GetMouseX(), GetMouseY());
		DrawTexture(texture, (int)position.x, (int)position.y, RLColor.WHITE);
	}
}

