using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Pertemuan1
{
    internal class Window : GameWindow
    {
        // constants are much like java. you use a static class.
        static class Constants
        {
            // this is convenient
            public const string shaderPath = "../../../Shaders/";
            //public const string shaderPath = "C:/Users/User/OneDrive/Desktop/College/Semester 6/Grafkom/Pertemuan1/Pertemuan1/Pertemuan1/Shaders/";
            public const float hSecondLength = 0.35f;
            public const float hMinuteLength = 0.30f;
            public const float hHourLength = 0.25f;
        }

        //we have moved the components necessary for making 2d shapes to Asset2d.cs
        // to store objects:
        Asset2d circle = new Asset2d(new float[] { }, new uint[] { });
        Asset2d hand_second = new Asset2d(new float[] { }, new uint[] { });
        Asset2d hand_minute = new Asset2d(new float[] { }, new uint[] { });
        Asset2d hand_hour = new Asset2d(new float[] { }, new uint[] { });

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            // change bg
            GL.ClearColor(.0f, .0f, .0f, .0f); //RGBA

            // make the clock frame
            circle.createCircle(0f, 0f, 0.5f);
            circle.Load(Constants.shaderPath + "shader.vert", Constants.shaderPath + "shader.frag", 0);


            // load the clock hands positions (actually simpler if using sthe time advance function)
            そして時は動き出す(hand_second, hand_minute, hand_hour);


            // to find out how many attrib pointers we have
            GL.GetInteger(GetPName.MaxVertexAttribs, out int maxAttributeCount);
            Console.WriteLine($"Max number of vertex attributes supported : { maxAttributeCount}");
            
        }

        //rendering here
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            //set 
            GL.Clear(ClearBufferMask.ColorBufferBit);


            circle.Render(3); // render with 
            そして時は動き出す(hand_second, hand_minute, hand_hour);
            hand_hour.Render(4); // render as line
            hand_minute.Render(4); // render as line
            hand_second.Render(4); // render as line
            SwapBuffers();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Size.X, Size.Y);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {   
            base.OnUpdateFrame(args);
            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }
            if (KeyboardState.IsKeyReleased(Keys.A))
            {
                Console.WriteLine("AEUGH");
            }
        }

        // this only for mouse intervention
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            // if left click save the position (normalized to 0.0 - 1.0)
            if(e.Button == MouseButton.Left)
            {
                float _x = (MousePosition.X - Size.X / 2) / (Size.X / 2);
                float _y = -(MousePosition.Y - Size.Y / 2) / (Size.Y / 2);
                Console.WriteLine(_x + " | " + _y);
                //line.updateMousePosition(_x, _y, 0
                
            }

        }

        // advancing time
        public void そして時は動き出す(Asset2d hSecond, Asset2d hMinute, Asset2d hHour)
        {
            int h = DateTime.Now.Hour;
            int m = DateTime.Now.Minute;
            int s = DateTime.Now.Second;
            hHour.createClockHand(h, Constants.hHourLength, true);
            hand_second.Load(Constants.shaderPath + "shader.vert", Constants.shaderPath + "shader.frag", 0);
            hMinute.createClockHand(m, Constants.hMinuteLength, false);
            hand_minute.Load(Constants.shaderPath + "bezier.vert", Constants.shaderPath + "bezier.frag", 0);
            hSecond.createClockHand(s, Constants.hSecondLength, false);
            hand_hour.Load(Constants.shaderPath + "kotak.vert", Constants.shaderPath + "kotak.frag", 0);
            //hSecond.createClockHand(h, )
        }
    }
} 
