using MathClasses;

// --------------------- //
// Main Menu of the game //
// --------------------- //

class MainMenu : UI
{
    public MainMenu(Vector2 _size)
    {
        Container menu_options_container;

        //create options menu container //
        menu_options_container = new Container(_size * 0.25f, _size * 0.5f);
        menu_options_container._set_parent(this);
        menu_options_container.set_sort(Container.sort_vertically);


        // create buttons //
        var button_size = new Vector2(128, 96);

        Button button_editor = new Button("Editor", Vector2.ZERO, button_size);
        Button button_game = new Button("Game", Vector2.ZERO, button_size);
        
        button_editor._set_parent(menu_options_container);
        button_game._set_parent(menu_options_container);

        // setup button functionality //
        button_editor.set_action(Global.game.change_to_editor);
        button_game.set_action(Global.game.change_to_game);

    }
}

