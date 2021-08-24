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
    class simpleAnimation
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
            get
            {
                return myImageSpeed;
            }
            set
            {
                myImageSpeed = value;
            }
        }
        public Vector2 AccessCurrentFrame
        {
            get
            {
                return myCurrentFrame;
            }
            set
            {
                myCurrentFrame = value;
            }
        }
        public Vector2 AccessSheetSize
        {
            get
            {
                return mySheetSize;
            }
            set
            {
                mySheetSize = value;    
            }
        }
        public Color AccessColour
        {
            get
            {
                return myColour;
            }
            set
            {
                myColour = value;
            }
        }

        public simpleAnimation(Texture2D aTexture, Vector2 aFrameSize, Vector2 aCurrentFrame, Vector2 aSheetSize, int aImageSpeed)
        {
            this.myTexture = aTexture;
            this.myFrameSize = aFrameSize;
            this.myCurrentFrame = aCurrentFrame;
            this.mySheetSize = aSheetSize;
            this.myImageSpeed = aImageSpeed;
            this.myColour = Color.White;
        }

        public void Draw(SpriteBatch aSpriteBatch, Rectangle aDestRect)
        {
            aSpriteBatch.Draw(myTexture, aDestRect, new Rectangle((int)myCurrentFrame.X * (int)myFrameSize.X, (int)myCurrentFrame.Y * (int)myFrameSize.Y, (int)myFrameSize.X, (int)myFrameSize.Y), myColour, 0, new Vector2(0, 0), SpriteEffects.None, 0);
            if (Game.myGameStateNow != Game.myGameState.myPausing)
            {
                if (myTicks % myImageSpeed == 0)
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
