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
    public class SaveFile
    {
        private Vector2 myPosition;
        private int
            mySizeX,
            mySizeY,
            myIncrSize,
            myIncrDelButtonSize;
        private string myFilePath;
        private bool myIsLevelSaveOrNot;

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

        public SaveFile(Vector2 aPosition, int aSizeX, int aSizeY, string aFilePath, bool aIsLevelSaveOrNot)
        {
            myPosition = aPosition;
            mySizeX = aSizeX;
            mySizeY = aSizeY;
            myFilePath = aFilePath;
            myIsLevelSaveOrNot = aIsLevelSaveOrNot;
        }

        public void Update(GameWindow aWindow, GameTime aGameTime)
        {
            #region LoadSaveFile
            if (Mouse.GetState().X > myPosition.X && Mouse.GetState().X < myPosition.X + mySizeX && Mouse.GetState().Y > myPosition.Y && Mouse.GetState().Y < myPosition.Y + mySizeY)
            {
                myIncrSize = 2;
                if (Game.AccessPreviousMouseState.LeftButton == ButtonState.Released && Game.AccessCurrentMouseState.LeftButton == ButtonState.Pressed)
                {
                    Game.AccessMenuClickSound.Play();

                    if (!myIsLevelSaveOrNot)
                    {
                        Game.AccessCurrentLoadedFile = myFilePath;
                        Game.LoadSaveFile(myFilePath);

                        Game.AccessMenuButtons.Clear();
                        Game.AccessSaveFiles.Clear();
                    }
                    else
                    {
                        Game.LoadLevelSaveFile(myFilePath);

                        Game.AccessMenuClickSound.Play();
                        Game.AccessMenuButtons.Clear();

                        SelectLevel01 tempSelectLevel01Button = new SelectLevel01();
                        tempSelectLevel01Button.Init(new Vector2(100, 53), 512, 406);

                        SelectLevel02 tempSelectLevel02Button = new SelectLevel02();
                        tempSelectLevel02Button.Init(new Vector2(700, 53), 512, 406);

                        ReturnButton tempReturnButton = new ReturnButton();
                        tempReturnButton.Init(new Vector2(4, 466), 128, 40);

                        ChangeLevelSettings tempLevelSettings = new ChangeLevelSettings();
                        tempLevelSettings.Init(new Vector2(955, 485), 52, 52);

                        Game.AccessMenuButtons.Add(tempReturnButton);
                        Game.AccessMenuButtons.Add(tempSelectLevel01Button);
                        Game.AccessMenuButtons.Add(tempSelectLevel02Button);
                        Game.AccessMenuButtons.Add(tempLevelSettings);

                        Game.AccessSaveFiles.Clear();
                    }
                }
            }
            else
            {
                myIncrSize = 0;
            }
            #endregion

            #region DeleteSaveFile
            if (Mouse.GetState().X > myPosition.X - 88 && Mouse.GetState().X < myPosition.X + (mySizeX - 64) - 88 && Mouse.GetState().Y > myPosition.Y && Mouse.GetState().Y < myPosition.Y + mySizeY - 20)
            {
                myIncrDelButtonSize = 2;
                if (Game.AccessPreviousMouseState.LeftButton == ButtonState.Released && Game.AccessCurrentMouseState.LeftButton == ButtonState.Pressed)
                {
                    File.Delete(myFilePath);

                    Game.AccessMenuClickSound.Play();
                    Game.AccessSaveFiles.Remove(this);
                }
            }
            else
            {
                myIncrDelButtonSize = 0;
            }
            #endregion
        }

        public void Draw(SpriteBatch aSpriteBatch)
        {
            string tempPathName = Path.GetFileName(myFilePath).Split('.')[0];
            for (int i = 0; i < tempPathName.Length; i++)
            {
                try
                {
                    aSpriteBatch.DrawString(Game.AccessGlobalFont, tempPathName[i].ToString(), new Vector2(myPosition.X + (mySizeX + 12) + i * 20, myPosition.Y), Color.OrangeRed);
                }
                catch { }
            }
            aSpriteBatch.Draw(Game.AccessSandstoneSprite, new Rectangle((int)myPosition.X - myIncrSize / 2, (int)myPosition.Y - myIncrSize / 2, mySizeX + myIncrSize, mySizeY + myIncrSize), Color.White);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, "LOAD", new Vector2(myPosition.X + 24, myPosition.Y + 4), Color.Black);

            aSpriteBatch.Draw(Game.AccessSandstoneSprite, new Rectangle((int)myPosition.X - 88 - (myIncrDelButtonSize / 2), (int)myPosition.Y - (myIncrDelButtonSize / 2), (mySizeX - 64) + myIncrDelButtonSize, (mySizeY - 20) + myIncrDelButtonSize), null, Color.White);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, "DEL", new Vector2(myPosition.X - 76, myPosition.Y - 2), Color.Red, 0f, new Vector2(0, 0), 0.75f, SpriteEffects.None, 0f);
        }
    }
}
