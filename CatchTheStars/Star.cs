using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CatchTheStars
{
    public class Star
    {
        public Vector2 Position { get; private set; }
        private Texture2D texture;

        private int screenHeight;
        private float scale = 0.2f; // Scale the star to half its size

        public Star(Texture2D texture, Vector2 initialPosition, int screenHeight = 600)
        {
            this.texture = texture;
            this.Position = initialPosition;
            this.screenHeight = screenHeight;
        }

        public void Update(float speed, GameTime gameTime)
        {
            // Move the star downward
            Position = new Vector2(Position.X, Position.Y + speed * (float)gameTime.ElapsedGameTime.TotalSeconds);

            // Reset position if the star goes off the bottom of the screen
            if (Position.Y > screenHeight)
            {
                ResetPosition();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the star with the scaling factor
            spriteBatch.Draw(texture, Position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }

        public void ResetPosition()
        {
            // Reset the star to a random X position and place it back at the top
            Position = new Vector2(RandomHelper.GetRandomX(), -50);
        }

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, (int)(texture.Width * scale), (int)(texture.Height * scale));
            }
        }
    }

    // A helper class for generating random positions
    public static class RandomHelper
    {
        private static readonly System.Random random = new System.Random();

        public static int GetRandomX(int min = 0, int max = 800) // Screen width default: 800
        {
            return random.Next(min, max);
        }
    }
}
