using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CatchTheStars
{
    public class Player
    {
        public Vector2 Position { get; private set; }
        private Texture2D texture;

        private float speed = 300f; // Adjusted player speed for smoother gameplay

        public Player(Texture2D texture, Vector2 initialPosition)
        {
            this.texture = texture;
            this.Position = initialPosition;
        }

        public void Update(GameTime gameTime, int screenWidth)
        {
            var keyboardState = Keyboard.GetState();

            // Move left
            if (keyboardState.IsKeyDown(Keys.Left) && Position.X > 0)
            {
                Position = new Vector2(Position.X - speed * (float)gameTime.ElapsedGameTime.TotalSeconds, Position.Y);
            }

            // Move right
            if (keyboardState.IsKeyDown(Keys.Right) && Position.X + texture.Width < screenWidth)
            {
                Position = new Vector2(Position.X + speed * (float)gameTime.ElapsedGameTime.TotalSeconds, Position.Y);
            }

           
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }

        public Rectangle BoundingBox => new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
    }
}
