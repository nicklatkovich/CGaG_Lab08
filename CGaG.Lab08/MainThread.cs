using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CGaG.Lab08 {
    public class MainThread : Game {
        GraphicsDeviceManager Graphics;
        SpriteBatch SpriteBatch;
        Color BackgroundColor = Color.Black;
        Effect DiffusionSphereShapder;
        Texture2D ShaderTexture;

        public MainThread( ) {
            Graphics = new GraphicsDeviceManager(this);
            Graphics.PreferredBackBufferWidth = 640;
            Graphics.PreferredBackBufferHeight = 640;
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
        }

        protected override void Initialize( ) {
            // TODO: initialization logic
            ShaderTexture = new Texture2D(GraphicsDevice, 2, 2);
            ShaderTexture.SetData(new Color[ ] {
                Color.White, Color.White, Color.White, Color.White
            });

            base.Initialize( );
        }

        protected override void LoadContent( ) {
            // create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: load content
            DiffusionSphereShapder = Content.Load<Effect>("GLSLDiffusionSphere");
        }

        protected override void UnloadContent( ) {
            // TODO: Unload any non ContentManager content
        }

        protected override void Update(GameTime time) {
            if (Keyboard.GetState( ).IsKeyDown(Keys.Escape)) {
                Exit( );
            }

            // TODO: update logic
            Vector3 VectorToLight = new Vector3((float)time.TotalGameTime.TotalSeconds, 0, 0);
            DiffusionSphereShapder.Parameters["VectorToLight"].SetValue(VectorToLight);
            DiffusionSphereShapder.Parameters["VectorToLightLength"].SetValue(VectorToLight.Length( ));

            base.Update(time);
        }

        protected override void Draw(GameTime time) {
            GraphicsDevice.Clear(BackgroundColor);

            // TODO: drawing code
            SpriteBatch.Begin(effect: DiffusionSphereShapder);
            SpriteBatch.Draw(ShaderTexture, new Rectangle(64, 64, 512, 512), Color.White);
            SpriteBatch.End( );

            base.Draw(time);
        }
    }
}
