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
    public class Boulder
    {
        private float myAngle;
        private Vector2 myPosition;
        private List<Rectangle> myCollisionRectangles;

        public List<Rectangle> AccessRectangles
        {
            get => myCollisionRectangles;
            set => myCollisionRectangles = value;
        }

        public Boulder(Vector2 aPosition)
        {
            myCollisionRectangles = new List<Rectangle>();
            myPosition = aPosition;

            myCollisionRectangles.Add(new Rectangle(0, 0, 160, 150));
            myCollisionRectangles.Add(new Rectangle(0, 150, 150, 20));
            myCollisionRectangles.Add(new Rectangle(0, 170, 140, 20));
            myCollisionRectangles.Add(new Rectangle(0, 190, 130, 20));
            myCollisionRectangles.Add(new Rectangle(0, 210, 115, 20));
            myCollisionRectangles.Add(new Rectangle(0, 230, 100, 20));
            myCollisionRectangles.Add(new Rectangle(0, 250, 80, 20));
            myCollisionRectangles.Add(new Rectangle(0, 270, 55, 20));
            myCollisionRectangles.Add(new Rectangle(0, 290, 20, 20));
        }

        public void Update(GameTime aGametime)
        {
            myAngle += (float)Game.AccessGameSpeed * 0.01f;

            Collisions();
        }

        public void Collisions()
        {
            for (int i = 0; i < myCollisionRectangles.Count; i++)
            {
                foreach (BaseEnemy enemy in Game.AccessBaseEnemies)
                {
                    if (HitBox.Calculate(myCollisionRectangles[i].Width, myCollisionRectangles[i].Height, enemy.AccessSizeX, enemy.AccessSizeY, new Vector2(myCollisionRectangles[i].X, myCollisionRectangles[i].Y), enemy.AccessPosition)) //Ifall om spelaren kolliderar med fiende 1
                    {
                        enemy.AccessIsAlive = false;
                    }
                }
            }
        }

        public void Draw(SpriteBatch aSpriteBatch, GameWindow aWindow)
        {
            Rectangle tempDestRect = new Rectangle((int)myPosition.X, (int)myPosition.Y, 512, aWindow.ClientBounds.Height);
            aSpriteBatch.Draw(Game.AccessBoulderSprite, tempDestRect, null, Color.White, myAngle, new Vector2(Game.AccessBoulderSprite.Width / 2, Game.AccessBoulderSprite.Height / 2), SpriteEffects.None, 0f);
        }
    }
}
