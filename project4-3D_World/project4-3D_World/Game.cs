using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Input; //for keyboard input
using OpenTK.Graphics.OpenGL; //for rendering
using System.Drawing; //for colours

namespace project4_3D_World
{
    public class Game : IDisposable
    {
        protected float gCameraY = 0;
        protected float gCameraX = 0;

        protected GameWindow game;

        //constructor
        public Game()
        {
            game = new GameWindow(
                800, //Width
                600, //Height
                new OpenTK.Graphics.GraphicsMode(32, 24, 0, 4), //GraphicsMode
                "First Game" //Title
                );

            game.Load += LoadResources;
            game.Resize += Resize;
            game.KeyDown += Input;
            game.RenderFrame += Render;

            InitGL();
        }

        //run game
        public void Run(double fps)
        {
            game.Run(fps);
        }

        //setup GL
        public bool InitGL()
        {
            gCameraY = 0;
            gCameraX = 0;

            //setup viewport
            GL.Viewport(0, 0, game.Width, game.Height);

            /*
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0.0, game.Width, game.Height, 0.0, 1.0, -1.0);
            */
            
            //initalize projection matrix with perspective
            float aspect_ratio = (float)game.Width / (float)game.Height;
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspect_ratio, 0.1f, 100f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);
            

            //initalize Modelview matrix
            //use modeview instead of projection
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            //check for errors
            ErrorCode error = GL.GetError();
            if (error != ErrorCode.NoError)
            {
                System.Console.WriteLine("Error initializing OpenTK, error: " + error.ToString());
                return false;
            }
            return true;
        }

        protected void LoadResources(object sender, EventArgs e)
        {
            game.VSync = VSyncMode.On;
        }

        protected void Resize(object sender, EventArgs e)
        {
            OpenTK.Graphics.OpenGL.GL.Viewport(0, 0, game.Width, game.Height);
        }

        //user input
        protected void Input(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                game.Exit();
            }
        }

        //update display
        protected void Render(object sender, FrameEventArgs e)
        {
            //set clear colour to black
            GL.Viewport(0, 0, game.Width, game.Height);
            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            //clear the screen
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 lookat = Matrix4.LookAt(new OpenTK.Vector3(10, 20, 30), OpenTK.Vector3.Zero, OpenTK.Vector3.UnitY);
            Matrix4 modelLookAt = Convert(body.MotionState.WorldTransform) * lookat;
            GL.MatrixMode(MatrixMode.Modelview);

            /*
            GL.LoadIdentity();
            GL.Translate(game.Width / 2.0f, game.Height / 2.0f, 0.0f);
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.MidnightBlue);
            GL.Vertex2(-50.0f, -50.0f);
            GL.Vertex2(-50.0f, 50.0f);
            GL.Vertex2(50.0f, 50.0f);
            GL.Vertex2(50.0f, -50.0f);
            GL.End();
            */
            //swap Display memory with virtual memory
            game.SwapBuffers();

        }


        public void Dispose()
        {
            game.Dispose();
        }
    }

}
