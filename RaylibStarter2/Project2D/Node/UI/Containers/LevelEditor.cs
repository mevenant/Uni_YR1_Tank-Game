using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathClasses;
using Raylib;
using static Raylib.Raylib;
class LevelEditor : UI
{
    //GridContainer grid_editor;
    Container block_picker;

    public LevelEditor()
    {
        

    }

    public override void _recieve_signal(Signal _signal)
    {
        base._recieve_signal(_signal);
        
        
        if (_signal.source is Button button)
        {
            if (_signal.message.Equals(Signal.MOUSE_ENTERED))
                button.set_texture(Graphics.texture_wall);
            else if (_signal.message.Equals(Signal.MOUSE_EXITED))
                button.set_texture(Graphics.texture_empty_grid);
        }
        
    }
}

