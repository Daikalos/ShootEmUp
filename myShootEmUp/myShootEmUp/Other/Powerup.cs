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
    public class Powerup
    {
        private float mySpeed;
        private Vector2 myPosition;
        private Other.Animation myAnimationForPowerUp;

        public Powerup(Texture2D aTexture, Vector2 aPosition, float aSpeed)
        {
            mySpeed = aSpeed;
            myPosition = aPosition;
            myAnimationForPowerUp = new Other.Animation(aTexture, new Vector2(30, 30), new Vector2(0, 0), new Vector2(3, 13), 10);
        }

        public void Update(Random aRNG)
        {
            myPosition.X -= (float)Game.AccessGameSpeed;

            if (myPosition.X + 64 < 0)
            {
                Game.AccessPowerUps.Remove(this);
            }

            Collision(aRNG);
        }

        public void Collision(Random aRNG)
        {
            if (Game.AccessPlayer != null)
            {
                if (Game.AccessPlayer.AccessHealth > 0)
                {
                    if ((HitBox.Calculate(48, 48, Game.AccessPlayer.AccessSizeX, Game.AccessPlayer.AccessSizeY, myPosition, Game.AccessPlayer.AccessPosition)))
                    {
                        Game.AccessPlayer.UpgradePlayer(aRNG);
                        Game.AccessPowerUps.Remove(this);
                    }
                }
            }
        }

        public void Draw(SpriteBatch aSpriteBatch)
        {
            Rectangle destRect = new Rectangle((int)myPosition.X, (int)myPosition.Y, 48, 48); //Storleken på explosionen
            myAnimationForPowerUp.Draw(aSpriteBatch, destRect, 0);
        }
    }
}
