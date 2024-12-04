using CatchTheStars;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Final_Project_Game
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private int windowWidth = 800;
        private int windowHeight = 600;

        private Texture2D playerTexture, obstacleTexture;

        private Player player;
        private List<Obstacle> obstacles = new List<Obstacle>();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = windowWidth,
                PreferredBackBufferHeight = windowHeight
            };
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // Initialization logic
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            playerTexture = Content.Load<Texture2D>("Player");
            obstacleTexture = Content.Load<Texture2D>("Obstacles");

            // Initialize player
            player = new Player(playerTexture, new Vector2(windowWidth / 2 - playerTexture.Width / 2, windowHeight - 100));

            // Initialize obstacles
            for (int i = 0; i < 3; i++)
            {
                obstacles.Add(new Obstacle(obstacleTexture, new Vector2(i * 200, -100), windowHeight));
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Update player
            player.Update(gameTime, _graphics.PreferredBackBufferWidth);

            // Update obstacles
            foreach (var obstacle in obstacles)
            {
                obstacle.Update(150f, gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);

            _spriteBatch.Begin();

            // Draw player
            player.Draw(_spriteBatch);

            // Draw obstacles
            foreach (var obstacle in obstacles)
                obstacle.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
