using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class VerticalScrollSubscreen : Subscreen
    {
        public float ScrollLocationFloat { get; private set; }
        public int ScrollLocation
        {
            get { return (int)(ScreenHeight * ScrollLocationFloat); }
        }

        readonly float PanSpeed = 0.0004f;
        protected float MaxLocationFloatX { get; set; }
        protected float MinLocationFloatX { get; set; }

        float MovementCache;
        readonly float MovementPercentage = 0.13f;
        readonly float MaxMovement = 0.1f;
        readonly float StoppingThreshold = 0.0001f;

        readonly float MaxBounceBack = 0.01f;

        public VerticalScrollSubscreen(BaseGame baseGame)
            : base(baseGame)
        {

        }

        public VerticalScrollSubscreen(BaseGame baseGame, float initialScrollLocationFloat)
            : base(baseGame)
        {
            ScrollLocationFloat = initialScrollLocationFloat;
        }
        
        public override void Update(GameTime gameTime, 
                                    KeyboardState keyboardState,
                                    KeyboardState previousKeyboardState, 
                                    MouseState mouseState, 
                                    MouseState previousMouseState)
        {
            int AmountScrolled = mouseState.ScrollWheelValue - previousMouseState.ScrollWheelValue;
            float ScrollScaled = PanSpeed * AmountScrolled;
            MovementCache -= ScrollScaled;

            float ToMove = MovementCache * MovementPercentage;
            ToMove = Math.Clamp(ToMove, -MaxMovement, MaxMovement);
            ScrollLocationFloat += ToMove;
            MovementCache -= ToMove;
            if (ScrollLocationFloat > MaxLocationFloatX || ScrollLocationFloat < MinLocationFloatX)
            {
                ScrollLocationFloat = Math.Clamp(ScrollLocationFloat, MinLocationFloatX, MaxLocationFloatX);
                MovementCache = Math.Clamp(-ToMove, -MaxBounceBack, MaxBounceBack);
            }

            if (Math.Abs(MovementCache) < StoppingThreshold)
            {
                MovementCache = 0f;
            }
        }
    }
}
