using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HelloGame
{
    public class HelloGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Ball information
        // TODO: Convert to object?
        private Vector2 ballPosition;
        private Vector2 ballVelocity;
        private Texture2D ballTexture;

        // Notes from lecture on the Hello Game, available at https://www.youtube.com/watch?v=WjrNZ2zLAdA.

        /// <summary>
        /// Constructor for the window. Don't put game logic here.
        /// </summary>
        public HelloGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.Title = "Hello Game";
        }

        /// <summary>
        /// Initialize game logic here.
        /// </summary>
        protected override void Initialize()
        {
            // Add your initialization logic here
            // Position ball in center of screen
            ballPosition = new Vector2(
                GraphicsDevice.Viewport.Width / 2,      // Integer division, but that's fine because
                GraphicsDevice.Viewport.Height / 2      // we need to place the ball in pixel measurements
                );                                      // on screen anyway.

            // Give the ball a random velocity
            System.Random random = new System.Random(); // Not typically as good as game designers want.

            ballVelocity = new Vector2(
                (float) random.NextDouble(),
                (float) random.NextDouble()
                );

            // Normalize to a unit vector
            ballVelocity.Normalize();

            // Scale to 100px of velocity
            ballVelocity *= 100;

            base.Initialize();
        }

        /// <summary>
        /// Load images here because this is called after the graphics device has
        /// been initialized.
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Use this.Content to load your game content here
            // File name includes subfolder names, but not file extensions
            ballTexture = Content.Load<Texture2D>("ball");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Add your update logic here
            // Vector2 overrides the typical arithmetic operators
            // Velocity is now measured in pixels per second
            ballPosition += ballVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Bounce the ball off of the edges of the screen. If the ball can get further off the screen than
            // one frame's velocity, that can create problems.

            // Subtract width of ball because upper-left is origin for the ball
            if (ballPosition.X < GraphicsDevice.Viewport.X ||
                ballPosition.X > GraphicsDevice.Viewport.Width - 64)
            {
                ballVelocity.X *= -1;
            }

            if (ballPosition.Y < GraphicsDevice.Viewport.Y ||
                ballPosition.Y > GraphicsDevice.Viewport.Height - 64)
            {
                ballVelocity.Y *= -1;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Add your drawing code here
            // Batch up sprites and draw them only at the end
            _spriteBatch.Begin();

            // Color used to tint the sprite, not determine the color
            _spriteBatch.Draw(ballTexture, ballPosition, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
