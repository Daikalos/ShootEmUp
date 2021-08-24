using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace myShootEmUp.Other
{
    class Animation
    {
        private Texture2D myTexture;
        private Vector2 myFrameSize;
        private Vector2 myCurrentFrame;
        private Vector2 mySheetSize;
        private int myImageSpeed;
        private int myTicks;
        private Color myColour;

        public int AccessImageSpeed
        {
            get => myImageSpeed;
            set => myImageSpeed = value;
        }
        public Vector2 AccessCurrentFrame
        {
            get => myCurrentFrame;
            set => myCurrentFrame = value;
        }
        public Vector2 AccessSheetSize
        {
            get => mySheetSize;
            set => mySheetSize = value;
        }
        public Color AccessColour
        {
            get => myColour;
            set => myColour = value;
        }

        public Animation(Texture2D aTexture, Vector2 aFrameSize, Vector2 aCurrentFrame, Vector2 aSheetSize, int aImageSpeed)
        {
            myTexture = aTexture;
            myFrameSize = aFrameSize;
            myCurrentFrame = aCurrentFrame;
            mySheetSize = aSheetSize;
            myImageSpeed = aImageSpeed;
            myColour = Color.White;
        }

        public void Draw(SpriteBatch aSpriteBatch, Rectangle aDestRect, float aRotation)
        {
            aSpriteBatch.Draw(myTexture, aDestRect, new Rectangle((int)myCurrentFrame.X * (int)myFrameSize.X, (int)myCurrentFrame.Y * (int)myFrameSize.Y, (int)myFrameSize.X, (int)myFrameSize.Y), myColour, aRotation, new Vector2(0, 0), SpriteEffects.None, 0);
            if (Game.myGameStateNow != Game.MyGameState.myPausing)
            {
                if (myTicks % (int)(myImageSpeed / Game.AccessUpdateSpeed) == 0)
                {
                    myCurrentFrame.X++;
                    if (myCurrentFrame.X >= mySheetSize.X)
                    {
                        myCurrentFrame.X = 0;
                        myCurrentFrame.Y++;
                        if (myCurrentFrame.Y >= mySheetSize.Y)
                        {
                            myCurrentFrame.Y = 0;
                        }
                    }
                }
                myTicks++;
            }
        }
    }
}
