using System;
using System.Runtime.InteropServices;

namespace NightCap.Core
{
    public class Updater
    {
        private IntPtr _hookID = IntPtr.Zero;
        private GCHandle _delegateHandle;

        private readonly Win32.WinEventDelegate _winEventProc;

        public event Action<Window> WindowCreated;

        public Updater()
        {
            _winEventProc = WinEventCallback;
            _delegateHandle = GCHandle.Alloc(_winEventProc);
        }

        public void Start()
        {
            _hookID = Win32.SetWinEventHook( Win32.EventObjectCreate, Win32.EventObjectCreate, IntPtr.Zero, _winEventProc, 0, 0, Win32.WineventOutOfContext);
            if (_hookID == IntPtr.Zero)
            {
                throw new InvalidOperationException("Failed to set window creation hook.");
            }
        }

        public void Stop()
        {
            if (_hookID != IntPtr.Zero)
            {
                Win32.UnhookWinEvent(_hookID);
                _hookID = IntPtr.Zero;
            }
        }

        private void WinEventCallback(IntPtr hWinEventHook, uint eventType, IntPtr hWnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            if (eventType != Win32.EventObjectCreate || hWnd == IntPtr.Zero || idObject != Win32.OBJID_WINDOW || !IsTopWindow(hWnd))
            {
                return;
            }
            WindowCreated?.Invoke(new Window(hWnd, GetWindowStyle(hWnd)));
        }

        private static bool IsTopWindow(IntPtr hWnd) => (Win32.GetWindowLong(hWnd, Win32.GWL_STYLE) & Win32.WS_CHILD) == 0;

        private static Window.Style GetWindowStyle(IntPtr hWnd)
        {
            return Win32.DwmGetWindowAttribute(hWnd, Win32.DWMWA_USE_IMMERSIVE_DARK_MODE, out int isDarkMode, sizeof(int)) == 1 ? Window.Style.Dark : Window.Style.Light;
        }
    }
}
