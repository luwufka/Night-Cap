using System;
using System.Runtime.InteropServices;

namespace NightCap.Core
{
    public class Window
    {
        public enum Style { Dark, Light }

        public Style WindowStyle;
        public IntPtr Handle;

        public Window(IntPtr handle, Style style) 
        {
            Handle = handle;
            WindowStyle = style;
        }

        public void IsDark(bool style)
        {
            Win32.DwmSetWindowAttribute(this.Handle, Win32.DWMWA_USE_IMMERSIVE_DARK_MODE, ref style, Marshal.SizeOf(typeof(bool)));
        }
    }
}
