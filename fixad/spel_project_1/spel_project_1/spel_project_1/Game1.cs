using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace spel_project_1
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont gameFont;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 640;
        }

        saveState saveState = new saveState();
        string gameState = "menu";
        healthbar healthbar = new healthbar();
        levelManager levelManager = new levelManager(); 
        Rectangle camera;
        level level = new level();
        player player = new player();
        List<enemy> enemies = new List<enemy>();
        List<powerUp> powerUps = new List<powerUp>();
        List<enemyBullet> enemyBullets = new List<enemyBullet>();
        List<bullet> bullets = new List<bullet>();
        List<particle> particles = new List<particle>();
        List<explosion> explosions = new List<explosion>();
        List<titleCard> titleCards = new List<titleCard>();
        List<boss> bosses = new List<boss>();
        bool cameraFree = false;
        menu menu;

        public bool mech;
        
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            camera = new Rectangle(0, 0, 320, 240);
            menu = new menu(titleCards);
            base.Initialize();
        }
        Texture2D spritesheet;
        Texture2D tilesheet;
        Texture2D tileSet5;
        Texture2D backgroundLevel5;
        protected override void LoadContent()
        {

            gameFont = Content.Load<SpriteFont>("SpriteFont1");
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spritesheet = Content.Load<Texture2D>("spritesheet");
            backgroundLevel5 = Content.Load<Texture2D>("background5");
            tileSet5 = Content.Load<Texture2D>("tileSet5");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        public void cameraLogic()
        {
            Rectangle playerCamera = new Rectangle((int)player.renderX, (int)player.renderY, 32, 32);
            Rectangle left = new Rectangle(0, 0, 300, 480);
            Rectangle right = new Rectangle(640-300, 0, 300, 480);
            Rectangle down = new Rectangle(0, 480 - 200, 640, 200);
            Rectangle up = new Rectangle(0, 0, 640, 200);

            if (camera.X <= 0)
            {
                camera.X = 0;
            }
            if (camera.X >= levelManager.currentSection.GetLength(0) * 16)
            {
                camera.X = levelManager.currentSection.GetLength(0) * 16;
            }

            if (playerCamera.Intersects(left) && !cameraFree)
            {
                camera.X -= 3;
            }
            if (playerCamera.Intersects(right) && !cameraFree)
            {
                camera.X += 3;
            }
            if (playerCamera.Intersects(down) && !cameraFree && levelManager.currentSection.GetLength(0) > 30)
            {
                if (player.onLadder)
                {
                    camera.Y += 2;
                }
                else
                {
                    camera.Y += (int)player.gravity;
                }
            }
            if (playerCamera.Intersects(up) && !cameraFree && levelManager.currentSection.GetLength(0) > 30)
            {
                camera.Y -= 3;
            }
            if (levelManager.currentSection.GetLength(1) == 40 && levelManager.currentSection.GetLength(0) == 30)
            {
                camera.X = 0;
                camera.Y = 0;
                cameraFree = true;
            }
            else
            {
                cameraFree = false;
            }
        }
        bool collisionTile(Rectangle rect, int[,] mapC, int number)
        {
            int row = 0;
            int col = 0;

            const int WIDTH = 16;
            const int HEIGHT = 16;

            const int CORNERONE = 5; // x
            const int CORNERTWO = 17; // y

            const int CORNERS = 2;

            if (rect.X < 0)
            {
                rect.X = 0;
            }
            if (rect.Y < 0)
            {
                rect.Y = 0;
            }
            if (rect.X > levelManager.currentSectionC.GetLength(1) * 16 - 32)
            {
                rect.X = levelManager.currentSectionC.GetLength(1) * 16 - 32;
            }
            if (rect.Y > levelManager.currentSectionC.GetLength(0) * 16 - 38)
            {
                rect.Y = levelManager.currentSectionC.GetLength(0) * 16 - 38;
            }

            for (int i = 0; i < 4; i++)
            {
                col = (rect.X + i % CORNERS * CORNERONE) / WIDTH;
                row = (rect.Y + i / CORNERS * CORNERTWO) / HEIGHT;
               // if (rect.X > 0 && rect.Y > 0 && rect.X < levelManager.currentSectionC.GetLength(1) * 16 - 32 && rect.Y < levelManager.currentSectionC.GetLength(0) * 16 - 32)
              //  {
                if (mapC[row + 1, col] == number || mapC[row + 1, col + 1] == number || mapC[row, col] == number || mapC[row, col + 1] == number)
                {
                    return true;
                }
               // }
            }
            return false;
        }

        protected override void Update(GameTime gameTime)
        {
            levelManager.checkSection(level);
            levelManager.checkLevel();
            KeyboardState keyboard = Keyboard.GetState();
            GamePadState gamepad = GamePad.GetState(PlayerIndex.One);

            if (keyboard.IsKeyDown(Keys.F1))
                saveState.load(levelManager);

            if (keyboard.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            switch(gameState)
            {
                case "start screen":
                    if (keyboard.IsKeyDown(Keys.Space))
                    {
                        gameState = "menu";
                    }
                    break;
                case "menu":
                    menu.updateMenu(titleCards, levelManager);
                    enemies.Clear();
                    bosses.Clear();
                    explosions.Clear();
                    particles.Clear();
                    bullets.Clear();
                    foreach (titleCard tc in titleCards)
                    {
                        tc.update(levelManager, ref gameState, enemies, bullets, player, particles, powerUps, camera, enemyBullets, bosses);
                        if (levelManager.levelsBeaten[tc.bossNumber])
                        {
                            tc.imgy = tc.frame(6);
                        }
                    }
                    //levelManager.resetLevel(enemies, bullets);
                    break;
                case "game":
                    Rectangle playerFeet = new Rectangle((int)player.x, (int)player.y + 2, 32, 32);
                    Rectangle playerBodyL = new Rectangle((int)player.x - 3, (int)player.y - 3, 32, 32);
                    Rectangle playerBodyR = new Rectangle((int)player.x + 3, (int)player.y - 3, 32, 32);
                    Rectangle playerHead = new Rectangle((int)player.x, (int)player.y - 6, 32, 32);
                    Rectangle playerLadderHead = new Rectangle((int)player.x, (int)player.y - 24, 32, 32);
                    Rectangle playerRC = new Rectangle((int)player.renderX + 10, (int)player.renderY, 10, 32);
                    Rectangle hammerParticleC;
                    if (player.direction == 3)
                    {
                        hammerParticleC = new Rectangle((int)player.x - 16 * 4, (int)player.y + 32, 32, 32);
                    }
                    else
                    {
                        hammerParticleC = new Rectangle((int)player.x + 16 * 2, (int)player.y + 32, 32, 32);
                    }
                    Rectangle ebulletRC;
                    Rectangle bulletRC;
                    Rectangle powerUpRC;
                    Rectangle enemyRC;
                    Rectangle enemyC;
                    Rectangle enemyCL;
                    Rectangle enemyCR;
                    Rectangle bossRC;
                    Rectangle hammerParticleRC;

                    cameraLogic();

                    foreach (boss b in bosses)
                    {
                        b.movment();
                        b.attacking(enemyBullets, enemies);
                        b.animation();
                        b.applyOffset(camera);
                        b.checkHealth(levelManager, ref gameState, explosions);
                        bossRC = new Rectangle((int)b.x, (int)b.y, b.width, b.height);
                        if (playerRC.Intersects(bossRC))
                        {
                            player.hp = 0;
                        }
                        foreach (bullet bu in bullets)
                        {
                            bulletRC = new Rectangle((int)bu.x, (int)bu.y, bu.width, bu.height);
                            if (bulletRC.Intersects(bossRC))
                            {
                                if (bu.type == 2)
                                {
                                    b.hp -= 2;
                                }
                                else
                                {
                                    b.hp -= 1;
                                }
                                bu.destroy = true;
                            }
                        }
                    }

                    foreach (powerUp pu in powerUps)
                    {
                        pu.update(camera);
                        powerUpRC = new Rectangle((int)pu.renderX, (int)pu.renderY, 16, 16);
                        if (playerRC.Intersects(powerUpRC))
                        {
                            if (pu.type == 1 && player.hp < 10)
                            {
                                player.healed = true;
                                player.hp += 1;
                            }
                            if (pu.type == 2)
                            {
                                player.rocketAmmo += 2;
                            }
                            if (pu.type == 3)
                            {
                                player.shotgunAmmo += 5;
                            }
                            if (pu.type == 4)
                            {
                                player.rifleAmmo += 10;
                            }
                            pu.destroy = true;
                        }
                    }

                    foreach (explosion ex in explosions)
                    {
                        ex.animation();
                        ex.applyOffset(camera);
                    }

                    foreach (enemy e in enemies)
                    {
                        e.movment();
                        e.attacking(enemyBullets, player, particles);
                        e.animation();
                        e.applyOffset(camera);
                        e.checkHealth(explosions);
                        enemyC = new Rectangle((int)e.x, (int)e.y + 2, e.width, e.height);
                        enemyCL = new Rectangle((int)e.x - 3, (int)e.y - 5, e.width, e.height);
                        enemyCR = new Rectangle((int)e.x + 3, (int)e.y - 5, e.width, e.height);

                        enemyRC = new Rectangle((int)e.renderX, (int)e.renderY, 32, 32);

                        if (playerRC.Intersects(enemyRC))
                        {
                            if (e.type == 3 || e.type == 2 || e.type == 5 || e.type == 1)
                            {
                                player.hp = 0;
                                if (!player.dead)
                                {
                                    player.hit = true;
                                }
                            }
                        }

                        if (collisionTile(enemyCL, levelManager.currentSectionC, 1) || collisionTile(enemyCR, levelManager.currentSectionC, 1))
                        {
                            e.onWall = true;
                        }
                        else
                        {
                            e.onWall = false;
                        }
                        if (collisionTile(enemyC, levelManager.currentSectionC, 1) && e.type == 2)
                        {
                            e.y -= 6;
                        }
                        if (collisionTile(enemyCR, levelManager.currentSectionC, 1) && e.type == 2)
                        {
                            e.x -= 1;
                        }
                        if (collisionTile(enemyCR, levelManager.currentSectionC, 1) && e.type == 4 && e.maxSwitchDirection <= 0)
                        {
                            e.direction = 3;
                        }
                        if (collisionTile(enemyCL, levelManager.currentSectionC, 1) && e.type == 4 && e.maxSwitchDirection <= 0)
                        {
                            e.direction = 4;
                        }
                        if (collisionTile(enemyCL, levelManager.currentSectionC, 1) && e.type == 2)
                        {
                            e.x += 1;
                        }
                    }

                    foreach (particle p in particles)
                    {
                        p.movment();
                        p.update(camera);
                        hammerParticleRC = new Rectangle();
                        if(p.hammerPart)
                            hammerParticleRC = new Rectangle((int)p.renderX, (int)p.renderY, p.width, p.height);
                        foreach (enemy e in enemies)
                        {
                            enemyRC = new Rectangle((int)e.renderX, (int)e.renderY, e.width, e.height);
                            if (hammerParticleRC.Intersects(enemyRC) && e.vulnerable)
                            {
                                e.hp = 0;
                            }
                        }
                    }

                    foreach (enemyBullet eb in enemyBullets)
                    {
                        eb.movment();
                        eb.update(camera);
                        ebulletRC = new Rectangle((int)eb.renderX, (int)eb.renderY, eb.width, eb.height);
                        if (playerRC.Intersects(ebulletRC))
                        {
                            player.hp -= 1;
                            player.hit = true;
                            eb.destroy = true;
                        }
                    }

                    foreach (bullet b in bullets)
                    {
                        b.movment();
                        b.update(camera, particles);
                        bulletRC = new Rectangle((int)b.renderX, (int)b.renderY, b.width, b.height);
                        foreach (enemy e in enemies)
                        {
                            enemyRC = new Rectangle((int)e.renderX, (int)e.renderY, e.width, e.height);
                            if (bulletRC.Intersects(enemyRC))
                            {
                                b.destroy = true;
                                if (e.vulnerable)
                                {
                                    if (b.type == 2)
                                    {
                                        e.hp -= 3;
                                        explosions.Add(new explosion(b.x - 16, b.y - 16, 32));
                                    }
                                    else
                                    {
                                        e.hp -= 1;
                                    }
                                }
                            }
                        }
                    }

                    player.movemnt();
                    player.input(bullets, particles, ref camera);
                    player.applyOffset(camera);
                    player.checkHealth(healthbar, particles, levelManager, enemies, bullets, powerUps, ref camera, enemyBullets, bosses);
                    player.animation();

                    if (collisionTile(hammerParticleC, levelManager.currentSectionC, 1))
                    {
                        player.spawnHammerEffectCheck = true;
                    }
                    else
                    {
                        player.spawnHammerEffectCheck = false;
                    }
                    if (collisionTile(playerFeet, levelManager.currentSectionC, 1))
                    {
                        player.onGround = true;
                        player.y -= player.gravity;
                    }
                    else
                    {
                        player.onGround = false;
                    }
                    if (collisionTile(playerBodyL, levelManager.currentSectionC, 1))
                    {
                        player.onWall = true;
                        player.inputActive = false;
                        player.x += 3;
                    }
                    else
                    {
                        player.onWall = false;
                        player.inputActive = true;
                    }
                    if (collisionTile(playerBodyR, levelManager.currentSectionC, 1))
                    {
                        player.onWall = true;
                        player.inputActive = false;
                        player.x -= 3;
                    }
                    else
                    {
                        player.onWall = false;
                        player.inputActive = true;
                    }
                    if (player.inputActive && !player.dead && player.hammerDelay <= 0)
                    {
                        if (keyboard.IsKeyDown(Keys.Left) && keyboard.IsKeyUp(Keys.Right) || keyboard.IsKeyDown(Keys.A) && keyboard.IsKeyUp(Keys.D) || gamepad.ThumbSticks.Left.X == -1.0f && gamepad.ThumbSticks.Left.X != 1.0f)
                        {
                            if (player.animationActive)
                                player.animationCount += 1;
                            player.x -= 3;
                            player.direction = 3;
                        }
                        if (keyboard.IsKeyDown(Keys.Right) && keyboard.IsKeyUp(Keys.Left) || keyboard.IsKeyDown(Keys.D) && keyboard.IsKeyUp(Keys.A) || gamepad.ThumbSticks.Left.X == 1.0f && gamepad.ThumbSticks.Left.X != -1.0f)
                        {
                            if (player.animationActive)
                                player.animationCount += 1;
                            player.x += 3;
                            player.direction = 4;
                        }
                    }
            
                    if (collisionTile(playerHead, levelManager.currentSectionC, 1) && !player.onGround)
                    {
                        player.onRoof = true;
                        if (player.gravityCount > player.gravity + 1)
                        {
                            player.y += player.gravityCount;
                        }
                        else
                        {
                            player.y += player.gravity;
                        }
                        player.jumping = false;
                        player.direction = 0;
                    }
                    else
                    {
                        player.onRoof = false;
                    }
                    if (!collisionTile(playerLadderHead, levelManager.currentSectionC, 2) && player.onLadder)
                    {
                        player.onGround = true;
                        player.onLadder = false;
                        if (keyboard.IsKeyDown(Keys.Down) || keyboard.IsKeyDown(Keys.S) || gamepad.ThumbSticks.Left.Y == -1.0f)
                        {
                            if (player.inputActive)
                            {
                                player.y += 4;
                            }
                        }
                        if (keyboard.IsKeyDown(Keys.Up) || gamepad.ThumbSticks.Left.Y == 1.0f)
                        {
                            player.jumping = true;
                            player.gravityCount = 12;
                        }
                    }
                    if (collisionTile(playerLadderHead, levelManager.currentSectionC, 2) && player.onLadder)
                    {
                        player.jumping = false;
                    }
                    if (collisionTile(playerFeet, levelManager.currentSectionC, 2))
                    {
                        player.onLadder = true;
                        if (keyboard.IsKeyDown(Keys.Up) || keyboard.IsKeyDown(Keys.W) || gamepad.ThumbSticks.Left.Y == 1.0f)
                        {
                            if (player.inputActive)
                            {
                                player.y -= 1;
                            }
                        }
                    }
                    else
                    {
                        player.onLadder = false;
                    }
                    if (collisionTile(playerFeet, levelManager.currentSectionC, 3))
                    {
                        levelManager.section += 1;
                        levelManager.roomTransition(ref player.inputActive, enemies, bullets, player, particles, powerUps, ref camera, enemyBullets, bosses);
                    }
                    if (collisionTile(playerFeet, levelManager.currentSectionC, 4))
                    {
                        if (!player.dead)
                        {
                            player.hit = true;
                        }
                        player.hp = 0;
                    }
                    for (int i = 0; i < bullets.Count; i++)
                    {
                        if (bullets[i].destroy)
                        {
                            bullets.RemoveAt(i);
                        }
                    }
                    for (int i = 0; i < enemyBullets.Count; i++)
                    {
                        if (enemyBullets[i].destroy)
                        {
                            enemyBullets.RemoveAt(i);
                        }
                    }
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        if (enemies[i].destroy)
                        {
                            enemies.RemoveAt(i);
                        }
                    }
                    for (int i = 0; i < particles.Count; i++)
                    {
                        if (particles[i].destroy)
                        {
                            particles.RemoveAt(i);
                        }
                    }
                    for (int i = 0; i < explosions.Count; i++)
                    {
                        if (explosions[i].destroy)
                        {
                            explosions.RemoveAt(i);
                        }
                    }
                    for (int i = 0; i < powerUps.Count; i++)
                    {
                        if (powerUps[i].destroy)
                        {
                            powerUps.RemoveAt(i);
                        }
                    }
                    for (int i = 0; i < bosses.Count; i++)
                    {
                        if (bosses[i].destroy)
                        {
                            bosses.RemoveAt(i);
                        }
                    }
                    break;
        }
            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            switch (gameState)
            {
                case "start screen":
                    break;
                case "menu":
                    foreach (titleCard tc in titleCards)
                    {
                        tc.drawSprite(spriteBatch, spritesheet);
                        menu.drawMenu(spriteBatch, spritesheet, tc.cutSceneCount);
                    }
                    
                    break;
                case "game":
                    if (levelManager.currentLevel != 4)
                    {
                        level.drawLevel(spriteBatch, spritesheet, levelManager.currentSection, camera, levelManager.currentSection.GetLength(1), levelManager.currentSection.GetLength(0));
                    }
                    else
                    {
                        spriteBatch.Draw(backgroundLevel5, new Vector2(0, 0), Color.White);
                        level.drawLevel(spriteBatch, tileSet5, levelManager.currentSection, camera, levelManager.currentSection.GetLength(1), levelManager.currentSection.GetLength(0));
                    }
                    player.drawSpriteOffset(spriteBatch, spritesheet);
                    foreach (bullet b in bullets) { b.drawSpriteOffset(spriteBatch, spritesheet); }
                    foreach (enemy e in enemies) { e.drawSpriteOffset(spriteBatch, spritesheet); }
                    foreach (boss b in bosses) { b.drawSpriteOffset(spriteBatch, spritesheet); }
                    foreach (enemyBullet eb in enemyBullets) { eb.drawSpriteOffset(spriteBatch, spritesheet); }
                    foreach (powerUp pu in powerUps) { pu.drawSpriteOffset(spriteBatch, spritesheet); }
                    foreach (explosion ex in explosions) { ex.drawSpriteOffset(spriteBatch, spritesheet); }
                    foreach (particle p in particles) { p.drawSpriteOffset(spriteBatch, spritesheet); }
                    healthbar.drawSprite(spriteBatch, spritesheet);
                    if (player.gunType == 1)
                    {
                        spriteBatch.DrawString(gameFont, "Pistol: Infinate", new Vector2(500, 30), Color.Coral);
                    }
                    if (player.gunType == 2)
                    {
                        spriteBatch.DrawString(gameFont, "Rocket: " + player.rocketAmmo, new Vector2(500, 30), Color.Coral);
                    }
                    if (player.gunType == 3)
                    {
                        spriteBatch.DrawString(gameFont, "Shotgun: " + player.shotgunAmmo, new Vector2(500, 30), Color.Coral);
                    }
                    if (player.gunType == 4)
                    {
                        spriteBatch.DrawString(gameFont, "Rifle: " + player.rifleAmmo, new Vector2(500, 30), Color.Coral);
                    }
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
