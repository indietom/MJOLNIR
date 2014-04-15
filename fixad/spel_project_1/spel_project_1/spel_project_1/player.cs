using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace spel_project_1
{
    class player:objects
    {
        public int gunType;
        public bool jumping;
        public bool onGround;
        public bool decreseGravity;
        public bool onWall;
        public bool onRoof;
        public bool inputActive;
        public float gravityCount;
        public int direction2;
        public bool keyFalse;
        public bool shoothingFalse;
        public bool shoothingFalse2;
        public int fireRate;
        public bool hit;
        public bool healed;
        public bool onLadder;
        public int acquiredGuns;
        public bool bleeding;
        public int bleedCount;
        public bool shootingFalse3;
        public bool dead;
        public int shotgunAmmo;
        public int rifleAmmo;
        public int rocketAmmo;
        public int hammerDelay;
        public bool spawnHammerEffectCheck;
        public int respawnCounter;

        public player()
        {
            shotgunAmmo = 0;
            rifleAmmo = 0;
            rocketAmmo = 0;
            gunType = 1;
            gravityCount = 0f;
            setSpriteCoords(1, imgx);
            setSize(32, 32);
            setCoords(300, 15);
            inputActive = true;
            hp = 10;
            acquiredGuns = 4;
            animationActive = true;
        }
        public void checkHealth(healthbar healthbar, List<particle> particles, levelManager lm, List<enemy> enemies, List<bullet> bullets, List<powerUp> powerUps, ref Rectangle camera, List<enemyBullet> enemyBullets, List<boss> bosses)
        {
            Random random = new Random();

            if (hp >= 11)
            {
                hp = 10;
            }

            healthbar.height = hp * 10;

            if (lm.currentLevel == 4 || lm.currentLevel == 3 || lm.currentLevel == 6 || lm.currentLevel == 5)
            {
                if (y >= 500)
                {
                    hp = 0;
                }
            }
            if (lm.currentLevel == 3)
            {
                if(x <= -100)
                    hp = 0;
                if (x >= 740)
                    hp = 0;
            }

            if (dead)
            {
                shotgunAmmo = 0;
                rifleAmmo = 0;
                rocketAmmo = 0;
                gunType = 1;
                respawnCounter += 1;
                if (respawnCounter >= 128)
                {
                    lm.section = 1;
                    if (lm.currentLevel == 5)
                    {
                        // det fanns en konstig bug på din banna så jag fick göra detta
                        setCoords(100, 100);
                    }
                    lm.roomTransition(ref inputActive, enemies, bullets, new player(), particles, powerUps, ref camera, enemyBullets, bosses); 
                    hp = 10;
                    respawnCounter = 0;
                    if(lm.currentLevel == 4)
                        setCoords(300, 15);
                    if (lm.currentLevel == 3)
                        setCoords(50, 300);
                    if (lm.currentLevel == 6)
                    {
                        setCoords(50, 300);
                        camera.X = 0;
                        camera.Y = 0;
                    }
                    
                }
            }

            if (hp <= 0)
            {
                dead = true;
            }
            else
            {
                dead = false;
            }
            if (hit)
            {
                for (int i = 0; i < 50; i++)
                {
                    particles.Add(new particle(x + random.Next(32), y + random.Next(32), 200, 3, "red", random.Next(-270, -80), 10));
                }
                hit = false;
            }
            if (healed)
            {
                healthbar.height += 10;
                healed = false;
            }
            if (bleeding)
            {
                bleedCount += 1;
                if (bleedCount >= 10)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        particles.Add(new particle(x + random.Next(32), y + random.Next(32), 50, 4, "red", random.Next(-290, -250), random.Next(2, 5)));
                    }
                    bleedCount = 0;
                }
            }
            if (hp <= 3)
            {
                bleeding = true;
            }
            else
            {
                bleeding = false;
            }
        }
        public void input(List<bullet> bullets, List<particle> particles, ref Rectangle camera)
        {
            Random random = new Random();
            KeyboardState keyboard = Keyboard.GetState();
            GamePadState gamepad = GamePad.GetState(PlayerIndex.One);
            if (keyboard.IsKeyDown(Keys.R))
            {
                camera.X = (int)x;
                camera.Y = 0;
                setCoords(210, 100);
                rocketAmmo += 1000;
                rifleAmmo += 1000;
                shotgunAmmo += 1000;
                hp = 10;
                respawnCounter = 0;
            }
            if (keyboard.IsKeyDown(Keys.F1))
            {
                Console.WriteLine(x);
                Console.WriteLine(y);
            }
            if (dead)
            {
                bleeding = true;
                jumping = false;
                inputActive = false;
            }
            if (hammerDelay >= 1)
            {
                inputActive = false;
                hammerDelay += 1;
                if (hammerDelay == 32 && spawnHammerEffectCheck)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        if (direction == 3)
                        {
                            particles.Add(new particle(x - 16 * 2, y + 32, 50, 1, "light blue", random.Next(-150, -40), random.Next(5, 7), true));
                        }
                        else
                        {
                            particles.Add(new particle(x + 16 * 4, y + 32, 50, 1, "light blue", random.Next(-150, -40), random.Next(5, 7), true));
                        }
                    }
                }
                if (hammerDelay >= 64)
                {
                    hammerDelay = 0;
                    inputActive = true;
                }
            }
            if (inputActive)
            {
                if (keyboard.IsKeyDown(Keys.C) || keyboard.IsKeyDown(Keys.LeftShift))
                {
                    if (hammerDelay <= 0 && onGround && !jumping && !onLadder && !onWall)
                    {
                        hammerDelay = 1;
                    }
                }
                if (keyboard.IsKeyDown(Keys.X) || keyboard.IsKeyDown(Keys.Space) || gamepad.Buttons.B == ButtonState.Pressed)
                {
                    if (gunType == 1 && !shoothingFalse && !shoothingFalse2 && !shootingFalse3 && fireRate <= 0)
                    {
                        if (direction == 3)
                        {
                            bullets.Add(new bullet(x + 16, y + 10, -180, 100, 1));
                            particles.Add(new particle(x + 16, y + 10, 200, 3, "yellow", -70, 7));
                        }
                        else
                        {
                            bullets.Add(new bullet(x + 16, y + 10, 0, 100, 1));
                            particles.Add(new particle(x + 16, y + 10, 200, 3, "yellow", -110, 7));
                        }
                        fireRate = 1;
                        shootingFalse3 = true;
                        shoothingFalse = true;
                        shoothingFalse2 = true;
                    }
                }
                if (keyboard.IsKeyDown(Keys.X) || keyboard.IsKeyDown(Keys.Space) || gamepad.Buttons.B == ButtonState.Pressed)
                {
                    if (gunType == 2 && !shoothingFalse && !shoothingFalse2 && !shootingFalse3 && fireRate <= 0)
                    {
                        rocketAmmo -= 1;
                        if (rocketAmmo <= 0)
                        {
                            gunType = 1;
                        }
                        if (direction == 3)
                        {
                            bullets.Add(new bullet(x + 16, y + 10, -180, 100, 2));
                        }
                        else
                        {
                            bullets.Add(new bullet(x + 16, y + 10, 0, 100, 2));
                        }
                        fireRate = 1;
                        shootingFalse3 = true;
                        shoothingFalse = true;
                        shoothingFalse2 = true;
                    }
                }
                if (keyboard.IsKeyDown(Keys.X) || keyboard.IsKeyDown(Keys.Space) || gamepad.Buttons.B == ButtonState.Pressed)
                {
                    if (gunType == 3 && !shoothingFalse && !shoothingFalse2 && !shootingFalse3 && fireRate <= 0)
                    {
                        shotgunAmmo -= 1;
                        if (shotgunAmmo <= 0)
                        {
                            gunType = 1;
                        }
                        if (direction == 3)
                        {
                            bullets.Add(new bullet(x + 16, y + 10, -185, 100, 1));
                            bullets.Add(new bullet(x + 16, y + 10, -180, 100, 1));
                            bullets.Add(new bullet(x + 16, y + 10, -175, 100, 1));
                        }
                        else
                        {
                            bullets.Add(new bullet(x + 16, y + 10, -5, 100, 1));
                            bullets.Add(new bullet(x + 16, y + 10, 0, 100, 1));
                            bullets.Add(new bullet(x + 16, y + 10, 5, 100, 1));
                        }
                        fireRate = 1;
                        shootingFalse3 = true;
                        shoothingFalse2 = true;
                        shoothingFalse = true;
                    }
                }
                if (gunType == 3 && fireRate == 31)
                {
                    if (direction == 3)
                    {
                        particles.Add(new particle(x + 16, y + 10, 200, 3, "shotgunshell", -70, 7));
                    }
                    else
                    {
                        particles.Add(new particle(x + 16, y + 10, 200, 3, "shotgunshell", -110, 7));
                    }
                    
                }
                if (keyboard.IsKeyDown(Keys.X) || keyboard.IsKeyDown(Keys.Space) || gamepad.Buttons.B == ButtonState.Pressed)
                {
                    if (gunType == 4 && fireRate <= 0)
                    {
                        rifleAmmo -= 1;
                        if (rifleAmmo <= 0)
                        {
                            gunType = 1;
                        }
                        if (direction == 3)
                        {
                            bullets.Add(new bullet(x + 16, y + 10, -180, 100, 1));
                            particles.Add(new particle(x + 16, y + 10, 200, 3, "yellow", -70, random.Next(4, 7)));
                        }
                        else
                        {
                            bullets.Add(new bullet(x + 16, y + 10, 0, 100, 1));
                            particles.Add(new particle(x + 16, y + 10, 200, 3, "yellow", -110, random.Next(4, 7)));
                        }
                        fireRate = 16;
                    }
                }
                if (keyboard.IsKeyDown(Keys.D1))
                    gunType = 1;
                if (keyboard.IsKeyDown(Keys.D2) && rocketAmmo >= 1)
                    gunType = 2;
                if (keyboard.IsKeyDown(Keys.D3) && shotgunAmmo >= 1)
                    gunType = 3;
                if (keyboard.IsKeyDown(Keys.D4) && rifleAmmo >= 1)
                    gunType = 4;
                
                if (shoothingFalse)
                {
                    if (keyboard.IsKeyUp(Keys.X))
                    {
                        shoothingFalse = false;
                    }
                }
                if (shoothingFalse2)
                {
                    if (keyboard.IsKeyUp(Keys.Space))
                    {
                        shoothingFalse2 = false;
                    }
                }
                
                if (shootingFalse3)
                {
                    if (gamepad.Buttons.B == ButtonState.Released)
                    {
                        shootingFalse3 = false;
                    }
                }
                if (fireRate >= 1)
                {
                    fireRate += 1;
                    if (fireRate >= 32)
                    {
                        fireRate = 0;
                    }
                }

                if (keyboard.IsKeyUp(Keys.Right) && keyboard.IsKeyUp(Keys.Left) && keyboard.IsKeyUp(Keys.D) && keyboard.IsKeyUp(Keys.A) && gamepad.ThumbSticks.Left.X != 1.0f && gamepad.ThumbSticks.Left.X != -1.0f)
                {
                    if (animationActive)
                    {
                        imgx = 1;
                        animationCount = 0;
                    }
                }
                if (keyboard.IsKeyDown(Keys.Right) && keyboard.IsKeyDown(Keys.Left) || keyboard.IsKeyDown(Keys.D) && keyboard.IsKeyDown(Keys.A))
                {
                    if (animationActive)
                    {
                        imgx = 1;
                        animationCount = 0;
                    }
                }
                if (onLadder)
                {
                    if (keyboard.IsKeyDown(Keys.Up) || keyboard.IsKeyDown(Keys.W) || gamepad.ThumbSticks.Left.Y == 1.0f)
                    {
                        if (!onRoof)
                        {
                            y -= 1;
                        }
                    }
                    if (keyboard.IsKeyDown(Keys.Down) || keyboard.IsKeyDown(Keys.S) || gamepad.ThumbSticks.Left.Y == -1.0f)
                    {
                        y += 2;
                    }
                }
                if (keyboard.IsKeyDown(Keys.Z) || keyboard.IsKeyDown(Keys.W) || gamepad.Buttons.A == ButtonState.Pressed)
                {
                    if (!jumping && onGround && !onWall && !onRoof)
                    {
                        gravityCount = 13f;
                        jumping = true;
                        keyFalse = true;
                    }
                }
            }
        }

        public void animation()
        {
            if (jumping || !onGround && !onWall || dead || onLadder)
            {
                animationActive = false;
                if (dead && !animationActive)
                {
                    setSpriteCoords(1, 67);
                }
                if (onLadder && !animationActive)
                {
                    setSpriteCoords(frame(5), 1);
                }

                if (!onGround && !onWall && !onLadder && !animationActive)
                {
                    imgx = frame(4);
                    if (direction == 3)
                    {
                        imgy = frame(1);
                    }
                    if (direction == 4)
                    {
                        imgy = 1;
                    }
                }

            }
            else
            {
                animationActive = true;
            }
            if (animationActive)
            {
                if (direction == 3)
                {
                    imgy = frame(1);
                }
                if (direction == 4)
                {
                    imgy = 1;
                }
                if (animationCount == 5)
                {
                    imgx = frame(1);
                }
                if (animationCount == 10)
                {
                    imgx = frame(2);
                }
                if (animationCount == 15)
                {
                    imgx = frame(3);
                }
                if (animationCount == 20)
                {
                    animationCount = 5;
                }
            }
        }

        public void movemnt()
        {
            if (onGround && gravityCount <= 10)
            {
                jumping = false;
            }
            if (!jumping)
            {
                gravityCount = 0;
            }
            if (jumping)
            {
                y -= gravityCount;
                if (gravityCount >= 0)
                {
                    gravityCount -= gravity/10;
                }
                if (gravityCount <= 1f)
                {
                    jumping = false;
                }
                
            }
            if (onLadder && onGround)
            {
                onLadder = false;
            }
            if (!onLadder)
            {
                y += gravity;
            }
        }
    }
}
