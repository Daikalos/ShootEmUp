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
    public abstract class LevelSettings
    {
        protected Vector2 myPosition;
        protected int
            mySizeX,
            mySizeY;
        protected string
            myHealthStat,
            myDamageStat,
            mySpawnRateStat;
        protected bool
            myIsEditingHealthStat,
            myIsEditingDamageStat,
            myIsEditingSpawnRateStat;

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
        public string AccessHealthStat
        {
            get
            {
                return myHealthStat;
            }
            set
            {
                myHealthStat = value;
            }
        }
        public string AccessDamageStat
        {
            get
            {
                return myDamageStat;
            }
            set
            {
                myDamageStat = value;
            }
        }
        public string AccessSpawnRateStat
        {
            get
            {
                return mySpawnRateStat;
            }
            set
            {
                mySpawnRateStat = value;
            }
        }
        public bool AccessIsEditingHealthStat
        {
            get
            {
                return myIsEditingHealthStat;
            }
            set
            {
                myIsEditingHealthStat = value;
            }
        }
        public bool AccessIsEditingDamageStat
        {
            get
            {
                return myIsEditingDamageStat;
            }
            set
            {
                myIsEditingDamageStat = value;
            }
        }
        public bool AccessIsEditingSpawnRateStat
        {
            get
            {
                return myIsEditingSpawnRateStat;
            }
            set
            {
                myIsEditingSpawnRateStat = value;
            }
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
        public void InputText(GameTime aGameTime)
        {
            if (myIsEditingSpawnRateStat)
            {
                Game.KeyboardNumberInput(aGameTime, 8, ref mySpawnRateStat);
            }
            if (myIsEditingHealthStat)
            {
                Game.KeyboardNumberInput(aGameTime, 8, ref myHealthStat);
            }
            if (myIsEditingDamageStat)
            {
                Game.KeyboardNumberInput(aGameTime, 8, ref myDamageStat);
            }

            if (Game.AccessPreviousKeyboardState.IsKeyUp(Keys.Enter) && Game.AccessCurrentKeyboardState.IsKeyDown(Keys.Enter))
            {
                Game.ConfirmLevelSettingChange();
            }
        }

        public abstract void Init(Vector2 aPosition, int aSizeX, int aSizeY);

        public abstract void Update(GameWindow aWindow, GameTime aGameTime);

        public abstract void Draw(SpriteBatch aSpriteBatch);
    }

    class BasicEnemyStats : LevelSettings
    {
        public override void Init(Vector2 aPosition, int aSizeX, int aSizeY)
        {
            myPosition = aPosition;
            mySizeX = aSizeX;
            mySizeY = aSizeY;
            mySpawnRateStat = Game.AccessDefaultRespawnTimeBasicEnemies.ToString();
            myDamageStat = Game.AccessBasicEnemyDamage.ToString();
            myHealthStat = Game.AccessBasicEnemyHealth.ToString();
        }

        public override void Update(GameWindow aWindow, GameTime aGameTime)
        {
            switch (CheckWhatBoxSelected())
            {
                case 1:
                    Game.ConfirmLevelSettingChange();
                    myIsEditingSpawnRateStat = true;
                    break;
                case 2:
                    Game.ConfirmLevelSettingChange();
                    myIsEditingHealthStat = true;
                    break;
                case 3:
                    Game.ConfirmLevelSettingChange();
                    myIsEditingDamageStat = true;
                    break;
            }

            InputText(aGameTime);
        }

        public override void Draw(SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.Draw(Game.AccessSandstoneSprite, new Rectangle((int)myPosition.X, (int)myPosition.Y, mySizeX, mySizeY), Color.White);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, "SR:", new Vector2(myPosition.X - 46, myPosition.Y - 10), Color.OrangeRed);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, mySpawnRateStat, new Vector2(myPosition.X + 14, myPosition.Y - 6), Color.OrangeRed);
            if (myIsEditingSpawnRateStat && mySpawnRateStat.Length < 8)
            {
                aSpriteBatch.DrawString(Game.AccessGlobalFont, "_", new Vector2(myPosition.X + 14 + (19 * mySpawnRateStat.Length), myPosition.Y - 6), Color.OrangeRed);
            }

            aSpriteBatch.Draw(Game.AccessSandstoneSprite, new Rectangle((int)myPosition.X + mySizeX + 100, (int)myPosition.Y, mySizeX, mySizeY), Color.White);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, "HP: ", new Vector2(myPosition.X - 46 + mySizeX + 100, myPosition.Y - 10), Color.OrangeRed);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, myHealthStat, new Vector2(myPosition.X + 14 + mySizeX + 100, myPosition.Y - 6), Color.OrangeRed);
            if (myIsEditingHealthStat && myHealthStat.Length < 8)
            {
                aSpriteBatch.DrawString(Game.AccessGlobalFont, "_", new Vector2(myPosition.X + 14 + (19 * myHealthStat.Length) + mySizeX + 100, myPosition.Y - 6), Color.OrangeRed);
            }

            aSpriteBatch.Draw(Game.AccessSandstoneSprite, new Rectangle((int)myPosition.X + (mySizeX * 2) + 200, (int)myPosition.Y, mySizeX, mySizeY), Color.White);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, "DMG: ", new Vector2(myPosition.X - 65 + (mySizeX * 2) + 200, myPosition.Y - 10), Color.OrangeRed);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, myDamageStat, new Vector2(myPosition.X + 14 + (mySizeX * 2) + 200, myPosition.Y - 6), Color.OrangeRed);
            if (myIsEditingDamageStat && myDamageStat.Length < 8)
            {
                aSpriteBatch.DrawString(Game.AccessGlobalFont, "_", new Vector2(myPosition.X + 14 + (19 * myDamageStat.Length) + (mySizeX * 2) + 200, myPosition.Y - 6), Color.OrangeRed);
            }

            aSpriteBatch.Draw(Game.AccessEnemyMovementSprite, new Rectangle((int)myPosition.X + 750, (int)myPosition.Y - 30, 128, 128), new Rectangle(0, 0, 190, 208), Color.White);
        }
    }

    class ShootingEnemyStats : LevelSettings
    {
        public override void Init(Vector2 aPosition, int aSizeX, int aSizeY)
        {
            myPosition = aPosition;
            mySizeX = aSizeX;
            mySizeY = aSizeY;
            mySpawnRateStat = Game.AccessDefaultRespawnTimeShootingEnemies.ToString();
            myDamageStat = Game.AccessShootingEnemyDamage.ToString();
            myHealthStat = Game.AccessShootingEnemyHealth.ToString();
        }

        public override void Update(GameWindow aWindow, GameTime aGameTime)
        {
            switch (CheckWhatBoxSelected())
            {
                case 1:
                    Game.ConfirmLevelSettingChange();
                    myIsEditingSpawnRateStat = true;
                    break;
                case 2:
                    Game.ConfirmLevelSettingChange();
                    myIsEditingHealthStat = true;
                    break;
                case 3:
                    Game.ConfirmLevelSettingChange();
                    myIsEditingDamageStat = true;
                    break;
            }

            InputText(aGameTime);
        }

        public override void Draw(SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.Draw(Game.AccessSandstoneSprite, new Rectangle((int)myPosition.X, (int)myPosition.Y, mySizeX, mySizeY), Color.White);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, "SR:", new Vector2(myPosition.X - 46, myPosition.Y - 10), Color.OrangeRed);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, mySpawnRateStat, new Vector2(myPosition.X + 14, myPosition.Y - 6), Color.OrangeRed);
            if (myIsEditingSpawnRateStat && mySpawnRateStat.Length < 8)
            {
                aSpriteBatch.DrawString(Game.AccessGlobalFont, "_", new Vector2(myPosition.X + 14 + (19 * mySpawnRateStat.Length), myPosition.Y - 6), Color.OrangeRed);
            }

            aSpriteBatch.Draw(Game.AccessSandstoneSprite, new Rectangle((int)myPosition.X + mySizeX + 100, (int)myPosition.Y, mySizeX, mySizeY), Color.White);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, "HP: ", new Vector2(myPosition.X - 46 + mySizeX + 100, myPosition.Y - 10), Color.OrangeRed);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, myHealthStat, new Vector2(myPosition.X + 14 + mySizeX + 100, myPosition.Y - 6), Color.OrangeRed);
            if (myIsEditingHealthStat && myHealthStat.Length < 8)
            {
                aSpriteBatch.DrawString(Game.AccessGlobalFont, "_", new Vector2(myPosition.X + 14 + (19 * myHealthStat.Length) + mySizeX + 100, myPosition.Y - 6), Color.OrangeRed);
            }

            aSpriteBatch.Draw(Game.AccessSandstoneSprite, new Rectangle((int)myPosition.X + (mySizeX * 2) + 200, (int)myPosition.Y, mySizeX, mySizeY), Color.White);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, "DMG: ", new Vector2(myPosition.X - 65 + (mySizeX * 2) + 200, myPosition.Y - 10), Color.OrangeRed);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, myDamageStat, new Vector2(myPosition.X + 14 + (mySizeX * 2) + 200, myPosition.Y - 6), Color.OrangeRed);
            if (myIsEditingDamageStat && myDamageStat.Length < 8)
            {
                aSpriteBatch.DrawString(Game.AccessGlobalFont, "_", new Vector2(myPosition.X + 14 + (19 * myDamageStat.Length) + (mySizeX * 2) + 200, myPosition.Y - 6), Color.OrangeRed);
            }

            aSpriteBatch.Draw(Game.AccessEnemyAttackSprite, new Rectangle((int)myPosition.X + 760, (int)myPosition.Y - 6, 80, 80), new Rectangle(0, 0, 48, 60), Color.White);
        }
    }

    class SpikeballEnemyStats : LevelSettings
    {
        public override void Init(Vector2 aPosition, int aSizeX, int aSizeY)
        {
            myPosition = aPosition;
            mySizeX = aSizeX;
            mySizeY = aSizeY;
            mySpawnRateStat = Game.AccessDefaultRespawnTimeSpikeballs.ToString();
            myDamageStat = Game.AccessSpikeballDamage.ToString();
            myHealthStat = Game.AccessSpikeballHealth.ToString();
        }

        public override void Update(GameWindow aWindow, GameTime aGameTime)
        {
            switch (CheckWhatBoxSelected())
            {
                case 1:
                    Game.ConfirmLevelSettingChange();
                    myIsEditingSpawnRateStat = true;
                    break;
                case 2:
                    Game.ConfirmLevelSettingChange();
                    myIsEditingHealthStat = true;
                    break;
                case 3:
                    Game.ConfirmLevelSettingChange();
                    myIsEditingDamageStat = true;
                    break;
            }

            InputText(aGameTime);
        }

        public override void Draw(SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.Draw(Game.AccessSandstoneSprite, new Rectangle((int)myPosition.X, (int)myPosition.Y, mySizeX, mySizeY), Color.White);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, "SR:", new Vector2(myPosition.X - 46, myPosition.Y - 10), Color.OrangeRed);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, mySpawnRateStat, new Vector2(myPosition.X + 14, myPosition.Y - 6), Color.OrangeRed);
            if (myIsEditingSpawnRateStat && mySpawnRateStat.Length < 8)
            {
                aSpriteBatch.DrawString(Game.AccessGlobalFont, "_", new Vector2(myPosition.X + 14 + (19 * mySpawnRateStat.Length), myPosition.Y - 6), Color.OrangeRed);
            }

            aSpriteBatch.Draw(Game.AccessSandstoneSprite, new Rectangle((int)myPosition.X + mySizeX + 100, (int)myPosition.Y, mySizeX, mySizeY), Color.White);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, "HP: ", new Vector2(myPosition.X - 46 + mySizeX + 100, myPosition.Y - 10), Color.OrangeRed);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, myHealthStat, new Vector2(myPosition.X + 14 + mySizeX + 100, myPosition.Y - 6), Color.OrangeRed);
            if (myIsEditingHealthStat && myHealthStat.Length < 8)
            {
                aSpriteBatch.DrawString(Game.AccessGlobalFont, "_", new Vector2(myPosition.X + 14 + (19 * myHealthStat.Length) + mySizeX + 100, myPosition.Y - 6), Color.OrangeRed);
            }

            aSpriteBatch.Draw(Game.AccessSandstoneSprite, new Rectangle((int)myPosition.X + (mySizeX * 2) + 200, (int)myPosition.Y, mySizeX, mySizeY), Color.White);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, "DMG: ", new Vector2(myPosition.X - 65 + (mySizeX * 2) + 200, myPosition.Y - 10), Color.OrangeRed);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, myDamageStat, new Vector2(myPosition.X + 14 + (mySizeX * 2) + 200, myPosition.Y - 6), Color.OrangeRed);
            if (myIsEditingDamageStat && myDamageStat.Length < 8)
            {
                aSpriteBatch.DrawString(Game.AccessGlobalFont, "_", new Vector2(myPosition.X + 14 + (19 * myDamageStat.Length) + (mySizeX * 2) + 200, myPosition.Y - 6), Color.OrangeRed);
            }

            aSpriteBatch.Draw(Game.AccessSpikeballSprite, new Rectangle((int)myPosition.X + 760, (int)myPosition.Y - 6, 80, 80), null, Color.White);
        }
    }

    class YetiEnemyStats : LevelSettings
    {
        public override void Init(Vector2 aPosition, int aSizeX, int aSizeY)
        {
            myPosition = aPosition;
            mySizeX = aSizeX;
            mySizeY = aSizeY;
            mySpawnRateStat = Game.AccessDefaultRespawnTimeYeti.ToString();
            myDamageStat = Game.AccessYetiDamage.ToString();
            myHealthStat = Game.AccessYetiHealth.ToString();
        }

        public override void Update(GameWindow aWindow, GameTime aGameTime)
        {
            switch (CheckWhatBoxSelected())
            {
                case 1:
                    Game.ConfirmLevelSettingChange();
                    myIsEditingSpawnRateStat = true;
                    break;
                case 2:
                    Game.ConfirmLevelSettingChange();
                    myIsEditingHealthStat = true;
                    break;
                case 3:
                    Game.ConfirmLevelSettingChange();
                    myIsEditingDamageStat = true;
                    break;
            }

            InputText(aGameTime);
        }

        public override void Draw(SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.Draw(Game.AccessSandstoneSprite, new Rectangle((int)myPosition.X, (int)myPosition.Y, mySizeX, mySizeY), Color.White);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, "SR:", new Vector2(myPosition.X - 46, myPosition.Y - 10), Color.OrangeRed);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, mySpawnRateStat, new Vector2(myPosition.X + 14, myPosition.Y - 6), Color.OrangeRed);
            if (myIsEditingSpawnRateStat && mySpawnRateStat.Length < 8)
            {
                aSpriteBatch.DrawString(Game.AccessGlobalFont, "_", new Vector2(myPosition.X + 14 + (19 * mySpawnRateStat.Length), myPosition.Y - 6), Color.OrangeRed);
            }

            aSpriteBatch.Draw(Game.AccessSandstoneSprite, new Rectangle((int)myPosition.X + mySizeX + 100, (int)myPosition.Y, mySizeX, mySizeY), Color.White);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, "HP: ", new Vector2(myPosition.X - 46 + mySizeX + 100, myPosition.Y - 10), Color.OrangeRed);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, myHealthStat, new Vector2(myPosition.X + 14 + mySizeX + 100, myPosition.Y - 6), Color.OrangeRed);
            if (myIsEditingHealthStat && myHealthStat.Length < 8)
            {
                aSpriteBatch.DrawString(Game.AccessGlobalFont, "_", new Vector2(myPosition.X + 14 + (19 * myHealthStat.Length) + mySizeX + 100, myPosition.Y - 6), Color.OrangeRed);
            }

            aSpriteBatch.Draw(Game.AccessSandstoneSprite, new Rectangle((int)myPosition.X + (mySizeX * 2) + 200, (int)myPosition.Y, mySizeX, mySizeY), Color.White);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, "DMG: ", new Vector2(myPosition.X - 65 + (mySizeX * 2) + 200, myPosition.Y - 10), Color.OrangeRed);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, myDamageStat, new Vector2(myPosition.X + 14 + (mySizeX * 2) + 200, myPosition.Y - 6), Color.OrangeRed);
            if (myIsEditingDamageStat && myDamageStat.Length < 8)
            {
                aSpriteBatch.DrawString(Game.AccessGlobalFont, "_", new Vector2(myPosition.X + 14 + (19 * myDamageStat.Length) + (mySizeX * 2) + 200, myPosition.Y - 6), Color.OrangeRed);
            }

            aSpriteBatch.Draw(Game.AccessYetiSprite, new Rectangle((int)myPosition.X + 706, (int)myPosition.Y - 56, 120, 120), new Rectangle(0, 0, 282, 219), Color.White);
        }
    }

    class BlockSpawnRate : LevelSettings
    {
        public override void Init(Vector2 aPosition, int aSizeX, int aSizeY)
        {
            myPosition = aPosition;
            mySizeX = aSizeX;
            mySizeY = aSizeY;
            mySpawnRateStat = Game.AccessDefaultRespawnTimeBlocks.ToString();
        }

        public override void Update(GameWindow aWindow, GameTime aGameTime)
        {
            if ((Mouse.GetState().X > myPosition.X - 16 && Mouse.GetState().X < myPosition.X + mySizeX && Mouse.GetState().Y > myPosition.Y && Mouse.GetState().Y < myPosition.Y + mySizeY))
            {
                if (Game.AccessPreviousMouseState.LeftButton == ButtonState.Released && Game.AccessCurrentMouseState.LeftButton == ButtonState.Pressed)
                {
                    Game.ConfirmLevelSettingChange();
                    myIsEditingSpawnRateStat = true;
                }
            }

            if (myIsEditingSpawnRateStat)
            {
                Game.KeyboardNumberInput(aGameTime, 8, ref mySpawnRateStat);
            }

            if (Game.AccessPreviousKeyboardState.IsKeyUp(Keys.Enter) && Game.AccessCurrentKeyboardState.IsKeyDown(Keys.Enter))
            {
                Game.ConfirmLevelSettingChange();
            }
        }

        public override void Draw(SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.Draw(Game.AccessSandstoneSprite, new Rectangle((int)myPosition.X, (int)myPosition.Y, mySizeX, mySizeY), Color.White);
            aSpriteBatch.Draw(Game.AccessSandstoneSprite, new Rectangle((int)myPosition.X + 54, (int)myPosition.Y - 44, mySizeX - 144, mySizeY + 4), Color.White);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, "SR:", new Vector2(myPosition.X + 3, myPosition.Y - 34), Color.OrangeRed);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, "Block", new Vector2(myPosition.X + 98, myPosition.Y - 38), Color.OrangeRed);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, mySpawnRateStat, new Vector2(myPosition.X + 14, myPosition.Y - 6), Color.OrangeRed);
            if (myIsEditingSpawnRateStat && mySpawnRateStat.Length < 8)
            {
                aSpriteBatch.DrawString(Game.AccessGlobalFont, "_", new Vector2(myPosition.X + 14 + (19 * mySpawnRateStat.Length), myPosition.Y - 6), Color.OrangeRed);
            }
        }
    }

    class PowerupSpawnRate : LevelSettings
    {
        private float myDelayClick;

        public override void Init(Vector2 aPosition, int aSizeX, int aSizeY)
        {
            myPosition = aPosition;
            mySizeX = aSizeX;
            mySizeY = aSizeY;
            myDelayClick = 100;
            mySpawnRateStat = Game.AccessDefaultRespawnTimePowerup.ToString();
        }

        public override void Update(GameWindow aWindow, GameTime aGameTime)
        {
            myDelayClick -= (float)aGameTime.ElapsedGameTime.TotalMilliseconds;
            if ((Mouse.GetState().X > myPosition.X - 16 && Mouse.GetState().X < myPosition.X + mySizeX && Mouse.GetState().Y > myPosition.Y && Mouse.GetState().Y < myPosition.Y + mySizeY))
            {
                if (Game.AccessPreviousMouseState.LeftButton == ButtonState.Released && Game.AccessCurrentMouseState.LeftButton == ButtonState.Pressed && myDelayClick <= 0)
                {
                    Game.ConfirmLevelSettingChange();
                    myIsEditingSpawnRateStat = true;
                }
            }

            if (myIsEditingSpawnRateStat)
            {
                Game.KeyboardNumberInput(aGameTime, 8, ref mySpawnRateStat);
            }

            if (Game.AccessPreviousKeyboardState.IsKeyUp(Keys.Enter) && Game.AccessCurrentKeyboardState.IsKeyDown(Keys.Enter))
            {
                Game.ConfirmLevelSettingChange();
            }
        }

        public override void Draw(SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.Draw(Game.AccessSandstoneSprite, new Rectangle((int)myPosition.X, (int)myPosition.Y, mySizeX, mySizeY), Color.White);
            aSpriteBatch.Draw(Game.AccessPowerupSprite, new Rectangle((int)myPosition.X + 54, (int)myPosition.Y - 44, 44, 44), new Rectangle(0, 0, 30, 30), Color.White);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, "SR:", new Vector2(myPosition.X + 3, myPosition.Y - 34), Color.OrangeRed);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, "PU", new Vector2(myPosition.X + 106, myPosition.Y - 38), Color.OrangeRed);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, mySpawnRateStat, new Vector2(myPosition.X + 14, myPosition.Y - 6), Color.OrangeRed);
            if (myIsEditingSpawnRateStat && mySpawnRateStat.Length < 8)
            {
                aSpriteBatch.DrawString(Game.AccessGlobalFont, "_", new Vector2(myPosition.X + 14 + (19 * mySpawnRateStat.Length), myPosition.Y - 6), Color.OrangeRed);
            }
        }
    }

    class SaveLevelSettings : LevelSettings
    {
        private int myIncrSize;
        private bool myUserTypeIn;
        private string myFileNameInput;
        private float myDelayIfSaveLimitReached;

        public override void Init(Vector2 aPosition, int aSizeX, int aSizeY)
        {
            myPosition = aPosition;
            mySizeX = aSizeX;
            mySizeY = aSizeY;
            myFileNameInput = "";
        }

        public override void Update(GameWindow aWindow, GameTime aGameTime)
        {
            myDelayIfSaveLimitReached -= (float)aGameTime.ElapsedGameTime.TotalSeconds;
            if (Mouse.GetState().X > myPosition.X && Mouse.GetState().X < myPosition.X + mySizeX && Mouse.GetState().Y > myPosition.Y && Mouse.GetState().Y < myPosition.Y + mySizeY)
            {
                myIncrSize = 2;
                if (Game.AccessPreviousMouseState.LeftButton == ButtonState.Released && Game.AccessCurrentMouseState.LeftButton == ButtonState.Pressed)
                {
                    Game.AccessMenuClickSound.Play();
                    Game.AccessSaveFilesPath.Clear();
                    Directory.CreateDirectory(@"..\..\..\..\LevelFiles\");
                    DirectoryInfo tempGetDirectory = new DirectoryInfo(@"..\..\..\..\LevelFiles\");
                    FileInfo[] tempFiles = tempGetDirectory.GetFiles(" *.txt"); //Getting Text files
                    string tempFilePathString = "";
                    foreach (FileInfo file in tempFiles)
                    {
                        tempFilePathString = tempFilePathString + ", " + file.Name;
                        Game.AccessSaveFilesPath.Add(@"..\..\..\..\LevelFiles\" + file.Name);
                    }

                    if (Game.AccessSaveFilesPath.Count < 7)
                    {
                        myUserTypeIn = true;
                    }
                    else
                    {
                        myDelayIfSaveLimitReached = 7;
                    }
                }
            }
            else
            {
                myIncrSize = 0;
            }

            if (myUserTypeIn)
            {
                Game.KeyboardTextInput(aGameTime, 20, ref myFileNameInput);

                if (Keyboard.GetState().GetPressedKeys().Length > 0 && Keyboard.GetState().GetPressedKeys()[0] == Keys.Enter)
                {
                    myUserTypeIn = false;
                    Game.SaveLevelSettingsData(myFileNameInput);
                    myFileNameInput = "";
                }
            }
        }

        public override void Draw(SpriteBatch aSpriteBatch)
        {
            if (myUserTypeIn)
            {
                aSpriteBatch.DrawString(Game.AccessGlobalFont, "ENTER NAME FOR SAVEFILE:", new Vector2(myPosition.X + 265, myPosition.Y - 100), Color.OrangeRed);
                aSpriteBatch.DrawString(Game.AccessGlobalFont, myFileNameInput, new Vector2(myPosition.X + 265, myPosition.Y - 60), Color.OrangeRed);
                if (myFileNameInput.Length < 20)
                {
                    aSpriteBatch.DrawString(Game.AccessGlobalFont, "_", new Vector2(myPosition.X + 265 + myFileNameInput.Length * 19, myPosition.Y - 60), Color.OrangeRed);
                }
                aSpriteBatch.DrawString(Game.AccessGlobalFont, "PRESS ENTER TO FINISH", new Vector2(myPosition.X + 265, myPosition.Y), Color.OrangeRed);
            }
            if (myDelayIfSaveLimitReached > 0)
            {
                aSpriteBatch.DrawString(Game.AccessGlobalFont, "MAX AMOUNT OF SAVE FILES REACHED", new Vector2(12, 470), Color.OrangeRed);
            }
            aSpriteBatch.Draw(Game.AccessSandstoneSprite, new Rectangle((int)myPosition.X - myIncrSize / 2, (int)myPosition.Y - myIncrSize / 2, mySizeX + myIncrSize, mySizeY + myIncrSize), Color.White);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, "SAVE", new Vector2(myPosition.X + (mySizeX / 2) - 38, myPosition.Y), Color.Black);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, "SR = Time until the object", new Vector2(6, myPosition.Y - 50), Color.OrangeRed, 0f, new Vector2(0, 0), 0.75f, SpriteEffects.None, 0f);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, "spawns in (ms)", new Vector2(6, myPosition.Y - 34), Color.OrangeRed, 0f, new Vector2(0, 0), 0.75f, SpriteEffects.None, 0f);
        }
    }

    class LoadLevelSettings : LevelSettings
    {
        private int myIncrSize;

        public override void Init(Vector2 aPosition, int aSizeX, int aSizeY)
        {
            myPosition = aPosition;
            mySizeX = aSizeX;
            mySizeY = aSizeY;
        }

        public override void Update(GameWindow aWindow, GameTime aGameTime)
        {
            if (Mouse.GetState().X > myPosition.X && Mouse.GetState().X < myPosition.X + mySizeX && Mouse.GetState().Y > myPosition.Y && Mouse.GetState().Y < myPosition.Y + mySizeY)
            {
                myIncrSize = 2;
                if (Game.AccessPreviousMouseState.LeftButton == ButtonState.Released && Game.AccessCurrentMouseState.LeftButton == ButtonState.Pressed)
                {
                    Game.AccessMenuClickSound.Play();
                    Game.AccessMenuButtons.Clear();
                    Game.AccessLevelSettings.Clear();

                    ReturnButton tempReturnButton = new ReturnButton();
                    tempReturnButton.Init(new Vector2(4, 466), 128, 40);

                    Game.AccessMenuButtons.Add(tempReturnButton);

                    Game.AccessSaveFilesPath.Clear();
                    DirectoryInfo tempGetDirectory = new DirectoryInfo(@"..\..\..\..\LevelFiles\");
                    FileInfo[] tempFiles = tempGetDirectory.GetFiles("*.txt"); //Getting Text files
                    string tempFilePathString = "";
                    foreach (FileInfo file in tempFiles)
                    {
                        tempFilePathString = tempFilePathString + ", " + file.Name;
                        Game.AccessSaveFilesPath.Add(@"..\..\..\..\LevelFiles\" + file.Name);
                    }

                    for (int i = 0; i < Game.AccessSaveFilesPath.Count; i++)
                    {
                        SaveFile tempSaveFile = new SaveFile(new Vector2((aWindow.ClientBounds.Width / 2) - 256, 12 + i * 72), 128, 48, Game.AccessSaveFilesPath[i], true);
                        Game.AccessSaveFiles.Add(tempSaveFile);
                    }
                }
            }
            else
            {
                myIncrSize = 0;
            }
        }

        public override void Draw(SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.Draw(Game.AccessSandstoneSprite, new Rectangle((int)myPosition.X - myIncrSize / 2, (int)myPosition.Y - myIncrSize / 2, mySizeX + myIncrSize, mySizeY + myIncrSize), Color.White);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, "LOAD", new Vector2(myPosition.X + (mySizeX / 2) - 38, myPosition.Y), Color.Black);
        }
    }
}
