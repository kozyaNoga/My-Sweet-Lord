using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;

namespace ConsoleApp1
{
    public class Game : GameWindow
    {
        private float i = 0.01f, sec = 0.0f;
        private int fps;
        private int VBOVertex, VBOColor;
        private float[] vertices = new float[] 
        {
            -0.5f, -0.5f, 0.0f,
            0.5f, -0.5f, 0.0f,
            -0.5f, 0.5f, 0.0f,
            0.5f, 0.5f, 0.0f
        };
        private float[] colors = new float[]
        {
            1.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 1.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 1.0f, 0.0f,
            0.8f, 0.6f, 0.2f, 0.0f
        };
        public Game(GameWindowSettings GWsettings, NativeWindowSettings NWsettings) : base(GWsettings, NWsettings)
        {
            VSync = VSyncMode.On;
        }
        protected override void OnLoad()
        {
            
            base.OnLoad();
            GL.ClearColor(0 / 0.0f, 0 / 0.0f, 0 / 0.0f, 0 / 0.0f);
            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);
            initVertexBufferObject();
        }
        protected override void OnResize(ResizeEventArgs e) 
        {
            base.OnResize(e);
        }
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            sec += (float)args.Time;
            if(sec >= 1.0f)
            {
                Title = $"Linear Algebra: fps - {(float)fps}";
                fps = 0;
                sec = 0.0f;
            }
            fps += 1;
            
            base.OnUpdateFrame(args);
        }
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            DrawVertexBufferObject();
            SwapBuffers();
            base.OnRenderFrame(args);
        }
        protected override void OnUnload()
        {
            DeleteVertexBufferObject();
            base.OnUnload();
        }

        //..........................................................................
<<<<<<< HEAD
        // Vertex Buffer Object (VBO)
=======
        // Vertex Buffer Object (VBO) 
>>>>>>> a7351cbed21e808025fec694cac05a56437563de
        private int CreateVertexBufferObject(float[] data)
        {
            int indexVBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, indexVBO);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            return indexVBO;
        }
        private void initVertexBufferObject()
        {
            VBOVertex = CreateVertexBufferObject(vertices);
            VBOColor = CreateVertexBufferObject(colors);
        }
        private void DrawVertexBufferObject()
        {
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.EnableClientState(ArrayCap.ColorArray);

            GL.BindBuffer(BufferTarget.ArrayBuffer, VBOVertex);
            GL.VertexPointer(3, VertexPointerType.Float, 0, 0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, VBOColor);
            GL.ColorPointer(4, ColorPointerType.Float, 0, 0);

            GL.DrawArrays(PrimitiveType.TriangleStrip, 0, 4);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DisableClientState(ArrayCap.VertexArray);
            GL.DisableClientState(ArrayCap.ColorArray);
        }
<<<<<<< HEAD
        private void DeleteVertexBufferObject()
        {
            GL.DeleteBuffer(VBOVertex);
            GL.DeleteBuffer(VBOColor);
        }
=======
>>>>>>> a7351cbed21e808025fec694cac05a56437563de
    }
    class Program
    {
        static void Main(string[] args)
        {
            NativeWindowSettings NWsettings = new NativeWindowSettings()
            {
                Size = new Vector2i(800, 600),
                //Location = new Vector2i(0, 0),
                WindowBorder = WindowBorder.Fixed,
                WindowState = WindowState.Normal,
                Title = "Linear Algebra",

                APIVersion = new Version(3, 3),
                Flags = ContextFlags.Default,
                Profile = ContextProfile.Compatability,
                API = ContextAPI.OpenGL,

                NumberOfSamples = 0

            };
            using(Game game = new Game(GameWindowSettings.Default, NWsettings))
            {
                game.Run();
            }
        }
    }
}