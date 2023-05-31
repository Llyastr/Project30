using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class VerticalScrollButton : Button
    {
        protected VerticalScrollSubscreen ScrollSubscreen;
        
        int ScrollLocation
        {
            get { return ScrollSubscreen.ScrollLocation; }
        }

        private int LocationStaticY;
        protected override int LocationY
        {
            get { return LocationStaticY - ScrollLocation; }
        }
        protected override int TextLocationY
        {
            get { return LocationStaticY + (Height - (int)TextMeasure.Y) / 2 - ScrollLocation; }
        }

        public VerticalScrollButton(BaseGame baseGame, VerticalScrollSubscreen scrollSubscreen,
                                     string text, int locationX, int locationY, int width, int height)
            : base(baseGame, text, locationX, locationY, width, height)
        {
            LocationStaticY = locationY;
            ScrollSubscreen = scrollSubscreen;
        }
    }
}
