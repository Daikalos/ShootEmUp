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

namespace myShootEmUp
{
    public abstract class PlayerAbility
    {
        protected Vector2 myPosition;
        protected int mySizeX, mySizeY, mySpeed;
        protected bool myIsAlive;

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

        public abstract void Init(Vector2 aPosition, int aSpeed);

        public abstract void Update(GameTime aGameTime, GameWindow aWindow);

        public abstract void Draw(SpriteBatch aSpriteBatch);
    }

    public class Grenade : PlayerAbility
    {
        private bool
            myCollisionDetected,
            mySoundPlaying;
        private float myRotation;
        Vector2 myVelocity;
        Other.Animation myExplosionAnimation;

        public override void Init(Vector2 aPosition, int aSpeed)
        {
            myPosition = aPosition;
            mySpeed = aSpeed;
            myExplosionAnimation = new Other.Animation(Game.AccessExplosionSprite, new Vector2(200, 400), new Vector2(0, 0), new Vector2(21, 1), 4);

            myVelocity.Y = -5f;
            myRotation = 2.8f;
            myIsAlive = true;
            myCollisionDetected = false;
            mySoundPlaying = false;
        }

        public override void Update(GameTime aGameTime, GameWindow aWindow)
        {
            if (myPosition.Y < 315 && !myCollisionDetected)
            {
                myVelocity.Y += 0.20f * (float)aGameTime.ElapsedGameTime.TotalSeconds * 40 * (float)Game.AccessUpdateSpeed;
                myRotation += 0.08f * (float)aGameTime.ElapsedGameTime.TotalSeconds * 40 * (float)Game.AccessUpdateSpeed;
                myPosition.X += mySpeed * (float)aGameTime.ElapsedGameTime.TotalSeconds * 40 * (float)Game.AccessUpdateSpeed;
                myPosition += myVelocity * (float)Game.AccessUpdateSpeed;

                Collision();
            }
            else
            {
                myPosition.X -= (float)Game.AccessGameSpeed;

                if (myExplosionAnimation.AccessCurrentFrame == new Vector2(myExplosionAnimation.AccessSheetSize.X - 1, myExplosionAnimation.AccessSheetSize.Y - 1))
                {
                    myIsAlive = false;
                }

                if (!mySoundPlaying)
                {
                    Game.AccessExplosionSound.Play();
                    mySoundPlaying = true;
                }
            }
        }

        private void Collision()
        {
            if (!myCollisionDetected)
            {
                foreach (BaseEnemy enemy in Game.AccessBaseEnemies)
                {
                    if (HitBox.Calculate(64, 128, enemy.AccessSizeX, enemy.AccessSizeY, new Vector2(myPosition.X, myPosition.Y - 114), enemy.AccessPosition))
                    {
                        myCollisionDetected = true;
                        if (enemy.AccessEnemyHealth != 0) //Reducera inte spikeball fiendens hp, så ett specifikt ljud kan fungera / Kolla PlayerBullet -> CollisionCheck för fiender
                        {
                            enemy.AccessEnemyHealth -= 100;
                            if (enemy.AccessEnemyHealth <= 0)
                            {
                                Game.AccessAddCoins += 2;
                            }
                        }
                    }
                }
                if (HitBox.Calculate(16, 16, 210, 130, new Vector2(myPosition.X + 32, myPosition.Y + 8), new Vector2(Game.AccessTankBoss.AccessPosition.X, Game.AccessTankBoss.AccessPosition.Y + 70)))
                {
                    myCollisionDetected = true;
                    Game.AccessTankBoss.AccessBossHealth--;
                    Game.AccessTankBoss.AccessDefaultColor = Color.Red;
                    Game.AccessTankBoss.AccessTakeDamageDelay = 100;
                }
                if (HitBox.Calculate(16, 16, 135, 50, new Vector2(myPosition.X + 32, myPosition.Y + 8), new Vector2(Game.AccessTankBoss.AccessPosition.X + 40, Game.AccessTankBoss.AccessPosition.Y + 20)))
                {
                    myCollisionDetected = true;
                    Game.AccessTankBoss.AccessBossHealth--;
                    Game.AccessTankBoss.AccessDefaultColor = Color.Red;
                    Game.AccessTankBoss.AccessTakeDamageDelay = 100;
                }

                if (HitBox.Calculate(16, 16, (int)(140 * 0.75), (int)(140 * 0.75), new Vector2(myPosition.X + 32, myPosition.Y + 8), new Vector2(Game.AccessChopperBoss.AccessPosition.X + 10, Game.AccessChopperBoss.AccessPosition.Y + 50)))
                {
                    myCollisionDetected = true;
                    Game.AccessChopperBoss.AccessBossHealth--;
                    Game.AccessChopperBoss.AccessDefaultColor = Color.Red;
                    Game.AccessChopperBoss.AccessTakeDamageDelay = 100;
                }
                if (HitBox.Calculate(16, 16, (int)(140 * 0.75), (int)(140 * 0.75), new Vector2(myPosition.X + 32, myPosition.Y + 8), new Vector2(Game.AccessChopperBoss.AccessPosition.X + 60, Game.AccessChopperBoss.AccessPosition.Y + 20)))
                {
                    myCollisionDetected = true;
                    Game.AccessChopperBoss.AccessBossHealth--;
                    Game.AccessChopperBoss.AccessDefaultColor = Color.Red;
                    Game.AccessChopperBoss.AccessTakeDamageDelay = 100;
                }
                if (HitBox.Calculate(16, 16, (int)(60 * 0.75), (int)(80 * 0.75), new Vector2(myPosition.X + 32, myPosition.Y + 8), new Vector2(Game.AccessChopperBoss.AccessPosition.X + 150, Game.AccessChopperBoss.AccessPosition.Y)))
                {
                    myCollisionDetected = true;
                    Game.AccessChopperBoss.AccessBossHealth--;
                    Game.AccessChopperBoss.AccessDefaultColor = Color.Red;
                    Game.AccessChopperBoss.AccessTakeDamageDelay = 100;
                }
            }
        }

        public override void Draw(SpriteBatch aSpriteBatch)
        {
            if (myPosition.Y >= 315 || myCollisionDetected)
            {
                myVelocity.Y = 0;

                myExplosionAnimation.Draw(aSpriteBatch, new Rectangle((int)myPosition.X, (int)myPosition.Y - 114, 64, 128), 0);
            }
            else
            {
                aSpriteBatch.Draw(Game.AccessGrenadeSprite, new Rectangle((int)myPosition.X + 32, (int)myPosition.Y + 8, 24, 12), null, Color.White, myRotation, new Vector2(Game.AccessGrenadeSprite.Width / 2, Game.AccessGrenadeSprite.Height / 2), SpriteEffects.None, 0f);
            }
        }
    }

    public class Missile : PlayerAbility
    {
        private bool
            myCollisionDetected,
            mySoundPlaying;
        Other.Animation myExplosionAnimation;
        Other.Animation myMissileAnimation;

        public override void Init(Vector2 aPosition, int aSpeed)
        {
            myPosition = aPosition;
            mySpeed = aSpeed;
            myExplosionAnimation = new Other.Animation(Game.AccessExplosionSprite, new Vector2(200, 400), new Vector2(0, 0), new Vector2(21, 1), 4);
            myMissileAnimation = new Other.Animation(Game.AccessPlayerMissileSprite, new Vector2(222, 72), new Vector2(0, 0), new Vector2(1, 2), 3);

            myIsAlive = true;
            myCollisionDetected = false;
            mySoundPlaying = false;
        }

        public override void Update(GameTime aGameTime, GameWindow aWindow)
        {
            if (myPosition.Y < 315 && !myCollisionDetected)
            {
                myPosition.X += mySpeed * 20 * (float)aGameTime.ElapsedGameTime.TotalSeconds * (float)Game.AccessUpdateSpeed;
            }
            else
            {
                myPosition.X -= (float)Game.AccessGameSpeed;

                if (myExplosionAnimation.AccessCurrentFrame == new Vector2(myExplosionAnimation.AccessSheetSize.X - 1, myExplosionAnimation.AccessSheetSize.Y - 1))
                {
                    myIsAlive = false;
                }

                if (!mySoundPlaying)
                {
                    Game.AccessExplosionSound.Play();
                    mySoundPlaying = true;
                }
            }
            Collision();

            if (myPosition.X + mySizeX * 2 < 0 || myPosition.X - mySizeX / 2 > aWindow.ClientBounds.Width || myPosition.Y + mySizeY * 2 < 0 || myPosition.Y - mySizeY / 2 > aWindow.ClientBounds.Height)
            {
                myIsAlive = false;
            }
        }

        private void Collision()
        {
            if (!myCollisionDetected)
            {
                foreach (BaseEnemy enemy in Game.AccessBaseEnemies)
                {
                    if (HitBox.Calculate(64, 32, enemy.AccessSizeX, enemy.AccessSizeY, new Vector2(myPosition.X + 32, myPosition.Y + 8), enemy.AccessPosition))
                    {
                        myCollisionDetected = true;
                        if (enemy.AccessEnemyHealth != 0) //Reducera inte spikeball fiendens hp, så ett specifikt ljud kan fungera / Kolla PlayerBullet -> CollisionCheck för fiender
                        {
                            enemy.AccessEnemyHealth -= 75;
                            if (enemy.AccessEnemyHealth <= 0)
                            {
                                Game.AccessAddCoins += 2;
                            }
                        }
                    }
                }
                foreach (Block block in Game.AccessBlocks)
                {
                    if (HitBox.Calculate(64, 32, block.AccessSizeX, block.AccessSizeY, new Vector2(myPosition.X + 32, myPosition.Y + 8), block.AccessPosition))
                    {
                        myCollisionDetected = true;
                    }
                }

                if (HitBox.Calculate(64, 32, 210, 130, new Vector2(myPosition.X + 32, myPosition.Y + 8), new Vector2(Game.AccessTankBoss.AccessPosition.X, Game.AccessTankBoss.AccessPosition.Y + 70)))
                {
                    myCollisionDetected = true;
                    Game.AccessTankBoss.AccessBossHealth--;
                    Game.AccessTankBoss.AccessDefaultColor = Color.Red;
                    Game.AccessTankBoss.AccessTakeDamageDelay = 100;
                }
                if (HitBox.Calculate(64, 32, 135, 50, new Vector2(myPosition.X + 32, myPosition.Y + 8), new Vector2(Game.AccessTankBoss.AccessPosition.X + 40, Game.AccessTankBoss.AccessPosition.Y + 20)))
                {
                    myCollisionDetected = true;
                    Game.AccessTankBoss.AccessBossHealth--;
                    Game.AccessTankBoss.AccessDefaultColor = Color.Red;
                    Game.AccessTankBoss.AccessTakeDamageDelay = 100;
                }

                if (HitBox.Calculate(64, 32, (int)(140 * 0.75), (int)(140 * 0.75), new Vector2(myPosition.X + 32, myPosition.Y + 8), new Vector2(Game.AccessChopperBoss.AccessPosition.X + 10, Game.AccessChopperBoss.AccessPosition.Y + 50)))
                {
                    myCollisionDetected = true;
                    Game.AccessChopperBoss.AccessBossHealth--;
                    Game.AccessChopperBoss.AccessDefaultColor = Color.Red;
                    Game.AccessChopperBoss.AccessTakeDamageDelay = 100;
                }
                if (HitBox.Calculate(64, 32, (int)(140 * 0.75), (int)(140 * 0.75), new Vector2(myPosition.X + 32, myPosition.Y + 8), new Vector2(Game.AccessChopperBoss.AccessPosition.X + 60, Game.AccessChopperBoss.AccessPosition.Y + 20)))
                {
                    myCollisionDetected = true;
                    Game.AccessChopperBoss.AccessBossHealth--;
                    Game.AccessChopperBoss.AccessDefaultColor = Color.Red;
                    Game.AccessChopperBoss.AccessTakeDamageDelay = 100;
                }
                if (HitBox.Calculate(64, 32, (int)(60 * 0.75), (int)(80 * 0.75), new Vector2(myPosition.X + 32, myPosition.Y + 8), new Vector2(Game.AccessChopperBoss.AccessPosition.X + 150, Game.AccessChopperBoss.AccessPosition.Y)))
                {
                    myCollisionDetected = true;
                    Game.AccessChopperBoss.AccessBossHealth--;
                    Game.AccessChopperBoss.AccessDefaultColor = Color.Red;
                    Game.AccessChopperBoss.AccessTakeDamageDelay = 100;
                }
            }
        }

        public override void Draw(SpriteBatch aSpriteBatch)
        {
            if (myPosition.Y >= 315 || myCollisionDetected)
            {
                myExplosionAnimation.Draw(aSpriteBatch, new Rectangle((int)myPosition.X + 32, (int)myPosition.Y - 114, 64, 128), 0);
            }
            else
            {
                myMissileAnimation.Draw(aSpriteBatch, new Rectangle((int)myPosition.X + 32, (int)myPosition.Y + 8, 64, 32), 0);
            }
        }
    }
}
