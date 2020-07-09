﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.InteropServices;

namespace game1
{
    public struct IconInfo
    {
        public bool fIcon;
        public int xHotspot;
        public int yHotspot;
        public IntPtr hbmMask;
        public IntPtr hbmColor;
    }
    class CustomCurser
    {
        [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo pIconInfo);
      [DllImport("user32.dll")]
 public static extern IntPtr CreateIconIndirect(ref IconInfo icon);
public static System.Windows.Forms.Cursor CreateCursor(Bitmap bmp , int xHotSpot, int yHotSpot)
{
    IntPtr ptr =bmp.GetHicon();
    IconInfo tmp = new IconInfo();
    GetIconInfo(ptr, ref tmp);
    tmp.xHotspot = xHotSpot;
    tmp.yHotspot = yHotSpot;
    tmp.fIcon = false;
    ptr = CreateIconIndirect(ref tmp);
    return new System.Windows.Forms.Cursor(ptr);
}

    }
}
