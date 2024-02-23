using System.Drawing;
using System.Runtime.InteropServices;

namespace DeskCnn.Libs
{
    
    /// <summary>
    /// Win32 API imports.
    /// </summary>
    public static class WinApi
    {
        private const string User32 = "user32.dll";
        

        
        /// <summary>
        /// Creates, updates or deletes the taskbar icon.
        /// </summary>
        [DllImport("shell32.Dll", CharSet = CharSet.Unicode)]
        public static extern bool Shell_NotifyIcon(NotifyCommand cmd, [In] ref NotifyIconData data);


        /// <summary>
        /// Creates the helper window that receives messages from the taskar icon.
        /// </summary>
        [DllImport(User32, EntryPoint = "CreateWindowExW", SetLastError = true)]
        public static extern IntPtr CreateWindowEx(int dwExStyle, [MarshalAs(UnmanagedType.LPWStr)] string lpClassName,
            [MarshalAs(UnmanagedType.LPWStr)] string lpWindowName, int dwStyle, int x, int y,
            int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance,
            IntPtr lpParam);


        /// <summary>
        /// Processes a default windows procedure.
        /// </summary>
        [DllImport(User32)]
        public static extern IntPtr DefWindowProc(IntPtr hWnd, uint msg, IntPtr wparam, IntPtr lparam);

        /// <summary>
        /// Registers the helper window class.
        /// </summary>
        [DllImport(User32, EntryPoint = "RegisterClassW", SetLastError = true)]
        public static extern short RegisterClass(ref WindowClass lpWndClass);

        /// <summary>
        /// Registers a listener for a window message.
        /// </summary>
        /// <param name="lpString"></param>
        /// <returns>uint</returns>
        [DllImport(User32, EntryPoint = "RegisterWindowMessageW")]
        public static extern uint RegisterWindowMessage([MarshalAs(UnmanagedType.LPWStr)] string lpString);

        /// <summary>
        /// Used to destroy the hidden helper window that receives messages from the
        /// taskbar icon.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns>bool</returns>
        [DllImport(User32, SetLastError = true)]
        public static extern bool DestroyWindow(IntPtr hWnd);


        /// <summary>
        /// Gives focus to a given window.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns>bool</returns>
        [DllImport(User32)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);


        /// <summary>
        /// Gets the maximum number of milliseconds that can elapse between a
        /// first click and a second click for the OS to consider the
        /// mouse action a double-click.
        /// </summary>
        /// <returns>The maximum amount of time, in milliseconds, that can
        /// elapse between a first click and a second click for the OS to
        /// consider the mouse action a double-click.</returns>
        [DllImport(User32, CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int GetDoubleClickTime();


        /// <summary>
        /// Gets the screen coordinates of the current mouse position.
        /// </summary>
        [DllImport(User32, SetLastError = true)]
        public static extern bool GetPhysicalCursorPos(ref WPoint lpPoint);


        [DllImport(User32, SetLastError = true)]
        public static extern bool GetCursorPos(out WPoint lpPoint);
        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        public const int GWL_EXSTYLE = -20;
        public const int WS_EX_TOOLWINDOW = 0x00000080;
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public const int SW_HIDE = 0;
        public const int SW_SHOW = 1;
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        public const int WM_SYSCOMMAND = 0x112;
        public const int SC_MINIMIZE = 0xF020;
        public const int GWL_STYLE = -16;
        public const int WS_SYSMENU = 0x80000;
        private const int CP_NOCLOSE_BUTTON = 0x200;
        
        // Import the SetWindowLongPtr function
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);


        [DllImport("user32.dll")]
        static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        const int SW_SHOWMINIMIZED = 2;
        const int SW_RESTORE = 9;


        public static void HideCloseButton(IntPtr hWnd)
        {
            try
            {
                // Get current window styles
                IntPtr style = GetWindowLongPtr(hWnd, GWL_STYLE);
                if (style == IntPtr.Zero)
                {
                    // Handle error gracefully
                    Console.WriteLine("Failed to get window styles.");
                    return;
                }

                // Remove the WS_SYSMENU style to hide the close button
                style = (IntPtr)(style.ToInt64() & ~WS_SYSMENU);

                // Apply the modified styles
                SetWindowLongPtr(hWnd, GWL_STYLE, style);
            }
            catch (Exception ex)
            {
                // Log or handle exceptions appropriately
                Console.WriteLine("Error hiding close button: {0}", ex.Message);
            }
        }
    }
}