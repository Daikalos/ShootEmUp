using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace myShootEmUp.Menu
{
    public abstract class Shop
    {
        protected Vector2 myPosition;
        protected int 
            mySizeX, 
            mySizeY, 
            myIncrSize;

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

        public int CheckWhatBoxSelected()
        {
            if ((Mouse.GetState().X > myPosition.X && Mouse.GetState().X < myPosition.X + mySizeX && Mouse.GetState().Y > myPosition.Y && Mouse.GetState().Y < myPosition.Y + mySizeY))
            {
                if (Game.AccessPreviousMouseState.LeftButton == ButtonState.Released && Game.AccessCurrentMouseState.LeftButton == ButtonState.Pressed)
                {
                    return 1;
                }
            }
            if ((Mouse.GetState().X > myPosition.X + mySizeX + 100 && Mouse.GetState().X < myPosition.X + (mySizeX * 2) + 100 && Mouse.GetState().Y > myPosition.Y && Mouse.GetState().Y < myPosition.Y + mySizeY))
            {
                if (Game.AccessPreviousMouseState.LeftButton == ButtonState.Released && Game.AccessCurrentMouseState.LeftButton == ButtonState.Pressed)
                {
                    return 2;
                }
            }
            if ((Mouse.GetState().X > myPosition.X + (mySizeX * 2) + 200 && Mouse.GetState().X < myPosition.X + (mySizeX * 3) + 200 && Mouse.GetState().Y > myPosition.Y && Mouse.GetState().Y < myPosition.Y + mySizeY))
            {
                if (Game.AccessPreviousMouseState.LeftButton == ButtonState.Released && Game.AccessCurrentMouseState.LeftButton == ButtonState.Pressed)
                {
                    return 3;
                }
            }

            return 0;
        }
        public int CheckWhatBoxHoveringOver()
        {
            if ((Mouse.GetState().X > myPosition.X && Mouse.GetState().X < myPosition.X + mySizeX && Mouse.GetState().Y > myPosition.Y && Mouse.GetState().Y < myPosition.Y + mySizeY))
            {
                return 1;
            }
            if ((Mouse.GetState().X > myPosition.X + mySizeX + 100 && Mouse.GetState().X < myPosition.X + (mySizeX * 2) + 100 && Mouse.GetState().Y > myPosition.Y && Mouse.GetState().Y < myPosition.Y + mySizeY))
            {
                return 2;
            }
            if ((Mouse.GetState().X > myPosition.X + (mySizeX * 2) + 200 && Mouse.GetState().X < myPosition.X + (mySizeX * 3) + 200 && Mouse.GetState().Y > myPosition.Y && Mouse.GetState().Y < myPosition.Y + mySizeY))
            {
                return 3;
            }

            return 0;
        }

        public abstract void Init(Vector2 aPosition, int aSizeX, int aSizeY);

        public abstract void Update(GameWindow aWindow, GameTime aGameTime);

        public abstract void Draw(SpriteBatch aSpriteBatch);
    }

    public class BulletTimeUpgrade : Shop
    {
        public override void Init(Vector2 aPosition, int aSizeX, int aSizeY)
        {
            myPosition = aPosition;
            mySizeX = aSizeX;
            mySizeY = aSizeY;
        }

        public override void Update(GameWindow aWindow, GameTime aGameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                Game.AccessCoins += 1000;
            }
            switch (CheckWhatBoxSelected())
            {
                case 1:
                    if (Game.AccessCoins >= 500 && Game.AccessBulletTimeUpgradeStatus == 0)
                    {
                        Game.AccessBulletTimeUpgradeStatus = 1;
                        Game.AccessCoins -= 500;
                    }
                    break;
                case 2:
                    if (Game.AccessCoins >= 600 && Game.AccessBulletTimeUpgradeStatus == 1)
                    {
                        Game.AccessBulletTimeUpgradeStatus = 2;
                        Game.AccessCoins -= 600;
                    }
                    break;
                case 3:
                    if (Game.AccessCoins >= 1000 && Game.AccessBulletTimeUpgradeStatus == 2)
                    {
                        Game.AccessBulletTimeUpgradeStatus = 3;
                        Game.AccessCoins -= 1000;
                    }
                    break;
            }
        }

        public override void Draw(SpriteBatch aSpriteBatch)
        {
            switch (Game.AccessBulletTimeUpgradeStatus)
            {
                case 0:
                    aSpriteBatch.Draw(Game.AccessBulletTimeUpgradeSprite, new Rectangle((int)myPosition.X, (int)myPosition.Y, mySizeX, mySizeY), Color.Gray);
                    aSpriteBatch.Draw(Game.AccessBulletTimeUpgradeSprite, new Rectangle((int)myPosition.X + mySizeX + 100, (int)myPosition.Y, mySizeX, mySizeY), Color.Gray);
                    aSpriteBatch.Draw(Game.AccessBulletTimeUpgradeSprite, new Rectangle((int)myPosition.X + (mySizeX * 2) + 200, (int)myPosition.Y, mySizeX, mySizeY), Color.Gray);
                    break;
                case 1:
                    aSpriteBatch.Draw(Game.AccessBulletTimeUpgradeSprite, new Rectangle((int)myPosition.X, (int)myPosition.Y, mySizeX, mySizeY), Color.White);
                    aSpriteBatch.Draw(Game.AccessBulletTimeUpgradeSprite, new Rectangle((int)myPosition.X + mySizeX + 100, (int)myPosition.Y, mySizeX, mySizeY), Color.Gray);
                    aSpriteBatch.Draw(Game.AccessBulletTimeUpgradeSprite, new Rectangle((int)myPosition.X + (mySizeX * 2) + 200, (int)myPosition.Y, mySizeX, mySizeY), Color.Gray);
                    break;
                case 2:
                    aSpriteBatch.Draw(Game.AccessBulletTimeUpgradeSprite, new Rectangle((int)myPosition.X, (int)myPosition.Y, mySizeX, mySizeY), Color.White);
                    aSpriteBatch.Draw(Game.AccessBulletTimeUpgradeSprite, new Rectangle((int)myPosition.X + mySizeX + 100, (int)myPosition.Y, mySizeX, mySizeY), Color.White);
                    aSpriteBatch.Draw(Game.AccessBulletTimeUpgradeSprite, new Rectangle((int)myPosition.X + (mySizeX * 2) + 200, (int)myPosition.Y, mySizeX, mySizeY), Color.Gray);
                    break;
                case 3:
                    aSpriteBatch.Draw(Game.AccessBulletTimeUpgradeSprite, new Rectangle((int)myPosition.X, (int)myPosition.Y, mySizeX, mySizeY), Color.White);
                    aSpriteBatch.Draw(Game.AccessBulletTimeUpgradeSprite, new Rectangle((int)myPosition.X + mySizeX + 100, (int)myPosition.Y, mySizeX, mySizeY), Color.White);
                    aSpriteBatch.Draw(Game.AccessBulletTimeUpgradeSprite, new Rectangle((int)myPosition.X + (mySizeX * 2) + 200, (int)myPosition.Y, mySizeX, mySizeY), Color.White);
                    break;
            }
            aSpriteBatch.DrawString(Game.AccessGlobalFont, "- Bullet Time Upgrades", new Vector2(myPosition.X + (mySizeX * 2) + 310, myPosition.Y + mySizeY / 3), Color.OrangeRed);

            #region DescriptionBox
            switch (CheckWhatBoxHoveringOver())
            {
                case 1:
                    aSpriteBatch.Draw(Game.AccessSandstoneSprite, new Rectangle((int)Mouse.GetState().X, (int)Mouse.GetState().Y, 256, 128), Color.White);
                    if (Game.AccessCoins < 500)
                    {
                        aSpriteBatch.DrawString(Game.AccessHealthFont, "Cost: 500", new Vector2((int)Mouse.GetState().X + 8, (int)Mouse.GetState().Y + 6), Color.Red);
                    }
                    else
                    {
                        aSpriteBatch.DrawString(Game.AccessHealthFont, "Cost: 500", new Vector2((int)Mouse.GetState().X + 8, (int)Mouse.GetState().Y + 6), Color.Green);
                    }
                    aSpriteBatch.DrawString(Game.AccessHealthFont, "Gain the ability to temporarily", new Vector2((int)Mouse.GetState().X + 8, (int)Mouse.GetState().Y + 30), Color.Black);
                    aSpriteBatch.DrawString(Game.AccessHealthFont, "slow down time", new Vector2((int)Mouse.GetState().X + 8, (int)Mouse.GetState().Y + 50), Color.Black);
                    break;
                case 2:
                    aSpriteBatch.Draw(Game.AccessSandstoneSprite, new Rectangle((int)Mouse.GetState().X, (int)Mouse.GetState().Y, 256, 128), Color.White);
                    if (Game.AccessCoins < 600)
                    {
                        aSpriteBatch.DrawString(Game.AccessHealthFont, "Cost: 600", new Vector2((int)Mouse.GetState().X + 8, (int)Mouse.GetState().Y + 6), Color.Red);
                    }
                    else
                    {
                        aSpriteBatch.DrawString(Game.AccessHealthFont, "Cost: 600", new Vector2((int)Mouse.GetState().X + 8, (int)Mouse.GetState().Y + 6), Color.Green);
                    }
                    if (Game.AccessBulletTimeUpgradeStatus == 1)
                    {
                        aSpriteBatch.DrawString(Game.AccessHealthFont, "Req. Lvl 1", new Vector2((int)Mouse.GetState().X + 160, (int)Mouse.GetState().Y + 6), Color.Green);
                    }
                    else
                    {
                        aSpriteBatch.DrawString(Game.AccessHealthFont, "Req. Lvl 1", new Vector2((int)Mouse.GetState().X + 160, (int)Mouse.GetState().Y + 6), Color.Red);
                    }
                    aSpriteBatch.DrawString(Game.AccessHealthFont, "You can slow down time", new Vector2((int)Mouse.GetState().X + 8, (int)Mouse.GetState().Y + 30), Color.Black);
                    aSpriteBatch.DrawString(Game.AccessHealthFont, "much longer", new Vector2((int)Mouse.GetState().X + 8, (int)Mouse.GetState().Y + 50), Color.Black);
                    break;
                case 3:
                    aSpriteBatch.Draw(Game.AccessSandstoneSprite, new Rectangle((int)Mouse.GetState().X, (int)Mouse.GetState().Y, 256, 128), Color.White);
                    if (Game.AccessCoins < 1000)
                    {
                        aSpriteBatch.DrawString(Game.AccessHealthFont, "Cost: 1000", new Vector2((int)Mouse.GetState().X + 8, (int)Mouse.GetState().Y + 6), Color.Red);
                    }
                    else
                    {
                        aSpriteBatch.DrawString(Game.AccessHealthFont, "Cost: 1000", new Vector2((int)Mouse.GetState().X + 8, (int)Mouse.GetState().Y + 6), Color.Green);
                    }
                    if (Game.AccessBulletTimeUpgradeStatus == 2)
                    {
                        aSpriteBatch.DrawString(Game.AccessHealthFont, "Req. Lvl 2", new Vector2((int)Mouse.GetState().X + 160, (int)Mouse.GetState().Y + 6), Color.Green);
                    }
                    else
                    {
                        aSpriteBatch.DrawString(Game.AccessHealthFont, "Req. Lvl 2", new Vector2((int)Mouse.GetState().X + 160, (int)Mouse.GetState().Y + 6), Color.Red);
                    }
                    aSpriteBatch.DrawString(Game.AccessHealthFont, "You don't slow down during", new Vector2((int)Mouse.GetState().X + 8, (int)Mouse.GetState().Y + 30), Color.Black);
                    aSpriteBatch.DrawString(Game.AccessHealthFont, "bullet time", new Vector2((int)Mouse.GetState().X + 8, (int)Mouse.GetState().Y + 50), Color.Black);
                    break;
            }
            #endregion

            aSpriteBatch.DrawString(Game.AccessGlobalFont, "I", new Vector2(myPosition.X, myPosition.Y - 32), Color.OrangeRed);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, "II", new Vector2(myPosition.X + mySizeX + 100, myPosition.Y - 32), Color.OrangeRed);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, "III", new Vector2(myPosition.X + (mySizeX * 2) + 200, myPosition.Y - 32), Color.OrangeRed);

            aSpriteBatch.DrawString(Game.AccessGlobalFont, "Coins: " + Game.AccessCoins, new Vector2(160, 466), Color.OrangeRed);
        }
    }

    public class HealthUpgrade : Shop
    {
        public override void Init(Vector2 aPosition, int aSizeX, int aSizeY)
        {
            myPosition = aPosition;
            mySizeX = aSizeX;
            mySizeY = aSizeY;
        }

        public override void Update(GameWindow aWindow, GameTime aGameTime)
        {
            switch (CheckWhatBoxSelected())
            {
                case 1:
                    if (Game.AccessCoins >= 500 && Game.AccessHealthUpgradeStatus == 0)
                    {
                        Game.AccessHealthUpgradeStatus = 1;
                        Game.AccessCoins -= 500;
                    }
                    break;
                case 2:
                    if (Game.AccessCoins >= 500 && Game.AccessHealthUpgradeStatus == 1)
                    {
                        Game.AccessHealthUpgradeStatus = 2;
                        Game.AccessCoins -= 500;
                    }
                    break;
                case 3:
                    if (Game.AccessCoins >= 600 && Game.AccessHealthUpgradeStatus == 2)
                    {
                        Game.AccessHealthUpgradeStatus = 3;
                        Game.AccessCoins -= 600;
                    }
                    break;
            }
        }

        public override void Draw(SpriteBatch aSpriteBatch)
        {
            switch(Game.AccessHealthUpgradeStatus)
            {
                case 0:
                    aSpriteBatch.Draw(Game.AccessHealthUpgradeSprite, new Rectangle((int)myPosition.X, (int)myPosition.Y, mySizeX, mySizeY), Color.Gray);
                    aSpriteBatch.Draw(Game.AccessHealthUpgradeSprite, new Rectangle((int)myPosition.X + mySizeX + 100, (int)myPosition.Y, mySizeX, mySizeY), Color.Gray);
                    aSpriteBatch.Draw(Game.AccessHealthUpgradeSprite, new Rectangle((int)myPosition.X + (mySizeX * 2) + 200, (int)myPosition.Y, mySizeX, mySizeY), Color.Gray);
                    break;
                case 1:
                    aSpriteBatch.Draw(Game.AccessHealthUpgradeSprite, new Rectangle((int)myPosition.X, (int)myPosition.Y, mySizeX, mySizeY), Color.White);
                    aSpriteBatch.Draw(Game.AccessHealthUpgradeSprite, new Rectangle((int)myPosition.X + mySizeX + 100, (int)myPosition.Y, mySizeX, mySizeY), Color.Gray);
                    aSpriteBatch.Draw(Game.AccessHealthUpgradeSprite, new Rectangle((int)myPosition.X + (mySizeX * 2) + 200, (int)myPosition.Y, mySizeX, mySizeY), Color.Gray);
                    break;
                case 2:
                    aSpriteBatch.Draw(Game.AccessHealthUpgradeSprite, new Rectangle((int)myPosition.X, (int)myPosition.Y, mySizeX, mySizeY), Color.White);
                    aSpriteBatch.Draw(Game.AccessHealthUpgradeSprite, new Rectangle((int)myPosition.X + mySizeX + 100, (int)myPosition.Y, mySizeX, mySizeY), Color.White);
                    aSpriteBatch.Draw(Game.AccessHealthUpgradeSprite, new Rectangle((int)myPosition.X + (mySizeX * 2) + 200, (int)myPosition.Y, mySizeX, mySizeY), Color.Gray);
                    break;
                case 3:
                    aSpriteBatch.Draw(Game.AccessHealthUpgradeSprite, new Rectangle((int)myPosition.X, (int)myPosition.Y, mySizeX, mySizeY), Color.White);
                    aSpriteBatch.Draw(Game.AccessHealthUpgradeSprite, new Rectangle((int)myPosition.X + mySizeX + 100, (int)myPosition.Y, mySizeX, mySizeY), Color.White);
                    aSpriteBatch.Draw(Game.AccessHealthUpgradeSprite, new Rectangle((int)myPosition.X + (mySizeX * 2) + 200, (int)myPosition.Y, mySizeX, mySizeY), Color.White);
                    break;
            }
            aSpriteBatch.DrawString(Game.AccessGlobalFont, "- Health Upgrades", new Vector2(myPosition.X + (mySizeX * 2) + 310, myPosition.Y + mySizeY / 3), Color.OrangeRed);

            #region DescriptionBox
            switch (CheckWhatBoxHoveringOver())
            {
                case 1:
                    aSpriteBatch.Draw(Game.AccessSandstoneSprite, new Rectangle((int)Mouse.GetState().X, (int)Mouse.GetState().Y, 256, 128), Color.White);
                    if (Game.AccessCoins < 500)
                    {
                        aSpriteBatch.DrawString(Game.AccessHealthFont, "Cost: 500", new Vector2((int)Mouse.GetState().X + 8, (int)Mouse.GetState().Y + 6), Color.Red);
                    }
                    else
                    {
                        aSpriteBatch.DrawString(Game.AccessHealthFont, "Cost: 500", new Vector2((int)Mouse.GetState().X + 8, (int)Mouse.GetState().Y + 6), Color.Green);
                    }
                    aSpriteBatch.DrawString(Game.AccessHealthFont, "Increase health by 50", new Vector2((int)Mouse.GetState().X + 8, (int)Mouse.GetState().Y + 30), Color.Black);
                    break;
                case 2:
                    aSpriteBatch.Draw(Game.AccessSandstoneSprite, new Rectangle((int)Mouse.GetState().X, (int)Mouse.GetState().Y, 256, 128), Color.White);
                    if (Game.AccessCoins < 500)
                    {
                        aSpriteBatch.DrawString(Game.AccessHealthFont, "Cost: 500", new Vector2((int)Mouse.GetState().X + 8, (int)Mouse.GetState().Y + 6), Color.Red);
                    }
                    else
                    {
                        aSpriteBatch.DrawString(Game.AccessHealthFont, "Cost: 500", new Vector2((int)Mouse.GetState().X + 8, (int)Mouse.GetState().Y + 6), Color.Green);
                    }
                    if (Game.AccessHealthUpgradeStatus == 1)
                    {
                        aSpriteBatch.DrawString(Game.AccessHealthFont, "Req. Lvl 1", new Vector2((int)Mouse.GetState().X + 160, (int)Mouse.GetState().Y + 6), Color.Green);
                    }
                    else
                    {
                        aSpriteBatch.DrawString(Game.AccessHealthFont, "Req. Lvl 1", new Vector2((int)Mouse.GetState().X + 160, (int)Mouse.GetState().Y + 6), Color.Red);
                    }
                    aSpriteBatch.DrawString(Game.AccessHealthFont, "Increase health by 50", new Vector2((int)Mouse.GetState().X + 8, (int)Mouse.GetState().Y + 30), Color.Black);
                    break;
                case 3:
                    aSpriteBatch.Draw(Game.AccessSandstoneSprite, new Rectangle((int)Mouse.GetState().X, (int)Mouse.GetState().Y, 256, 128), Color.White);
                    if (Game.AccessCoins < 600)
                    {
                        aSpriteBatch.DrawString(Game.AccessHealthFont, "Cost: 600", new Vector2((int)Mouse.GetState().X + 8, (int)Mouse.GetState().Y + 6), Color.Red);
                    }
                    else
                    {
                        aSpriteBatch.DrawString(Game.AccessHealthFont, "Cost: 600", new Vector2((int)Mouse.GetState().X + 8, (int)Mouse.GetState().Y + 6), Color.Green);
                    }
                    if (Game.AccessHealthUpgradeStatus == 2)
                    {
                        aSpriteBatch.DrawString(Game.AccessHealthFont, "Req. Lvl 2", new Vector2((int)Mouse.GetState().X + 160, (int)Mouse.GetState().Y + 6), Color.Green);
                    }
                    else
                    {
                        aSpriteBatch.DrawString(Game.AccessHealthFont, "Req. Lvl 2", new Vector2((int)Mouse.GetState().X + 160, (int)Mouse.GetState().Y + 6), Color.Red);
                    }
                    aSpriteBatch.DrawString(Game.AccessHealthFont, "Increase health by 100", new Vector2((int)Mouse.GetState().X + 8, (int)Mouse.GetState().Y + 30), Color.Black);
                    break;
            }
            #endregion
        }
    }

    public class ShieldUpgrade : Shop
    {
        public override void Init(Vector2 aPosition, int aSizeX, int aSizeY)
        {
            myPosition = aPosition;
            mySizeX = aSizeX;
            mySizeY = aSizeY;
        }

        public override void Update(GameWindow aWindow, GameTime aGameTime)
        {
            switch (CheckWhatBoxSelected())
            {
                case 1:
                    if (Game.AccessCoins >= 800 && Game.AccessShieldUpgradeStatus == 0)
                    {
                        Game.AccessShieldUpgradeStatus = 1;
                        Game.AccessCoins -= 800;
                    }
                    break;
                case 2:
                    if (Game.AccessCoins >= 1000 && Game.AccessShieldUpgradeStatus == 1)
                    {
                        Game.AccessShieldUpgradeStatus = 2;
                        Game.AccessCoins -= 1000;
                    }
                    break;
                case 3:
                    if (Game.AccessCoins >= 1400 && Game.AccessShieldUpgradeStatus == 2)
                    {
                        Game.AccessShieldUpgradeStatus = 3;
                        Game.AccessCoins -= 1400;
                    }
                    break;
            }
        }

        public override void Draw(SpriteBatch aSpriteBatch)
        {
            switch (Game.AccessShieldUpgradeStatus)
            {
                case 0:
                    aSpriteBatch.Draw(Game.AccessShieldUpgradeSprite, new Rectangle((int)myPosition.X, (int)myPosition.Y, mySizeX, mySizeY), Color.Gray);
                    aSpriteBatch.Draw(Game.AccessShieldUpgradeSprite, new Rectangle((int)myPosition.X + mySizeX + 100, (int)myPosition.Y, mySizeX, mySizeY), Color.Gray);
                    aSpriteBatch.Draw(Game.AccessShieldUpgradeSprite, new Rectangle((int)myPosition.X + (mySizeX * 2) + 200, (int)myPosition.Y, mySizeX, mySizeY), Color.Gray);
                    break;
                case 1:
                    aSpriteBatch.Draw(Game.AccessShieldUpgradeSprite, new Rectangle((int)myPosition.X, (int)myPosition.Y, mySizeX, mySizeY), Color.White);
                    aSpriteBatch.Draw(Game.AccessShieldUpgradeSprite, new Rectangle((int)myPosition.X + mySizeX + 100, (int)myPosition.Y, mySizeX, mySizeY), Color.Gray);
                    aSpriteBatch.Draw(Game.AccessShieldUpgradeSprite, new Rectangle((int)myPosition.X + (mySizeX * 2) + 200, (int)myPosition.Y, mySizeX, mySizeY), Color.Gray);
                    break;
                case 2:
                    aSpriteBatch.Draw(Game.AccessShieldUpgradeSprite, new Rectangle((int)myPosition.X, (int)myPosition.Y, mySizeX, mySizeY), Color.White);
                    aSpriteBatch.Draw(Game.AccessShieldUpgradeSprite, new Rectangle((int)myPosition.X + mySizeX + 100, (int)myPosition.Y, mySizeX, mySizeY), Color.White);
                    aSpriteBatch.Draw(Game.AccessShieldUpgradeSprite, new Rectangle((int)myPosition.X + (mySizeX * 2) + 200, (int)myPosition.Y, mySizeX, mySizeY), Color.Gray);
                    break;
                case 3:
                    aSpriteBatch.Draw(Game.AccessShieldUpgradeSprite, new Rectangle((int)myPosition.X, (int)myPosition.Y, mySizeX, mySizeY), Color.White);
                    aSpriteBatch.Draw(Game.AccessShieldUpgradeSprite, new Rectangle((int)myPosition.X + mySizeX + 100, (int)myPosition.Y, mySizeX, mySizeY), Color.White);
                    aSpriteBatch.Draw(Game.AccessShieldUpgradeSprite, new Rectangle((int)myPosition.X + (mySizeX * 2) + 200, (int)myPosition.Y, mySizeX, mySizeY), Color.White);
                    break;
            }
            aSpriteBatch.DrawString(Game.AccessGlobalFont, "- Shield Upgrades", new Vector2(myPosition.X + (mySizeX * 2) + 310, myPosition.Y + mySizeY / 3), Color.OrangeRed);

            #region DescriptionBox
            switch (CheckWhatBoxHoveringOver())
            {
                case 1:
                    aSpriteBatch.Draw(Game.AccessSandstoneSprite, new Rectangle((int)Mouse.GetState().X, (int)Mouse.GetState().Y, 256, 128), Color.White);
                    if (Game.AccessCoins < 800)
                    {
                        aSpriteBatch.DrawString(Game.AccessHealthFont, "Cost: 800", new Vector2((int)Mouse.GetState().X + 8, (int)Mouse.GetState().Y + 6), Color.Red);
                    }
                    else
                    {
                        aSpriteBatch.DrawString(Game.AccessHealthFont, "Cost: 800", new Vector2((int)Mouse.GetState().X + 8, (int)Mouse.GetState().Y + 6), Color.Green);
                    }
                    aSpriteBatch.DrawString(Game.AccessHealthFont, "Gain the ability to recieve a", new Vector2((int)Mouse.GetState().X + 8, (int)Mouse.GetState().Y + 30), Color.Black);
                    aSpriteBatch.DrawString(Game.AccessHealthFont, "temporary shield when", new Vector2((int)Mouse.GetState().X + 8, (int)Mouse.GetState().Y + 50), Color.Black);
                    aSpriteBatch.DrawString(Game.AccessHealthFont, "damaged", new Vector2((int)Mouse.GetState().X + 8, (int)Mouse.GetState().Y + 70), Color.Black);
                    break;
                case 2:
                    aSpriteBatch.Draw(Game.AccessSandstoneSprite, new Rectangle((int)Mouse.GetState().X, (int)Mouse.GetState().Y, 256, 128), Color.White);
                    if (Game.AccessCoins < 1000)
                    {
                        aSpriteBatch.DrawString(Game.AccessHealthFont, "Cost: 1000", new Vector2((int)Mouse.GetState().X + 8, (int)Mouse.GetState().Y + 6), Color.Red);
                    }
                    else
                    {
                        aSpriteBatch.DrawString(Game.AccessHealthFont, "Cost: 1000", new Vector2((int)Mouse.GetState().X + 8, (int)Mouse.GetState().Y + 6), Color.Green);
                    }
                    if (Game.AccessShieldUpgradeStatus == 1)
                    {
                        aSpriteBatch.DrawString(Game.AccessHealthFont, "Req. Lvl 1", new Vector2((int)Mouse.GetState().X + 160, (int)Mouse.GetState().Y + 6), Color.Green);
                    }
                    else
                    {
                        aSpriteBatch.DrawString(Game.AccessHealthFont, "Req. Lvl 1", new Vector2((int)Mouse.GetState().X + 160, (int)Mouse.GetState().Y + 6), Color.Red);
                    }
                    aSpriteBatch.DrawString(Game.AccessHealthFont, "Decrease shield recharge", new Vector2((int)Mouse.GetState().X + 8, (int)Mouse.GetState().Y + 30), Color.Black);
                    aSpriteBatch.DrawString(Game.AccessHealthFont, "time", new Vector2((int)Mouse.GetState().X + 8, (int)Mouse.GetState().Y + 50), Color.Black);
                    break;
                case 3:
                    aSpriteBatch.Draw(Game.AccessSandstoneSprite, new Rectangle((int)Mouse.GetState().X, (int)Mouse.GetState().Y, 256, 128), Color.White);
                    if (Game.AccessCoins < 1400)
                    {
                        aSpriteBatch.DrawString(Game.AccessHealthFont, "Cost: 1400", new Vector2((int)Mouse.GetState().X + 8, (int)Mouse.GetState().Y + 6), Color.Red);
                    }
                    else
                    {
                        aSpriteBatch.DrawString(Game.AccessHealthFont, "Cost: 1400", new Vector2((int)Mouse.GetState().X + 8, (int)Mouse.GetState().Y + 6), Color.Green);
                    }
                    if (Game.AccessShieldUpgradeStatus == 2)
                    {
                        aSpriteBatch.DrawString(Game.AccessHealthFont, "Req. Lvl 2", new Vector2((int)Mouse.GetState().X + 160, (int)Mouse.GetState().Y + 6), Color.Green);
                    }
                    else
                    {
                        aSpriteBatch.DrawString(Game.AccessHealthFont, "Req. Lvl 2", new Vector2((int)Mouse.GetState().X + 160, (int)Mouse.GetState().Y + 6), Color.Red);
                    }
                    aSpriteBatch.DrawString(Game.AccessHealthFont, "Shield lasts longer", new Vector2((int)Mouse.GetState().X + 8, (int)Mouse.GetState().Y + 30), Color.Black);
                    break;
            }
            #endregion
        }
    }
}
