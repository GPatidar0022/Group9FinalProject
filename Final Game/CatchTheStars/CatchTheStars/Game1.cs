using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace CatchTheStars
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private int windowWidth = 800;
        private int windowHeight = 600;

        private Texture2D playerTexture, starTexture, obstacleTexture;
        private SpriteFont gameFont;

        private Player player;
        private List<Star> stars = new List<Star>();
        private List<Obstacle> obstacles = new List<Obstacle>();

        private int score = 0;
        private float timer = 60f;

        private SoundEffect starCollectedSound;
        private SoundEffect obstacleCollisionSound;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = windowWidth,
                PreferredBackBufferHeight = windowHeight
            };
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            playerTexture = Content.Load<Texture2D>("player");
            starTexture = Content.Load<Texture2D>("star");
            obstacleTexture = Content.Load<Texture2D>("obstacles");
            gameFont = Content.Load<SpriteFont>("gameFont"); 

            starCollectedSound = Content.Load<SoundEffect>("shooting-star-2-104073"); 
            obstacleCollisionSound = Content.Load<SoundEffect>("collision-83248");    

            player = new Player(playerTexture, new Vector2(windowWidth / 2 - playerTexture.Width / 2, windowHeight - 100));

            for (int i = 0; i < 5; i++)
            {
                stars.Add(new Star(starTexture, new Vector2(RandomHelper.GetRandomX(), -50), windowHeight));
            }

            for (int i = 0; i < 3; i++)
            {
                obstacles.Add(new Obstacle(obstacleTexture, new Vector2(RandomHelper.GetRandomX(), -100), windowHeight));
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (timer > 0)
            {
                // Update timer
                timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                // Update player
                player.Update(gameTime, graphics.PreferredBackBufferWidth);

                // Update stars and check collisions
                foreach (var star in stars)
                {
                    star.Update(100f, gameTime);
                    if (player.BoundingBox.Intersects(star.BoundingBox))
                    {
                        score += 10;
                        star.ResetPosition();
                        starCollectedSound.Play(); // Play sound when a star is collected
                    }
                }

                // Update obstacles and check collisions
                foreach (var obstacle in obstacles)
                {
                    obstacle.Update(150f, gameTime);
                    if (player.BoundingBox.Intersects(obstacle.BoundingBox))
                    {
                        score -= 5;
                        obstacle.ResetPosition();
                        obstacleCollisionSound.Play(); // Play sound on collision
                    }
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            // Draw player
            player.Draw(spriteBatch);

            // Draw stars
            foreach (var star in stars)
                star.Draw(spriteBatch);

            // Draw obstacles
            foreach (var obstacle in obstacles)
                obstacle.Draw(spriteBatch);

            spriteBatch.DrawString(gameFont, $"Score: {score}", new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(gameFont, $"Time: {timer:F1}", new Vector2(10, 40), Color.White);

            if (timer <= 0)
            {
                spriteBatch.DrawString(gameFont, "Game Over!", new Vector2(windowWidth / 2 - 70, windowHeight / 2), Color.Red);
                spriteBatch.DrawString(gameFont, $"Final Score: {score}", new Vector2(windowWidth / 2 - 80, windowHeight / 2 + 30), Color.Red);
            }

           
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}