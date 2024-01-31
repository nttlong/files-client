﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XWindows
{
    
    public static class XWindowsExtension
    {
        public static IntPtr Hwnd { get; set; }
        public static IntPtr HwndIcon { get; set; }

        public static void SetIcon(string iconFilename)
        {
            if (Hwnd == IntPtr.Zero)
                return;

            HwndIcon = PInvoke.User32.LoadImage(IntPtr.Zero, iconFilename,
               PInvoke.User32.ImageType.IMAGE_ICON, 16, 16, PInvoke.User32.LoadImageFlags.LR_LOADFROMFILE);

            PInvoke.User32.SendMessage(Hwnd, PInvoke.User32.WindowMessage.WM_SETICON, (IntPtr)0, HwndIcon);
        }

        public static void BringToFront()
        {
            PInvoke.User32.ShowWindow(Hwnd, PInvoke.User32.WindowShowStyle.SW_SHOW);
            PInvoke.User32.ShowWindow(Hwnd, PInvoke.User32.WindowShowStyle.SW_RESTORE);

            _ = PInvoke.User32.SetForegroundWindow(Hwnd);
        }

        public static void MinimizeToTray()
        {
            PInvoke.User32.ShowWindow(Hwnd, PInvoke.User32.WindowShowStyle.SW_MINIMIZE);
            PInvoke.User32.ShowWindow(Hwnd, PInvoke.User32.WindowShowStyle.SW_HIDE);
            PInvoke.User32.SendMessage(Hwnd, PInvoke.User32.WindowMessage.WM_SETICON, (IntPtr)0, HwndIcon);
        }
    }
}
