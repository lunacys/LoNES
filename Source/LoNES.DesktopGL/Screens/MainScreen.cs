using System;
using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;

namespace LoNES.DesktopGL.Screens
{
    public class MainScreen : Screen
    {
        private ImGuiRenderer _imGuiRenderer;
        private Game _game;
        
        private Texture2D _xnaTexture;
        private IntPtr _imGuiTexture;

        private bool _showTestWindow = false;
        
        private System.Numerics.Vector3 clear_color = new System.Numerics.Vector3(114f / 255f, 144f / 255f, 154f / 255f);
        private byte[] _textBuffer = new byte[100];

        private float f = .0f;
        
        public MainScreen(Game game)
        {
            _game = game;
        }

        public override void Initialize()
        {
            _imGuiRenderer = new ImGuiRenderer(_game);
            _imGuiRenderer.RebuildFontAtlas();

            base.Initialize();
        }

        public override void LoadContent()
        {
            _xnaTexture = CreateTexture(_game.GraphicsDevice, 300, 150, pixel =>
            {
                var red = (pixel % 300) / 2;
                return new Color(red, 1, 1);
            });

            _imGuiTexture = _imGuiRenderer.BindTexture(_xnaTexture);
            
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(GameTime gameTime)
        {
            _imGuiRenderer.BeforeLayout(gameTime);
            
            ImGui.Text("Hello, world!");
            ImGui.SliderFloat("float", ref f, 0.0f, 1.0f, string.Empty, 1f);
            ImGui.ColorEdit3("clear color", ref clear_color);
            if (ImGui.Button("Test Window")) _showTestWindow = !_showTestWindow;
            ImGui.Text(string.Format("Application average {0:F3} ms/frame ({1:F1} FPS)", 1000f / ImGui.GetIO().Framerate, ImGui.GetIO().Framerate));

            ImGui.InputText("Text input", _textBuffer, 100);

            ImGui.Text("Texture sample");
            /*ImGui.Image(_imGuiTexture, 
                new System.Numerics.Vector2(300, 150), 
                System.Numerics.Vector2.Zero, 
                System.Numerics.Vector2.One, 
                System.Numerics.Vector4.One, 
                System.Numerics.Vector4.One);*/
            
            if (_showTestWindow)
                ImGui.ShowDemoWindow(ref _showTestWindow);
            
            _imGuiRenderer.AfterLayout();
        }
        
        public static Texture2D CreateTexture(GraphicsDevice device, int width, int height, Func<int, Color> paint)
        {
            //initialize a texture
            var texture = new Texture2D(device, width, height);

            //the array holds the color for each pixel in the texture
            Color[] data = new Color[width * height];
            for(var pixel = 0; pixel < data.Length; pixel++)
            {
                //the function applies the color according to the specified pixel
                data[pixel] = paint( pixel );
            }

            //set the color
            texture.SetData( data );

            return texture;
        }
    }
}