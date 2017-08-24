using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;

using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;

using System.Drawing;
using System.Drawing.Imaging;

namespace L2Helper
{
    public static partial class L2Manager
    {
        /*
        private static int UseAddress = 1;
        public static Dictionary<string,UInt32[][]> AddressBook= new Dictionary<string,uint[][]> {
            ["hp"] = new UInt32[][] {   new UInt32[] { GetDllAddress(Char.p, "NWindow.DLL") + 0x00528BD8, 0xA4, 0x1F4, 0x2E4, 0x34, 0x234 },
                                        new UInt32[] { GetDllAddress(Char.p, "NWindow.DLL") + 0x006705DC, 0xA0, 0xF4, 0x5A0, 0x1C4, 0x234 }},
            ["mhp"] = new UInt32[][] {  new UInt32[] { GetDllAddress(Char.p, "NWindow.DLL") + 0x00528BD8, 0xA4, 0x1F4, 0x2E4, 0x34, 0x234 },
                                        new UInt32[] { GetDllAddress(Char.p, "NWindow.DLL") + 0x006705DC, 0xA0, 0xF4, 0x5A0, 0x1C4, 0x22C }},
            ["mp"] = new UInt32[][] {   new UInt32[] { GetDllAddress(Char.p, "NWindow.DLL") + 0x00528BD8, 0xA4, 0x1F4, 0x2E4, 0x34, 0x534 },
                                        new UInt32[] { GetDllAddress(Char.p, "NWindow.DLL") + 0x006705DC, 0xA0, 0xF4, 0x5A0, 0x1C4, 0x534 }},
            ["mmp"] = new UInt32[][] {  new UInt32[] { GetDllAddress(Char.p, "NWindow.DLL") + 0x00528BD8, 0xA4, 0x1F4, 0x2E4, 0x34, 0x52C },
                                        new UInt32[] { GetDllAddress(Char.p, "NWindow.DLL") + 0x006705DC, 0xA0, 0xF4, 0x5A0, 0x1C4, 0x52C}},          
        };
        */
        /* Reading address algorithm (reduced to ReadInt function):
        UInt32 Address = GetDllAddress("NWindow.DLL")+0x00528BD8;
        UInt32 Ptr1 = (UInt32)ReadInt32(Address, 4, target.Handle);
        UInt32 Ptr2 = (UInt32)ReadInt32(Ptr1 + 0xA4, 4, target.Handle);
        UInt32 Ptr3 = (UInt32)ReadInt32(Ptr2 + 0x1F4, 4, target.Handle);
        UInt32 Ptr4 = (UInt32)ReadInt32(Ptr3 + 0x2E4, 4, target.Handle);
        UInt32 Ptr5 = (UInt32)ReadInt32(Ptr4 + 0x34, 4, target.Handle);
        UInt32 Ptr6 = (UInt32)ReadInt32(Ptr5 + 0x234, 4, target.Handle);
        IntPtr ptr = new IntPtr(Ptr6);
        return ptr.ToInt32();*/

        public static int GetHP()
        {
            //Windows 7 before Update 0x00528BD8, 0xA4, 0x1F4, 0x2E4, 0x34, 0x234
            Char.hp.SetValue(ReadInt(new UInt32[] { GetDllAddress(Char.p, "NWindow.DLL") + 0x006705DC, 0xA0, 0xF4, 0x5A0, 0x1C4, 0x234 }));
            return Char.hp.val;
        }

        public static int GetMHP()
        {
            //Windows 7 before Update 0x00528BD8, 0xA4, 0x1F4, 0x2E4, 0x34, 0x234  
            Char.hp.max = ReadInt(new UInt32[] { GetDllAddress(Char.p, "NWindow.DLL") + 0x006705DC, 0xA0, 0xF4, 0x5A0, 0x1C4, 0x22C });
            return Char.hp.max;
        }

        public static int GetMP()
        {
            //Windows 7 before Update 0x00528BD8, 0xA4, 0x1F4, 0x2E4, 0x34, 0x534 
            Char.mp.SetValue(ReadInt(new UInt32[] { GetDllAddress(Char.p, "NWindow.DLL") + 0x006705DC, 0xA0, 0xF4, 0x5A0, 0x1C4, 0x534 }));
            return Char.mp.val;
        }

        public static int GetMMP()
        {
            //Windows 7 before Update 0x00528BD8, 0xA4, 0x1F4, 0x2E4, 0x34, 0x52C
            Char.mp.val = ReadInt(new UInt32[] { GetDllAddress(Char.p, "NWindow.DLL") + 0x006705DC, 0xA0, 0xF4, 0x5A0, 0x1C4, 0x52C });
            return Char.mp.val;
        }

        public static int GetTHP()
        {
            //RECT rct = new RECT();
            Bitmap bitmap0;
            try
            {
                bitmap0 = (Bitmap)Image.FromFile(@"resources\thp.bmp", true);

                Rectangle bounds = Screen.GetBounds(Point.Empty);
                using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height, PixelFormat.Format32bppRgb))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        //g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                        g.CopyFromScreen(new Point(300, 0), Point.Empty, bounds.Size);
                        //GetWindowRect(activeProcess.Handle, ref rct);
                        //g.CopyFromScreen(new Point(rct.Left, rct.Top), Point.Empty, new Size(rct.Right-rct.Left, rct.Bottom-rct.Top));
                    }

                    int count = FindBitmapsEntry(bitmap, bitmap0).Count;
                    if (count > THP.max)
                        THP.max = count;
                    THP.val = count;
                    return count;
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("There was an error opening the bitmap." +
                    "Please check the path.");
            }
            return 0;
        }

    }
}
