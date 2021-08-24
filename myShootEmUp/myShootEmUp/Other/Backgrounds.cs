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
    public class Backgrounds
    {
        protected Texture2D myTexture;
        protected Rectangle myRectangle;
        protected Vector2 myPosition;

        public Vector2 AccessPosition
        {
            get => myPosition;
            set => myPosition = value;
        }
        public Texture2D AccessTexture
        {
            get => myTexture;
            set => myTexture = value;
        }
        public Rectangle AccessRectangle
        {
            get => myRectangle;
            set => myRectangle = value;
        }

        public void Draw(SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.Draw(myTexture, myRectangle, Color.White);
        }
    }

    public class Scrolling : Backgrounds
    {
        public Scrolling(Texture2D aNewTexture, Rectangle aNewRectangle)
        {
            AccessTexture = aNewTexture;
            AccessRectangle = aNewRectangle;
            AccessPosition = new Vector2(aNewRectangle.X, 0);
        }

        public void Update()
        {
            AccessPosition = new Vector2((float)AccessPosition.X - (float)Game.AccessGameSpeed, (float)AccessPosition.Y);
            AccessRectangle = new Rectangle((int)AccessPosition.X, (int)AccessPosition.Y, AccessRectangle.Width, AccessRectangle.Height);
        }
    }
}
