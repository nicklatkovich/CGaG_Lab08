using CGaG.Lab07;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace CGaG.Lab08 {
    public class MainThread : Game {
        GraphicsDeviceManager Graphics;
        SpriteBatch SpriteBatch;
        Color BackgroundColor = new Color(30, 30, 30);
        Effect DiffusionSphereShapder;
        Texture2D ShaderTexture;
        Vector3 SphereLightPosition = new Vector3(5, 30, -15);
        float angleX = 30f;
        KeyboardState Keyboard;
        Color LightColor = Color.White;

        public MainThread( ) {
            Graphics = new GraphicsDeviceManager(this);
            Graphics.PreferredBackBufferWidth = 640;
            Graphics.PreferredBackBufferHeight = 640;
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += Window_ClientSizeChanged;
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
        }

        private void Window_ClientSizeChanged(Object sender, EventArgs e) {
            Graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
            Graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
            Graphics.ApplyChanges( );
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
                SphereLightPosition.Y--;
            }
            if (Keyboard.IsKeyDown(Keys.Right)) {
                SphereLightPosition.Y++;
            }
            if (Keyboard.IsKeyDown(Keys.Up)) {
                SphereLightPosition.Z--;
            }
            if (Keyboard.IsKeyDown(Keys.Down)) {
                SphereLightPosition.Z++;
            }
            if (Keyboard.IsKeyDown(Keys.W)) {
                angleX++;
            }
            if (Keyboard.IsKeyDown(Keys.S)) {
                angleX--;
            }
            Lab07.SimpleUtils.Median(ref angleX, 0, 90);
            SphereLightPosition.X = 10f - (10f - 1.1f) * (float) Math.Cos(MathHelper.ToRadians(angleX));
            Vector3 vector_to_light = Lab07.SimpleUtils.SphereToCart(SphereLightPosition);
            DiffusionSphereShapder.Parameters["VectorToLight"].SetValue(vector_to_light);
            DiffusionSphereShapder.Parameters["LightColor"].SetValue(new Vector4(LightColor.R / 256f, LightColor.G / 256f, LightColor.B / 256f, LightColor.A / 256f));

            base.Update(time);
        }

        protected override void Draw(GameTime time) {
            GraphicsDevice.Clear(BackgroundColor);

            // TODO: drawing code
            SpriteBatch.Begin(effect: DiffusionSphereShapder);
            int SphereRadius = Math.Min(Window.ClientBounds.Width, Window.ClientBounds.Height) - 128;
            SpriteBatch.Draw(ShaderTexture, new Rectangle(64, 64, SphereRadius, SphereRadius), Color.White);
            SpriteBatch.End( );

            base.Draw(time);
        }
    }
}
