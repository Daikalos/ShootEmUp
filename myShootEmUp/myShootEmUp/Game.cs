using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;

namespace myShootEmUp
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        public enum MyGameState
        {
            myMenu,
            myPlaying,
            myPausing
        }
        public static MyGameState myGameStateNow;
        static Exception myException;
        GraphicsDeviceManager myGraphics;
        SpriteBatch mySpriteBatch;
        Random myRNG;

        #region GameVariables
        static List<Scrolling> myScrollingBackgrounds;
        static MouseState
            myCurrentMouseState,
            myPreviousMouseState;
        static KeyboardState
            myCurrentKeyboardState,
            myPreviousKeyboardState;
        static SpriteFont
            myGameOverFont,
            myGlobalFont,
            myHealthFont;
        static Texture2D
            myPlayer1RunningSprite,
            myPlayer1JumpingSprite,
            myPlayer2RunningSprite,
            myPlayer2JumpingSprite,
            myPlayer2IdleSprite,
            myPlayer2ShootingSprite,
            myPlayer2IdleSlideSprite,
            myPlayer2ShootingSlideSprite,
            myHealthbarSprite,
            myPowerUpSprite,
            mySandstoneSprite,
            myEnemyAttackingSprite,
            myFireballSprite,
            myPlayerBulletSprite,
            myEnemyMovementSprite,
            mySpikeballSprite,
            myBloodParticleSprite,
            myBoulderSprite,
            myGrenadeSprite,
            myExplosionSprite,
            myTankDrivingSprite,
            myTankShootingSprite,
            myTankDeathSprite,
            myTankBulletSprite,
            myYetiSprite,
            myLevel01SelectSprite,
            myLevel02SelectSprite,
            myCogwheelSprite,
            myPlayerMissileSprite,
            myChopperFlyingSprite,
            myChopperMissileSprite,
            myChopperBombSprite,
            myBulletTimeUpgradeSprite,
            myHealthUpgradeSprite,
            myShieldUpgradeSprite,
            myBulletTimeDurationBarSprite,
            myShieldDurationBarSprite,
            myShieldSprite;
        static SoundEffect
            myMenuClick,
            myExplosion,
            myMachineGun,
            myHitMetal,
            myTankFire,
            myTankIdle,
            myBossVictoryMusic,
            myGrenadeUpgradeSound,
            myMachineGunUpgradeSound,
            myChopperIdle;
        static Song
            myMenuMusic,
            myLevel01Music,
            myLevel02Music,
            myLevel03Music,
            myBossMusic;
        static Player myPlayer;
        static Other.Boulder myBoulder;
        static Enemies.TankBoss myTankBoss;
        static Enemies.ChopperBoss myChopperBoss;
        static List<Enemies.ChopperMissile> myChopperMissiles;
        static List<Enemies.ChopperBomb> myChopperBombs;
        static List<Enemies.TankBullet> myTankBullets;
        static List<BaseEnemy> myBaseEnemies;
        static List<Enemies.EnemyBullet> myEnemyBullets;
        static List<PlayerBullet> myPlayerBullets;
        static List<Block> myBlocks;
        static List<Other.Powerup> myPowerups;
        static List<Other.BloodParticle> myBloodParticles;
        static List<PlayerAbility> myPlayerAbilityList;
        static List<Menu.MenuButtons> myMenuButtons;
        static List<Menu.SaveFile> mySaveFiles;
        static List<Menu.LevelSettings> myLevelSettings;
        static List<Menu.Shop> myShopButtons;
        static List<string> mySaveFilesPath;
        static string myCurrentFileLoaded;
        static int
            myScore,
            myCoins,
            myAddCoins,
            myActiveLevel,
            myBasicEnemyHealth,
            myBasicEnemyDamage,
            myShootingEnemyHealth,
            myShootingEnemyDamage,
            mySpikeballHealth,
            mySpikeballDamage,
            myYetiHealth,
            myYetiDamage,
            myBulletTimeUpgradeStatus,
            myHealthUpgradeStatus,
            myShieldUpgradeStatus;
        private static double
            myGameSpeedDelay,
            myAddScoreDelay;
        private static float
            myRespawnTimeBasicEnemies,
            myDefaultRespawnTimeBasicEnemies,
            myRespawnTimeShootingEnemies,
            myDefaultRespawnTimeShootingEnemies,
            myRespawnTimeSpikeballs,
            myDefaultRespawnTimeSpikeballs,
            myRespawnTimeYeti,
            myDefaultRespawnTimeYeti,
            myRespawnTimePowerUps,
            myDefaultRespawnTimePowerUps,
            myRespawnTimeBlock,
            myDefaultRespawnTimeBlock,
            myDelayRemoveLetter,
            myDelayAddLetter,
            myOpenMenuDelay,
            myResetException;
        static float
            myGametimeSpeed,
            myUpdateSpeed,
            myAmountOfHitsOnEnemies,
            myHighScore,
            myGameSpeed,
            myMusicVolume,
            mySoundEffectVolume;
        static bool
            mySpawnBoss,
            mySwitchMusic,
            myResetGame,
            myCloseGame,
            myGameOver,
            mySelectLevel,
            myLevel02Unlocked,
            myLevel03Unlocked,
            myCharacter2Unlocked;
        #endregion

        #region AccessMethods

        #region Miscellaneous
        public static string AccessCurrentLoadedFile
        {
            get => myCurrentFileLoaded;
            set => myCurrentFileLoaded = value;
        }
        public static float AccessSoundEffectVolume
        {
            get => mySoundEffectVolume;
            set => mySoundEffectVolume = value;
        }
        public static float AccessMusicVolume
        {
            get => myMusicVolume;
            set => myMusicVolume = value;
        }
        public static float AccessAmountOfHitsOnEnemies
        {
            get => myAmountOfHitsOnEnemies;
            set => myAmountOfHitsOnEnemies = value;
        }
        public static float AccessHighScore
        {
            get => myHighScore;
            set => myHighScore = value;
        }
        public static float AccessGameSpeed
        {
            get => myGameSpeed * myGametimeSpeed * 40;
            set => myGameSpeed = value;
        }
        public static float AccessTrueGameSpeed
        {
            get => myGameSpeed;
            set => myGameSpeed = value;
        }
        public static float AccessUpdateSpeed
        {
            get => myUpdateSpeed * myGametimeSpeed * 60;
            set => myUpdateSpeed = value;
        }
        public static float AccessTrueUpdateSpeed
        {
            get => myUpdateSpeed;
            set => myUpdateSpeed = value;
        }
        public static int AccessActiveLevel
        {
            get => myActiveLevel;
            set => myActiveLevel = value;
        }
        public static int AccessScore
        {
            get => myScore;
            set => myScore = value;
        }
        public static int AccessCoins
        {
            get => myCoins;
            set => myCoins = value;
        }
        public static int AccessAddCoins
        {
            get => myAddCoins;
            set => myAddCoins = value;
        }
        public static bool AccessCharacter2Unlocked
        {
            get => myCharacter2Unlocked;
            set => myCharacter2Unlocked = value;
        }
        public static bool AccessLevel02Unlocked
        {
            get => myLevel02Unlocked;
            set => myLevel02Unlocked = value;
        }
        public static bool AccessLevel03Unlocked
        {
            get => myLevel03Unlocked;
            set => myLevel03Unlocked = value;
        }
        public static bool AccessResetGame
        {
            get => myResetGame;
            set => myResetGame = value;
        }
        public static bool AccessCloseGame
        {
            get => myCloseGame;
            set => myCloseGame = value;
        }
        public static bool AccessSelectLevel
        {
            get => mySelectLevel;
            set => mySelectLevel = value;
        }
        public static bool AccessSwitchMusic
        {
            get => mySwitchMusic;
            set => mySwitchMusic = value;
        }
        public static List<Scrolling> AccessScrollingBackgrounds
        {
            get => myScrollingBackgrounds;
            set => myScrollingBackgrounds = value;
        }
        #endregion

        #region SpawnTimes
        public static float AccessSpawnRateBasicEnemies
        {
            get => myRespawnTimeBasicEnemies;
        }
        public static float AccessSpawnRateShootingEnemies
        {
            get => myRespawnTimeShootingEnemies;
        }
        public static float AccessRespawnTimeSpikeballs
        {
            get => myRespawnTimeSpikeballs;
        }
        public static float AccessRespawnTimeYeti
        {
            get => myRespawnTimeYeti;
        }
        public static float AccessRespawnTimePowerup
        {
            get => myRespawnTimePowerUps;
        }
        public static float AccessRespawnTimeBlocks
        {
            get => myRespawnTimeBlock;
            set => myRespawnTimeBlock = value;
        }
        public static float AccessDefaultRespawnTimeBasicEnemies
        {
            get => myDefaultRespawnTimeBasicEnemies;
            set => myDefaultRespawnTimeBasicEnemies = value;
        }
        public static float AccessDefaultRespawnTimeShootingEnemies
        {
            get => myDefaultRespawnTimeShootingEnemies;
            set => myDefaultRespawnTimeShootingEnemies = value;
        }
        public static float AccessDefaultRespawnTimeSpikeballs
        {
            get => myDefaultRespawnTimeSpikeballs;
            set => myDefaultRespawnTimeSpikeballs = value;
        }
        public static float AccessDefaultRespawnTimeYeti
        {
            get => myDefaultRespawnTimeYeti;
            set => myDefaultRespawnTimeYeti = value;
        }
        public static float AccessDefaultRespawnTimePowerup
        {
            get => myDefaultRespawnTimePowerUps;
            set => myDefaultRespawnTimePowerUps = value;
        }
        public static float AccessDefaultRespawnTimeBlocks
        {
            get => myDefaultRespawnTimeBlock;
            set => myDefaultRespawnTimeBlock = value;
        }
        #endregion

        #region EnemyHealth&Damage
        public static int AccessBasicEnemyHealth
        {
            get => myBasicEnemyHealth;
            set => myBasicEnemyHealth = value;
        }
        public static int AccessBasicEnemyDamage
        {
            get => myBasicEnemyDamage;
            set => myBasicEnemyDamage = value;
        }
        public static int AccessShootingEnemyHealth
        {
            get => myShootingEnemyHealth;
            set => myShootingEnemyHealth = value;
        }
        public static int AccessShootingEnemyDamage
        {
            get => myShootingEnemyDamage;
            set => myShootingEnemyDamage = value;
        }
        public static int AccessSpikeballHealth
        {
            get => mySpikeballHealth;
            set => mySpikeballHealth = value;
        }
        public static int AccessSpikeballDamage
        {
            get => mySpikeballDamage;
            set => mySpikeballDamage = value;
        }
        public static int AccessYetiHealth
        {
            get => myYetiHealth;
            set => myYetiHealth = value;
        }
        public static int AccessYetiDamage
        {
            get => myYetiDamage;
            set => myYetiDamage = value;
        }
        public static int AccessBulletTimeUpgradeStatus
        {
            get => myBulletTimeUpgradeStatus;
            set => myBulletTimeUpgradeStatus = value;
        }
        public static int AccessHealthUpgradeStatus
        {
            get => myHealthUpgradeStatus;
            set => myHealthUpgradeStatus = value;
        }
        public static int AccessShieldUpgradeStatus
        {
            get => myShieldUpgradeStatus;
            set => myShieldUpgradeStatus = value;
        }
        #endregion

        #region Lists
        public static List<Enemies.EnemyBullet> AccessEnemyBullets
        {
            get => myEnemyBullets;
            set => myEnemyBullets = value;
        }
        public static List<BaseEnemy> AccessBaseEnemies
        {
            get => myBaseEnemies;
            set => myBaseEnemies = value;
        }
        public static List<PlayerBullet> AccessPlayerBullets
        {
            get => myPlayerBullets;
            set => myPlayerBullets = value;
        }
        public static List<Block> AccessBlocks
        {
            get => myBlocks;
            set => myBlocks = value;
        }
        public static List<Other.Powerup> AccessPowerUps
        {
            get => myPowerups;
            set => myPowerups = value;
        }
        public static List<Menu.MenuButtons> AccessMenuButtons
        {
            get => myMenuButtons;
        }
        public static List<Menu.SaveFile> AccessSaveFiles
        {
            get => mySaveFiles;
        }
        public static List<Menu.LevelSettings> AccessLevelSettings
        {
            get => myLevelSettings;
            set => myLevelSettings = value;
        }
        public static List<Menu.Shop> AccessShopButtons
        {
            get => myShopButtons;
            set => myShopButtons = value;
        }
        public static List<PlayerAbility> AccessPlayerAbilityList
        {
            get => myPlayerAbilityList;
            set => myPlayerAbilityList = value;
        }
        public static List<Enemies.ChopperMissile> AccessChopperMissiles
        {
            get => myChopperMissiles;
            set => myChopperMissiles = value;
        }
        public static List<Enemies.ChopperBomb> AccessChopperBombs
        {
            get => myChopperBombs;
            set => myChopperBombs = value;
        }
        public static List<Enemies.TankBullet> AccessTankBullets
        {
            get => myTankBullets;
            set => myTankBullets = value;
        }
        public static List<string> AccessSaveFilesPath
        {
            get => mySaveFilesPath;
            set => mySaveFilesPath = value;
        }
        #endregion

        #region Objects
        public static Player AccessPlayer
        {
            get => myPlayer;
            set => myPlayer = value;
        }
        public static Enemies.TankBoss AccessTankBoss
        {
            get => myTankBoss;
            set => myTankBoss = value;
        }
        public static Enemies.ChopperBoss AccessChopperBoss
        {
            get => myChopperBoss;
            set => myChopperBoss = value;
        }
        public static Other.Boulder AccessBoulder
        {
            get => myBoulder;
            set => myBoulder = value;
        }
        #endregion

        #region SoundEffects&Song
        public static SoundEffect AccessMenuClickSound
        {
            get => myMenuClick;
        }
        public static SoundEffect AccessExplosionSound
        {
            get => myExplosion;
        }
        public static SoundEffect AccessMachineGunSound
        {
            get => myMachineGun;
        }
        public static SoundEffect AccessMetalHitSound
        {
            get => myHitMetal;
        }
        public static SoundEffect AccessTankFireSound
        {
            get => myTankFire;
        }
        public static SoundEffect AccessTankIdleSound
        {
            get => myTankIdle;
        }
        public static SoundEffect AccessBossVictorySound
        {
            get => myBossVictoryMusic;
        }
        public static SoundEffect AccessGrenadeUpgradeSound
        {
            get => myGrenadeUpgradeSound;
        }
        public static SoundEffect AccessMachineGunUpgradeSound
        {
            get => myMachineGunUpgradeSound;
        }
        public static SoundEffect AccessChopperIdleSound
        {
            get => myChopperIdle;
        }
        public static Song AccessMenuMusic
        {
            get => myMenuMusic;
        }
        #endregion

        #region Textures
        public static Texture2D AccessPlayer1RunningSprite
        {
            get => myPlayer1RunningSprite;
        }
        public static Texture2D AccessPlayer1JumpingSprite
        {
            get => myPlayer1JumpingSprite;
        }
        public static Texture2D AccessPlayer2RunningSprite
        {
            get => myPlayer2RunningSprite;
        }
        public static Texture2D AccessPlayer2JumpingSprite
        {
            get => myPlayer2JumpingSprite;
        }
        public static Texture2D AccessPlayer2IdleSprite
        {
            get => myPlayer2IdleSprite;
        }
        public static Texture2D AccessPlayer2ShootingSprite
        {
            get => myPlayer2ShootingSprite;
        }
        public static Texture2D AccessPlayer2IdleSlideSprite
        {
            get => myPlayer2IdleSlideSprite;
        }
        public static Texture2D AccessPlayer2ShootSlideSprite
        {
            get => myPlayer2ShootingSlideSprite;
        }
        public static Texture2D AccessHealthbarSprite
        {
            get => myHealthbarSprite;
        }
        public static Texture2D AccessPowerupSprite
        {
            get => myPowerUpSprite;
        }
        public static Texture2D AccessSandstoneSprite
        {
            get => mySandstoneSprite;
        }
        public static Texture2D AccessEnemyAttackSprite
        {
            get => myEnemyAttackingSprite;
        }
        public static Texture2D AccessFireballSprite
        {
            get => myFireballSprite;
        }
        public static Texture2D AccessPlayerBulletSprite
        {
            get => myPlayerBulletSprite;
        }
        public static Texture2D AccessEnemyMovementSprite
        {
            get => myEnemyMovementSprite;
        }
        public static Texture2D AccessSpikeballSprite
        {
            get => mySpikeballSprite;
        }
        public static Texture2D AccessBloodParticleSprite
        {
            get => myBloodParticleSprite;
        }
        public static Texture2D AccessBoulderSprite
        {
            get => myBoulderSprite;
        }
        public static Texture2D AccessGrenadeSprite
        {
            get => myGrenadeSprite;
        }
        public static Texture2D AccessExplosionSprite
        {
            get => myExplosionSprite;
        }
        public static Texture2D AccessTankDrivingSprite
        {
            get => myTankDrivingSprite;
        }
        public static Texture2D AccessTankShootingSprite
        {
            get => myTankShootingSprite;
        }
        public static Texture2D AccessTankDeathSprite
        {
            get => myTankDeathSprite;
        }
        public static Texture2D AccessTankBulletSprite
        {
            get => myTankBulletSprite;
        }
        public static Texture2D AccessYetiSprite
        {
            get => myYetiSprite;
        }
        public static Texture2D AccessLevel01SelectSprite
        {
            get => myLevel01SelectSprite;
        }
        public static Texture2D AccessLevel02SelectSprite
        {
            get => myLevel02SelectSprite;
        }
        public static Texture2D AccessCogwheelSprite
        {
            get => myCogwheelSprite;
        }
        public static Texture2D AccessPlayerMissileSprite
        {
            get => myPlayerMissileSprite;
        }
        public static Texture2D AccessChopperBossFlyingSprite
        {
            get => myChopperFlyingSprite;
        }
        public static Texture2D AccessChopperBossMissileSprite
        {
            get => myChopperMissileSprite;
        }
        public static Texture2D AccessChopperBossBombSprite
        {
            get => myChopperBombSprite;
        }
        public static Texture2D AccessBulletTimeUpgradeSprite
        {
            get => myBulletTimeUpgradeSprite;
        }
        public static Texture2D AccessHealthUpgradeSprite
        {
            get => myHealthUpgradeSprite;
        }
        public static Texture2D AccessShieldUpgradeSprite
        {
            get => myShieldUpgradeSprite;
        }
        public static Texture2D AccessBulletTimeDurationBarSprite
        {
            get => myBulletTimeDurationBarSprite;
        }
        public static Texture2D AccessShieldDurationBarSprite
        {
            get => myShieldDurationBarSprite;
        }
        public static Texture2D AccessShieldSprite
        {
            get => myShieldSprite;
        }
        #endregion

        #region Fonts
        public static SpriteFont AccessGlobalFont
        {
            get => myGlobalFont;
        }
        public static SpriteFont AccessGameOverFont
        {
            get => myGameOverFont;
        }
        public static SpriteFont AccessHealthFont
        {
            get => myHealthFont;
        }
        #endregion

        #region States
        public static MouseState AccessCurrentMouseState
        {
            get => myCurrentMouseState;
        }
        public static MouseState AccessPreviousMouseState
        {
            get => myPreviousMouseState;
        }
        public static KeyboardState AccessCurrentKeyboardState
        {
            get => myCurrentKeyboardState;
        }
        public static KeyboardState AccessPreviousKeyboardState
        {
            get => myPreviousKeyboardState;
        }
        #endregion

        #endregion

        public Game()
        {
            myGraphics = new GraphicsDeviceManager(this)
            {
                GraphicsProfile = GraphicsProfile.HiDef,
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 512
            };
            myGraphics.ApplyChanges();
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
            MediaPlayer.Volume = 0f;
            SoundEffect.MasterVolume = 0f;

            myBaseEnemies = new List<BaseEnemy>();
            myEnemyBullets = new List<Enemies.EnemyBullet>();
            myPlayerBullets = new List<PlayerBullet>();
            myPlayerAbilityList = new List<PlayerAbility>();
            myPowerups = new List<Other.Powerup>();
            myBlocks = new List<Block>();
            myBloodParticles = new List<Other.BloodParticle>();
            myBoulder = new Other.Boulder(new Vector2(-100, Window.ClientBounds.Height - 436));
            myRNG = new Random();
            myTankBoss = new Enemies.TankBoss(new Vector2(Window.ClientBounds.Width + 126, Window.ClientBounds.Height / 2 - 116));
            myTankBullets = new List<Enemies.TankBullet>();
            myChopperBoss = new Enemies.ChopperBoss(new Vector2(Window.ClientBounds.Width + 126, 10));
            myChopperMissiles = new List<Enemies.ChopperMissile>();
            myChopperBombs = new List<Enemies.ChopperBomb>();
            myMenuButtons = new List<Menu.MenuButtons>();
            myShopButtons = new List<Menu.Shop>();
            mySaveFiles = new List<Menu.SaveFile>();
            myLevelSettings = new List<Menu.LevelSettings>();
            mySaveFilesPath = new List<string>();

            myScrollingBackgrounds = new List<Scrolling>
            {
                new Scrolling(Content.Load<Texture2D>("mySprites/Sand_Village_BackgroundSprite"), new Rectangle(0, 0, 1588, 512)),
                new Scrolling(Content.Load<Texture2D>("mySprites/Sand_Village_BackgroundSprite2"), new Rectangle(1588, 0, 1588, 512)),
                new Scrolling(Content.Load<Texture2D>("mySprites/Snow_Village_BackgroundSprite"), new Rectangle(0, 0, 1588, 512)),
                new Scrolling(Content.Load<Texture2D>("mySprites/Snow_Village_BackgroundSprite2"), new Rectangle(1588, 0, 1588, 512)),
                new Scrolling(Content.Load<Texture2D>("mySprites/Snow_Village_BackgroundSprite3"), new Rectangle(1588, 0, 1588, 512)),
                new Scrolling(Content.Load<Texture2D>("mySprites/Night_Village_BackgroundSprite"), new Rectangle(0, 0, 1588, 512)),
                new Scrolling(Content.Load<Texture2D>("mySprites/Night_Village_BackgroundSprite2"), new Rectangle(1588, 0, 1588, 512)),
                new Scrolling(Content.Load<Texture2D>("mySprites/Night_Village_BackgroundSprite3"), new Rectangle(1588, 0, 1588, 512))
            };

            myBlocks.Add(new Block(new Vector2(1280, 260), new Rectangle(1280, 260, 64, 64), 64, 64));
            myBlocks.Add(new Block(new Vector2(1580, 260), new Rectangle(1580, 260, 64, 64), 64, 64));
            myBlocks.Add(new Block(new Vector2(1880, 260), new Rectangle(1880, 260, 64, 64), 64, 64));

            myGameSpeed = 4;
            myScore = (myActiveLevel - 1) * 120;

            myGameSpeedDelay = 30;
            myAddScoreDelay = 1;
            myActiveLevel = 0;
            myUpdateSpeed = 1f;

            myBasicEnemyHealth = 100;
            myBasicEnemyDamage = 10;
            myShootingEnemyHealth = 100;
            myShootingEnemyDamage = 10;
            mySpikeballHealth = 150;
            mySpikeballDamage = 10;
            myYetiHealth = 175;
            myYetiDamage = 10;

            myRespawnTimeBasicEnemies = 5000;
            myDefaultRespawnTimeBasicEnemies = 3000;
            myRespawnTimeShootingEnemies = 60000;
            myDefaultRespawnTimeShootingEnemies = 5000;
            myRespawnTimeSpikeballs = 80000;
            myDefaultRespawnTimeSpikeballs = 4500;
            myRespawnTimeYeti = 14000;
            myDefaultRespawnTimeYeti = 3000;
            myRespawnTimePowerUps = 1000;
            myDefaultRespawnTimePowerUps = 20000;
            myRespawnTimeBlock = 0;
            myDefaultRespawnTimeBlock = 20000;
            myOpenMenuDelay = 2;
            myResetException = 7;

            mySpawnBoss = false;
            mySwitchMusic = false;
            myResetGame = false;
            myCloseGame = false;
            mySelectLevel = false;
            myGameOver = false;

            myGameStateNow = MyGameState.myMenu;
        }

        public static void SemiResetVariables(GameWindow aWindow)
        {
            myBaseEnemies = new List<BaseEnemy>();
            myEnemyBullets = new List<Enemies.EnemyBullet>();
            myPlayerBullets = new List<PlayerBullet>();
            myPlayerAbilityList = new List<PlayerAbility>();
            myPowerups = new List<Other.Powerup>();
            myBlocks = new List<Block>();
            myBloodParticles = new List<Other.BloodParticle>();
            myBoulder = new Other.Boulder(new Vector2(-100, aWindow.ClientBounds.Height - 436));
            myTankBoss = new Enemies.TankBoss(new Vector2(aWindow.ClientBounds.Width + 126, aWindow.ClientBounds.Height / 2 - 116));
            myTankBullets = new List<Enemies.TankBullet>();
            myChopperBoss = new Enemies.ChopperBoss(new Vector2(aWindow.ClientBounds.Width + 126, 10));
            myChopperMissiles = new List<Enemies.ChopperMissile>();
            myChopperBombs = new List<Enemies.ChopperBomb>();
            myMenuButtons = new List<Menu.MenuButtons>();
            myShopButtons = new List<Menu.Shop>();
            mySaveFiles = new List<Menu.SaveFile>();
            myLevelSettings = new List<Menu.LevelSettings>();
            mySaveFilesPath = new List<string>();

            myBlocks.Add(new Block(new Vector2(1280, 260), new Rectangle(1280, 260, 64, 64), 64, 64));
            myBlocks.Add(new Block(new Vector2(1580, 260), new Rectangle(1580, 260, 64, 64), 64, 64));
            myBlocks.Add(new Block(new Vector2(1880, 260), new Rectangle(1880, 260, 64, 64), 64, 64));

            myGameSpeed = 4;
            myScore = (myActiveLevel - 1) * 120;

            myGameSpeedDelay = 30;
            myAddScoreDelay = 1;
            myUpdateSpeed = 1f;

            myRespawnTimeBasicEnemies = 5000;
            myRespawnTimeShootingEnemies = 60000;
            myRespawnTimeSpikeballs = 80000;
            myRespawnTimeYeti = 14000;
            myRespawnTimePowerUps = 1000;
            myRespawnTimeBlock = 0;
            myOpenMenuDelay = 2;
            myResetException = 7;

            mySpawnBoss = false;
            myGameOver = false;
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            mySpriteBatch = new SpriteBatch(GraphicsDevice);

            myPlayer1RunningSprite = Content.Load<Texture2D>("mySprites/playerRunningSpriteSheet");
            myPlayer1JumpingSprite = Content.Load<Texture2D>("mySprites/playerJumpingSpriteSheet");
            myGrenadeSprite = Content.Load<Texture2D>("mySprites/grenade");

            myPlayer2RunningSprite = Content.Load<Texture2D>("mySprites/player2RunningSpriteSheet");
            myPlayer2JumpingSprite = Content.Load<Texture2D>("mySprites/player2JumpingSpriteSheet");
            myPlayer2IdleSprite = Content.Load<Texture2D>("mySprites/player2Idle");
            myPlayer2ShootingSprite = Content.Load<Texture2D>("mySprites/player2ShootingSpriteSheet");
            myPlayer2IdleSlideSprite = Content.Load<Texture2D>("mySprites/player2Slide");
            myPlayer2ShootingSlideSprite = Content.Load<Texture2D>("mySprites/player2SlideShoot");
            myPlayerMissileSprite = Content.Load<Texture2D>("mySprites/player2MissileSpriteSheet");

            myExplosionSprite = Content.Load<Texture2D>("mySprites/explosionSpriteSheet");
            myHealthbarSprite = Content.Load<Texture2D>("mySprites/Healthbar");
            myPowerUpSprite = Content.Load<Texture2D>("mySprites/DiamondSpinning");
            myEnemyAttackingSprite = Content.Load<Texture2D>("mySprites/enemyAttacking");
            myFireballSprite = Content.Load<Texture2D>("mySprites/fireballSpriteSheet");
            myPlayerBulletSprite = Content.Load<Texture2D>("mySprites/playerBullet");
            myEnemyMovementSprite = Content.Load<Texture2D>("mySprites/enemyMovement");
            mySpikeballSprite = Content.Load<Texture2D>("mySprites/Spikeball");
            myBloodParticleSprite = Content.Load<Texture2D>("mySprites/bloodParticle");
            myBoulderSprite = Content.Load<Texture2D>("mySprites/boulderSprite");
            mySandstoneSprite = Content.Load<Texture2D>("mySprites/sandstone");
            myTankBulletSprite = Content.Load<Texture2D>("mySprites/tankBullet");
            myYetiSprite = Content.Load<Texture2D>("mySprites/yeti");
            myLevel01SelectSprite = Content.Load<Texture2D>("mySprites/LevelSelect01");
            myLevel02SelectSprite = Content.Load<Texture2D>("mySprites/LevelSelect02");
            myCogwheelSprite = Content.Load<Texture2D>("mySprites/cogwheel");

            myTankDrivingSprite = Content.Load<Texture2D>("mySprites/tankDriving");
            myTankShootingSprite = Content.Load<Texture2D>("mySprites/tankShooting");
            myTankDeathSprite = Content.Load<Texture2D>("mySprites/tankDeath");

            myChopperFlyingSprite = Content.Load<Texture2D>("mySprites/chopperBossFlying");
            myChopperMissileSprite = Content.Load<Texture2D>("mySprites/chopperBossMissile");
            myChopperBombSprite = Content.Load<Texture2D>("mySprites/chopperBossBomb");

            myBulletTimeUpgradeSprite = Content.Load<Texture2D>("mySprites/bulletTimeUpgrade");
            myHealthUpgradeSprite = Content.Load<Texture2D>("mySprites/healthUpgrade");
            myShieldUpgradeSprite = Content.Load<Texture2D>("mySprites/shieldUpgrade");
            myBulletTimeDurationBarSprite = Content.Load<Texture2D>("mySprites/bulletTimeDurationBar");
            myShieldDurationBarSprite = Content.Load<Texture2D>("mySprites/shieldDurationBar");
            myShieldSprite = Content.Load<Texture2D>("mySprites/shield");

            myGameOverFont = Content.Load<SpriteFont>("myFonts/myGameOverFont");
            myGlobalFont = Content.Load<SpriteFont>("myFonts/myGlobalFont");
            myHealthFont = Content.Load<SpriteFont>("myFonts/myHealthFont");

            myMenuClick = Content.Load<SoundEffect>("mySoundFiles/menuClickSoundEffect");
            myHitMetal = Content.Load<SoundEffect>("mySoundFiles/hitMetalSound");
            myExplosion = Content.Load<SoundEffect>("mySoundFiles/grenadeExplosionSound");
            myMachineGun = Content.Load<SoundEffect>("mySoundFiles/machineGunSound");
            myTankFire = Content.Load<SoundEffect>("mySoundFiles/tankFire");
            myTankIdle = Content.Load<SoundEffect>("mySoundFiles/tankIdle");
            myChopperIdle = Content.Load<SoundEffect>("mySoundFiles/chopperFlyingSound");
            myBossVictoryMusic = Content.Load<SoundEffect>("mySoundFiles/bossVictory");
            myGrenadeUpgradeSound = Content.Load<SoundEffect>("mySoundFiles/grenadeUpgradeSound");
            myMachineGunUpgradeSound = Content.Load<SoundEffect>("mySoundFiles/machineGunUpgradeSound");

            myLevel01Music = Content.Load<Song>("mySoundFiles/level01Music");
            myLevel02Music = Content.Load<Song>("mySoundFiles/level02Music");
            myLevel03Music = Content.Load<Song>("mySoundFiles/level03Music");
            myBossMusic = Content.Load<Song>("mySoundFiles/bossMusic");
            myMenuMusic = Content.Load<Song>("mySoundFiles/mainMenuMusic");

            MediaPlayer.Play(myMenuMusic);
            MediaPlayer.IsRepeating = true;

            LoadSaveFile(@"..\..\..\..\Savefiles\Savefile.txt");

            myCurrentMouseState = Mouse.GetState();
            myPreviousMouseState = myCurrentMouseState;

            myCurrentKeyboardState = Keyboard.GetState();
            myPreviousKeyboardState = myCurrentKeyboardState;
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime aGameTime)
        {
            double tempTimer = aGameTime.ElapsedGameTime.TotalSeconds;
            myGametimeSpeed = (float)tempTimer;
            myOpenMenuDelay -= (float)tempTimer;
            myResetException -= (float)tempTimer;

            myPreviousMouseState = myCurrentMouseState;
            myCurrentMouseState = Mouse.GetState();

            myPreviousKeyboardState = myCurrentKeyboardState;
            myCurrentKeyboardState = Keyboard.GetState();

            if (myGameStateNow != MyGameState.myPausing)
            {
                for (int i = myBlocks.Count; i > 0; i--)
                {
                    myBlocks[i - 1].Update(myRNG, aGameTime);
                }
                myBoulder.Update(aGameTime);
                ScrollingBackgrounds();
            }

            if (myGameStateNow == MyGameState.myPlaying)
            {
                if (mySwitchMusic)
                {
                    if (myActiveLevel == 1)
                    {
                        MediaPlayer.Stop();
                        MediaPlayer.Play(myLevel01Music);
                    }
                    if (myActiveLevel == 2)
                    {
                        MediaPlayer.Stop();
                        MediaPlayer.Play(myLevel02Music);
                    }
                    if (myActiveLevel == 3)
                    {
                        MediaPlayer.Stop();
                        MediaPlayer.Play(myLevel03Music);
                    }
                    mySwitchMusic = false;
                }
                myGameSpeedDelay -= tempTimer * myUpdateSpeed;
                myAddScoreDelay -= tempTimer * myUpdateSpeed;

                if (myGameSpeedDelay <= 0)
                {
                    myGameSpeed += 2;
                    myGameSpeedDelay = 20;
                }

                if (myAddScoreDelay <= 0 && !myGameOver)
                {
                    if (!myTankBoss.AccessIsAlive && !myChopperBoss.AccessIsAlive)
                    {
                        myScore++;
                        myAddScoreDelay = 1;
                    }
                }

                for (int i = myBaseEnemies.Count; i > 0; i--)
                {
                    myBaseEnemies[i - 1].Update(Window, aGameTime);
                    if (!myBaseEnemies[i - 1].AccessIsAlive)
                    {
                        for (int j = 0; j < myRNG.Next(24, 30); j++)
                        {
                            myBloodParticles.Add(new Other.BloodParticle(
                                aGameTime,
                                new Vector2(myBaseEnemies[i - 1].AccessPosition.X + myBaseEnemies[i - 1].AccessSizeX / 2, myBaseEnemies[i - 1].AccessPosition.Y + myBaseEnemies[i - 1].AccessSizeY / 2),
                                new Vector2(myRNG.Next(0, Window.ClientBounds.Width), myRNG.Next(0, Window.ClientBounds.Height)) - myBaseEnemies[i - 1].AccessPosition,
                                myRNG.Next(10, 50), 4, 5));
                        }
                        myBaseEnemies.RemoveAt(i - 1);
                    }
                }
                for (int i = myEnemyBullets.Count; i > 0; i--)
                {
                    myEnemyBullets[i - 1].Update(Window, aGameTime);
                }
                for (int i = myPlayerBullets.Count; i > 0; i--)
                {
                    myPlayerBullets[i - 1].Update(Window, aGameTime);
                }
                for (int i = myPowerups.Count; i > 0; i--)
                {
                    myPowerups[i - 1].Update(myRNG);
                }
                for (int i = myBloodParticles.Count; i > 0; i--)
                {
                    myBloodParticles[i - 1].Update(aGameTime, Window);
                    if (!myBloodParticles[i - 1].AccessIsAlive)
                    {
                        myBloodParticles.RemoveAt(i - 1);
                    }
                }
                for (int i = myPlayerAbilityList.Count; i > 0; i--)
                {
                    myPlayerAbilityList[i - 1].Update(aGameTime, Window);
                    if (!myPlayerAbilityList[i - 1].AccessIsAlive)
                    {
                        myPlayerAbilityList.RemoveAt(i - 1);
                    }
                }
                myPlayer.Update(aGameTime, Window);

                TankBossUpdate(aGameTime);
                ChopperBossUpdate(aGameTime);

                GameOver();
                CreateEnemies(aGameTime);
                CreatePowerup(aGameTime);

                if (myAmountOfHitsOnEnemies >= 800)
                {
                    myCharacter2Unlocked = true;
                }
            }
            else if (myGameStateNow == MyGameState.myMenu)
            {
                if (myMenuButtons.Count == 0)
                {
                    Menu.MenuButtonPlay tempPlayButton = new Menu.MenuButtonPlay();
                    tempPlayButton.Init(new Vector2(100, 130), 256, 64);

                    Menu.MenuButtonSelectLevels tempSelectLevelsButton = new Menu.MenuButtonSelectLevels();
                    tempSelectLevelsButton.Init(new Vector2(100, 230), 256, 64);

                    Menu.ExitButton tempExitButton = new Menu.ExitButton();
                    tempExitButton.Init(new Vector2(100, 330), 256, 64);

                    Menu.SaveButton tempSaveButton = new Menu.SaveButton();
                    tempSaveButton.Init(new Vector2(4, 4), 128, 40);

                    Menu.LoadButton tempLoadButton = new Menu.LoadButton();
                    tempLoadButton.Init(new Vector2(140, 4), 128, 40);

                    Menu.OpenShop tempOpenShopButton = new Menu.OpenShop();
                    tempOpenShopButton.Init(new Vector2(Window.ClientBounds.Width - 260, 12), 256, 64);

                    Menu.SettingsButton tempSettingsButton = new Menu.SettingsButton();
                    tempSettingsButton.Init(new Vector2(Window.ClientBounds.Width - 80, Window.ClientBounds.Height - 80), 64, 64);

                    myMenuButtons.Add(tempPlayButton);
                    myMenuButtons.Add(tempSelectLevelsButton);
                    myMenuButtons.Add(tempExitButton);
                    myMenuButtons.Add(tempSaveButton);
                    myMenuButtons.Add(tempLoadButton);
                    myMenuButtons.Add(tempSettingsButton);
                    myMenuButtons.Add(tempOpenShopButton);
                }
            }
            for (int i = myMenuButtons.Count; i > 0; i--)
            {
                if (myMenuButtons.Count != 0 && myMenuButtons.Count > (i - 1))
                {
                    myMenuButtons[i - 1].Update(Window, aGameTime);
                }
            }
            for (int i = myShopButtons.Count; i > 0; i--)
            {
                if (myShopButtons.Count != 0 && myShopButtons.Count > (i - 1))
                {
                    myShopButtons[i - 1].Update(Window, aGameTime);
                }
            }
            for (int i = myLevelSettings.Count; i > 0; i--)
            {
                if (myLevelSettings.Count != 0 && myLevelSettings.Count > (i - 1))
                {
                    myLevelSettings[i - 1].Update(Window, aGameTime);
                }
            }
            for (int i = mySaveFiles.Count; i > 0; i--)
            {
                if (mySaveFiles.Count != 0 && mySaveFiles.Count > (i - 1))
                {
                    mySaveFiles[i - 1].Update(Window, aGameTime);
                }
            }

            if (myException != null) //Om ett error gavs när man laddar in LevelSaveFile eller normal savefile så ska errormeddelandet finnas på skärmen i 7sek
            {
                myResetException -= (float)tempTimer;
                if (myResetException <= 0)
                {
                    myException = null;
                    myResetException = 7;
                }
            }

            PauseMenu(Window);

            if (myResetGame) //Om spelet behöver en full reset
            {
                Initialize();
                myGameStateNow = MyGameState.myPlaying;
                myResetGame = false;
                mySwitchMusic = true;
            }
            if (myCloseGame)
            {
                Exit();
            }

            base.Update(aGameTime);
        }

        /// <summary>
        /// Tar in en karaktär från tangentbordet med 88ms delay mellan varje tryckning och lägger på karaktären på en specifierad sträng (endast bokstäver)
        /// </summary>
        public static void KeyboardTextInput(GameTime aGameTime, int aLengthLimitToString, ref string aStringToAddCharacters)
        {
            var tempKeys = Keyboard.GetState().GetPressedKeys();
            myDelayAddLetter -= (float)aGameTime.ElapsedGameTime.TotalMilliseconds;
            myDelayRemoveLetter -= (float)aGameTime.ElapsedGameTime.TotalMilliseconds;
            if (aStringToAddCharacters.Length < aLengthLimitToString)
            {
                if (tempKeys.Length > 0 && myDelayAddLetter <= 0 && (tempKeys[0] >= Keys.A && tempKeys[0] <= Keys.Z || tempKeys[0] == Keys.Space))
                {
                    if (tempKeys[0] != Keys.Space)
                    {
                        string tempKeyValue = tempKeys[0].ToString();
                        aStringToAddCharacters += tempKeyValue;
                    }
                    else
                    {
                        aStringToAddCharacters += " ";
                    }
                    myDelayAddLetter = 88;
                }
            }
            if (tempKeys.Length > 0 && tempKeys[0] == Keys.Back && myDelayRemoveLetter <= 0)
            {
                try
                {
                    aStringToAddCharacters = aStringToAddCharacters.Remove(aStringToAddCharacters.Length - 1);
                }
                catch { }
                myDelayRemoveLetter = 80;
            }
        }
        /// <summary>
        /// Tar in en karaktär från tangentbordet med 110ms delay mellan varje tryckning och lägger på karaktären på en specifierad sträng (endast siffor)
        /// </summary>
        public static void KeyboardNumberInput(GameTime aGameTime, int aLengthLimitToString, ref string aStringToAddCharacters)
        {
            var tempKeys = Keyboard.GetState().GetPressedKeys();

            myDelayAddLetter -= (float)aGameTime.ElapsedGameTime.TotalMilliseconds;
            myDelayRemoveLetter -= (float)aGameTime.ElapsedGameTime.TotalMilliseconds;
            if (aStringToAddCharacters.Length < aLengthLimitToString)
            {
                if (tempKeys.Length > 0 && myDelayAddLetter <= 0 && (tempKeys[0] >= Keys.D0 && tempKeys[0] <= Keys.D9 || tempKeys[0] == Keys.Space))
                {
                    if (tempKeys[0] != Keys.Space)
                    {
                        string tempKeyValue = tempKeys[0].ToString().Split('D')[1];
                        aStringToAddCharacters += tempKeyValue;
                    }
                    else
                    {
                        aStringToAddCharacters += " ";
                    }
                    myDelayAddLetter = 110;
                }
            }
            if (tempKeys.Length > 0 && tempKeys[0] == Keys.Back && myDelayRemoveLetter <= 0)
            {
                try
                {
                    aStringToAddCharacters = aStringToAddCharacters.Remove(aStringToAddCharacters.Length - 1);
                }
                catch { }
                myDelayRemoveLetter = 80;
            }
        }
        /// <summary>
        /// Ladda in varje nödvändig variabel från ett valt textdokument i savefoldern
        /// </summary>
        public static void LoadSaveFile(string aFilePath)
        {
            try
            {
                string[] tempLoadSaveFile = File.ReadAllLines(aFilePath);
                for (int i = tempLoadSaveFile.Length; i > 0; i--)
                {
                    if (tempLoadSaveFile[i - 1].Split('=')[0] == "Level02Unlocked")
                    {
                        myLevel02Unlocked = Convert.ToBoolean(Convert.ToInt32(tempLoadSaveFile[i - 1].Split('=')[1]));
                    }
                    if (tempLoadSaveFile[i - 1].Split('=')[0] == "Level03Unlocked")
                    {
                        myLevel03Unlocked = Convert.ToBoolean(Convert.ToInt32(tempLoadSaveFile[i - 1].Split('=')[1]));
                    }
                    if (tempLoadSaveFile[i - 1].Split('=')[0] == "AmountOfHitsOnEnemies")
                    {
                        myAmountOfHitsOnEnemies = Convert.ToInt32(tempLoadSaveFile[i - 1].Split('=')[1]);
                    }
                    if (tempLoadSaveFile[i - 1].Split('=')[0] == "Character2Unlocked")
                    {
                        myCharacter2Unlocked = Convert.ToBoolean(Convert.ToInt32(tempLoadSaveFile[i - 1].Split('=')[1]));
                    }
                    if (tempLoadSaveFile[i - 1].Split('=')[0] == "HighScore")
                    {
                        myHighScore = Convert.ToInt32(tempLoadSaveFile[i - 1].Split('=')[1]);
                    }
                    if (tempLoadSaveFile[i - 1].Split('=')[0] == "BulletTimeUpgradeStatus")
                    {
                        myBulletTimeUpgradeStatus = Convert.ToInt32(tempLoadSaveFile[i - 1].Split('=')[1]);
                    }
                    if (tempLoadSaveFile[i - 1].Split('=')[0] == "HealthUpgradeStatus")
                    {
                        myHealthUpgradeStatus = Convert.ToInt32(tempLoadSaveFile[i - 1].Split('=')[1]);
                    }
                    if (tempLoadSaveFile[i - 1].Split('=')[0] == "ShieldUpgradeStatus")
                    {
                        myShieldUpgradeStatus = Convert.ToInt32(tempLoadSaveFile[i - 1].Split('=')[1]);
                    }
                    if (tempLoadSaveFile[i - 1].Split('=')[0] == "Coins")
                    {
                        myCoins = Convert.ToInt32(tempLoadSaveFile[i - 1].Split('=')[1]);
                    }
                }
            }
            catch (Exception anException)
            {
                myException = anException;
            }
        }
        /// <summary>
        /// Ladda in varje nödvändig variabel för en egen-gjord level 
        /// </summary>
        public static void LoadLevelSaveFile(string aFilePath)
        {
            try
            {
                string[] tempLoadSaveFile = File.ReadAllLines(aFilePath);
                for (int i = tempLoadSaveFile.Length; i > 0; i--)
                {
                    #region BASIC ENEMY
                    if (tempLoadSaveFile[i - 1].Split('=')[0] == "BasicEnemyDamage")
                    {
                        myBasicEnemyDamage = Convert.ToInt32(tempLoadSaveFile[i - 1].Split('=')[1]);
                    }
                    if (tempLoadSaveFile[i - 1].Split('=')[0] == "BasicEnemyHealth")
                    {
                        myBasicEnemyHealth = Convert.ToInt32(tempLoadSaveFile[i - 1].Split('=')[1]);
                    }
                    if (tempLoadSaveFile[i - 1].Split('=')[0] == "BasicEnemySpawnRate")
                    {
                        myDefaultRespawnTimeBasicEnemies = Convert.ToInt32(tempLoadSaveFile[i - 1].Split('=')[1]);
                    }
                    #endregion

                    #region SHOOTING ENEMY
                    if (tempLoadSaveFile[i - 1].Split('=')[0] == "ShootingEnemyDamage")
                    {
                        myShootingEnemyDamage = Convert.ToInt32(tempLoadSaveFile[i - 1].Split('=')[1]);
                    }
                    if (tempLoadSaveFile[i - 1].Split('=')[0] == "ShootingEnemyHealth")
                    {
                        myShootingEnemyHealth = Convert.ToInt32(tempLoadSaveFile[i - 1].Split('=')[1]);
                    }
                    if (tempLoadSaveFile[i - 1].Split('=')[0] == "ShootingEnemySpawnRate")
                    {
                        myDefaultRespawnTimeShootingEnemies = Convert.ToInt32(tempLoadSaveFile[i - 1].Split('=')[1]);
                    }
                    #endregion

                    #region Spikeball ENEMY
                    if (tempLoadSaveFile[i - 1].Split('=')[0] == "SpikeballDamage")
                    {
                        mySpikeballDamage = Convert.ToInt32(tempLoadSaveFile[i - 1].Split('=')[1]);
                    }
                    if (tempLoadSaveFile[i - 1].Split('=')[0] == "SpikeballHealth")
                    {
                        mySpikeballHealth = Convert.ToInt32(tempLoadSaveFile[i - 1].Split('=')[1]);
                    }
                    if (tempLoadSaveFile[i - 1].Split('=')[0] == "SpikeballSpawnRate")
                    {
                        myDefaultRespawnTimeSpikeballs = Convert.ToInt32(tempLoadSaveFile[i - 1].Split('=')[1]);
                    }
                    #endregion

                    #region YETI
                    if (tempLoadSaveFile[i - 1].Split('=')[0] == "YetiDamage")
                    {
                        myYetiDamage = Convert.ToInt32(tempLoadSaveFile[i - 1].Split('=')[1]);
                    }
                    if (tempLoadSaveFile[i - 1].Split('=')[0] == "YetiHealth")
                    {
                        myYetiHealth = Convert.ToInt32(tempLoadSaveFile[i - 1].Split('=')[1]);
                    }
                    if (tempLoadSaveFile[i - 1].Split('=')[0] == "YetiSpawnRate")
                    {
                        myDefaultRespawnTimeYeti = Convert.ToInt32(tempLoadSaveFile[i - 1].Split('=')[1]);
                    }
                    #endregion

                    if (tempLoadSaveFile[i - 1].Split('=')[0] == "BlockSpawnRate")
                    {
                        myDefaultRespawnTimeBlock = Convert.ToInt32(tempLoadSaveFile[i - 1].Split('=')[1]);
                    }

                    if (tempLoadSaveFile[i - 1].Split('=')[0] == "PowerUpSpawnRate")
                    {
                        myDefaultRespawnTimePowerUps = Convert.ToInt32(tempLoadSaveFile[i - 1].Split('=')[1]);
                    }
                }
            }
            catch (Exception anException)
            {
                myException = anException;
            }
        }
        /// <summary>
        /// Spara varje nödvändig variabel i ett nytt eller redan existerande textdokument med samma namn i savefoldern
        /// </summary>
        public static void SaveData(string aFileName)
        {
            string tempPathSaveFolder = @"..\..\..\..\Savefiles\" + aFileName + ".txt";
            string[] tempAllSaveData =
            {
                "Level02Unlocked=" + Convert.ToInt32(myLevel02Unlocked).ToString(),
                "Level03Unlocked=" + Convert.ToInt32(myLevel03Unlocked).ToString(),
                "Character2Unlocked=" + Convert.ToInt32(myCharacter2Unlocked).ToString(),
                "HighScore=" + myHighScore,
                "AmountOfHitsOnEnemies=" + myAmountOfHitsOnEnemies,
                "BulletTimeUpgradeStatus=" + myBulletTimeUpgradeStatus,
                "HealthUpgradeStatus=" + myHealthUpgradeStatus,
                "ShieldUpgradeStatus=" + myShieldUpgradeStatus,
                "Coins=" + myCoins
            };
            File.WriteAllLines(tempPathSaveFolder, tempAllSaveData);
        }
        /// <summary>
        /// Spara varje nödvändig variabel i ett nytt eller redan existerande textdokument med samma namn i levelsavefoldern
        /// </summary>
        public static void SaveLevelSettingsData(string aFileName)
        {
            string tempPathSaveFolder = @"..\..\..\..\LevelFiles\" + aFileName + ".txt";
            string[] tempSaveNewStats = new string[14];

            #region SaveNewLevelSettings
            for (int i = Game.AccessLevelSettings.Count; i > 0; i--)
            {
                if (Game.AccessLevelSettings[i - 1].GetType().ToString() == "myShootEmUp.Menu.BasicEnemyStats")
                {
                    tempSaveNewStats[0] = Game.AccessLevelSettings[i - 1].AccessHealthStat;
                    tempSaveNewStats[1] = Game.AccessLevelSettings[i - 1].AccessDamageStat;
                    tempSaveNewStats[2] = Game.AccessLevelSettings[i - 1].AccessSpawnRateStat;
                }
                if (Game.AccessLevelSettings[i - 1].GetType().ToString() == "myShootEmUp.Menu.ShootingEnemyStats")
                {
                    tempSaveNewStats[3] = Game.AccessLevelSettings[i - 1].AccessHealthStat;
                    tempSaveNewStats[4] = Game.AccessLevelSettings[i - 1].AccessDamageStat;
                    tempSaveNewStats[5] = Game.AccessLevelSettings[i - 1].AccessSpawnRateStat;
                }
                if (Game.AccessLevelSettings[i - 1].GetType().ToString() == "myShootEmUp.Menu.SpikeballEnemyStats")
                {
                    tempSaveNewStats[6] = Game.AccessLevelSettings[i - 1].AccessHealthStat;
                    tempSaveNewStats[7] = Game.AccessLevelSettings[i - 1].AccessDamageStat;
                    tempSaveNewStats[8] = Game.AccessLevelSettings[i - 1].AccessSpawnRateStat;
                }
                if (Game.AccessLevelSettings[i - 1].GetType().ToString() == "myShootEmUp.Menu.YetiEnemyStats")
                {
                    tempSaveNewStats[9] = Game.AccessLevelSettings[i - 1].AccessHealthStat;
                    tempSaveNewStats[10] = Game.AccessLevelSettings[i - 1].AccessDamageStat;
                    tempSaveNewStats[11] = Game.AccessLevelSettings[i - 1].AccessSpawnRateStat;
                }
                if (Game.AccessLevelSettings[i - 1].GetType().ToString() == "myShootEmUp.Menu.BlockSpawnRate")
                {
                    tempSaveNewStats[12] = Game.AccessLevelSettings[i - 1].AccessSpawnRateStat;
                }
                if (Game.AccessLevelSettings[i - 1].GetType().ToString() == "myShootEmUp.Menu.PowerupSpawnRate")
                {
                    tempSaveNewStats[13] = Game.AccessLevelSettings[i - 1].AccessSpawnRateStat;
                }
            }
            #endregion

            string[] tempAllSaveData =
            {
                "[BASIC ENEMY]",
                "BasicEnemyHealth=" + tempSaveNewStats[0],
                "BasicEnemyDamage=" + tempSaveNewStats[1],
                "BasicEnemySpawnRate=" + tempSaveNewStats[2],
                "",
                "[SHOOTING ENEMY]",
                "ShootingEnemyHealth=" + tempSaveNewStats[3],
                "ShootingEnemyDamage=" + tempSaveNewStats[4],
                "ShootingEnemySpawnRate=" + tempSaveNewStats[5],
                "",
                "[Spikeball ENEMY]",
                "SpikeballHealth=" + tempSaveNewStats[6],
                "SpikeballDamage=" + tempSaveNewStats[7],
                "SpikeballSpawnRate=" + tempSaveNewStats[8],
                "",
                "[YETI ENEMY]",
                "YetiHealth=" + tempSaveNewStats[9],
                "YetiDamage=" + tempSaveNewStats[10],
                "YetiSpawnRate=" + tempSaveNewStats[11],
                "",
                "[BLOCKS]",
                "BlockSpawnRate=" + tempSaveNewStats[12],
                "",
                "[POWER UP]",
                "PowerUpSpawnRate=" + tempSaveNewStats[13],
            };
            File.WriteAllLines(tempPathSaveFolder, tempAllSaveData);
        }
        /// <summary>
        /// Används för att se till att man endast skriver i en text-ruta i level-editorn
        /// </summary>
        public static void ConfirmLevelSettingChange()
        {
            for (int i = myLevelSettings.Count; i > 0; i--)
            {
                if (myLevelSettings[i - 1].AccessIsEditingSpawnRateStat || myLevelSettings[i - 1].AccessIsEditingHealthStat || myLevelSettings[i - 1].AccessIsEditingDamageStat)
                {
                    for (int j = myLevelSettings.Count; j > 0; j--)
                    {
                        myLevelSettings[j - 1].AccessIsEditingSpawnRateStat = false;
                        myLevelSettings[j - 1].AccessIsEditingDamageStat = false;
                        myLevelSettings[j - 1].AccessIsEditingHealthStat = false;
                    }
                }
                if (myLevelSettings[i - 1].AccessSpawnRateStat == "")
                {
                    myLevelSettings[i - 1].AccessSpawnRateStat = "0";
                }
                if (myLevelSettings[i - 1].AccessHealthStat == "")
                {
                    myLevelSettings[i - 1].AccessHealthStat = "0";
                }
                if (myLevelSettings[i - 1].AccessDamageStat == "")
                {
                    myLevelSettings[i - 1].AccessDamageStat = "0";
                }
            }
        }

        private void TankBossUpdate(GameTime aGameTime)
        {
            if (myScore >= 120 && !mySpawnBoss && myActiveLevel == 1 || Keyboard.GetState().IsKeyDown(Keys.I))
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(myBossMusic);
                myTankBoss.AccessIsAlive = true;
                mySpawnBoss = true;
                myBlocks.Clear();
            }
            if (myTankBoss.AccessIsAlive)
            {
                myTankBoss.Update(aGameTime);
                for (int i = myTankBullets.Count; i > 0; i--)
                {
                    myTankBullets[i - 1].Update(aGameTime, Window);
                    if (!myTankBullets[i - 1].AccessIsAlive)
                    {
                        myTankBullets.RemoveAt(i - 1);
                    }
                }
                if (myGameSpeed > 4)
                {
                    myGameSpeed--;
                }
            }
            else if (myActiveLevel == 1 && mySpawnBoss)
            {
                myBlocks.Add(new Block(new Vector2(1280, 260), new Rectangle(1280, 260, 64, 64), 64, 64));
                myBlocks.Add(new Block(new Vector2(1580, 260), new Rectangle(1580, 260, 64, 64), 64, 64));
                myBlocks.Add(new Block(new Vector2(1880, 260), new Rectangle(1880, 260, 64, 64), 64, 64));
            }
        }
        private void ChopperBossUpdate(GameTime aGameTime)
        {
            if (myScore >= 240 && !mySpawnBoss && myActiveLevel == 2 || Keyboard.GetState().IsKeyDown(Keys.P))
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(myBossMusic);
                myChopperBoss.AccessIsAlive = true;
                mySpawnBoss = true;
            }
            if (myChopperBoss.AccessIsAlive)
            {
                myChopperBoss.Update(aGameTime, Window);
                for (int i = myChopperMissiles.Count; i > 0; i--)
                {
                    myChopperMissiles[i - 1].Update(Window, aGameTime);
                    if (!myChopperMissiles[i - 1].AccessIsAlive)
                    {
                        myChopperMissiles.RemoveAt(i - 1);
                    }
                }
                for (int i = myChopperBombs.Count; i > 0; i--)
                {
                    myChopperBombs[i - 1].Update(Window, aGameTime);
                    if (!myChopperBombs[i - 1].AccessIsAlive)
                    {
                        myChopperBombs.RemoveAt(i - 1);
                    }
                }
                if (myGameSpeed > 4)
                {
                    myGameSpeed--;
                }
            }
        }
        private void CreateEnemies(GameTime aGameTime)
        {
            if (!myTankBoss.AccessIsAlive)
            {
                if (myActiveLevel == 1 || myActiveLevel == 3)
                {
                    myRespawnTimeBasicEnemies -= (float)aGameTime.ElapsedGameTime.Milliseconds * myUpdateSpeed;
                    myRespawnTimeShootingEnemies -= (float)aGameTime.ElapsedGameTime.Milliseconds * myUpdateSpeed;
                    myRespawnTimeSpikeballs -= (float)aGameTime.ElapsedGameTime.Milliseconds * myUpdateSpeed;
                    if (myRespawnTimeBasicEnemies <= 0)
                    {
                        BasicEnemy tempBasicEnemy = new BasicEnemy();
                        tempBasicEnemy.Init(new Vector2(Window.ClientBounds.Width + 64, myRNG.Next(0, Window.ClientBounds.Height)), myBasicEnemyHealth, myBasicEnemyDamage, 300, 64, 64);
                        myBaseEnemies.Add(tempBasicEnemy);
                        if (myDefaultRespawnTimeBasicEnemies - ((int)Game.AccessGameSpeed) * 250 > 0)
                        {
                            myRespawnTimeBasicEnemies = myDefaultRespawnTimeBasicEnemies - ((int)Game.AccessGameSpeed) * 250;
                        }
                        else
                        {
                            myRespawnTimeBasicEnemies = myDefaultRespawnTimeBasicEnemies / 6;
                        }
                    }
                    if (myRespawnTimeShootingEnemies <= 0)
                    {
                        ShootingEnemy tempShootingEnemy = new ShootingEnemy();
                        tempShootingEnemy.Init(new Vector2(1360, 260), myShootingEnemyHealth, myShootingEnemyDamage, 80, 64, 64);
                        myBaseEnemies.Add(tempShootingEnemy);
                        if (myDefaultRespawnTimeShootingEnemies - ((int)Game.AccessGameSpeed) * 50 > 0)
                        {
                            myRespawnTimeShootingEnemies = myDefaultRespawnTimeShootingEnemies - ((int)Game.AccessGameSpeed) * 50;
                        }
                        else
                        {
                            myRespawnTimeShootingEnemies = myDefaultRespawnTimeShootingEnemies / 4;
                        }
                    }
                    if (myRespawnTimeSpikeballs <= 0)
                    {
                        Spikeball tempSpikeball = new Spikeball();
                        tempSpikeball.Init(new Vector2(1360, Window.ClientBounds.Height - 300), mySpikeballHealth, mySpikeballDamage, 4, 64, 64);
                        myBaseEnemies.Add(tempSpikeball);
                        if (myDefaultRespawnTimeSpikeballs - ((int)Game.AccessGameSpeed) * 150 > 0)
                        {
                            myRespawnTimeSpikeballs = myDefaultRespawnTimeSpikeballs - ((int)Game.AccessGameSpeed) * 150;
                        }
                        else
                        {
                            myRespawnTimeSpikeballs = myDefaultRespawnTimeSpikeballs / 4;
                        }
                    }
                }
                if (myActiveLevel == 2 || myActiveLevel == 3)
                {
                    myRespawnTimeYeti -= (float)aGameTime.ElapsedGameTime.TotalMilliseconds * myUpdateSpeed;
                    if (myRespawnTimeYeti <= 0)
                    {
                        Yeti tempYeti = new Yeti();
                        tempYeti.Init(new Vector2(myRNG.Next(1300, 1520 + (int)Game.AccessGameSpeed) * 2, 244), myYetiHealth, myYetiDamage, 4, 64, 64);
                        myBaseEnemies.Add(tempYeti);
                        if (myActiveLevel == 2)
                        {
                            if (myDefaultRespawnTimeYeti - ((int)Game.AccessGameSpeed) * 250 > 0)
                            {
                                myRespawnTimeYeti = myDefaultRespawnTimeYeti - ((int)Game.AccessGameSpeed) * 250;
                            }
                            else
                            {
                                myRespawnTimeYeti = myDefaultRespawnTimeYeti / 4;
                            }
                        }
                        else
                        {
                            if (myDefaultRespawnTimeYeti - ((int)Game.AccessGameSpeed) * 250 > 0)
                            {
                                myRespawnTimeYeti = (myDefaultRespawnTimeYeti * 3) - ((int)Game.AccessGameSpeed) * 250;
                            }
                            else
                            {
                                myRespawnTimeYeti = myDefaultRespawnTimeYeti / 4;
                            }
                        }
                    }
                }
            }
        }
        private void CreatePowerup(GameTime aGameTime)
        {
            if (!myTankBoss.AccessIsAlive && !myChopperBoss.AccessIsAlive)
            {
                myRespawnTimePowerUps -= (float)aGameTime.ElapsedGameTime.TotalMilliseconds;
                if (myRespawnTimePowerUps <= 0)
                {
                    myPowerups.Add(new Other.Powerup(myPowerUpSprite, new Vector2(Window.ClientBounds.Width + 32, myRNG.Next(150, Window.ClientBounds.Height - 300)), 5));
                    myRespawnTimePowerUps = myDefaultRespawnTimePowerUps;
                }
            }
        }
        private void ScrollingBackgrounds()
        {
            myScrollingBackgrounds[0].Update();
            myScrollingBackgrounds[1].Update();
            if (Keyboard.GetState().IsKeyDown(Keys.Y))
            {
                Game.AccessChopperBoss.AccessBossHealth -= 100;
                Game.AccessTankBoss.AccessBossHealth -= 100;
            }
            if (myActiveLevel == 1 || myActiveLevel == 0)
            {
                if (myScrollingBackgrounds[0].AccessPosition.X <= -1588)
                {
                    myScrollingBackgrounds[0].AccessPosition = new Vector2(0, 0);
                }

                myScrollingBackgrounds[1].AccessPosition = new Vector2(myScrollingBackgrounds[0].AccessPosition.X + myScrollingBackgrounds[0].AccessRectangle.Width, 0);
            }
            else
            {
                myScrollingBackgrounds[2].Update();
                myScrollingBackgrounds[3].Update();
                myScrollingBackgrounds[4].Update();

                if (myActiveLevel == 2)
                {
                    if (myScrollingBackgrounds[4].AccessPosition.X + myScrollingBackgrounds[4].AccessRectangle.Width > 0)
                    {
                        myScrollingBackgrounds[4].AccessPosition = new Vector2(myScrollingBackgrounds[1].AccessPosition.X + myScrollingBackgrounds[1].AccessRectangle.Width, 0);
                        myScrollingBackgrounds[2].AccessPosition = new Vector2(myScrollingBackgrounds[4].AccessPosition.X + myScrollingBackgrounds[4].AccessRectangle.Width, 0);
                    }
                    else
                    {
                        if (myScrollingBackgrounds[2].AccessPosition.X <= -1588)
                        {
                            myScrollingBackgrounds[2].AccessPosition = new Vector2(0, 0);
                        }

                        myScrollingBackgrounds[3].AccessPosition = new Vector2(myScrollingBackgrounds[2].AccessPosition.X + myScrollingBackgrounds[2].AccessRectangle.Width, 0);
                    }
                }
                else
                {
                    myScrollingBackgrounds[5].Update();
                    myScrollingBackgrounds[6].Update();
                    myScrollingBackgrounds[7].Update();

                    if (myScrollingBackgrounds[7].AccessPosition.X + myScrollingBackgrounds[7].AccessRectangle.Width > 0)
                    {
                        myScrollingBackgrounds[7].AccessPosition = new Vector2(myScrollingBackgrounds[2].AccessPosition.X + myScrollingBackgrounds[2].AccessRectangle.Width, 0);
                        myScrollingBackgrounds[5].AccessPosition = new Vector2(myScrollingBackgrounds[7].AccessPosition.X + myScrollingBackgrounds[7].AccessRectangle.Width, 0);
                    }
                    else
                    {
                        if (myScrollingBackgrounds[5].AccessPosition.X <= -1588)
                        {
                            myScrollingBackgrounds[5].AccessPosition = new Vector2(0, 0);
                        }

                        myScrollingBackgrounds[6].AccessPosition = new Vector2(myScrollingBackgrounds[5].AccessPosition.X + myScrollingBackgrounds[5].AccessRectangle.Width, 0);
                    }
                }
            }
        }
        private void GameOver()
        {
            if (myPlayer.AccessHealth <= 0)
            {
                myGameOver = true;
                if (myScore > myHighScore)
                {
                    myHighScore = myScore;
                }
                if (myAddCoins > 0)
                {
                    myCoins += myAddCoins;
                    myAddCoins = 0;
                }
                if (Path.GetFileName(myCurrentFileLoaded) != null)
                {
                    SaveData(Path.GetFileName(myCurrentFileLoaded).Split('.')[0]);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    Initialize();
                }
            }
        }
        private static void PauseMenu(GameWindow aWindow)
        {
            if (myGameStateNow != MyGameState.myMenu)
            {
                if (myPreviousKeyboardState.IsKeyUp(Keys.Escape) && myCurrentKeyboardState.IsKeyDown(Keys.Escape) && myGameStateNow == MyGameState.myPausing)
                {
                    myMenuButtons.Clear();
                    myGameStateNow = MyGameState.myPlaying;
                }
                else if (myPreviousKeyboardState.IsKeyUp(Keys.Escape) && myCurrentKeyboardState.IsKeyDown(Keys.Escape) && myGameStateNow != MyGameState.myPausing)
                {
                    Menu.MenuButtonPlay tempPlayButton = new Menu.MenuButtonPlay();
                    tempPlayButton.Init(new Vector2(100, 150), 256, 64);

                    Menu.ExitButton tempExitButton = new Menu.ExitButton();
                    tempExitButton.Init(new Vector2(100, 250), 256, 64);

                    Menu.ReturnButton tempReturnButton = new Menu.ReturnButton();
                    tempReturnButton.Init(new Vector2(4, 466), 128, 40);

                    myMenuButtons.Add(tempPlayButton);
                    myMenuButtons.Add(tempExitButton);
                    myMenuButtons.Add(tempReturnButton);

                    myGameStateNow = MyGameState.myPausing;
                }
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime aGameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            mySpriteBatch.Begin();

            base.Draw(aGameTime);

            if (myActiveLevel == 3)
            {
                myScrollingBackgrounds[5].Draw(mySpriteBatch);
                myScrollingBackgrounds[6].Draw(mySpriteBatch);
                myScrollingBackgrounds[7].Draw(mySpriteBatch);
            }
            if (myActiveLevel == 2 || myActiveLevel == 3) //Så snö-bakgrunderna ritas ut när level 2 börjar
            {
                myScrollingBackgrounds[2].Draw(mySpriteBatch);
                myScrollingBackgrounds[3].Draw(mySpriteBatch);
                myScrollingBackgrounds[4].Draw(mySpriteBatch);
            }

            myScrollingBackgrounds[0].Draw(mySpriteBatch);
            myScrollingBackgrounds[1].Draw(mySpriteBatch);

            myBoulder.Draw(mySpriteBatch, Window);

            foreach (Block block in myBlocks)
            {
                block.Draw(mySpriteBatch);
            }

            if (myGameStateNow == MyGameState.myPlaying || myGameStateNow == MyGameState.myPausing)
            {
                if (myTankBoss.AccessIsAlive)
                {
                    myTankBoss.Draw(mySpriteBatch, myRNG);
                }
                for (int i = myTankBullets.Count; i > 0; i--)
                {
                    myTankBullets[i - 1].Draw(mySpriteBatch);
                }

                for (int i = myChopperBombs.Count; i > 0; i--)
                {
                    myChopperBombs[i - 1].Draw(mySpriteBatch);
                }
                if (myChopperBoss.AccessIsAlive)
                {
                    myChopperBoss.Draw(mySpriteBatch, myRNG);
                }
                for (int i = myChopperMissiles.Count; i > 0; i--)
                {
                    myChopperMissiles[i - 1].Draw(mySpriteBatch);
                }

                for (int i = myEnemyBullets.Count; i > 0; i--)
                {
                    myEnemyBullets[i - 1].Draw(mySpriteBatch);
                }
                for (int i = myPlayerBullets.Count; i > 0; i--)
                {
                    myPlayerBullets[i - 1].Draw(mySpriteBatch);
                }
                for (int i = myPowerups.Count; i > 0; i--)
                {
                    myPowerups[i - 1].Draw(mySpriteBatch);
                }
                myPlayer.Draw(mySpriteBatch, Window);

                for (int i = myBaseEnemies.Count; i > 0; i--)
                {
                    myBaseEnemies[i - 1].Draw(mySpriteBatch);
                }
                for (int i = myBloodParticles.Count; i > 0; i--)
                {
                    myBloodParticles[i - 1].Draw(mySpriteBatch);
                }
                for (int i = myPlayerAbilityList.Count; i > 0; i--)
                {
                    myPlayerAbilityList[i - 1].Draw(mySpriteBatch);
                }
                mySpriteBatch.DrawString(myGlobalFont, "Score: " + myScore, new Vector2(10, 0), Color.OrangeRed);
            }
            else if (myGameStateNow == MyGameState.myMenu)
            {
                IsMouseVisible = true;

                if (myException != null)
                {
                    if (myException is FormatException || myException is ArgumentOutOfRangeException)
                    {
                        mySpriteBatch.DrawString(myGlobalFont, "Failed to Load SaveFile", new Vector2(16, Window.ClientBounds.Height - 40), Color.Red);
                    }
                }

                for (int i = mySaveFiles.Count; i > 0; i--)
                {
                    mySaveFiles[i - 1].Draw(mySpriteBatch);
                }
                for (int i = myLevelSettings.Count; i > 0; i--)
                {
                    myLevelSettings[i - 1].Draw(mySpriteBatch);
                }
                for (int i = myShopButtons.Count; i > 0; i--)
                {
                    myShopButtons[i - 1].Draw(mySpriteBatch);
                }
            }
            if (myGameStateNow == MyGameState.myPausing)
            {
                IsMouseVisible = true;
                mySpriteBatch.DrawString(myGlobalFont, "PAUSED", new Vector2(170, 95), Color.OrangeRed);
            }
            for (int i = myMenuButtons.Count; i > 0; i--)
            {
                myMenuButtons[i - 1].Draw(mySpriteBatch);
            }

            mySpriteBatch.End();
        }
    }
}
