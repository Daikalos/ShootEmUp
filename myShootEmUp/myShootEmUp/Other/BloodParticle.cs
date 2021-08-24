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
    class BloodParticle
    {
        private int
            mySizeX,
            mySizeY,
            mySpeed;
        private Vector2
            myPosition,
            myVelocity,
            myDirection;
        private bool myIsAlive = true;

        public Vector2 AccessPosition
        {
            get => myPosition;
            set => myPosition = value;
        }
        public bool AccessIsAlive
        {
            get => myIsAlive;
            set => myIsAlive = value;
        }


        public BloodParticle(GameTime aGametime, Vector2 aPosition, Vector2 aDirection, int aSpeed, int aSizeX, int aSizeY)
        {
            myVelocity = new Vector2(0, 0);
            myPosition = aPosition;
            mySizeX = aSizeX;
            mySizeY = aSizeY;
            mySpeed = aSpeed;
            myDirection = aDirection;
            myPosition.Y -= 50f * (float)aGametime.ElapsedGameTime.TotalSeconds;
            myVelocity.Y = -150f * (float)aGametime.ElapsedGameTime.TotalSeconds;
        }

        public void Update(GameTime aGametime, GameWindow aWindow)
        {
            myPosition.X -= (float)Game.AccessGameSpeed;
            myPosition += myVelocity * (float)Game.AccessUpdateSpeed; ;

            if (myPosition.Y >= 325 && myPosition.Y <= 330)
            {
                myVelocity = new Vector2(0, 0);
            }
            else
            {
                myDirection.Normalize();
                myPosition += myDirection * mySpeed * (float)aGametime.ElapsedGameTime.TotalSeconds;

                myVelocity.Y += 0.10f;
            }

            if ((myPosition.X < (mySizeX) * -1) || (myPosition.Y > aWindow.ClientBounds.Height))
            {
                myIsAlive = false;
            }
        }

        public void Draw(SpriteBatch aSpriteBatch)
        {
            Rectangle tempDestRect = new Rectangle((int)myPosition.X, (int)myPosition.Y, mySizeX, mySizeY);
            aSpriteBatch.Draw(Game.AccessBloodParticleSprite, tempDestRect, Color.White);
        }
    }
}
