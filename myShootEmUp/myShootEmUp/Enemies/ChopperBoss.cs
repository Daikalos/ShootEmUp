using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace myShootEmUp.Enemies
{
    public class ChopperBoss
    {
        private Vector2 myPosition;
        private Other.Animation myFlyingAnimation;
        private List<Other.Animation> myExplosionAnimations;
        private List<bool> myPlayExplSoundList;
        private float
            myShootingDelay,
            myEntranceDelay,
            myExplosionAddDelay,
            myChangeBackColorDelay,
            myBombingRunDelay,
            myBombingRunDuration,
            myDropBombDelay,
            myRotation;
        private bool
            myIsAlive,
            myPlayVictorySound;
        private int myBossHealth;
        private int[]
            myXPosForExplosion,
            myYPosForExplosion;
        private SoundEffectInstance
            myChopperIdlingSound,
            myVictorySound;
        private Color myDefaultColor;

        public Vector2 AccessPosition
        {
            get => myPosition;
            set => myPosition = value;
        }
        public Color AccessDefaultColor
        {
            get => myDefaultColor;
            set => myDefaultColor = value;
        }
        public bool AccessIsAlive
        {
            get => myIsAlive;
            set => myIsAlive = value;
        }
        public int AccessBossHealth
        {
            get => myBossHealth;
            set => myBossHealth = value;
        }
        public float AccessTakeDamageDelay
        {
            get => myChangeBackColorDelay;
            set => myChangeBackColorDelay = value;
        }

        public ChopperBoss(Vector2 aPosition)
        {
            myPosition = aPosition;
            myShootingDelay = 1500;
            myEntranceDelay = 2400;
            myBombingRunDelay = 8000;
            myBombingRunDuration = 6630;
            myDropBombDelay = 1000;
            myBossHealth = 50;
            myExplosionAddDelay = 200;
            myXPosForExplosion = new int[7];
            myYPosForExplosion = new int[7];
            myDefaultColor = Color.White;
            myPlayExplSoundList = new List<bool>();
            myExplosionAnimations = new List<Other.Animation>();
            myChopperIdlingSound = Game.AccessChopperIdleSound.CreateInstance();
            myVictorySound = Game.AccessBossVictorySound.CreateInstance();
            myFlyingAnimation = new Other.Animation(Game.AccessChopperBossFlyingSprite, new Vector2(279, 210), new Vector2(0, 0), new Vector2(1, 3), 4);
        }

        public void Update(GameTime aGameTime, GameWindow aWindow)
        {
            if (myBossHealth > 0)
            {
                if (myChopperIdlingSound.State == SoundState.Stopped)
                {
                    myChopperIdlingSound.Play();
                }
                if (myDefaultColor != Color.White)
                {
                    myChangeBackColorDelay -= (float)aGameTime.ElapsedGameTime.TotalMilliseconds;
                }
                TakeDamage();

                if (myEntranceDelay <= 0)
                {
                    myShootingDelay -= (float)aGameTime.ElapsedGameTime.TotalMilliseconds * Game.AccessUpdateSpeed;
                    myBombingRunDelay -= (float)aGameTime.ElapsedGameTime.TotalMilliseconds * Game.AccessUpdateSpeed;
                    if (myShootingDelay <= 0 && myBombingRunDelay > 0)
                    {
                        Game.AccessChopperMissiles.Add(new ChopperMissile(new Vector2(myPosition.X + 20, myPosition.Y + 150), 7, (float)Math.Atan2(Game.AccessPlayer.AccessPosition.Y - myPosition.Y, Game.AccessPlayer.AccessPosition.X - myPosition.X)));
                        myShootingDelay = 1200;
                    }
                    BombingRun(aGameTime, aWindow);
                }
                else
                {
                    myEntranceDelay -= (float)aGameTime.ElapsedGameTime.TotalMilliseconds * Game.AccessUpdateSpeed;
                    myPosition.X -= 150f * (float)aGameTime.ElapsedGameTime.TotalSeconds * Game.AccessUpdateSpeed;
                }
            }
            else
            {
                myPosition.Y += (float)aGameTime.ElapsedGameTime.TotalSeconds * 70 * Game.AccessUpdateSpeed;
                myPosition.X -= 150f * (float)aGameTime.ElapsedGameTime.TotalSeconds * Game.AccessUpdateSpeed;
                myExplosionAddDelay -= (float)aGameTime.ElapsedGameTime.TotalMilliseconds * Game.AccessUpdateSpeed;

                if (myVictorySound.State == SoundState.Stopped && !myPlayVictorySound)
                {
                    myVictorySound.Play();
                    MediaPlayer.Stop();
                    myPlayVictorySound = true;
                }
                if (myPosition.Y > aWindow.ClientBounds.Height)
                {
                    Game.AccessActiveLevel = 3;
                    Game.AccessLevel03Unlocked = true;
                    Game.AccessSwitchMusic = true;

                    myIsAlive = false;
                }
            }
        }

        private void BombingRun(GameTime aGameTime, GameWindow aWindow)
        {
            if (myBombingRunDelay <= 0)
            {
                if (myRotation > -0.32)
                {
                    myRotation -= 0.005f * Game.AccessUpdateSpeed;
                }
                myPosition.X -= 9 * Game.AccessUpdateSpeed;
                myBombingRunDuration -= (float)aGameTime.ElapsedGameTime.TotalMilliseconds * Game.AccessUpdateSpeed;
                myDropBombDelay -= (float)aGameTime.ElapsedGameTime.TotalMilliseconds * Game.AccessUpdateSpeed;

                if (myDropBombDelay <= 0)
                {
                    Game.AccessChopperBombs.Add(new ChopperBomb(new Vector2(myPosition.X + 70, myPosition.Y + 20), 5));
                    myDropBombDelay = 400;
                }
                if (myPosition.X + 250 < 0)
                {
                    myPosition.X = aWindow.ClientBounds.Width + 250;
                }
                if (myBombingRunDuration <= 0)
                {
                    myPosition.X = aWindow.ClientBounds.Width - 250;
                    myBombingRunDelay = 8000;
                    myBombingRunDuration = 6630;
                }
            }
            if (myBombingRunDuration <= 2000)
            {
                if (myRotation < 0)
                {
                    myRotation += 0.01f * Game.AccessUpdateSpeed;
                }
                else
                {
                    myRotation = 0;
                }
            }
        }
        public void TakeDamage()
        {
            if (myChangeBackColorDelay <= 0)
            {
                myFlyingAnimation.AccessColour = Color.White;
                myDefaultColor = Color.White;
            }
            else
            {
                myFlyingAnimation.AccessColour = Color.IndianRed;
            }
        }

        public void Draw(SpriteBatch aSpriteBatch, Random aRNG)
        {
            Rectangle tempFlyingDestRect = new Rectangle((int)myPosition.X, (int)myPosition.Y, (int)(279 * 0.75), (int)(210 * 0.75));
            if (myBossHealth > 0)
            {
                myFlyingAnimation.Draw(aSpriteBatch, tempFlyingDestRect, myRotation);
            }
            else
            {
                myFlyingAnimation.Draw(aSpriteBatch, tempFlyingDestRect, myRotation);
                if (myExplosionAnimations.Count < 4 && myExplosionAddDelay <= 0)
                {
                    myXPosForExplosion[myExplosionAnimations.Count] = aRNG.Next(0, 120);
                    myYPosForExplosion[myExplosionAnimations.Count] = aRNG.Next(0, 60);
                    myExplosionAnimations.Add(new Other.Animation(Game.AccessExplosionSprite, new Vector2(200, 400), new Vector2(0, 0), new Vector2(21, 1), 3));
                    myPlayExplSoundList.Add(true);
                    myExplosionAddDelay = aRNG.Next(150, 200);
                    Game.AccessExplosionSound.Play();
                }

                for (int i = myExplosionAnimations.Count; i > 0; i--)
                {
                    myExplosionAnimations[i - 1].Draw(aSpriteBatch, new Rectangle((int)myPosition.X + myXPosForExplosion[i - 1], (int)myPosition.Y + myYPosForExplosion[i - 1], 64, 128), 0);
                    if (new Vector2(myExplosionAnimations[i - 1].AccessCurrentFrame.X + 1, myExplosionAnimations[i - 1].AccessCurrentFrame.Y + 1) == myExplosionAnimations[i - 1].AccessSheetSize)
                    {
                        myXPosForExplosion[i - 1] = aRNG.Next(0, 120);
                        myYPosForExplosion[i - 1] = aRNG.Next(0, 60);
                        if (myPlayExplSoundList[i - 1])
                        {
                            myPlayExplSoundList[i - 1] = false;
                            Game.AccessExplosionSound.Play();
                        }
                    }
                    else
                    {
                        myPlayExplSoundList[i - 1] = true;
                    }
                }
            }
            aSpriteBatch.DrawString(Game.AccessGlobalFont, "R-Shobu", new Vector2(630, 380), Color.OrangeRed);
            if (myBossHealth > 0)
            {
                aSpriteBatch.Draw(Game.AccessHealthbarSprite, new Rectangle(420 - (myBossHealth * 5) + 270, 342, myBossHealth * 10, 200), Color.White);
                aSpriteBatch.DrawString(Game.AccessGlobalFont, myBossHealth * 20 + "/1000", new Vector2(610, 421), Color.Black);
            }
        }
    }

    public class ChopperMissile
    {
        private Vector2 myPosition;
        private float
            mySpeed,
            myRotation;
        private bool
            myIsAlive,
            myCollisionDetected,
            myPlaySound;
        private int
            mySizeX,
            mySizeY;
        private Other.Animation
            myExplosionAnimation,
            myMissileAnimation;

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
        public bool AccessCollisionDetected
        {
            get => myCollisionDetected;
            set => myCollisionDetected = value;
        }
        public int AccessSizeX
        {
            get => mySizeX;
        }
        public int AccessSizeY
        {
            get => mySizeY;
        }

        public ChopperMissile(Vector2 aPosition, float aSpeed, float aRotation)
        {
            mySizeX = (int)(96 * 0.8);
            mySizeY = (int)(32 * 0.8);
            myPosition = aPosition;
            myRotation = aRotation;
            mySpeed = aSpeed;
            myIsAlive = true;
            myExplosionAnimation = new Other.Animation(Game.AccessExplosionSprite, new Vector2(200, 400), new Vector2(0, 0), new Vector2(21, 1), 4);
            myMissileAnimation = new Other.Animation(Game.AccessChopperBossMissileSprite, new Vector2(165, 48), new Vector2(0, 0), new Vector2(1, 3), 5);
        }

        public void Update(GameWindow aWindow, GameTime aGameTime)
        {
            if (myPosition.Y < 315 && !myCollisionDetected)
            {
                myPosition.Y += mySpeed * (float)Math.Sin(myRotation) * Game.AccessUpdateSpeed;
                myPosition.X += mySpeed * (float)Math.Cos(myRotation) * Game.AccessUpdateSpeed;
            }
            else //Explosion sker
            {
                myPosition.X -= (float)Game.AccessGameSpeed;
                myCollisionDetected = true;

                if (myExplosionAnimation.AccessCurrentFrame == new Vector2(myExplosionAnimation.AccessSheetSize.X - 1, myExplosionAnimation.AccessSheetSize.Y - 1))
                {
                    myIsAlive = false;
                }
            }
            Collision();

            if (myPosition.X > Game.AccessPlayer.AccessPosition.X) //Gör objektet homing
            {
                float angleTo = (float)Math.Atan2(Game.AccessPlayer.AccessPosition.Y - myPosition.Y, Game.AccessPlayer.AccessPosition.X - myPosition.X); //Angle till spelaren

                if (angleTo > myRotation)
                {
                    while (angleTo - myRotation > MathHelper.Pi) //Om anglen till spelaren - den nuvarande angle är större än pi
                    {
                        angleTo -= MathHelper.TwoPi; //Minus med två PI
                    }
                }
                else 
                {
                    while (angleTo - myRotation < -MathHelper.Pi) //Om anglen till spelaren - den nuvarande anglen är mindre än negativ pi
                    {
                        angleTo += MathHelper.TwoPi; //Addera med två pi
                    }
                }
                myRotation = MathHelper.Lerp(myRotation, angleTo, 0.03f * Game.AccessUpdateSpeed); //Ändra anglen "Linear Interpolation". 0.03f är hur snabbt missilen ska vända sig
            }

            if (myPosition.X + mySizeX * 2 < 0 || myPosition.X - mySizeX / 2 > aWindow.ClientBounds.Width || myPosition.Y + mySizeY * 2 < 0 || myPosition.Y - mySizeY / 2 > aWindow.ClientBounds.Height)
            { 
                myIsAlive = false;
            }
        }

        public void Collision()
        {
            foreach (Rectangle collisionBox in Game.AccessBoulder.AccessRectangles)
            {
                if (new Rectangle((int)myPosition.X, (int)myPosition.Y, mySizeX, mySizeY).Intersects(collisionBox))
                {
                    myCollisionDetected = true;
                }
            }
        }

        public void Draw(SpriteBatch aSpriteBatch)
        {
            if (myPosition.Y < 315 && !myCollisionDetected)
            {
                Rectangle tempDestRect = new Rectangle((int)myPosition.X, (int)myPosition.Y, mySizeX, mySizeY);
                myMissileAnimation.Draw(aSpriteBatch, tempDestRect, myRotation + ((float)Math.PI / 2) * 2);
            }
            else
            {
                myExplosionAnimation.Draw(aSpriteBatch, new Rectangle((int)myPosition.X, (int)myPosition.Y - 114, 64, 128), 0);
                if (!myPlaySound)
                {
                    myPlaySound = true;
                    Game.AccessExplosionSound.Play();
                }
            }
        }
    }

    public class ChopperBomb
    {
        private Vector2 myPosition;
        private float mySpeed;
        private bool
            myIsAlive,
            myCollisionDetected,
            myPlaySound;
        private int
            mySizeX,
            mySizeY;
        private Other.Animation myExplosionAnimation;

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
        public bool AccessCollisionDetected
        {
            get => myCollisionDetected;
            set => myCollisionDetected = value;
        }
        public int AccessSizeX
        {
            get => mySizeX;
        }
        public int AccessSizeY
        {
            get => mySizeY;
        }

        public ChopperBomb(Vector2 aPosition, float aSpeed)
        {
            myPosition = aPosition;
            mySpeed = aSpeed;
            mySizeX = (int)(36 * 0.7);
            mySizeY = (int)(67 * 0.7);
            myIsAlive = true;
            myExplosionAnimation = new Other.Animation(Game.AccessExplosionSprite, new Vector2(200, 400), new Vector2(0, 0), new Vector2(21, 1), 4);
        }

        public void Update(GameWindow aWindow, GameTime aGameTime)
        {
            if (myPosition.Y + mySizeY < 315 && !myCollisionDetected)
            {
                myPosition.Y += mySpeed * (float)aGameTime.ElapsedGameTime.TotalSeconds * 70 * Game.AccessUpdateSpeed;
                myPosition.X -= (float)Game.AccessGameSpeed;
            }
            else //Explosion sker
            {
                myPosition.X -= (float)Game.AccessGameSpeed;
                myCollisionDetected = true;

                if (myExplosionAnimation.AccessCurrentFrame == new Vector2(myExplosionAnimation.AccessSheetSize.X - 1, myExplosionAnimation.AccessSheetSize.Y - 1))
                {
                    myIsAlive = false;
                }
            }
            foreach (Block block in Game.AccessBlocks)
            {
                if (HitBox.Calculate(mySizeX, mySizeY, block.AccessSizeX, block.AccessSizeY, new Vector2(myPosition.X + 32, myPosition.Y + 8), block.AccessPosition))
                {
                    myCollisionDetected = true;
                }
            }
        }

        public void Draw(SpriteBatch aSpriteBatch)
        {
            if (myPosition.Y + mySizeY < 315 && !myCollisionDetected)
            {
                Rectangle tempDestRect = new Rectangle((int)myPosition.X, (int)myPosition.Y, mySizeX, mySizeY);
                aSpriteBatch.Draw(Game.AccessChopperBossBombSprite, tempDestRect, Color.White);
            }
            else
            {
                myExplosionAnimation.Draw(aSpriteBatch, new Rectangle((int)myPosition.X, (int)myPosition.Y - 70, 64, 128), 0);
                if (!myPlaySound)
                {
                    myPlaySound = true;
                    Game.AccessExplosionSound.Play();
                }
            }
        }
    }
}
