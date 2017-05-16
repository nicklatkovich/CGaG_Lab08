using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CGaG.Lab08 {
    public class MainThread : Game {
        GraphicsDeviceManager Graphics;
        SpriteBatch SpriteBatch;
        Color BackgroundColor = new Color(30, 30, 30);

        public MainThread( ) {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
        }

        protected override void Initialize( ) {
            // TODO: initialization logic

            base.Initialize( );
        }

        protected override void LoadContent( ) {
            // create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: load content
        }

        protected override void UnloadContent( ) {
            // TODO: Unload any non ContentManager content
        }

        protected override void Update(GameTime time) {
            if (Keyboard.GetState( ).IsKeyDown(Keys.Escape)) {
                Exit( );
            }

            // TODO: update logic

            base.Update(time);
        }

        protected override void Draw(GameTime time) {
            GraphicsDevice.Clear(BackgroundColor);

            // TODO: drawing code

            base.Draw(time);
        }
    }
}
