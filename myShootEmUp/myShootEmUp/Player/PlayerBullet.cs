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
    public class PlayerBullet
    {
        private Vector2 myPosition;
        private float mySpeed;
        private int mySizeX;
        private int mySizeY;
        private int myDamage;

        public PlayerBullet(Vector2 aPosition, int aDamage, float aSpeed)
        {
            myPosition = aPosition;
            mySpeed = aSpeed;
            myDamage = aDamage;
            mySizeX = 32;
            mySizeY = 32;
        }

        public void Update(GameWindow aWindow, GameTime aGameTime)
        {
            myPosition.X += mySpeed * (float)aGameTime.ElapsedGameTime.TotalSeconds * (float)Game.AccessUpdateSpeed;

            if (myPosition.X > aWindow.ClientBounds.Width)
            {
                Game.AccessPlayerBullets.Remove(this);
            }

            CollisionCheck();
        }
        public void CollisionCheck()
        {
            foreach (BaseEnemy enemy in Game.AccessBaseEnemies)
            {
                if (HitBox.Calculate(mySizeX - 6, mySizeY - 6, enemy.AccessSizeX, enemy.AccessSizeY, new Vector2(myPosition.X, myPosition.Y + 4), enemy.AccessPosition)) //Lämplig kollision för fiende eftersom fiende sprite är konstig med rektangel
                {
                    Game.AccessPlayerBullets.Remove(this);
                    if (enemy.AccessEnemyHealth == 0) //För Spikeball fiende
                    {
                        Game.AccessMetalHitSound.Play();
                    }
                    else
                    {
                        enemy.AccessEnemyHealth -= myDamage;
                        if (enemy.AccessEnemyHealth <= 0)
                        {
                            Game.AccessAddCoins += 2;
                        }
                    }
                    Game.AccessAmountOfHitsOnEnemies++;
                }
            }

            foreach (Enemies.TankBullet tankBullet in Game.AccessTankBullets)
            {
                if (!tankBullet.AccessCollisionDetected)
                {
                    if (HitBox.Calculate(mySizeX - 6, mySizeY - 6, tankBullet.AccessSizeX - 14, tankBullet.AccessSizeY - 12, new Vector2(myPosition.X, myPosition.Y + 4), new Vector2(tankBullet.AccessPosition.X - 8, tankBullet.AccessPosition.Y - 16)))
                    {
                        Game.AccessPlayerBullets.Remove(this);
                        tankBullet.AccessBulletHealth--;
                    }
                }
            }
            if (HitBox.Calculate(mySizeX - 6, mySizeY - 6, 210, 130, new Vector2(myPosition.X, myPosition.Y + 4), new Vector2(Game.AccessTankBoss.AccessPosition.X, Game.AccessTankBoss.AccessPosition.Y + 70)))
            {
                Game.AccessPlayerBullets.Remove(this);
                Game.AccessMetalHitSound.Play();
            }
            if (HitBox.Calculate(mySizeX - 6, mySizeY - 6, 135, 50, new Vector2(myPosition.X, myPosition.Y + 4), new Vector2(Game.AccessTankBoss.AccessPosition.X + 40, Game.AccessTankBoss.AccessPosition.Y + 20)))
            {
                Game.AccessPlayerBullets.Remove(this);
                Game.AccessMetalHitSound.Play();
            }

            foreach (Enemies.ChopperMissile chopperMissile in Game.AccessChopperMissiles)
            {
                if (!chopperMissile.AccessCollisionDetected)
                {
                    if (HitBox.Calculate(mySizeX - 6, mySizeY - 6, chopperMissile.AccessSizeX - 30, chopperMissile.AccessSizeY - 12, new Vector2(myPosition.X, myPosition.Y + 4), chopperMissile.AccessPosition))
                    {
                        Game.AccessPlayerBullets.Remove(this);
                        chopperMissile.AccessCollisionDetected = true;
                    }
                }
            }
            if (HitBox.Calculate(mySizeX - 6, mySizeY - 6, (int)(140 * 0.75), (int)(140 * 0.75), new Vector2(myPosition.X, myPosition.Y + 4), new Vector2(Game.AccessChopperBoss.AccessPosition.X + 10, Game.AccessChopperBoss.AccessPosition.Y + 50)))
            {
                Game.AccessPlayerBullets.Remove(this);
                Game.AccessMetalHitSound.Play();
            }
            if (HitBox.Calculate(mySizeX - 6, mySizeY - 6, (int)(140 * 0.75), (int)(140 * 0.75), new Vector2(myPosition.X, myPosition.Y + 4), new Vector2(Game.AccessChopperBoss.AccessPosition.X + 60, Game.AccessChopperBoss.AccessPosition.Y + 20)))
            {
                Game.AccessPlayerBullets.Remove(this);
                Game.AccessMetalHitSound.Play();
            }
            if (HitBox.Calculate(mySizeX - 6, mySizeY - 6, (int)(60 * 0.75), (int)(80 * 0.75), new Vector2(myPosition.X, myPosition.Y + 4), new Vector2(Game.AccessChopperBoss.AccessPosition.X + 150, Game.AccessChopperBoss.AccessPosition.Y)))
            {
                Game.AccessPlayerBullets.Remove(this);
                Game.AccessMetalHitSound.Play();
            }
        }

        public void Draw(SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.Draw(Game.AccessPlayerBulletSprite, new Rectangle((int)myPosition.X, (int)myPosition.Y, mySizeX, mySizeY), Color.White);
        }
    }
}
