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
    public class Grenade
    {
        private bool myIsAlive;
        private int mySpeed;
        private bool
            myCollisionDetected,
            mySoundPlaying;
        private float 
            myRotation,
            myRemoveExplosionTimer;
        Vector2
            myPosition,
            myVelocity;
        Other.simpleAnimation myAnimationForGrenade;

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

        public Grenade(Vector2 aPosition, int aSpeed, Texture2D aGrenadeExplosionSprite)
        {
            myPosition = aPosition;
            mySpeed = aSpeed;
            myAnimationForGrenade = new Other.simpleAnimation(aGrenadeExplosionSprite, new Vector2(200, 400), new Vector2(0, 0), new Vector2(21, 1), 3);

            myVelocity.Y = -5f;
            myRotation = 2.8f;
            myRemoveExplosionTimer = 976;
            myIsAlive = true;
            myCollisionDetected = false;
            mySoundPlaying = false;
        }

        public void Update(GameTime aGameTime)
        {
            if (myPosition.Y < 315 && !myCollisionDetected)
            {
                myVelocity.Y += 0.20f * (float)aGameTime.ElapsedGameTime.TotalSeconds * 40;
                myRotation += 0.08f * (float)aGameTime.ElapsedGameTime.TotalSeconds * 40; 
                myPosition.X += mySpeed * (float)aGameTime.ElapsedGameTime.TotalSeconds * 40;
                myPosition += myVelocity;

                Collision();
            }
            else
            {
                myPosition.X -= (float)Game.AccessGameSpeed;
                myRemoveExplosionTimer -= (float)aGameTime.ElapsedGameTime.Milliseconds;

                if (myRemoveExplosionTimer <= 0)
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

        public void Collision()
        {
            if (myPosition.Y >= 315 || myCollisionDetected)
            {
                foreach (BaseEnemy enemy in Game.AccessBaseEnemies)
                {
                    if (HitBox.Calculate(64, 128, enemy.AccessSizeX, enemy.AccessSizeY, new Vector2(myPosition.X, myPosition.Y - 114), enemy.AccessPosition))
                    {
                        myCollisionDetected = true;
                        enemy.AccessEnemyHealth -= 100;
                    }
                }
            }
            else
            {
                foreach (BaseEnemy enemy in Game.AccessBaseEnemies)
                {
                    if (HitBox.Calculate(16, 16, enemy.AccessSizeX, enemy.AccessSizeY, new Vector2(myPosition.X + 32, myPosition.Y + 8), enemy.AccessPosition))
                    {
                        myCollisionDetected = true;
                        enemy.AccessEnemyHealth -= 100;
                    }
                }
                
                if (HitBox.Calculate(16, 16, 210, 130, new Vector2(myPosition.X + 32, myPosition.Y + 8), new Vector2(Game.AccessBoss.AccessPosition.X, Game.AccessBoss.AccessPosition.Y + 70)))
                {
                    myCollisionDetected = true;
                    Game.AccessBoss.AccessBossHealth--;
                    Game.AccessBoss.AccessDefaultColor = Color.Red;
                    Game.AccessBoss.AccessTakeDamageDelay = 100;
                }
                if (HitBox.Calculate(16, 16, 135, 50, new Vector2(myPosition.X + 32, myPosition.Y + 8), new Vector2(Game.AccessBoss.AccessPosition.X + 40, Game.AccessBoss.AccessPosition.Y + 20)))
                {
                    myCollisionDetected = true;
                    Game.AccessBoss.AccessBossHealth--;
                    Game.AccessBoss.AccessDefaultColor = Color.Red;
                    Game.AccessBoss.AccessTakeDamageDelay = 100;
                }
            }
        }

        public void Draw(SpriteBatch aSpriteBatch)
        {
            if (myPosition.Y >= 315 || myCollisionDetected)
            {
                myVelocity.Y = 0;

                myAnimationForGrenade.Draw(aSpriteBatch, new Rectangle((int)myPosition.X, (int)myPosition.Y - 114, 64, 128));
            }
            else
            {
                aSpriteBatch.Draw(Game.AccessGrenadeSprite, new Rectangle((int)myPosition.X + 32, (int)myPosition.Y + 8, 24, 12), null, Color.White, myRotation, new Vector2(Game.AccessGrenadeSprite.Width / 2, Game.AccessGrenadeSprite.Height / 2), SpriteEffects.None, 0f);
            }
        }
    }
}
