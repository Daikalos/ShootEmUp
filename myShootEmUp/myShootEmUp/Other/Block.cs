using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace myShootEmUp
{
    public class Block
    {
        private Vector2 myPosition;
        private int[] myCollisionCases;
        private int
            mySizeX,
            mySizeY;

        public Vector2 AccessPosition
        {
            get => myPosition;
            set => myPosition = value;
        }
        public int[] AccessCollisionCases
        {
            get => myCollisionCases;
        }
        public int AccessSizeX
        {
            get => mySizeX;
            set => mySizeX = value;
        }
        public int AccessSizeY
        {
            get => mySizeY;
            set => mySizeY = value;
        }

        public Block(Vector2 aPosition, Rectangle aRectangle, int aSizeX, int aSizeY)
        {
            myPosition = aPosition;
            mySizeX = aSizeX;
            mySizeY = aSizeY;
            myCollisionCases = new int[4];
        }

        public void Update(Random aRNG, GameTime aGametime)
        {
            myPosition.X -= (float)Game.AccessGameSpeed;
            Game.AccessRespawnTimeBlocks -= (float)aGametime.ElapsedGameTime.TotalMilliseconds;

            if (!Game.AccessTankBoss.AccessIsAlive)
            {
                if (Game.AccessRespawnTimeBlocks < 0 && (myPosition.X + mySizeX) < 0)
                {
                    myPosition.X = 1520;
                    Game.AccessRespawnTimeBlocks = Game.AccessDefaultRespawnTimeBlocks - ((float)Game.AccessGameSpeed * 100);
                    if (Game.AccessActiveLevel == 1)
                    {
                        switch (aRNG.Next(0, 4))
                        {
                            case 1:
                                mySizeY = 128;
                                mySizeX = 64;
                                myPosition.Y = 150;
                                break;
                            case 2:
                                mySizeX = 96;
                                mySizeY = 32;
                                myPosition.Y = 220;
                                break;
                            case 3:
                                mySizeX = 192;
                                mySizeY = 64;
                                myPosition.Y = 260;
                                break;
                        }
                    }
                    else
                    {
                        switch (aRNG.Next(0, 4))
                        {
                            case 1:
                                mySizeY = 64;
                                mySizeX = 64;
                                myPosition.Y = 200;
                                break;
                            case 2:
                                mySizeX = 96;
                                mySizeY = 32;
                                myPosition.Y = 220;
                                break;
                            case 3:
                                mySizeX = 192;
                                mySizeY = 32;
                                myPosition.Y = 190;
                                break;
                        }

                    }
                }
            }
        }

        public void Collision(Rectangle anObjectToCollideWith, double aSpeedX)
        {
            Rectangle tempRectangleLeft = new Rectangle((int)myPosition.X - ((int)aSpeedX + (int)Game.AccessGameSpeed + 1), (int)myPosition.Y, mySizeX, mySizeY);
            Rectangle tempRectangleRight = new Rectangle((int)myPosition.X + ((int)aSpeedX + (int)Game.AccessGameSpeed + 1), (int)myPosition.Y, mySizeX, mySizeY);
            Rectangle tempRectangleDown = new Rectangle((int)myPosition.X, (int)myPosition.Y - 5, mySizeX, mySizeY);
            Rectangle tempRectangleAbove = new Rectangle((int)myPosition.X, (int)myPosition.Y + 5, mySizeX, mySizeY);

            if (anObjectToCollideWith.Intersects(tempRectangleLeft))
            {
                Game.AccessPlayer.AccessMovementStop[0] = 1;
                myCollisionCases[0] = 1;
            }
            else
            {
                myCollisionCases[0] = 0;
            }

            if (anObjectToCollideWith.Intersects(tempRectangleRight))
            {
                Game.AccessPlayer.AccessMovementStop[1] = 1;
                myCollisionCases[1] = 1;
            }
            else
            {
                myCollisionCases[1] = 0;
            }

            if (anObjectToCollideWith.Intersects(tempRectangleAbove))
            {
                Game.AccessPlayer.AccessMovementStop[2] = 1;
                myCollisionCases[2] = 1;
            }
            else
            {
                myCollisionCases[2] = 0;
            }

            if (anObjectToCollideWith.Intersects(tempRectangleDown))
            {
                Game.AccessPlayer.AccessMovementStop[3] = 1;
                myCollisionCases[3] = 1;
            }
            else
            {
                myCollisionCases[3] = 0;
            }
        }

        public void Draw(SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.Draw(Game.AccessSandstoneSprite, new Rectangle((int)myPosition.X, (int)myPosition.Y, mySizeX, mySizeY), Color.White);
        }
    }
}
