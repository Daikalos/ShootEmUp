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
    public abstract class BaseEnemy
    {
        protected Vector2 myPosition;
        protected bool
            myIsAlive = true,
            myAttackFrame;
        protected int
            mySizeX,
            mySizeY,
            mySpeed,
            myEnemyHealth,
            myEnemyDamage;

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
        public int AccessEnemyHealth
        {
            get => myEnemyHealth;
            set => myEnemyHealth = value;
        }
        public int AccessEnemyDamage
        {
            get => myEnemyDamage;
            set => myEnemyDamage = value;
        }
        public virtual bool AccessAttackFrame
        {
            get => myAttackFrame;
        }

        public abstract void Init(Vector2 aPosition, int aHealth, int aDamage, int aSpeed, int aSizeX, int aSizeY);

        public abstract void Update(GameWindow aWindow, GameTime aGametime);

        public abstract void Draw(SpriteBatch aSpriteBatch);
    }

    public class BasicEnemy : BaseEnemy
    {
        private Vector2 myDirection;
        private Other.Animation myAnimation;

        public override void Init(Vector2 aPosition, int aHealth, int aDamage, int aSpeed, int aSizeX, int aSizeY)
        {
            myPosition = aPosition;
            myEnemyHealth = aHealth;
            myEnemyDamage = aDamage;
            mySizeX = aSizeX;
            mySizeY = aSizeY;
            mySpeed = aSpeed;
            myAnimation = new Other.Animation(Game.AccessEnemyMovementSprite, new Vector2(190, 208), new Vector2(0, 0), new Vector2(5, 1), 15);
            myDirection = new Vector2(Game.AccessPlayer.AccessPosition.X - 32, Game.AccessPlayer.AccessPosition.Y - 32) - aPosition;
        }

        public override void Update(GameWindow aWindow, GameTime aGameTime)
        {
            myDirection.Normalize();

            myPosition += myDirection * mySpeed * (float)aGameTime.ElapsedGameTime.TotalSeconds * Game.AccessUpdateSpeed;

            if (myEnemyHealth <= 0 || myPosition.X < (mySizeX + myEnemyHealth * 2) * -1)
            {
                myIsAlive = false;
            }
        }

        public override void Draw(SpriteBatch aSpriteBatch)
        {
            Rectangle tempDestRect = new Rectangle((int)myPosition.X - mySizeX / 2, (int)myPosition.Y - mySizeY / 2, mySizeX * 2, mySizeY * 2); //En lämplig rektangel för spriten
            myAnimation.Draw(aSpriteBatch, tempDestRect, 0);
            aSpriteBatch.Draw(Game.AccessHealthbarSprite, new Rectangle((int)myPosition.X - myEnemyHealth + mySizeX - 32, (int)myPosition.Y + mySizeY - 32, myEnemyHealth * 2, 140), null, Color.White);
            aSpriteBatch.DrawString(Game.AccessHealthFont, myEnemyHealth + "/100", new Vector2((int)myPosition.X + mySizeX - 64, (int)myPosition.Y + (mySizeY * 2) - 37), Color.Black);
        }
    }

    public class ShootingEnemy : BaseEnemy
    {
        private float myShootingDelay;
        private Other.Animation myAnimation;

        public override void Init(Vector2 aPosition, int aHealth, int aDamage, int aSpeed, int aSizeX, int aSizeY)
        {
            myPosition = aPosition;
            myEnemyHealth = aHealth;
            myEnemyDamage = aDamage;
            mySizeX = aSizeX;
            mySizeY = aSizeY;
            mySpeed = aSpeed;
            myShootingDelay = 300;
            myAnimation = new Other.Animation(Game.AccessEnemyAttackSprite, new Vector2(48, 60), new Vector2(0, 0), new Vector2(1, 8), 10);
        }

        public override void Update(GameWindow aWindow, GameTime aGameTime)
        {
            myPosition.X -= (float)Game.AccessGameSpeed;
            myShootingDelay -= (float)aGameTime.ElapsedGameTime.TotalMilliseconds * Game.AccessUpdateSpeed;

            if (myShootingDelay <= 0)
            {
                Game.AccessEnemyBullets.Add(new Enemies.EnemyBullet(new Vector2(myPosition.X - 8, myPosition.Y + (mySizeY / 2) - 10), 450));
                myShootingDelay = 1400;
            }

            if (myEnemyHealth <= 0 || myPosition.X < (mySizeX + myEnemyHealth * 2) * -1)
            {
                myIsAlive = false;
            }
        }

        public override void Draw(SpriteBatch aSpriteBatch)
        {
            Rectangle tempDestRect = new Rectangle((int)myPosition.X - mySizeX / 4, (int)myPosition.Y - mySizeY / 3, mySizeX, mySizeY + mySizeY / 2);
            myAnimation.Draw(aSpriteBatch, tempDestRect, 0);
            aSpriteBatch.Draw(Game.AccessHealthbarSprite, new Rectangle((int)myPosition.X - myEnemyHealth + 26, (int)myPosition.Y + mySizeY - 44, myEnemyHealth * 2, 140), null, Color.White);
            aSpriteBatch.DrawString(Game.AccessHealthFont, myEnemyHealth + "/100", new Vector2((int)myPosition.X - 10, (int)myPosition.Y + mySizeY + 15), Color.Black);
        }
    }

    public class Spikeball : BaseEnemy
    {
        private float myAngle;
        private Vector2 myVelocity;

        public override void Init(Vector2 aPosition, int aHealth, int aDamage, int aSpeed, int aSizeX, int aSizeY)
        {
            myPosition = aPosition;
            myEnemyDamage = aDamage;
            mySizeX = aSizeX;
            mySizeY = aSizeY;
            mySpeed = aSpeed;
            myAngle = 0f;
            myVelocity = new Vector2(0, 0);
        }

        public override void Update(GameWindow aWindow, GameTime aGameTime)
        {
            myPosition.X -= mySpeed * Game.AccessUpdateSpeed;
            myPosition += myVelocity * Game.AccessUpdateSpeed;
            myAngle += 0.05f * Game.AccessUpdateSpeed;

            myVelocity.Y += 0.10f * Game.AccessUpdateSpeed;

            if (myPosition.Y + mySizeY / 2 >= 325)
            {
                myPosition.Y -= 350f * (float)aGameTime.ElapsedGameTime.TotalSeconds;
                myVelocity.Y = -450f * (float)aGameTime.ElapsedGameTime.TotalSeconds;
            }

            if (myPosition.X < (mySizeX) * -1)
            {
                myIsAlive = false;
            }
        }

        public override void Draw(SpriteBatch aSpriteBatch)
        {
            Rectangle tempDestRect = new Rectangle((int)myPosition.X + 32, (int)myPosition.Y + 32, mySizeX, mySizeY);
            aSpriteBatch.Draw(Game.AccessSpikeballSprite, tempDestRect, null, Color.White, myAngle, new Vector2(Game.AccessSpikeballSprite.Width / 2, Game.AccessSpikeballSprite.Height / 2), SpriteEffects.None, 0f);
        }
    }

    public class Yeti : BaseEnemy
    {
        private Other.Animation myAnimation;

        public override bool AccessAttackFrame
        {
            get
            {
                return myAttackFrame;
            }
        }

        public override void Init(Vector2 aPosition, int aHealth, int aDamage, int aSpeed, int aSizeX, int aSizeY)
        {
            myPosition = aPosition;
            myEnemyHealth = aHealth;
            mySizeX = aSizeX;
            mySizeY = aSizeY;
            mySpeed = aSpeed;
            myAnimation = new Other.Animation(Game.AccessYetiSprite, new Vector2(282, 219), new Vector2(0, 0), new Vector2(4, 4), 6);
        }

        public override void Update(GameWindow aWindow, GameTime aGameTime)
        {
            myPosition.X -= (float)Game.AccessGameSpeed;
            if ((myAnimation.AccessCurrentFrame.X == 3 && myAnimation.AccessCurrentFrame.Y == 1) || (myAnimation.AccessCurrentFrame.X == 0 && myAnimation.AccessCurrentFrame.Y == 2))
            {
                myAttackFrame = true;
            }
            else
            {
                myAttackFrame = false;
            }

            if (myEnemyHealth <= 0 || myPosition.X < (mySizeX + myEnemyHealth * 2) * -1)
            {
                myIsAlive = false;
            }
        }

        public override void Draw(SpriteBatch aSpriteBatch)
        {
            Rectangle tempDestRect = new Rectangle((int)myPosition.X - mySizeX - (mySizeX / 2) - 2, (int)myPosition.Y - mySizeY - (mySizeY / 2) - 8, mySizeX * 3, mySizeY * 3); //En lämplig rektangel för spriten
            myAnimation.Draw(aSpriteBatch, tempDestRect, 0);
            aSpriteBatch.Draw(Game.AccessHealthbarSprite, new Rectangle((int)myPosition.X - myEnemyHealth / 2 + mySizeX - 32, (int)myPosition.Y + mySizeY - 32, myEnemyHealth, 140), null, Color.White);
            aSpriteBatch.DrawString(Game.AccessHealthFont, myEnemyHealth + "/175", new Vector2((int)myPosition.X + mySizeX - 64, (int)myPosition.Y + (mySizeY * 2) - 37), Color.Black);
        }
    }
}
