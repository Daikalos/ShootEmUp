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
    public abstract class MenuButtons
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

        public abstract void Init(Vector2 aPosition, int aSizeX, int aSizeY);

        public abstract void Update(GameWindow aWindow, GameTime aGameTime);

        public abstract void Draw(SpriteBatch aSpriteBatch);
    }

    class MenuButtonPlay : MenuButtons
    {
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
                    if (Game.myGameStateNow != Game.MyGameState.myPausing)
                    {
                        Game.AccessMenuButtons.Clear();
                        if (Game.AccessLevel03Unlocked)
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                Game.AccessScrollingBackgrounds[i].AccessPosition = new Vector2(-1588, 0);
                            }
                            Game.AccessScrollingBackgrounds[5].AccessPosition = new Vector2(0, 0);
                            Game.AccessActiveLevel = 3;
                            Game.AccessSwitchMusic = true;
                        }
                        else if (Game.AccessLevel02Unlocked)
                        {
                            for (int i = 0; i < 2; i++)
                            {
                                Game.AccessScrollingBackgrounds[i].AccessPosition = new Vector2(-1588, 0);
                            }
                            Game.AccessScrollingBackgrounds[3].AccessPosition = new Vector2(0, 0);
                            Game.AccessActiveLevel = 2;
                            Game.AccessSwitchMusic = true;
                        }
                        else
                        {
                            Game.AccessActiveLevel = 1;
                            Game.AccessSwitchMusic = true;
                        }

                        SelectCharacter1 tempSelectCharacter1 = new SelectCharacter1();
                        tempSelectCharacter1.Init(new Vector2(40, 80), 530, 340);

                        SelectCharacter2 tempSelectCharacter2 = new SelectCharacter2();
                        tempSelectCharacter2.Init(new Vector2(aWindow.ClientBounds.Width - (530 + 40), 80), 530, 340);

                        ReturnButton tempReturnButton = new ReturnButton();
                        tempReturnButton.Init(new Vector2(4, 466), 128, 40);

                        Game.AccessMenuButtons.Add(tempSelectCharacter1);
                        Game.AccessMenuButtons.Add(tempSelectCharacter2);
                        Game.AccessMenuButtons.Add(tempReturnButton);
                    }
                    else
                    {
                        Game.myGameStateNow = Game.MyGameState.myPlaying;
                        Game.AccessMenuButtons.Clear();
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
            aSpriteBatch.DrawString(Game.AccessGlobalFont, "PLAY", new Vector2(myPosition.X + 85, myPosition.Y + 10), Color.Black);
            if (Game.AccessLevel03Unlocked)
            {
                aSpriteBatch.DrawString(Game.AccessGlobalFont, "Level 3", new Vector2(myPosition.X, myPosition.Y - 32), Color.OrangeRed);
            }
            else if (Game.AccessLevel02Unlocked)
            {
                aSpriteBatch.DrawString(Game.AccessGlobalFont, "Level 2", new Vector2(myPosition.X, myPosition.Y - 32), Color.OrangeRed);
            }
            else
            {
                aSpriteBatch.DrawString(Game.AccessGlobalFont, "Level 1", new Vector2(myPosition.X, myPosition.Y - 32), Color.OrangeRed);
            }
        }
    }

    class MenuButtonSelectLevels : MenuButtons
    {
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
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
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
            aSpriteBatch.DrawString(Game.AccessGlobalFont, "SELECT LEVEL", new Vector2(myPosition.X + 16, myPosition.Y + 10), Color.Black);
        }
    }

    class ChangeLevelSettings : MenuButtons
    {
        private float myRotation;

        public override void Init(Vector2 aPosition, int aSizeX, int aSizeY)
        {
            myPosition = aPosition;
            mySizeX = aSizeX;
            mySizeY = aSizeY;
        }

        public override void Update(GameWindow aWindow, GameTime aGameTime)
        {
            if (Mouse.GetState().X > myPosition.X - mySizeX / 2 && Mouse.GetState().X < myPosition.X + mySizeX / 2 && Mouse.GetState().Y > myPosition.Y - mySizeY / 2 && Mouse.GetState().Y < myPosition.Y + mySizeY / 2)
            {
                myRotation += 0.05f;
                if (Game.AccessPreviousMouseState.LeftButton == ButtonState.Released && Game.AccessCurrentMouseState.LeftButton == ButtonState.Pressed)
                {
                    Game.AccessMenuClickSound.Play();
                    Game.AccessMenuButtons.Clear();

                    BasicEnemyStats tempChangeBasicEnemyStats = new BasicEnemyStats();
                    tempChangeBasicEnemyStats.Init(new Vector2(112, 20), 180, 32);

                    ShootingEnemyStats tempChangeShootingEnemyStats = new ShootingEnemyStats();
                    tempChangeShootingEnemyStats.Init(new Vector2(112, 110), 180, 32);

                    SpikeballEnemyStats tempChangeSpikeballStats = new SpikeballEnemyStats();
                    tempChangeSpikeballStats.Init(new Vector2(112, 200), 180, 32);

                    YetiEnemyStats tempChangeYetiStats = new YetiEnemyStats();
                    tempChangeYetiStats.Init(new Vector2(112, 290), 180, 32);

                    PowerupSpawnRate tempChangePowerupSpawnRate = new PowerupSpawnRate();
                    tempChangePowerupSpawnRate.Init(new Vector2(850, 468), 180, 32);

                    BlockSpawnRate tempChangeBlockSpawnRate = new BlockSpawnRate();
                    tempChangeBlockSpawnRate.Init(new Vector2(1080, 468), 180, 32);

                    SaveLevelSettings tempSaveLevelSettingsButton = new SaveLevelSettings();
                    tempSaveLevelSettingsButton.Init(new Vector2(140, 466), 128, 40);

                    LoadLevelSettings tempLoadLevelSettingsButton = new LoadLevelSettings();
                    tempLoadLevelSettingsButton.Init(new Vector2(272, 466), 128, 40);

                    ReturnButton tempReturnButton = new ReturnButton();
                    tempReturnButton.Init(new Vector2(4, 466), 128, 40);

                    Game.AccessLevelSettings.Add(tempChangeBasicEnemyStats);
                    Game.AccessLevelSettings.Add(tempChangeShootingEnemyStats);
                    Game.AccessLevelSettings.Add(tempChangeSpikeballStats);
                    Game.AccessLevelSettings.Add(tempChangeYetiStats);
                    Game.AccessLevelSettings.Add(tempChangePowerupSpawnRate);
                    Game.AccessLevelSettings.Add(tempChangeBlockSpawnRate);
                    Game.AccessLevelSettings.Add(tempSaveLevelSettingsButton);
                    Game.AccessLevelSettings.Add(tempLoadLevelSettingsButton);
                    Game.AccessMenuButtons.Add(tempReturnButton);
                }
            }
            else
            {
                myRotation = 0;
            }
        }

        public override void Draw(SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.Draw(Game.AccessCogwheelSprite, new Rectangle((int)myPosition.X, (int)myPosition.Y, mySizeX, mySizeY), null, Color.White, myRotation, new Vector2(32, 32), SpriteEffects.None, 0f);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, "LEVEL SETTINGS", new Vector2(myPosition.X + 40, myPosition.Y - 20), Color.OrangeRed);
        }
    }

    class ExitButton : MenuButtons
    {
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
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    Game.AccessCloseGame = true;
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
            aSpriteBatch.DrawString(Game.AccessGlobalFont, "EXIT", new Vector2(myPosition.X + 85, myPosition.Y + 10), Color.Black);
        }
    }

    class ReturnButton : MenuButtons
    {
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
                    if (Game.myGameStateNow == Game.MyGameState.myPausing)
                    {
                        MediaPlayer.Play(Game.AccessMenuMusic);
                        Game.myGameStateNow = Game.MyGameState.myMenu;
                    }

                    #region ResetAllObjectsStats
                    Game.AccessBasicEnemyHealth = 100;
                    Game.AccessBasicEnemyDamage = 10;
                    Game.AccessShootingEnemyHealth = 100;
                    Game.AccessShootingEnemyDamage = 10;
                    Game.AccessSpikeballHealth = 150;
                    Game.AccessSpikeballDamage = 10;
                    Game.AccessYetiHealth = 175;
                    Game.AccessYetiDamage = 10;

                    Game.AccessDefaultRespawnTimeBasicEnemies = 3000;
                    Game.AccessDefaultRespawnTimeShootingEnemies = 5000;
                    Game.AccessDefaultRespawnTimeSpikeballs = 4500;
                    Game.AccessDefaultRespawnTimeYeti = 3000;
                    Game.AccessDefaultRespawnTimePowerup = 20000;
                    Game.AccessDefaultRespawnTimeBlocks = 20000;
                    #endregion

                    Game.AccessShopButtons.Clear();
                    Game.AccessMenuButtons.Clear();
                    Game.AccessSaveFiles.Clear();
                    Game.AccessLevelSettings.Clear();
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
            aSpriteBatch.DrawString(Game.AccessGlobalFont, "BACK", new Vector2(myPosition.X + (mySizeX / 2) - 38, myPosition.Y), Color.Black);
        }
    }

    class SaveButton : MenuButtons
    {
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
                    Directory.CreateDirectory(@"..\..\..\..\Savefiles\");
                    DirectoryInfo tempGetDirectory = new DirectoryInfo(@"..\..\..\..\Savefiles\");
                    FileInfo[] tempFiles = tempGetDirectory.GetFiles(" *.txt"); //Getting Text files
                    string tempFilePathString = "";
                    foreach (FileInfo file in tempFiles)
                    {
                        tempFilePathString = tempFilePathString + ", " + file.Name;
                        Game.AccessSaveFilesPath.Add(@"..\..\..\..\Savefiles\" + file.Name);
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
                    Game.SaveData(myFileNameInput);
                    Game.AccessCurrentLoadedFile = myFileNameInput;
                    myFileNameInput = "";
                }
            }
        }

        public override void Draw(SpriteBatch aSpriteBatch)
        {
            if (myUserTypeIn)
            {
                aSpriteBatch.DrawString(Game.AccessGlobalFont, "ENTER NAME FOR SAVEFILE:", new Vector2(myPosition.X + 500, myPosition.Y + 100), Color.OrangeRed);
                aSpriteBatch.DrawString(Game.AccessGlobalFont, myFileNameInput, new Vector2(myPosition.X + 500, myPosition.Y + 140), Color.OrangeRed);
                if (myFileNameInput.Length < 20)
                {
                    aSpriteBatch.DrawString(Game.AccessGlobalFont, "_", new Vector2(myPosition.X + 500 + myFileNameInput.Length * 19, myPosition.Y + 140), Color.OrangeRed);
                }
                aSpriteBatch.DrawString(Game.AccessGlobalFont, "PRESS Space TO FINISH", new Vector2(myPosition.X + 500, myPosition.Y + 200), Color.OrangeRed);
            }
            if (myDelayIfSaveLimitReached > 0)
            {
                aSpriteBatch.DrawString(Game.AccessGlobalFont, "MAX AMOUNT OF SAVE FILES REACHED", new Vector2(12, 470), Color.OrangeRed);
            }
            aSpriteBatch.Draw(Game.AccessSandstoneSprite, new Rectangle((int)myPosition.X - myIncrSize / 2, (int)myPosition.Y - myIncrSize / 2, mySizeX + myIncrSize, mySizeY + myIncrSize), Color.White);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, "SAVE", new Vector2(myPosition.X + (mySizeX / 2) - 38, myPosition.Y), Color.Black);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, "HIGH SCORE: " + Game.AccessHighScore.ToString(), new Vector2(myPosition.X + (mySizeX / 2) + 210, myPosition.Y), Color.OrangeRed);
        }
    }

    class LoadButton : MenuButtons
    {
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

                    ReturnButton tempReturnButton = new ReturnButton();
                    tempReturnButton.Init(new Vector2(4, 466), 128, 40);

                    Game.AccessMenuButtons.Add(tempReturnButton);

                    Game.AccessSaveFilesPath.Clear();
                    DirectoryInfo tempGetDirectory = new DirectoryInfo(@"..\..\..\..\Savefiles\");
                    FileInfo[] tempFiles = tempGetDirectory.GetFiles("*.txt"); //Getting Text files
                    string tempFilePathString = "";
                    foreach (FileInfo file in tempFiles)
                    {
                        tempFilePathString = tempFilePathString + ", " + file.Name;
                        Game.AccessSaveFilesPath.Add(@"..\..\..\..\Savefiles\" + file.Name);
                    }

                    for (int i = 0; i < Game.AccessSaveFilesPath.Count; i++)
                    {
                        SaveFile tempSaveFile = new SaveFile(new Vector2((aWindow.ClientBounds.Width / 2) - 256, 12 + i * 72), 128, 48, Game.AccessSaveFilesPath[i], false);
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

    public class OpenShop : MenuButtons
    {
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
                    Game.AccessMenuButtons.Clear();

                    ReturnButton tempReturnButton = new ReturnButton();
                    tempReturnButton.Init(new Vector2(4, 466), 128, 40);

                    BulletTimeUpgrade tempBulletTimeUpgradeButton = new BulletTimeUpgrade();
                    tempBulletTimeUpgradeButton.Init(new Vector2(160, 50), 96, 96);

                    HealthUpgrade tempHealthUpgradeButton = new HealthUpgrade();
                    tempHealthUpgradeButton.Init(new Vector2(160, 176), 96, 96);

                    ShieldUpgrade tempShieldUpgradeButton = new ShieldUpgrade();
                    tempShieldUpgradeButton.Init(new Vector2(160, 302), 96, 96);

                    Game.AccessShopButtons.Add(tempBulletTimeUpgradeButton);
                    Game.AccessShopButtons.Add(tempHealthUpgradeButton);
                    Game.AccessShopButtons.Add(tempShieldUpgradeButton);
                    Game.AccessMenuButtons.Add(tempReturnButton);
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
            aSpriteBatch.DrawString(Game.AccessGlobalFont, "SHOP", new Vector2(myPosition.X + 85, myPosition.Y + 10), Color.Black);
        }
    }

    class SettingsButton : MenuButtons
    {
        private float myRotation;

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
                myRotation += 0.05f;
                if (Game.AccessPreviousMouseState.LeftButton == ButtonState.Released && Game.AccessCurrentMouseState.LeftButton == ButtonState.Pressed)
                {
                    Game.AccessMenuClickSound.Play();
                    Game.AccessMenuButtons.Clear();

                    MusicVolumeSlider tempMusicSlider = new MusicVolumeSlider();
                    tempMusicSlider.Init(new Vector2(400, 150), 32, 64);
                    tempMusicSlider.AccessPosition = new Vector2(tempMusicSlider.AccessPosition.X * (Game.AccessMusicVolume + 1), tempMusicSlider.AccessPosition.Y);

                    SoundEffectVolumeSlider tempSoundEffectSlider = new SoundEffectVolumeSlider();
                    tempSoundEffectSlider.Init(new Vector2(400, 300), 32, 64);
                    tempSoundEffectSlider.AccessPosition = new Vector2(tempSoundEffectSlider.AccessPosition.X * (Game.AccessSoundEffectVolume + 1), tempSoundEffectSlider.AccessPosition.Y);

                    ReturnButton tempReturnButton = new ReturnButton();
                    tempReturnButton.Init(new Vector2(4, 466), 128, 40);

                    Game.AccessMenuButtons.Add(tempMusicSlider);
                    Game.AccessMenuButtons.Add(tempSoundEffectSlider);
                    Game.AccessMenuButtons.Add(tempReturnButton);
                }
            }
            else
            {
                myRotation = 0;
            }
        }

        public override void Draw(SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.Draw(Game.AccessCogwheelSprite, new Rectangle((int)myPosition.X + 32, (int)myPosition.Y + 32, mySizeX, mySizeY), null, Color.White, myRotation, new Vector2(mySizeX / 2, mySizeY / 2), SpriteEffects.None, 0f);
        }
    }

    class MusicVolumeSlider : MenuButtons
    {
        private bool myIsHoldingOnButton;

        public override void Init(Vector2 aPosition, int aSizeX, int aSizeY)
        {
            myPosition = aPosition;
            mySizeX = aSizeX;
            mySizeY = aSizeY;
        }

        public override void Update(GameWindow aWindow, GameTime aGameTime)
        {
            if ((Mouse.GetState().X > myPosition.X - 16 && Mouse.GetState().X < myPosition.X + mySizeX && Mouse.GetState().Y > myPosition.Y && Mouse.GetState().Y < myPosition.Y + mySizeY) || myIsHoldingOnButton)
            {
                if (Game.AccessCurrentMouseState.LeftButton == ButtonState.Pressed)
                {
                    myIsHoldingOnButton = true;
                    if ((myPosition.X >= 400 && myPosition.X <= 800))
                    {
                        myPosition.X = Mouse.GetState().X;

                        if (Mouse.GetState().X >= 400 && Mouse.GetState().X <= 800)
                        {
                            Game.AccessMusicVolume = ((myPosition.X - 400) / 400);
                            MediaPlayer.Volume = Game.AccessMusicVolume;
                        }
                    }
                }
            }
            if (myPosition.X < 400)
            {
                myPosition.X = 400;
                MediaPlayer.Volume = 0f;
            }
            if (myPosition.X > 800)
            {
                myPosition.X = 800;
                MediaPlayer.Volume = 1f;
            }
            if (Game.AccessCurrentMouseState.LeftButton == ButtonState.Released)
            {
                myIsHoldingOnButton = false;
            }
        }

        public override void Draw(SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.Draw(Game.AccessSandstoneSprite, new Rectangle(400, 174, 400, 20), Color.White);
            aSpriteBatch.Draw(Game.AccessSandstoneSprite, new Rectangle((int)myPosition.X - 16, (int)myPosition.Y, mySizeX, mySizeY), null, Color.White);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, "MUSIC VOLUME", new Vector2(400, 100), Color.OrangeRed);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, (Math.Round(MediaPlayer.Volume * 100)).ToString() + "%", new Vector2(820, 164), Color.OrangeRed);
        }
    }

    class SoundEffectVolumeSlider : MenuButtons
    {
        private bool myIsHoldingOnButton;

        public override void Init(Vector2 aPosition, int aSizeX, int aSizeY)
        {
            myPosition = aPosition;
            mySizeX = aSizeX;
            mySizeY = aSizeY;
        }

        public override void Update(GameWindow aWindow, GameTime aGameTime)
        {
            if ((Mouse.GetState().X > myPosition.X - 16 && Mouse.GetState().X < myPosition.X + mySizeX && Mouse.GetState().Y > myPosition.Y && Mouse.GetState().Y < myPosition.Y + mySizeY) || myIsHoldingOnButton)
            {
                if (Game.AccessCurrentMouseState.LeftButton == ButtonState.Pressed)
                {
                    myIsHoldingOnButton = true;
                    if ((myPosition.X >= 400 && myPosition.X <= 800))
                    {
                        myPosition.X = Mouse.GetState().X;

                        if (Mouse.GetState().X >= 400 && Mouse.GetState().X <= 800)
                        {
                            if ((int)myPosition.X % 50 == 0 && Game.AccessPreviousMouseState.X != Game.AccessCurrentMouseState.X)
                            {
                                Game.AccessExplosionSound.Play();
                            }
                            Game.AccessSoundEffectVolume = ((myPosition.X - 400) / 400);
                            SoundEffect.MasterVolume = Game.AccessSoundEffectVolume;
                        }
                    }
                }
            }
            if (myPosition.X < 400)
            {
                myPosition.X = 400;
                SoundEffect.MasterVolume = 0f;
            }
            if (myPosition.X > 800)
            {
                myPosition.X = 800;
                SoundEffect.MasterVolume = 1f;
            }
            if (Game.AccessCurrentMouseState.LeftButton == ButtonState.Released)
            {
                myIsHoldingOnButton = false;
            }
        }

        public override void Draw(SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.Draw(Game.AccessSandstoneSprite, new Rectangle(400, 324, 400, 20), Color.White);
            aSpriteBatch.Draw(Game.AccessSandstoneSprite, new Rectangle((int)myPosition.X - 16, (int)myPosition.Y, mySizeX, mySizeY), null, Color.White);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, "EFFECTS VOLUME", new Vector2(400, 250), Color.OrangeRed);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, (Math.Round(SoundEffect.MasterVolume * 100)).ToString() + "%", new Vector2(820, 314), Color.OrangeRed);
        }
    }

    class SelectCharacter1 : MenuButtons
    {
        private float myDelayClick;

        public override void Init(Vector2 aPosition, int aSizeX, int aSizeY)
        {
            myPosition = aPosition;
            mySizeX = aSizeX;
            mySizeY = aSizeY;
            myDelayClick = 0;
        }

        public override void Update(GameWindow aWindow, GameTime aGameTime)
        {
            myDelayClick += (float)aGameTime.ElapsedGameTime.TotalMilliseconds;
            if (Mouse.GetState().X > myPosition.X && Mouse.GetState().X < myPosition.X + mySizeX && Mouse.GetState().Y > myPosition.Y && Mouse.GetState().Y < myPosition.Y + mySizeY)
            {
                if (myDelayClick > 100)
                {
                    if (Game.AccessPreviousMouseState.LeftButton == ButtonState.Released && Game.AccessCurrentMouseState.LeftButton == ButtonState.Pressed)
                    {
                        Game.AccessMenuClickSound.Play();

                        Player1 tempPlayer = new Player1();
                        if (Game.AccessHealthUpgradeStatus == 3)
                        {
                            tempPlayer.Init(new Vector2(aWindow.ClientBounds.Width / 2, aWindow.ClientBounds.Height / 2), 300);
                        }
                        else
                        {
                            tempPlayer.Init(new Vector2(aWindow.ClientBounds.Width / 2, aWindow.ClientBounds.Height / 2), 100 + (Game.AccessHealthUpgradeStatus * 50));
                        }
                        Game.AccessPlayer = tempPlayer;

                        Game.SemiResetVariables(aWindow);
                        Game.AccessMenuButtons.Clear();
                        Game.myGameStateNow = Game.MyGameState.myPlaying;
                    }
                }
            }
        }

        public override void Draw(SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.Draw(Game.AccessSandstoneSprite, new Rectangle((int)myPosition.X, (int)myPosition.Y, mySizeX, mySizeY), Color.White * 0.9f);
            aSpriteBatch.Draw(Game.AccessPlayer1RunningSprite, new Rectangle((int)myPosition.X + 68, (int)myPosition.Y + 58, 249 + (249 / 2), 150 + (150 / 2)), new Rectangle(0, 0, 249, 150), Color.White);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, "SELECT", new Vector2(myPosition.X, myPosition.Y - 80), Color.OrangeRed);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, "CHARACTER", new Vector2(myPosition.X, myPosition.Y - 61), Color.OrangeRed);
        }
    }

    class SelectCharacter2 : MenuButtons
    {
        private float myDelayClick;

        public override void Init(Vector2 aPosition, int aSizeX, int aSizeY)
        {
            myPosition = aPosition;
            mySizeX = aSizeX;
            mySizeY = aSizeY;
            myDelayClick = 0;
        }

        public override void Update(GameWindow aWindow, GameTime aGameTime)
        {
            myDelayClick += (float)aGameTime.ElapsedGameTime.TotalMilliseconds;
            if (Mouse.GetState().X > myPosition.X && Mouse.GetState().X < myPosition.X + mySizeX && Mouse.GetState().Y > myPosition.Y && Mouse.GetState().Y < myPosition.Y + mySizeY)
            {
                if (myDelayClick > 100 && Game.AccessCharacter2Unlocked)
                {
                    if (Game.AccessPreviousMouseState.LeftButton == ButtonState.Released && Game.AccessCurrentMouseState.LeftButton == ButtonState.Pressed)
                    {
                        Game.AccessMenuClickSound.Play();

                        Player2 tempPlayer = new Player2();
                        tempPlayer.Init(new Vector2(aWindow.ClientBounds.Width / 2, aWindow.ClientBounds.Height / 2), 100);
                        Game.AccessPlayer = tempPlayer;

                        Game.SemiResetVariables(aWindow);
                        Game.AccessMenuButtons.Clear();
                        Game.myGameStateNow = Game.MyGameState.myPlaying;
                    }
                }
            }
        }

        public override void Draw(SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.Draw(Game.AccessSandstoneSprite, new Rectangle((int)myPosition.X, (int)myPosition.Y, mySizeX, mySizeY), Color.White * 0.9f);
            if (Game.AccessCharacter2Unlocked)
            {
                aSpriteBatch.Draw(Game.AccessPlayer2IdleSprite, new Vector2(myPosition.X + 180, myPosition.Y + 30), null, Color.White, 0f, new Vector2(0, 0), 1.7f, SpriteEffects.None, 0f);
                aSpriteBatch.Draw(Game.AccessPlayer2RunningSprite, new Vector2(myPosition.X + 160, myPosition.Y + 167), new Rectangle(0, 0, 80, 78), Color.White, 0f, new Vector2(0, 0), 1.7f, SpriteEffects.None, 0f);
            }
            else
            {
                aSpriteBatch.Draw(Game.AccessPlayer2IdleSprite, new Vector2(myPosition.X + 180, myPosition.Y + 30), null, Color.Black, 0f, new Vector2(0, 0), 1.7f, SpriteEffects.None, 0f);
                aSpriteBatch.Draw(Game.AccessPlayer2RunningSprite, new Vector2(myPosition.X + 160, myPosition.Y + 167), new Rectangle(0, 0, 80, 78), Color.Black, 0f, new Vector2(0, 0), 1.7f, SpriteEffects.None, 0f);
                aSpriteBatch.DrawString(Game.AccessGlobalFont, "YOU NEED " + (800 - Game.AccessAmountOfHitsOnEnemies).ToString() + " MORE", new Vector2(myPosition.X + 100, myPosition.Y + 120), Color.OrangeRed);
                aSpriteBatch.DrawString(Game.AccessGlobalFont, "HITS TO UNLOCK THIS", new Vector2(myPosition.X + 80, myPosition.Y + 160), Color.OrangeRed);
                aSpriteBatch.DrawString(Game.AccessGlobalFont, "CHARACTER", new Vector2(myPosition.X + 170, myPosition.Y + 200), Color.OrangeRed);
            }
        }
    }

    class SelectLevel01 : MenuButtons
    {
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
                if (Game.AccessPreviousMouseState.LeftButton == ButtonState.Released && Game.AccessCurrentMouseState.LeftButton == ButtonState.Pressed)
                {
                    Game.AccessMenuClickSound.Play();
                    Game.AccessActiveLevel = 1;
                    Game.AccessSwitchMusic = true;
                    Game.AccessMenuButtons.Clear();

                    SelectCharacter1 tempSelectCharacter1 = new SelectCharacter1();
                    tempSelectCharacter1.Init(new Vector2(40, 80), 530, 340);

                    SelectCharacter2 tempSelectCharacter2 = new SelectCharacter2();
                    tempSelectCharacter2.Init(new Vector2(aWindow.ClientBounds.Width - (530 + 40), 80), 530, 340);

                    ReturnButton tempReturnButton = new ReturnButton();
                    tempReturnButton.Init(new Vector2(4, 466), 128, 40);

                    Game.AccessMenuButtons.Add(tempSelectCharacter1);
                    Game.AccessMenuButtons.Add(tempSelectCharacter2);
                    Game.AccessMenuButtons.Add(tempReturnButton);
                }
            }
        }

        public override void Draw(SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.Draw(Game.AccessLevel01SelectSprite, new Rectangle((int)myPosition.X, (int)myPosition.Y, mySizeX, mySizeY), Color.White);
            aSpriteBatch.DrawString(Game.AccessGlobalFont, "Sand Desert", new Vector2(myPosition.X + (mySizeX / 2) - 96, myPosition.Y + 18), Color.OrangeRed);
        }
    }

    class SelectLevel02 : MenuButtons
    {
        public override void Init(Vector2 aPosition, int aSizeX, int aSizeY)
        {
            myPosition = aPosition;
            mySizeX = aSizeX;
            mySizeY = aSizeY;
        }

        public override void Update(GameWindow aWindow, GameTime aGameTime)
        {
            if (Game.AccessLevel02Unlocked)
            {
                if (Mouse.GetState().X > myPosition.X && Mouse.GetState().X < myPosition.X + mySizeX && Mouse.GetState().Y > myPosition.Y && Mouse.GetState().Y < myPosition.Y + mySizeY)
                {
                    if (Game.AccessPreviousMouseState.LeftButton == ButtonState.Released && Game.AccessCurrentMouseState.LeftButton == ButtonState.Pressed)
                    {
                        Game.AccessMenuClickSound.Play();
                        Game.AccessActiveLevel = 2;
                        Game.AccessSwitchMusic = true;
                        Game.AccessMenuButtons.Clear();

                        SelectCharacter1 tempSelectCharacter1 = new SelectCharacter1();
                        tempSelectCharacter1.Init(new Vector2(40, 80), 530, 340);

                        SelectCharacter2 tempSelectCharacter2 = new SelectCharacter2();
                        tempSelectCharacter2.Init(new Vector2(aWindow.ClientBounds.Width - (530 + 40), 80), 530, 340);

                        ReturnButton tempReturnButton = new ReturnButton();
                        tempReturnButton.Init(new Vector2(4, 466), 128, 40);

                        Game.AccessMenuButtons.Add(tempSelectCharacter1);
                        Game.AccessMenuButtons.Add(tempSelectCharacter2);
                        Game.AccessMenuButtons.Add(tempReturnButton);
                    }
                }
            }
        }

        public override void Draw(SpriteBatch aSpriteBatch)
        {
            if (Game.AccessLevel02Unlocked)
            {
                aSpriteBatch.Draw(Game.AccessLevel02SelectSprite, new Rectangle((int)myPosition.X, (int)myPosition.Y, mySizeX, mySizeY), Color.White);
                aSpriteBatch.DrawString(Game.AccessGlobalFont, "Snow Desert", new Vector2(myPosition.X + (mySizeX / 2) - 96, myPosition.Y + 18), Color.White);
            }
            else
            {
                aSpriteBatch.Draw(Game.AccessLevel02SelectSprite, new Rectangle((int)myPosition.X, (int)myPosition.Y, mySizeX, mySizeY), Color.Gray);
            }
        }
    }
}
