using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

namespace L2Helper
{
    public static partial class L2Manager
    {
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
            Bitmap bitmap0;
            try
            {
                bitmap0 = (Bitmap)Image.FromFile(@"resources\thp.bmp", true);

                Rectangle bounds = Screen.GetBounds(Point.Empty);
                using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height, PixelFormat.Format32bppRgb))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(new Point(300, 0), Point.Empty, bounds.Size);
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
