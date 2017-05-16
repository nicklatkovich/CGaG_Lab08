using CGaG.Lab07;
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
        Vector2 SphereLightDirection = new Vector2(0, 0);
        KeyboardState Keyboard;

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
            Keyboard = SimpleUtils.GetKeyboardState( );
            if (Keyboard.IsKeyDown(Keys.Escape)) {
                Exit( );
            }

            // TODO: update logic

            if (Keyboard.IsKeyDown(Keys.Left)) {
                SphereLightDirection.X--;
            }
            if (Keyboard.IsKeyDown(Keys.Right)) {
                SphereLightDirection.X++;
            }
            if (Keyboard.IsKeyDown(Keys.Up)) {
                SphereLightDirection.Y--;
            }
            if (Keyboard.IsKeyDown(Keys.Down)) {
                SphereLightDirection.Y++;
            }
            Vector3 vector_to_light = Lab07.SimpleUtils.SphereToCart(new Vector3(1f, SphereLightDirection.X, SphereLightDirection.Y));
            DiffusionSphereShapder.Parameters["VectorToLight"].SetValue(vector_to_light);
            DiffusionSphereShapder.Parameters["VectorToLightLength"].SetValue(vector_to_light.Length( ));

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
