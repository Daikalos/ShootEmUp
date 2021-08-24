using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace myShootEmUp.Enemies
{
    public class EnemyBullet
    {
        private float mySpeed;
        private int mySizeX, mySizeY;
        private Vector2 myPosition;
        private Other.Animation myAnimation;

        public Vector2 AccessPosition
        {
            get => myPosition;
            set => myPosition = value;
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

        public EnemyBullet(Vector2 aPosition, float aSpeed)
        {
            myPosition = aPosition;
            mySpeed = aSpeed;
            mySizeX = 32;
            mySizeY = 32;
            myAnimation = new Other.Animation(Game.AccessFireballSprite, new Vector2(288, 224), new Vector2(0, 0), new Vector2(5, 1), 15);
        }

        public void Update(GameWindow aWindow, GameTime aGameTime)
        {
            myPosition.X -= (float)Game.AccessGameSpeed * 2;

            if (myPosition.X + mySizeX < 0)
            {
                Game.AccessEnemyBullets.Remove(this);
            }
        }

        public void Draw(SpriteBatch aSpriteBatch)
        {
            Rectangle tempDestRect = new Rectangle((int)myPosition.X - 10, (int)myPosition.Y - 7, mySizeX, mySizeY);
            myAnimation.Draw(aSpriteBatch, tempDestRect, 0);
        }
    }
}
