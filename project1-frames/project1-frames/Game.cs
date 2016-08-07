using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Input; //for keyboard input
using OpenTK.Graphics.OpenGL; //for rendering
using System.Drawing; //for colours

namespace project1_frames
{
    public class Game : IDisposable
    {
        private const int SCREEN_WIDTH = 800;
        private const int SCREEN_HEIGHT = 600;

        GameWindow game;

        public Game()
        {
            game = new GameWindow(
                SCREEN_WIDTH, //Width
                SCREEN_HEIGHT, //Height
                new OpenTK.Graphics.GraphicsMode(32, 24, 0, 4), //GraphicsMode
                "First Game" //Title
                );
            game.Load += LoadResources;
            game.Resize += Resize;
            game.KeyDown += Input;
            game.RenderFrame += Render;

            InitGL();

        }

        public void Run(double fps)
        {
            game.Run(fps);
        }

        protected void InitGL()
        {
            //initalize projection matrix
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);

            //initalize Modelview matrix
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            ErrorCode error = GL.GetError();

            if(error != ErrorCode.NoError)
            {
                System.Console.WriteLine("Error initializing OpenTK, error: " + error.ToString());
            }
        }

        protected void LoadResources( object sender, EventArgs e)
        {
            game.VSync = VSyncMode.On;
        }

        protected void Resize(object sender, EventArgs e )
        {
            OpenTK.Graphics.OpenGL.GL.Viewport(0, 0, game.Width, game.Height);
        }

        protected void Input(object sender, KeyboardKeyEventArgs e)
        {
            if(e.Key == Key.Escape)
            {
                game.Exit();
            }
        }

        protected void Render(object sender, FrameEventArgs e)
        {
            //set clear colour to black
            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            //clear the screen
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //start the shader?
            GL.Begin(PrimitiveType.Triangles);

            GL.Color3(Color.MidnightBlue);
            GL.Vertex3(0.0f, 1.0f, 0.0f);
            GL.Color3(Color.SpringGreen);
            GL.Vertex3(0.0f, 0.0f, 0.0f);
            GL.Color3(Color.Ivory);
            GL.Vertex3(1.0f, 0.0f, 0.0f);

            //end the shader?
            GL.End();

            //swap buffers
            game.SwapBuffers();
        }

        /*
        protected void UpdateFrame( object sender, FrameEventArgs e )
        {
            
            if(game.Keyboard.KeyDown([OpenTK.Input.Key.Escape])
            {
                game.Exit();
            }
        }
        */


        public void Dispose()
        {
            game.Dispose();
        }
    }
}
