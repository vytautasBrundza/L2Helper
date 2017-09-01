using System;
using System.Collections.Generic;
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
        public static string appName = "Lineage";
        public static Process activeProcess;
        public static List<Process> processList = new List<Process>();
        public static bool running;
        public static Character charInUse;
        public static Character Char
        {
            get
            {
                if (Chars.Count > 0)
                {
                    return charInUse;
                }
                else
                    return new Character(new Process());
            }
            set { }
        }
        public static List<Character> Chars = new List<Character>();
        public static BarValue THP = new BarValue();
        public static Form1 form;

        [DllImport("kernel32.dll")]
        public static extern Int32 ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress,
            [In, Out] byte[] buffer, UInt32 size, out IntPtr lpNumberOfBytesRead);
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        public static void Init()
        {
            DoStatCheck = Properties.Settings.Default.DoStatCheck;
            PickDrop = Properties.Settings.Default.PickDrop;
            ReadDataJSON();
        }
        public static void GetProcess()
        {
            Process[] processes = Process.GetProcesses();
            processList.Clear();
            foreach (Process p in processes)
            {
                if (!String.IsNullOrEmpty(p.MainWindowTitle))
                {
                    if (p.MainWindowTitle.Contains(appName))
                    {
                        processList.Add(p);
                        activeProcess = p;
                    }
                }
            }
        }

        public static void SendKeystroke(IntPtr handle, ushort k)
        {
            //https://stackoverflow.com/questions/10407769/directly-sending-keystrokes-to-another-process-via-hooking
            const uint WM_KEYDOWN = 0x100;
            const uint WM_KEYUP = 0x0101;           

            IntPtr result3 = PostMessage(handle, WM_KEYDOWN, ((IntPtr)k), (IntPtr)0);
            Thread.Sleep(100);
            result3 = PostMessage(handle, WM_KEYUP, ((IntPtr)k), (IntPtr)0);
            Thread.Sleep(50);
        }

        public static void ActivateProcessWindow(int pid)
        {
            IntPtr h = processList.Find(p => p.Id == pid).MainWindowHandle;
            SetForegroundWindow(h);
        }
        public static void ActivateProcessWindow(Process p)
        {
            IntPtr h = p.MainWindowHandle;
            SetForegroundWindow(h);
        }
        public static int ReadInt(UInt32[] address)
        {
            try
            {
                UInt32 Address = address[0];
                UInt32 Ptr = (UInt32)ReadInt32(Address, 4, activeProcess.Handle);
                for (int i = 1; i < address.Length; i++)
                {
                    Ptr = (UInt32)ReadInt32(Ptr + address[i], 4, activeProcess.Handle);
                }
                return new IntPtr(Ptr).ToInt32();
            }
            catch
            {
                return 0;
            }
        }

        public static byte[] ReadBytes(IntPtr Handle, Int64 Address, uint BytesToRead)
        {
            byte[] buffer = new byte[BytesToRead];
            ReadProcessMemory(Handle, new IntPtr(Address), buffer, BytesToRead, out IntPtr ptrBytesRead);
            return buffer;
        }

        public static int ReadInt32(long Address, uint length = 4, IntPtr? Handle = null)
        {
            return BitConverter.ToInt32(ReadBytes((IntPtr)Handle, Address, length), 0);
        }

        public static UInt32 GetDllAddress(Process p, string ModuleName)
        {
            ProcessModuleCollection modules = activeProcess.Modules;
            ProcessModule dllBaseAdressIWant = null;
            foreach (ProcessModule i in modules)
            {
                if (i.ModuleName == ModuleName)
                {
                    dllBaseAdressIWant = i;
                }
            }
            if (dllBaseAdressIWant == null)
                throw new Exception("DLL address not found!");

            return (UInt32)dllBaseAdressIWant.BaseAddress.ToInt32();
        }

        public static List<Point> FindBitmapsEntry(Bitmap sourceBitmap, Bitmap serchingBitmap)
        {
            #region Arguments check

            if (sourceBitmap == null || serchingBitmap == null)
                throw new ArgumentNullException();

            if (sourceBitmap.PixelFormat != serchingBitmap.PixelFormat)
                throw new ArgumentException("Pixel formats arn't equal " + sourceBitmap.PixelFormat + "-" + serchingBitmap.PixelFormat);

            if (sourceBitmap.Width < serchingBitmap.Width || sourceBitmap.Height < serchingBitmap.Height)
                throw new ArgumentException("Size of serchingBitmap bigger then sourceBitmap");

            #endregion

            var pixelFormatSize = Image.GetPixelFormatSize(sourceBitmap.PixelFormat) / 8;

            // Copy sourceBitmap to byte array
            var sourceBitmapData = sourceBitmap.LockBits(new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height),
                ImageLockMode.ReadOnly, sourceBitmap.PixelFormat);
            var sourceBitmapBytesLength = sourceBitmapData.Stride * sourceBitmap.Height;
            var sourceBytes = new byte[sourceBitmapBytesLength];
            Marshal.Copy(sourceBitmapData.Scan0, sourceBytes, 0, sourceBitmapBytesLength);
            sourceBitmap.UnlockBits(sourceBitmapData);

            // Copy serchingBitmap to byte array
            var serchingBitmapData =
                serchingBitmap.LockBits(new Rectangle(0, 0, serchingBitmap.Width, serchingBitmap.Height),
                    ImageLockMode.ReadOnly, serchingBitmap.PixelFormat);
            var serchingBitmapBytesLength = serchingBitmapData.Stride * serchingBitmap.Height;
            var serchingBytes = new byte[serchingBitmapBytesLength];
            Marshal.Copy(serchingBitmapData.Scan0, serchingBytes, 0, serchingBitmapBytesLength);
            serchingBitmap.UnlockBits(serchingBitmapData);

            var pointsList = new List<Point>();

            // Serching entries
            // minimazing serching zone
            // sourceBitmap.Height - serchingBitmap.Height + 1
            for (var mainY = 0; mainY < sourceBitmap.Height - serchingBitmap.Height + 1; mainY++)
            {
                var sourceY = mainY * sourceBitmapData.Stride;

                for (var mainX = 0; mainX < sourceBitmap.Width - serchingBitmap.Width + 1; mainX++)
                {// mainY & mainX - pixel coordinates of sourceBitmap
                    // sourceY + sourceX = pointer in array sourceBitmap bytes
                    var sourceX = mainX * pixelFormatSize;

                    var isEqual = true;
                    for (var c = 0; c < pixelFormatSize; c++)
                    {// through the bytes in pixel
                        if (sourceBytes[sourceX + sourceY + c] == serchingBytes[c])
                            continue;
                        isEqual = false;
                        break;
                    }

                    if (!isEqual) continue;

                    var isStop = false;

                    // find fist equalation and now we go deeper) 
                    for (var secY = 0; secY < serchingBitmap.Height; secY++)
                    {
                        var serchY = secY * serchingBitmapData.Stride;

                        var sourceSecY = (mainY + secY) * sourceBitmapData.Stride;

                        for (var secX = 0; secX < serchingBitmap.Width; secX++)
                        {// secX & secY - coordinates of serchingBitmap
                            // serchX + serchY = pointer in array serchingBitmap bytes

                            var serchX = secX * pixelFormatSize;

                            var sourceSecX = (mainX + secX) * pixelFormatSize;

                            for (var c = 0; c < pixelFormatSize; c++)
                            {// through the bytes in pixel
                                if (sourceBytes[sourceSecX + sourceSecY + c] == serchingBytes[serchX + serchY + c]) continue;

                                // not equal - abort iteration
                                isStop = true;
                                break;
                            }

                            if (isStop) break;
                        }

                        if (isStop) break;
                    }

                    if (!isStop)
                    {// serching bitmap is founded!!
                        pointsList.Add(new Point(mainX, mainY));
                    }
                }
            }
            return pointsList;
        }

        static void Sleep(int time, int rand)
        {
            Thread.Sleep(time + rnd.Next(rand));
        }
    }
}
