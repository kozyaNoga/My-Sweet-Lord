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
        private int fps, DLobj;
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
            0.8f, 0.6f, 0.2f, 1.0f
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
            //DLobj = CreateDisplayList();
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
            //GL.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            //GL.PointSize(3);
            //GL.Begin(PrimitiveType.Points);
            //GL.Vertex2(0.0f, 0.0f);
            //GL.End();

            //..........................................................................
            // (DL)
            //DrawDisplayList();

            //..........................................................................
            // (VA)
            DrawVertexArray();

            SwapBuffers();
            base.OnRenderFrame(args);
        }
        protected override void OnUnload()
        {
            base.OnUnload();
            //DeleteDisplayList();
        }
        private int CreateDisplayList()
        {
            int index = GL.GenLists(1);
            GL.NewList(index, ListMode.Compile);
            GL.Begin(PrimitiveType.LineLoop);
            GL.PointSize(30);
            //GL.Color3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(0.5f, 0.0f, 0.0f);
            GL.Vertex3(0.0f, 0.5f, 0.0f);
            GL.Vertex3(0.0f, 0.0f, 0.0f);
            GL.End();
            GL.EndList();
            return index;
        }
        private void DrawDisplayList()
        {
            GL.CallList(DLobj);
        }
        private void DeleteDisplayList()
        {
            GL.DeleteLists(DLobj, 1);
        }
        //..........................................................................
        //Vertex Array (VA)
        public void DrawVertexArray()
        {
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.VertexPointer(3, VertexPointerType.Float, 0, vertices);

            GL.EnableClientState(ArrayCap.ColorArray);
            GL.ColorPointer(4,ColorPointerType.Float, 0, colors);


            GL.DrawArrays(PrimitiveType.TriangleStrip, 0, 4); 
            GL.DisableClientState(ArrayCap.VertexArray);
            GL.DisableClientState(ArrayCap.ColorArray);
        }

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