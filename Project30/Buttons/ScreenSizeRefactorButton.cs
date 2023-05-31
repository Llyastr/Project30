using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class ScreenSizeRefactorButton : VerticalScrollButton
    {
        private static readonly string ButtonTextStatic = "NullString";

        private readonly int TargetWidth;
        private readonly int TargetHeight;

        protected override string Text
        {
            get { return $"{TargetWidth} x { TargetHeight}"; }
        }

        private bool IsSelected
        {
            get { return BaseGame.ScreenWidth == TargetWidth && BaseGame.ScreenHeight == TargetHeight; }
        }

        private readonly Color ButtonSelectedColour = Color.AliceBlue;
        protected override Color ButtonColour
        {
            get
            {
                if (IsHovered)
                {
                    return ButtonHoverColour;
                }
                if (IsSelected)
                {
                    return ButtonSelectedColour;
                }
                return ButtonUnhoverColour;
            }
        }

        public ScreenSizeRefactorButton(BaseGame baseGame, VerticalScrollSubscreen scrollSubscreen, 
                                        int locationX, int locationY, int width, int height,
                                        int targetWidth, int targetHeight)
            : base(baseGame, scrollSubscreen, ButtonTextStatic, locationX, locationY, width, height)
        {
            TargetWidth = targetWidth;
            TargetHeight = targetHeight;
        }

        public override void OnClick()
        {
            BaseGame.SetScreenSize(TargetWidth, TargetHeight);
        }
    }
}
