using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace CWIcon
{
    internal class ManagedMouse
    {
        private Point position;
        public Point Position 
        { get 
            {
               position = new Point();
               if (GetCursorPos(out POINT p) == true)
               {
                    position.X = p.x;
                    position.Y = p.y;
               }
                
                return position; 
            } 
        }

        // [DllImport("user32.dll")]
        // private static extern uint SendInput(uint nInputs, LPINPUT pInputs, int cbSize);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint nInputs, Input[] pInputs, int cbSize);

        [DllImport("user32.dll")]
        private static extern IntPtr GetMessageExtraInfo();

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        [Flags]
        public enum InputType
        {
            Mouse = 0,
            Keyboard = 1,
            Hardware = 2
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MouseInput
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct KeyboardInput
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct HardwareInput
        {
            public uint uMsg;
            public ushort wParamL;
            public ushort wParamH;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct InputUnion
        {
            [FieldOffset(0)] public MouseInput mi;
            [FieldOffset(0)] public KeyboardInput ki;
            [FieldOffset(0)] public HardwareInput hi;
        }

        public struct Input
        {
            public int type;
            public InputUnion u;
        }

        [Flags]
        public enum MouseEventF
        {
            Absolute = 0x8000,
            HWheel = 0x01000,
            Move = 0x0001,
            MoveNoCoalesce = 0x2000,
            LeftDown = 0x0002,
            LeftUp = 0x0004,
            RightDown = 0x0008,
            RightUp = 0x0010,
            MiddleDown = 0x0020,
            MiddleUp = 0x0040,
            VirtualDesk = 0x4000,
            Wheel = 0x0800,
            XDown = 0x0080,
            XUp = 0x0100
        }

        public void MoveMouse(int dx, int dy)
        {
            Input moouseMoveInput = new Input();
            moouseMoveInput.type = (int)InputType.Mouse;
            moouseMoveInput.u = new InputUnion
            {
                mi = new MouseInput
                {
                    dx = dx,
                    dy = dy,
                    dwFlags = (uint)MouseEventF.Move,
                    dwExtraInfo = GetMessageExtraInfo()
                }
            };
            Input[] mouseInputs = { moouseMoveInput };

            SendInput((uint)mouseInputs.Length, mouseInputs, Marshal.SizeOf(typeof(Input)));
        }
    }
}
