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
    public class TankBoss
    {
        private Vector2 myPosition;
        private Other.Animation
            myDrivingAnimation,
            myShootingAnimation,
            myDeathAnimation;
        private List<Other.Animation> myExplosionAnimations;
        private List<bool> myPlayExplSoundList;
        private float
            myShootingDelay,
            myReturnToDrivingDelay,
            myEntranceDelay,
            myExplosionAddDelay,
            myChangeBackColorDelay;
        private bool
            myIsAlive,
            myShootBullet,
            myPlayVictorySound;
        private int
            myBossHealth,
            myBossStage;
        private int[]
            myXPosForExplosion,
            myYPosForExplosion;
        private SoundEffectInstance
            myTankIdlingSound,
            myTankDeathVictorySound;
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

        public TankBoss(Vector2 aPosition)
        {
            myDrivingAnimation = new Other.Animation(Game.AccessTankDrivingSprite, new Vector2(148, 148), new Vector2(0, 0), new Vector2(6, 1), 6);
            myShootingAnimation = new Other.Animation(Game.AccessTankShootingSprite, new Vector2(240, 148), new Vector2(0, 0), new Vector2(6, 6), 5);
            myDeathAnimation = new Other.Animation(Game.AccessTankDeathSprite, new Vector2(200, 148), new Vector2(0, 0), new Vector2(3, 4), 9);
            myExplosionAnimations = new List<Other.Animation>();
            myPosition = aPosition;
            myShootBullet = true;
            myPlayVictorySound = false;
            myPlayExplSoundList = new List<bool>();
            myShootingDelay = 2720 * 2;
            myReturnToDrivingDelay = 2720;
            myEntranceDelay = 4000;
            myChangeBackColorDelay = 0;
            myBossHealth = 40;
            myExplosionAddDelay = 200;
            myXPosForExplosion = new int[7];
            myYPosForExplosion = new int[7];
            myBossStage = 1;
            myDefaultColor = Color.White;
            myTankIdlingSound = Game.AccessTankIdleSound.CreateInstance();
            myTankDeathVictorySound = Game.AccessBossVictorySound.CreateInstance();
        }

        public void Update(GameTime aGameTime)
        {
            if (myBossHealth > 0)
            {
                if (myTankIdlingSound.State == SoundState.Stopped && myShootingDelay > 0)
                {
                    myTankIdlingSound.Play();
                }
                if (myDefaultColor != Color.White)
                {
                    myChangeBackColorDelay -= (float)aGameTime.ElapsedGameTime.TotalMilliseconds;
                }
                TakeDamage();

                if (myBossHealth <= 20)
                {
                    myBossStage = 2;
                }
                if (myEntranceDelay <= 0)
                {
                    if (myShootingDelay > 0)
                    {
                        myShootingDelay -= (float)aGameTime.ElapsedGameTime.TotalMilliseconds * Game.AccessUpdateSpeed;
                        myPosition.X += (float)Game.AccessGameSpeed / 2;
                        myShootingAnimation.AccessCurrentFrame = new Vector2(0, 0);
                        myShootBullet = true;
                    }
                    if (myShootingDelay <= 0)
                    {
                        myTankIdlingSound.Stop();
                        myPosition.X -= (float)Game.AccessGameSpeed;
                        myReturnToDrivingDelay -= (float)aGameTime.ElapsedGameTime.TotalMilliseconds * Game.AccessUpdateSpeed;
                        if (myReturnToDrivingDelay <= 0)
                        {
                            myShootingDelay = 2720 * 2;
                            myReturnToDrivingDelay = 2720;
                        }
                        if (myShootBullet && myReturnToDrivingDelay < 1400)
                        {
                            if (myBossStage == 1)
                            {
                                Game.AccessTankBullets.Add(new TankBullet(new Vector2(myPosition.X + 20, myPosition.Y + 50), (float)Math.Atan2(Game.AccessPlayer.AccessPosition.Y - myPosition.Y, Game.AccessPlayer.AccessPosition.X - myPosition.X)));
                            }
                            else
                            {
                                Game.AccessTankBullets.Add(new TankBullet(new Vector2(myPosition.X + 20, myPosition.Y + 50), (float)Math.Atan2(Game.AccessPlayer.AccessPosition.Y - myPosition.Y, Game.AccessPlayer.AccessPosition.X - myPosition.X)));
                                Game.AccessTankBullets.Add(new TankBullet(new Vector2(myPosition.X + 20, myPosition.Y + 50), (float)Math.Atan2(Game.AccessPlayer.AccessPosition.Y - myPosition.Y, Game.AccessPlayer.AccessPosition.X - myPosition.X) + 0.8f));
                                Game.AccessTankBullets.Add(new TankBullet(new Vector2(myPosition.X + 20, myPosition.Y + 50), (float)Math.Atan2(Game.AccessPlayer.AccessPosition.Y - myPosition.Y, Game.AccessPlayer.AccessPosition.X - myPosition.X) - 0.8f));
                            }
                            Game.AccessTankFireSound.Play();
                            myShootBullet = false;
                        }
                    }
                }
                else
                {
                    myEntranceDelay -= (float)aGameTime.ElapsedGameTime.TotalMilliseconds;
                    myPosition.X -= (float)Game.AccessGameSpeed;
                }
            }
            else
            {
                if (!myPlayVictorySound)
                {
                    myTankDeathVictorySound.Play();
                    myTankIdlingSound.Stop();
                    MediaPlayer.Stop();
                    myPlayVictorySound = true;
                }
                myExplosionAddDelay -= (float)aGameTime.ElapsedGameTime.TotalMilliseconds * Game.AccessUpdateSpeed;
                myPosition.X -= (float)Game.AccessGameSpeed;
                if (myPosition.X + 300 < 0)
                {
                    Game.AccessActiveLevel = 2;
                    Game.AccessLevel02Unlocked = true;
                    Game.AccessSwitchMusic = true;

                    myIsAlive = false;
                }
            }
        }

        public void TakeDamage()
        {
            if (myChangeBackColorDelay <= 0)
            {
                myDrivingAnimation.AccessColour = Color.White;
                myShootingAnimation.AccessColour = Color.White;
                myDeathAnimation.AccessColour = Color.White;
                myDefaultColor = Color.White;
            }
            else
            {
                myDrivingAnimation.AccessColour = Color.IndianRed;
                myShootingAnimation.AccessColour = Color.IndianRed;
                myDeathAnimation.AccessColour = Color.IndianRed;
            }
        }

        public void Draw(SpriteBatch aSpriteBatch, Random aRNG)
        {
            Rectangle tempDrivingDestRect = new Rectangle((int)myPosition.X, (int)myPosition.Y, 222, 237);
            Rectangle tempShootingDestRect = new Rectangle((int)myPosition.X - 138, (int)myPosition.Y - 42, 360, 237);
            Rectangle tempDeathDestRect = new Rectangle((int)myPosition.X - 40, (int)myPosition.Y - 37, 300, 237);
            if (myBossHealth > 0)
            {
                if (myEntranceDelay <= 0)
                {
                    if (myShootingDelay > 0)
                    {
                        myDrivingAnimation.Draw(aSpriteBatch, tempDrivingDestRect, 0);
                    }
                    else
                    {
                        myShootingAnimation.Draw(aSpriteBatch, tempShootingDestRect, 0);
                    }
                }
                else
                {
                    myDrivingAnimation.Draw(aSpriteBatch, tempDrivingDestRect, 0);
                    if (myDrivingAnimation.AccessCurrentFrame == new Vector2(2, 0))
                    {
                        myDrivingAnimation.AccessCurrentFrame = new Vector2(0, 0);
                    }
                }
            }
            else
            {
                myDeathAnimation.Draw(aSpriteBatch, tempDeathDestRect, 0);
                if (new Vector2(myDeathAnimation.AccessCurrentFrame.X + 1, myDeathAnimation.AccessCurrentFrame.Y + 1) == myDeathAnimation.AccessSheetSize)
                {
                    myDeathAnimation.AccessCurrentFrame = new Vector2(myDeathAnimation.AccessSheetSize.X - 2, myDeathAnimation.AccessSheetSize.Y - 1);
                }

                if (myExplosionAnimations.Count < 4 && myExplosionAddDelay <= 0)
                {
                    myXPosForExplosion[myExplosionAnimations.Count] = aRNG.Next(0, 160);
                    myYPosForExplosion[myExplosionAnimations.Count] = aRNG.Next(0, 70);
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
                        myXPosForExplosion[i - 1] = aRNG.Next(0, 160);
                        myYPosForExplosion[i - 1] = aRNG.Next(0, 70);
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
            aSpriteBatch.DrawString(Game.AccessGlobalFont, "Type II AFV Di-Cokka", new Vector2(480, 380), Color.OrangeRed);
            if (myBossHealth > 0)
            {
                aSpriteBatch.Draw(Game.AccessHealthbarSprite, new Rectangle(395 - (myBossHealth * 5) + 270, 342, myBossHealth * 10, 200), Color.White);
                aSpriteBatch.DrawString(Game.AccessGlobalFont, myBossHealth * 20 + "/800", new Vector2(610, 421), Color.Black);
            }
        }
    }

    public class TankBullet
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
            mySizeY,
            myBulletHealth;
        private Other.Animation myExplosionAnimation;

        public Vector2 AccessPosition
        {
            get
            {
                return myPosition;
            }
            set
            {
                myPosition = value;
            }
        }
        public bool AccessIsAlive
        {
            get
            {
                return myIsAlive;
            }
            set
            {
                myIsAlive = value;
            }
        }
        public bool AccessCollisionDetected
        {
            get
            {
                return myCollisionDetected;
            }
            set
            {
                myCollisionDetected = value;
            }
        }
        public int AccessSizeX
        {
            get
            {
                return mySizeX;
            }
        }
        public int AccessSizeY
        {
            get
            {
                return mySizeY;
            }
        }
        public int AccessBulletHealth
        {
            get
            {
                return myBulletHealth;
            }
            set
            {
                myBulletHealth = value;
            }
        }

        public TankBullet(Vector2 aPosition, float aRotation)
        {
            myPosition = aPosition;
            myRotation = aRotation;
            myIsAlive = true;
            myCollisionDetected = false;
            mySpeed = 5;
            mySizeX = 48;
            mySizeY = 48;
            myBulletHealth = 3;
            myPlaySound = false;
            myExplosionAnimation = new Other.Animation(Game.AccessExplosionSprite, new Vector2(200, 400), new Vector2(0, 0), new Vector2(21, 1), 4);
        }

        public void Update(GameTime aGameTime, GameWindow aWindow)
        {
            if (myPosition.Y < 315 && !myCollisionDetected)
            {
                myPosition.Y += mySpeed * (float)Math.Sin(myRotation) * Game.AccessUpdateSpeed;
                myPosition.X += mySpeed * (float)Math.Cos(myRotation) * Game.AccessUpdateSpeed;

                if (myBulletHealth <= 0)
                {
                    myCollisionDetected = true;
                }
            }
            else
            {
                myPosition.X -= (float)Game.AccessGameSpeed;

                if (myExplosionAnimation.AccessCurrentFrame == new Vector2(myExplosionAnimation.AccessSheetSize.X - 1, myExplosionAnimation.AccessSheetSize.Y - 1))
                {
                    myIsAlive = false;
                }
            }
            Collision();

            if (myPosition.X > Game.AccessPlayer.AccessPosition.X)
            {
                float angleTo = (float)Math.Atan2(Game.AccessPlayer.AccessPosition.Y - myPosition.Y, Game.AccessPlayer.AccessPosition.X - myPosition.X); //Angle till spelaren

                if (angleTo > myRotation) //Om anglen till spelaren - den nuvarande angle är större än pi
                {
                    while (angleTo - myRotation > MathHelper.Pi)
                    {
                        angleTo -= MathHelper.TwoPi; //Minus med två PI
                    }
                }
                else //Om Angle är större än angleTo
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
                aSpriteBatch.Draw(Game.AccessTankBulletSprite, tempDestRect, null, Color.White, myRotation + ((float)Math.PI / 2) * 2, new Vector2(mySizeX / 2, mySizeY / 2), SpriteEffects.None, 0f);
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
}
