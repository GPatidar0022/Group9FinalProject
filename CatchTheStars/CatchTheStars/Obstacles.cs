using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CatchTheStars
{
    public class Obstacle
    {
        public Vector2 Position { get; private set; }
        private Texture2D texture;

        private int screenHeight;

        public Obstacle(Texture2D texture, Vector2 initialPosition, int screenHeight)
        {
            this.texture = texture;
            this.Position = initialPosition;
            this.screenHeight = screenHeight;
        }

        public void Update(float speed, GameTime gameTime)
        {
            Position = new Vector2(Position.X, Position.Y + speed * (float)gameTime.ElapsedGameTime.TotalSeconds);

            if (Position.Y > screenHeight)
            {
                ResetPosition();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }

        public void ResetPosition()
        {
            Position = new Vector2(RandomHelper.GetRandomX(), -100);
        }

        public Rectangle BoundingBox => new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
    }
}
