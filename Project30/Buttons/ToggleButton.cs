using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class ToggleButton : VerticalScrollButton
    {
        protected bool Bool;

        protected override string Text 
        {
            get
            {
                if (Bool)
                {
                    return "On";
                }
                else
                {
                    return "Off";
                }
            }
        }

        public ToggleButton(BaseGame baseGame, VerticalScrollSubscreen scrollSubscreen, 
                            int locationX, int locationY, int width, int height)
            : base(baseGame, scrollSubscreen, "", locationX, locationY, width, height)
        {

        }

        public override void OnClick()
        {
            Bool = !Bool;
        }
    }
}
