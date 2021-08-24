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
    public abstract class Player
    {
        protected Vector2
            myPosition,
            myVelocity;
        protected int
            mySizeX,
            mySizeY,
            mySpeed,
            myHealth,
            myMaxHealth,
            myBulletTimeDuration,
            myShieldDuration;
        protected double mySpeedX;
        protected bool
            myPlayerIsJumping,
            myPlayerIsCrouching,
            myPlayerAttackedByYeti,
            myIsAlive,
            myActivateShield,
            myRechargeShield;
        protected float
            myFireRateDelay,
            myDecreaseFireRate,
            myPowerupDrawTextGun,
            myPowerupDrawTextAbility,
            myGrenadeThrowDelay,
            myDecreaseAbilityUseDelay,
            myDecreaseAbilityRechargeRate,
            myAmountOfAbilities,
            myMaxAmountOfAbility,
            myAddAbility,
            myAddDamageToBullet,
            mySaveGameSpeed,
            mySaveUpdateSpeed,
            myPlayerUpdateSpeed,
            myRechargeBulletTimeDelay,
            myRechargeShieldDelay;
        protected int[] myMovementStop;

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
        public int AccessHealth
        {
            get => myHealth;
            set => myHealth = value;
        }
        public int[] AccessMovementStop
        {
            get => myMovementStop;
            set => myMovementStop = value;
        }

        public abstract void Init(Vector2 aPosition, int aSpeed);

        public abstract void Update(GameTime aGameTime, GameWindow aWindow);

        public abstract void UpgradePlayer(Random aRNG);
        public void OverallCollisionChecks(GameTime aGameTime)
        {
            foreach (BaseEnemy enemy in Game.AccessBaseEnemies)
            {
                if (HitBox.Calculate(mySizeX, mySizeY, enemy.AccessSizeX, enemy.AccessSizeY, myPosition, enemy.AccessPosition)) //Lämplig kollision för fiende eftersom fiende sprite är konstig med rektangel
                {
                    enemy.AccessIsAlive = false;
                    if (myShieldDuration == 0 || myShieldDuration == 360 || myRechargeShield)
                    {
                        myHealth -= enemy.AccessEnemyDamage;
                    }
                    if (Game.AccessShieldUpgradeStatus > 0)
                    {
                        myActivateShield = true;
                    }
                }
                if (enemy.AccessAttackFrame)
                {
                    if (HitBox.Calculate(mySizeX, mySizeY, enemy.AccessSizeX + 48, enemy.AccessSizeY + 78, myPosition, new Vector2(enemy.AccessPosition.X - 64, enemy.AccessPosition.Y - 50)))
                    {
                        if (!myPlayerAttackedByYeti)
                        {
                            myPosition.Y -= 350f * (float)aGameTime.ElapsedGameTime.TotalSeconds;
                            myVelocity.Y = -350f * (float)aGameTime.ElapsedGameTime.TotalSeconds;
                            myVelocity.X = -500f * (float)aGameTime.ElapsedGameTime.TotalSeconds;
                            if (myShieldDuration > 0 && myShieldDuration < 360)
                            {
                                myHealth -= enemy.AccessEnemyDamage;
                            }
                            myPlayerAttackedByYeti = true;
                            if (Game.AccessShieldUpgradeStatus > 0)
                            {
                                myActivateShield = true;
                            }
                        }
                    }
                    else
                    {
                        myPlayerAttackedByYeti = false;
                    }
                }
            }
            for (int i = Game.AccessEnemyBullets.Count; i > 0; i--)
            {
                if (HitBox.Calculate(mySizeX, mySizeY, Game.AccessEnemyBullets[i - 1].AccessSizeX, Game.AccessEnemyBullets[i - 1].AccessSizeY, myPosition, Game.AccessEnemyBullets[i - 1].AccessPosition))
                {
                    Game.AccessEnemyBullets.RemoveAt(i - 1);
                    if (myShieldDuration == 0 || myShieldDuration == 360 || myRechargeShield)
                    {
                        myHealth -= 10;
                    }
                    if (Game.AccessShieldUpgradeStatus > 0)
                    {
                        myActivateShield = true;
                    }
                }
            }
            foreach (Enemies.TankBullet bossBullet in Game.AccessTankBullets)
            {
                if (!bossBullet.AccessCollisionDetected)
                {
                    if (HitBox.Calculate(mySizeX, mySizeY, bossBullet.AccessSizeX - 14, bossBullet.AccessSizeY - 12, myPosition, new Vector2(bossBullet.AccessPosition.X - 8, bossBullet.AccessPosition.Y - 16)))
                    {
                        if (myShieldDuration == 0 || myShieldDuration == 360 || myRechargeShield)
                        {
                            myHealth -= 10;
                        }
                        bossBullet.AccessCollisionDetected = true;
                        if (Game.AccessShieldUpgradeStatus > 0)
                        {
                            myActivateShield = true;
                        }
                    }
                }
            }
            foreach (Enemies.ChopperMissile chopperMissile in Game.AccessChopperMissiles)
            {
                if (!chopperMissile.AccessCollisionDetected)
                {
                    if (HitBox.Calculate(mySizeX, mySizeY, chopperMissile.AccessSizeX - 30, chopperMissile.AccessSizeY - 12, myPosition, new Vector2(chopperMissile.AccessPosition.X, chopperMissile.AccessPosition.Y - 6)))
                    {
                        if (myShieldDuration == 0 || myShieldDuration == 360 || myRechargeShield)
                        {
                            myHealth -= 10;
                        }
                        chopperMissile.AccessCollisionDetected = true;
                        if (Game.AccessShieldUpgradeStatus > 0)
                        {
                            myActivateShield = true;
                        }
                    }
                }
            }
            foreach (Enemies.ChopperBomb chopperBomb in Game.AccessChopperBombs)
            {
                if (!chopperBomb.AccessCollisionDetected)
                {
                    if (HitBox.Calculate(mySizeX, mySizeY, chopperBomb.AccessSizeX, chopperBomb.AccessSizeY, myPosition, chopperBomb.AccessPosition))
                    {
                        if (myShieldDuration == 0 || myShieldDuration == 360 || myRechargeShield)
                        {
                            myHealth -= 10;
                        }
                        chopperBomb.AccessCollisionDetected = true;
                        if (Game.AccessShieldUpgradeStatus > 0)
                        {
                            myActivateShield = true;
                        }
                    }
                }
            }
            foreach (Rectangle collisionBox in Game.AccessBoulder.AccessRectangles)
            {
                if (new Rectangle((int)myPosition.X, (int)myPosition.Y, mySizeX, mySizeY).Intersects(collisionBox))
                {
                    myPosition.X += 400f * (float)aGameTime.ElapsedGameTime.TotalSeconds; //Putta ut spelaren höger
                    if (myMovementStop[0] == 1)
                    {
                        myHealth--; //Om spelaren puttas av ett block och bouldern samtidigt, fastnar spelaren och tar skada för varje frame spelaren är fast
                    }
                }
            }
            for (int i = 0; i < 4; i++)
            {
                if (Game.AccessBlocks.All(Block => Block.AccessCollisionCases[i] == 0))
                {
                    myMovementStop[i] = 0; //Om en specifik kollision inte sker mellan alla block sätt tillbaka värdet
                }
            }
        }
        public void SlowMotion(GameTime aGametime)
        {
            if (Game.AccessBulletTimeUpgradeStatus > 0 && myBulletTimeDuration > 0)
            {
                if (Game.AccessPreviousKeyboardState.IsKeyUp(Keys.RightShift) && Game.AccessCurrentKeyboardState.IsKeyDown(Keys.RightShift))
                {
                    myRechargeBulletTimeDelay = 2000;
                    mySaveUpdateSpeed = Game.AccessTrueUpdateSpeed;
                    mySaveGameSpeed = Game.AccessTrueGameSpeed;
                    Game.AccessTrueUpdateSpeed = 0.5f;
                    Game.AccessGameSpeed *= 0.5f;

                    if (Game.AccessBulletTimeUpgradeStatus == 3)
                    {
                        myPlayerUpdateSpeed = 1f;
                    }
                    else
                    {
                        myPlayerUpdateSpeed = 0.5f;
                    }
                }

                if (Game.AccessPreviousKeyboardState.IsKeyDown(Keys.RightShift) && Game.AccessCurrentKeyboardState.IsKeyUp(Keys.RightShift))
                {
                    Game.AccessTrueUpdateSpeed = mySaveUpdateSpeed;
                    Game.AccessTrueGameSpeed = mySaveGameSpeed;
                    myPlayerUpdateSpeed = mySaveUpdateSpeed;
                }
            }
            else if (myBulletTimeDuration < 0)
            {
                Game.AccessTrueUpdateSpeed = mySaveUpdateSpeed;
                Game.AccessTrueGameSpeed = mySaveGameSpeed;
                myPlayerUpdateSpeed = mySaveUpdateSpeed;
            }
        }
        public void UsingBulletTimeOrShield(GameTime aGameTime)
        {
            if (Game.AccessCurrentKeyboardState.IsKeyDown(Keys.RightShift))
            {
                if (Game.AccessBulletTimeUpgradeStatus < 2)
                {
                    myBulletTimeDuration -= 2;
                }
                else
                {
                    myBulletTimeDuration--;
                }
            }
            else
            {
                myRechargeBulletTimeDelay -= (float)aGameTime.ElapsedGameTime.TotalMilliseconds;

                if (myRechargeBulletTimeDelay <= 0 && myBulletTimeDuration <= 360)
                {
                    myBulletTimeDuration += 2;
                }
            }

            if (myActivateShield && myShieldDuration > 0 && !myRechargeShield)
            {
                if (Game.AccessShieldUpgradeStatus == 3)
                {
                    myShieldDuration--;
                }
                else
                {
                    myShieldDuration -= 2;
                }
            }
            else if (myShieldDuration < 360)
            {
                if (Game.AccessShieldUpgradeStatus > 1)
                {
                    myRechargeShieldDelay -= (float)aGameTime.ElapsedGameTime.TotalMilliseconds * 2f;
                }
                else
                {
                    myRechargeShieldDelay -= (float)aGameTime.ElapsedGameTime.TotalMilliseconds;
                }
                myRechargeShield = true;

                if (myRechargeShieldDelay <= 0)
                {
                    myShieldDuration++;
                }
                if (myShieldDuration == 360)
                {
                    myActivateShield = false;
                    myRechargeShield = false;
                    myRechargeShieldDelay = 6000;
                }
            }
        }
        public void DrawAbilityBars(SpriteBatch aSpriteBatch, GameWindow aWindow)
        {
            if (Game.AccessBulletTimeUpgradeStatus > 0)
            {
                aSpriteBatch.Draw(Game.AccessSandstoneSprite, new Rectangle(aWindow.ClientBounds.Width - 400, aWindow.ClientBounds.Height - 80, 380, 24), Color.White);
                aSpriteBatch.Draw(Game.AccessBulletTimeDurationBarSprite, new Rectangle(aWindow.ClientBounds.Width - 390, aWindow.ClientBounds.Height - 79, myBulletTimeDuration, 22), Color.White);
            }

            if (Game.AccessShieldUpgradeStatus > 0)
            {
                aSpriteBatch.Draw(Game.AccessSandstoneSprite, new Rectangle(aWindow.ClientBounds.Width - 400, aWindow.ClientBounds.Height - 40, 380, 24), Color.White);
                aSpriteBatch.Draw(Game.AccessShieldDurationBarSprite, new Rectangle(aWindow.ClientBounds.Width - 390, aWindow.ClientBounds.Height - 39, myShieldDuration, 22), Color.White);
            }
        }

        public abstract void Draw(SpriteBatch aSpriteBatch, GameWindow aWindow);
    }

    public class Player1 : Player
    {
        Other.Animation myRunningAnimation;

        public override void Init(Vector2 aPosition, int aHealth) //Ge playern dens värden 
        {
            myPosition = aPosition;
            myHealth = aHealth;
            myMaxHealth = aHealth;
            mySizeX = 64;
            mySizeY = 64;
            myMovementStop = new int[4];
            myRunningAnimation = new Other.Animation(Game.AccessPlayer1RunningSprite, new Vector2(249, 150), new Vector2(0, 0), new Vector2(2, 3), 15);
            myMaxAmountOfAbility = 6;
            myFireRateDelay = 240;
            myGrenadeThrowDelay = 250;
            myAmountOfAbilities = 6;
            myAddAbility = 1200;
            myPlayerUpdateSpeed = 1f;
            myShieldDuration = 360;
            myBulletTimeDuration = 360;
            myRechargeShieldDelay = 6000;
            mySaveGameSpeed = Game.AccessGameSpeed;
            mySaveUpdateSpeed = Game.AccessUpdateSpeed;
        }

        public override void Update(GameTime aGameTime, GameWindow aWindow)
        {
            KeyboardState tempKeyboardState = Keyboard.GetState();
            if (myHealth > 0)
            {
                tempKeyboardState = Movement(tempKeyboardState, aWindow, aGameTime); //Rörelse

                Shooting(aGameTime, tempKeyboardState); //Spelarens skjutning

                Collisions(aGameTime); //Kollision för fienderna och mer

                Jumping();

                SlowMotion(aGameTime);

                UsingBulletTimeOrShield(aGameTime);

                myPosition += myVelocity * myPlayerUpdateSpeed;

                if (myPowerupDrawTextGun > 0)
                {
                    myPowerupDrawTextGun -= (float)aGameTime.ElapsedGameTime.TotalSeconds;
                }
                if (myPowerupDrawTextAbility > 0)
                {
                    myPowerupDrawTextAbility -= (float)aGameTime.ElapsedGameTime.TotalSeconds;
                }

                if (tempKeyboardState.IsKeyDown(Keys.U))
                {
                    myHealth = 10000;
                }
            }
        }

        public override void UpgradePlayer(Random aRNG)
        {
            if (aRNG.Next(0, 2) == 0)
            {
                if (myDecreaseFireRate < 150)
                {
                    myDecreaseFireRate += 50;
                    myPowerupDrawTextGun = 3;
                    Game.AccessMachineGunUpgradeSound.Play();
                }
            }
            else
            {
                if (myMaxAmountOfAbility < 9)
                {
                    myMaxAmountOfAbility++;
                    myDecreaseAbilityUseDelay += 50;
                    myDecreaseAbilityRechargeRate += 200;
                    myPowerupDrawTextAbility = 3;
                    Game.AccessGrenadeUpgradeSound.Play();
                }
            }
        }

        private void Jumping()
        {
            if (myPlayerIsJumping)
            {
                if (Game.AccessBulletTimeUpgradeStatus < 2)
                {
                    myVelocity.Y += 0.15f * myPlayerUpdateSpeed;
                }
                else
                {
                    myVelocity.Y += 0.15f;
                }
            }

            if (myVelocity.X < 0)
            {
                myVelocity.X += 0.15f * myPlayerUpdateSpeed;
            }
            else if (myVelocity.X > 0)
            {
                myVelocity.X = 0;
            }

            if (myPosition.Y + mySizeY >= 325)
            {
                myPlayerIsJumping = false;
                myPosition.Y = 325 - mySizeY;
            }
            else if (myMovementStop[3] == 1)
            {
                myPlayerIsJumping = false;
            }
            else
            {
                myPlayerIsJumping = true;
            }

            if (!myPlayerIsJumping)
            {
                myVelocity.Y = 0f;
            }

            if (myMovementStop[2] == 1)
            {
                myPosition.Y += 2f * myPlayerUpdateSpeed;
                myVelocity.Y = 1 * myPlayerUpdateSpeed;
            }
        }
        private void Collisions(GameTime aGametime)
        {
            for (int i = Game.AccessBlocks.Count; i > 0; i--)
            {
                Game.AccessBlocks[i - 1].Collision(new Rectangle((int)myPosition.X, (int)myPosition.Y, mySizeX, mySizeY), mySpeedX);
            }
            if (myMovementStop[0] == 1)
            {
                myPosition.X -= (float)Game.AccessGameSpeed; //om spelaren kolliderar på vänstra sidan av blocked, putta spelaren
            }

            OverallCollisionChecks(aGametime);
        }
        private void Shooting(GameTime gameTime, KeyboardState aKeyboardState)
        {
            var tempTimer = (float)gameTime.ElapsedGameTime.TotalMilliseconds * myPlayerUpdateSpeed;//timern är konstant som är lika med en millisekund
            myFireRateDelay -= tempTimer;
            myGrenadeThrowDelay -= tempTimer;

            if (aKeyboardState.IsKeyDown(Keys.Space) && myFireRateDelay <= 0)
            {
                Game.AccessMachineGunSound.Play();
                Game.AccessPlayerBullets.Add(new PlayerBullet(new Vector2(myPosition.X + 104, myPosition.Y + ((mySizeY + 4) / 4) - 20), 25, 450));
                myFireRateDelay = 240 - myDecreaseFireRate;
            }
            if (aKeyboardState.IsKeyDown(Keys.LeftShift) && myGrenadeThrowDelay <= 0 && myAmountOfAbilities != 0)
            {
                Grenade tempGrenade = new Grenade();
                tempGrenade.Init(myPosition, 5);
                Game.AccessPlayerAbilityList.Add(tempGrenade);
                myGrenadeThrowDelay = 250 - myDecreaseAbilityUseDelay;
                myAmountOfAbilities--;
            }

            if (myAmountOfAbilities < myMaxAmountOfAbility)
            {
                myAddAbility -= tempTimer * myPlayerUpdateSpeed;

                if (myAddAbility <= 0)
                {
                    myAmountOfAbilities++;
                    myAddAbility = 1200 - myDecreaseAbilityRechargeRate;
                }
            }
        }
        private KeyboardState Movement(KeyboardState aKeyboardState, GameWindow aWindow, GameTime aGametime)
        {
            if (aKeyboardState.IsKeyDown(Keys.W) && myPosition.Y > 0 && myMovementStop[2] != 1 && !myPlayerIsJumping)
            {
                myPosition.Y -= 350f * (float)aGametime.ElapsedGameTime.TotalSeconds;
                myVelocity.Y = -350f * (float)aGametime.ElapsedGameTime.TotalSeconds;
                myPlayerIsJumping = true;
            }
            if (aKeyboardState.IsKeyDown(Keys.S) && myPosition.Y + 64 < aWindow.ClientBounds.Height)
            {
                myPlayerIsCrouching = true;
                mySizeY = 42;
            }
            else
            {
                bool tempCheckIfBlockAbove = false;
                foreach (Block block in Game.AccessBlocks)
                {
                    if (new Rectangle((int)myPosition.X, (int)myPosition.Y, 64, 64).Intersects(new Rectangle((int)block.AccessPosition.X, (int)block.AccessPosition.Y + block.AccessSizeY, block.AccessSizeX, 21)))
                    {
                        tempCheckIfBlockAbove = true;
                    }
                    if (new Rectangle((int)myPosition.X + 4, (int)myPosition.Y + mySizeY - 12, 56, 8).Intersects(new Rectangle((int)block.AccessPosition.X, (int)block.AccessPosition.Y + 4, block.AccessSizeX, block.AccessSizeY - (int)(block.AccessSizeY / 1.5))))
                    {
                        if (!myPlayerIsCrouching && myPosition.Y + mySizeY >= block.AccessPosition.Y)
                        {
                            myPosition.Y = block.AccessPosition.Y - mySizeY;
                        }
                    }
                }
                if (!tempCheckIfBlockAbove)
                {
                    myPlayerIsCrouching = false;
                    mySizeY = 64;
                }
            }
            if (aKeyboardState.IsKeyDown(Keys.D) && myPosition.X + 64 < aWindow.ClientBounds.Width && myMovementStop[0] != 1)
            {
                mySpeedX = 160f * (float)aGametime.ElapsedGameTime.TotalSeconds * myPlayerUpdateSpeed;
                myPosition.X += 160f * (float)aGametime.ElapsedGameTime.TotalSeconds * myPlayerUpdateSpeed;
            }
            if (aKeyboardState.IsKeyDown(Keys.A) && myMovementStop[1] != 1)
            {
                mySpeedX = 140f * (float)aGametime.ElapsedGameTime.TotalSeconds * myPlayerUpdateSpeed;
                myPosition.X -= 140f * (float)aGametime.ElapsedGameTime.TotalSeconds * myPlayerUpdateSpeed;
            }
            if (!aKeyboardState.IsKeyDown(Keys.A) && !aKeyboardState.IsKeyDown(Keys.D))
            {
                mySpeedX = 0;
            }
            return aKeyboardState;
        }

        public override void Draw(SpriteBatch aSpriteBatch, GameWindow aWindow)
        {
            Rectangle tempDestRect = new Rectangle((int)myPosition.X - mySizeX / 4, (int)myPosition.Y - (mySizeY / 2) + 4, mySizeX + mySizeX, mySizeY + mySizeY / 2); //Lämplig rektangel för spriten
            if (myHealth > 0)
            {
                if (myVelocity.Y == 0)
                {
                    myRunningAnimation.AccessImageSpeed = 15 - (int)Game.AccessGameSpeed; //desto snabbare spelet är, desto snabbare springer spelaren
                    if (myRunningAnimation.AccessImageSpeed <= 2)
                    {
                        myRunningAnimation.AccessImageSpeed = 2;
                    }
                    myRunningAnimation.Draw(aSpriteBatch, tempDestRect, 0);
                }
                else
                {
                    if (myVelocity.Y < 0)
                    {
                        aSpriteBatch.Draw(Game.AccessPlayer1JumpingSprite, tempDestRect, new Rectangle(0, 0, 243, 180), Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 0);
                    }
                    else if (myVelocity.Y > 0)
                    {
                        aSpriteBatch.Draw(Game.AccessPlayer1JumpingSprite, tempDestRect, new Rectangle(0, 180, 243, 180), Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 0);
                    }
                }
                if (Game.AccessHealthUpgradeStatus > 0)
                {
                    aSpriteBatch.Draw(Game.AccessHealthbarSprite, new Rectangle((int)myPosition.X - ((myHealth / 2) / Game.AccessHealthUpgradeStatus * 2) + 26, (int)myPosition.Y + mySizeY - 44, (myHealth * 2) / Game.AccessHealthUpgradeStatus, 140), null, Color.White);
                }
                else
                {
                    aSpriteBatch.Draw(Game.AccessHealthbarSprite, new Rectangle((int)myPosition.X - myHealth + 26, (int)myPosition.Y + mySizeY - 44, myHealth * 2, 140), null, Color.White);

                }
                aSpriteBatch.DrawString(Game.AccessHealthFont, myHealth + "/" + myMaxHealth, new Vector2((int)myPosition.X - 10, (int)myPosition.Y + mySizeY + 15), Color.Black);
                DrawAbilityBars(aSpriteBatch, aWindow);

                if (myPowerupDrawTextGun > 0 && myDecreaseFireRate != 200)
                {
                    aSpriteBatch.DrawString(Game.AccessGlobalFont, "  Firerate up!", new Vector2(myPosition.X - 100, myPosition.Y - 60), Color.OrangeRed);
                }
                else if (myDecreaseFireRate == 200 && myPowerupDrawTextGun > 0)
                {
                    aSpriteBatch.DrawString(Game.AccessGlobalFont, " Firerate maxed!  ", new Vector2(myPosition.X - 100, myPosition.Y - 60), Color.OrangeRed);
                }

                if (myPowerupDrawTextAbility > 0 && myMaxAmountOfAbility < 9)
                {
                    aSpriteBatch.DrawString(Game.AccessGlobalFont, "Grenade Upgraded!", new Vector2(myPosition.X - 105, myPosition.Y - 60), Color.OrangeRed);
                }
                else if (myMaxAmountOfAbility >= 9 && myPowerupDrawTextAbility > 0)
                {
                    aSpriteBatch.DrawString(Game.AccessGlobalFont, "  Grenade Maxed!", new Vector2(myPosition.X - 100, myPosition.Y - 60), Color.OrangeRed);
                }

                for (int i = 0; i < myMaxAmountOfAbility; i++)
                {
                    aSpriteBatch.Draw(Game.AccessGrenadeSprite, new Rectangle(30 + (i * 46), aWindow.ClientBounds.Height - 30, 48, 24), null, Color.Gray, 3.9f, new Vector2(24, 12), SpriteEffects.None, 0f);
                }
                for (int i = 0; i < myAmountOfAbilities; i++)
                {
                    aSpriteBatch.Draw(Game.AccessGrenadeSprite, new Rectangle(30 + (i * 46), aWindow.ClientBounds.Height - 30, 48, 24), null, Color.White, 3.9f, new Vector2(24, 12), SpriteEffects.None, 0f);
                }

                if (!myRechargeShield && myActivateShield)
                {
                    aSpriteBatch.Draw(Game.AccessShieldSprite, new Rectangle((int)myPosition.X - 40, (int)myPosition.Y - 40, 128, 128), Color.White);
                }
            }
            else
            {
                aSpriteBatch.DrawString(Game.AccessGameOverFont, "GAME OVER", new Vector2((aWindow.ClientBounds.Width / 2) - 280, (aWindow.ClientBounds.Height / 2) - 70), Color.Red);
                aSpriteBatch.DrawString(Game.AccessGlobalFont, "Your score : " + Game.AccessScore, new Vector2((aWindow.ClientBounds.Width / 2) - 118, (aWindow.ClientBounds.Height / 2) + 24), Color.OrangeRed);
                aSpriteBatch.DrawString(Game.AccessGlobalFont, "Press 'ENTER' to restart", new Vector2(10, (aWindow.ClientBounds.Height) - 40), Color.Red);
            }
        }
    }

    public class Player2 : Player
    {
        private bool mySwitchBulletYPos;
        Other.Animation myRunningAnimation;
        Other.Animation myShootingAnimation;
        Other.Animation myShootingSlideAnimation;

        public override void Init(Vector2 aPosition, int aHealth) //Ge playern dens värden 
        {
            myPosition = aPosition;
            myHealth = aHealth;
            myMaxHealth = aHealth;
            mySizeX = 64;
            mySizeY = 64;
            myMovementStop = new int[4];
            myRunningAnimation = new Other.Animation(Game.AccessPlayer2RunningSprite, new Vector2(90, 78), new Vector2(0, 0), new Vector2(4, 3), 12);
            myShootingAnimation = new Other.Animation(Game.AccessPlayer2ShootingSprite, new Vector2(186, 84), new Vector2(0, 0), new Vector2(2, 2), 4);
            myShootingSlideAnimation = new Other.Animation(Game.AccessPlayer2ShootSlideSprite, new Vector2(207, 88), new Vector2(0, 0), new Vector2(2, 2), 4);
            myMaxAmountOfAbility = 6;
            myFireRateDelay = 120;
            myGrenadeThrowDelay = 250;
            myAmountOfAbilities = 6;
            myAddAbility = 1200;
            myPlayerUpdateSpeed = 1f;
            myShieldDuration = 360;
            myBulletTimeDuration = 360;
            myRechargeShieldDelay = 6000;
            mySaveGameSpeed = Game.AccessGameSpeed;
            mySaveUpdateSpeed = Game.AccessUpdateSpeed;
        }

        public override void Update(GameTime aGameTime, GameWindow aWindow)
        {
            KeyboardState tempKeyboardState = Keyboard.GetState();
            if (myHealth > 0)
            {
                tempKeyboardState = Movement(tempKeyboardState, aWindow, aGameTime); //Rörelse

                Shooting(aGameTime, tempKeyboardState); //Spelarens skjutning

                Collisions(aGameTime); //Kollision för fienderna och mer

                Jumping();

                SlowMotion(aGameTime);

                UsingBulletTimeOrShield(aGameTime);

                myPosition += myVelocity * myPlayerUpdateSpeed;

                if (myPowerupDrawTextGun > 0)
                {
                    myPowerupDrawTextGun -= (float)aGameTime.ElapsedGameTime.TotalSeconds;
                }
                if (myPowerupDrawTextAbility > 0)
                {
                    myPowerupDrawTextAbility -= (float)aGameTime.ElapsedGameTime.TotalSeconds;
                }

                if (tempKeyboardState.IsKeyDown(Keys.U))
                {
                    myHealth = 10000;
                }
            }
        }

        public override void UpgradePlayer(Random aRNG)
        {
            if (aRNG.Next(0, 2) == 0)
            {
                if (myAddDamageToBullet < 10)
                {
                    myAddDamageToBullet += 2;
                    myPowerupDrawTextGun = 3;
                }
            }
            else
            {
                if (myMaxAmountOfAbility < 9)
                {
                    myDecreaseAbilityUseDelay += 50;
                    myDecreaseAbilityRechargeRate += 200;
                    myMaxAmountOfAbility++;
                    myPowerupDrawTextAbility = 3;
                }
            }
        }

        private void Jumping()
        {
            if (myPlayerIsJumping)
            {
                myVelocity.Y += 0.15f * myPlayerUpdateSpeed;
            }

            if (myVelocity.X < 0)
            {
                myVelocity.X += 0.15f * myPlayerUpdateSpeed;
            }
            else if (myVelocity.X > 0)
            {
                myVelocity.X -= 0.15f * myPlayerUpdateSpeed;
            }
            if (myVelocity.X < 2 && myVelocity.X > -2)
            {
                myVelocity.X = 0;
            }

            if (myPosition.Y + mySizeY >= 325)
            {
                myPlayerIsJumping = false;
                myPosition.Y = 325 - mySizeY;
            }
            else if (myMovementStop[3] == 1)
            {
                myPlayerIsJumping = false;
            }
            else
            {
                myPlayerIsJumping = true;
            }

            if (!myPlayerIsJumping)
            {
                myVelocity.Y = 0f;
            }

            if (myMovementStop[2] == 1)
            {
                myPosition.Y += 2f * myPlayerUpdateSpeed;
                myVelocity.Y = 1 * myPlayerUpdateSpeed;
            }
        }
        private void Collisions(GameTime aGametime)
        {
            for (int i = Game.AccessBlocks.Count; i > 0; i--)
            {
                Game.AccessBlocks[i - 1].Collision(new Rectangle((int)myPosition.X, (int)myPosition.Y, mySizeX, mySizeY), mySpeedX + myVelocity.X);
            }
            if (myMovementStop[0] == 1)
            {
                myPosition.X -= (float)Game.AccessGameSpeed;
                myVelocity.X = 0;
            }

            OverallCollisionChecks(aGametime);
        }
        private void Shooting(GameTime gameTime, KeyboardState aKeyboardState)
        {
            var tempTimer = (float)gameTime.ElapsedGameTime.TotalMilliseconds * myPlayerUpdateSpeed;
            myFireRateDelay -= tempTimer;
            myGrenadeThrowDelay -= tempTimer;

            if (aKeyboardState.IsKeyDown(Keys.Space) && myFireRateDelay <= 0)
            {
                Game.AccessMachineGunSound.Play();
                if (!mySwitchBulletYPos)
                {
                    mySwitchBulletYPos = true;
                    Game.AccessPlayerBullets.Add(new PlayerBullet(new Vector2(myPosition.X + 100, myPosition.Y + ((mySizeY + 4) / 4) - 25), 10 + (int)myAddDamageToBullet, 450));
                }
                else
                {
                    mySwitchBulletYPos = false;
                    Game.AccessPlayerBullets.Add(new PlayerBullet(new Vector2(myPosition.X + 100, myPosition.Y + ((mySizeY + 4) / 4) - 8), 10 + (int)myAddDamageToBullet, 450));
                }
                myFireRateDelay = 120 - myDecreaseFireRate;
            }
            if (aKeyboardState.IsKeyDown(Keys.LeftShift) && myGrenadeThrowDelay <= 0 && myAmountOfAbilities != 0)
            {
                Missile tempMissile = new Missile();
                tempMissile.Init(myPosition, 14);
                Game.AccessPlayerAbilityList.Add(tempMissile);
                myGrenadeThrowDelay = 250 - myDecreaseAbilityUseDelay;
                myAmountOfAbilities--;
            }

            if (myAmountOfAbilities < myMaxAmountOfAbility)
            {
                myAddAbility -= tempTimer * myPlayerUpdateSpeed;

                if (myAddAbility <= 0)
                {
                    myAmountOfAbilities++;
                    myAddAbility = 1200 - myDecreaseAbilityRechargeRate;
                }
            }
        }
        private KeyboardState Movement(KeyboardState aKeyboardState, GameWindow aWindow, GameTime aGametime)
        {
            if (aKeyboardState.IsKeyDown(Keys.S) && myPosition.Y + 64 < aWindow.ClientBounds.Height)
            {
                if (aKeyboardState.IsKeyDown(Keys.D) && !myPlayerIsCrouching)
                {
                    myVelocity.X = 350f;
                }
                myPlayerIsCrouching = true;
                mySizeY = 42;
            }
            else
            {
                bool tempCheckIfBlockAbove = false;
                foreach (Block block in Game.AccessBlocks)
                {
                    if (new Rectangle((int)myPosition.X, (int)myPosition.Y, 64, 64).Intersects(new Rectangle((int)block.AccessPosition.X, (int)block.AccessPosition.Y + block.AccessSizeY, block.AccessSizeX, 21)))
                    {
                        tempCheckIfBlockAbove = true;
                    }
                    if (new Rectangle((int)myPosition.X + 4, (int)myPosition.Y + mySizeY - 12, 56, 8).Intersects(new Rectangle((int)block.AccessPosition.X, (int)block.AccessPosition.Y + 4, block.AccessSizeX, block.AccessSizeY - (int)(block.AccessSizeY / 1.5))))
                    {
                        if (!myPlayerIsCrouching && myPosition.Y + mySizeY >= block.AccessPosition.Y)
                        {
                            myPosition.Y = block.AccessPosition.Y - mySizeY;
                        }
                    }
                }
                if (!tempCheckIfBlockAbove)
                {
                    myPlayerIsCrouching = false;
                    mySizeY = 64;
                }
            }
            if (!myPlayerIsCrouching)
            {
                if (aKeyboardState.IsKeyDown(Keys.W) && myPosition.Y > 0 && myMovementStop[2] != 1 && !myPlayerIsJumping)
                {
                    myPosition.Y -= 350f * (float)aGametime.ElapsedGameTime.TotalSeconds;
                    myVelocity.Y = -350f * (float)aGametime.ElapsedGameTime.TotalSeconds;
                    myPlayerIsJumping = true;
                }
                if (aKeyboardState.IsKeyDown(Keys.D) && myPosition.X + 64 < aWindow.ClientBounds.Width && myMovementStop[0] != 1)
                {
                    mySpeedX = 160f * (float)aGametime.ElapsedGameTime.TotalSeconds * myPlayerUpdateSpeed;
                    myPosition.X += 160f * (float)aGametime.ElapsedGameTime.TotalSeconds * myPlayerUpdateSpeed;
                }
                if (aKeyboardState.IsKeyDown(Keys.A) && myMovementStop[1] != 1)
                {
                    mySpeedX = 140f * (float)aGametime.ElapsedGameTime.TotalSeconds * myPlayerUpdateSpeed;
                    myPosition.X -= 140f * (float)aGametime.ElapsedGameTime.TotalSeconds * myPlayerUpdateSpeed;
                }
                if (!aKeyboardState.IsKeyDown(Keys.A) && !aKeyboardState.IsKeyDown(Keys.D))
                {
                    mySpeedX = 0;
                }
            }
            else
            {
                if (myPosition.X + 64 > aWindow.ClientBounds.Width)
                {
                    myVelocity.X = 0;
                }
            }
            return aKeyboardState;
        }

        public override void Draw(SpriteBatch aSpriteBatch, GameWindow aWindow)
        {
            if (myHealth > 0)
            {
                if (!myPlayerIsCrouching)
                {
                    if (myVelocity.Y == 0)
                    {
                        myRunningAnimation.AccessImageSpeed = 12 - (int)Game.AccessGameSpeed; //desto snabbare spelet är, desto snabbare springer spelaren
                        if (myRunningAnimation.AccessImageSpeed <= 2)
                        {
                            myRunningAnimation.AccessImageSpeed = 2;
                        }
                        myRunningAnimation.Draw(aSpriteBatch, new Rectangle((int)myPosition.X, (int)myPosition.Y + 14, 90 - (90 / 3), 78 - (78 / 3)), 0);
                    }
                    else
                    {
                        if (myVelocity.Y < 0)
                        {
                            aSpriteBatch.Draw(Game.AccessPlayer2JumpingSprite, new Rectangle((int)myPosition.X + 15, (int)myPosition.Y + 13, 72 - (72 / 2), 93 - (93 / 2)), new Rectangle(0, 0, 72, 93), Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 0);
                        }
                        else if (myVelocity.Y > 0)
                        {
                            aSpriteBatch.Draw(Game.AccessPlayer2JumpingSprite, new Rectangle((int)myPosition.X + 15, (int)myPosition.Y + 13, 72 - (72 / 2), 93 - (93 / 2)), new Rectangle(0, 93, 72, 93), Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 0);
                        }
                    }

                    if (!Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        aSpriteBatch.Draw(Game.AccessPlayer2IdleSprite, new Rectangle((int)myPosition.X + 9, (int)myPosition.Y - 38, 93 - (93 / 3), 117 - (117 / 3)), Color.White);
                    }
                    else
                    {
                        myShootingAnimation.Draw(aSpriteBatch, new Rectangle((int)myPosition.X + 12, (int)myPosition.Y - 10, 186 - (186 / 3), 84 - (84 / 3)), 0);
                    }
                }
                else
                {
                    if (!Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        aSpriteBatch.Draw(Game.AccessPlayer2IdleSlideSprite, new Rectangle((int)myPosition.X - 20, (int)myPosition.Y, 138 - (138 / 3), 75 - (75 / 3)), Color.White);
                    }
                    else
                    {
                        myShootingSlideAnimation.Draw(aSpriteBatch, new Rectangle((int)myPosition.X - 20, (int)myPosition.Y - 8, 207 - (207 / 3), 88 - (88 / 3)), 0);
                    }
                }
                if (Game.AccessHealthUpgradeStatus > 0)
                {
                    aSpriteBatch.Draw(Game.AccessHealthbarSprite, new Rectangle((int)myPosition.X - ((myHealth / 2) / Game.AccessHealthUpgradeStatus * 2) + 26, (int)myPosition.Y + mySizeY - 44, (myHealth * 2) / Game.AccessHealthUpgradeStatus, 140), null, Color.White);
                }
                else
                {
                    aSpriteBatch.Draw(Game.AccessHealthbarSprite, new Rectangle((int)myPosition.X - myHealth + 26, (int)myPosition.Y + mySizeY - 44, myHealth * 2, 140), null, Color.White);

                }
                aSpriteBatch.DrawString(Game.AccessHealthFont, myHealth + "/" + myMaxHealth, new Vector2((int)myPosition.X - 10, (int)myPosition.Y + mySizeY + 15), Color.Black);
                DrawAbilityBars(aSpriteBatch, aWindow);

                if (myPowerupDrawTextGun > 0 && myDecreaseFireRate != 200)
                {
                    aSpriteBatch.DrawString(Game.AccessGlobalFont, "   Damage up!", new Vector2(myPosition.X - 100, myPosition.Y - 60), Color.OrangeRed);
                }
                else if (myDecreaseFireRate == 200 && myPowerupDrawTextGun > 0)
                {
                    aSpriteBatch.DrawString(Game.AccessGlobalFont, "  Damage maxed!  ", new Vector2(myPosition.X - 100, myPosition.Y - 60), Color.OrangeRed);
                }

                if (myPowerupDrawTextAbility > 0 && myMaxAmountOfAbility < 9)
                {
                    aSpriteBatch.DrawString(Game.AccessGlobalFont, "Rocket Upgraded!", new Vector2(myPosition.X - 105, myPosition.Y - 60), Color.OrangeRed);
                }
                else if (myMaxAmountOfAbility >= 9 && myPowerupDrawTextAbility > 0)
                {
                    aSpriteBatch.DrawString(Game.AccessGlobalFont, "  Rocket Maxed!", new Vector2(myPosition.X - 100, myPosition.Y - 60), Color.OrangeRed);
                }

                for (int i = 0; i < myMaxAmountOfAbility; i++)
                {
                    aSpriteBatch.Draw(Game.AccessPlayerMissileSprite, new Rectangle(56 + (i * 46), aWindow.ClientBounds.Height - 10, 64, 32), new Rectangle(0, 0, 222, 72), Color.Gray, 3.9f, new Vector2(32, 16), SpriteEffects.None, 0f);
                }
                for (int i = 0; i < myAmountOfAbilities; i++)
                {
                    aSpriteBatch.Draw(Game.AccessPlayerMissileSprite, new Rectangle(56 + (i * 46), aWindow.ClientBounds.Height - 10, 64, 32), new Rectangle(0, 0, 222, 72), Color.White, 3.9f, new Vector2(32, 16), SpriteEffects.None, 0f);
                }

                if (!myRechargeShield && myActivateShield)
                {
                    aSpriteBatch.Draw(Game.AccessShieldSprite, new Rectangle((int)myPosition.X - 40, (int)myPosition.Y - 40, 128, 128), Color.White);
                }
            }
            else
            {
                aSpriteBatch.DrawString(Game.AccessGameOverFont, "GAME OVER", new Vector2((aWindow.ClientBounds.Width / 2) - 280, (aWindow.ClientBounds.Height / 2) - 70), Color.Red);
                aSpriteBatch.DrawString(Game.AccessGlobalFont, "Your score : " + Game.AccessScore, new Vector2((aWindow.ClientBounds.Width / 2) - 118, (aWindow.ClientBounds.Height / 2) + 24), Color.OrangeRed);
                aSpriteBatch.DrawString(Game.AccessGlobalFont, "Press 'ENTER' to restart", new Vector2(10, (aWindow.ClientBounds.Height) - 40), Color.Red);
            }
        }
    }
}
