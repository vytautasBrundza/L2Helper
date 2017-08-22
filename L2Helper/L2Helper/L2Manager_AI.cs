using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;

using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;



namespace L2Helper
{

    public static partial class L2Manager
    {
        static Random rnd = new Random();
        static int lastTHP;
        static bool AIrunning = false;
        public static bool runOnActive = true;

        public static async void AILoopStart()
        {
            if (!Char.AIrun)
            {
                Char.AIrun = true;
                if (!AIrunning)
                {
                    AIrunning = true;
                    await Task.Run(() => AILoop());
                }
                SetAILog("AI start for " + Char.p.Id);
            }
        }

        public static async void AILoopStartAll()
        {
            if (!AIrunning)
            {
                AIrunning = true;
                await Task.Run(() => AILoop());
            }
            foreach (Character c in Chars)
            {
                c.AIrun = true;
            }
            SetAILog("AI start: ALL");

        }

        public static async void AILoopStop()
        {
            if (Char.AIrun)
            {
                Char.AIrun = false;
                SetAILog("AI stop for " + Char.p.Id);
                bool oldAIrunning = true;
                AIrunning = false;
                foreach (Character c in Chars)
                {
                    c.AIrun = true;
                }
                if (oldAIrunning != AIrunning)
                {
                    SetAILog("<< AI stop >>");
                }
            }
        }

        public static async void AILoopStopAll()
        {
            foreach (Character c in Chars)
            {
                c.AIrun = false;
            }
            AIrunning = false;
            SetAILog("AI stop: ALL");
            SetAILog("<< AI stop >>");
        }

        static async void AILoop()
        {
            SetAILog("<< AI start>>");
            while (AIrunning)
            {

                int t = await Task.Run(() => AIUpdate());
                int sleepTime = 2000;
                sleepTime -= Chars.Count * 300;
                if (sleepTime < 0)
                    sleepTime = 0;
                Sleep(sleepTime, 100);
            }
        }

        delegate void SetAILogCallback(string text);

        public static void SetAILog(string text)
        {
            try
            {
                if (form.AItextBox.InvokeRequired)
                {
                    SetAILogCallback d = new SetAILogCallback(SetAILog);
                    form.Invoke(d, new object[] { text });
                }
                else
                {
                    form.AItextBox.Text = text + "\r\n" + form.AItextBox.Text;
                }
            }
            catch { }
        }

        delegate void ClearAILogCallback();

        public static void ClearAILog()
        {
            try
            {
                if (form.AItextBox.InvokeRequired)
                {
                    ClearAILogCallback d = new ClearAILogCallback(ClearAILog);
                    form.Invoke(d, new object[] {});
                }
                else
                {
                    form.AItextBox.Text = "";
                }
            }
            catch { }
        }


        static int AIUpdate()
        {
            ClearAILog();
            Sleep(500, 100);
            foreach (Character c in Chars)
            {
                if (c.AIrun && c.hp>0)
                {
                    if (c.active)
                    {
                        if (runOnActive)
                        {
                            ActivateProcessWindow(c.p);
                            string log = "char " + Char.name + " hp " + Char.php + "% THP " + THP + "/" + THPmax;

                            if (THP == 0 && lastTHP > 0)
                            {
                                lastTHP = 0;
                                //SendKeys.SendWait("{ESC}");	
                                sendKeystroke(c.p.MainWindowHandle, VK.ESC);

                            }
                            log += " > clear target";
                            Sleep(100, 20);

                            if ((THP == 0 || THP == THPmax) && Char.php > 50)
                            {
                                log += " > next target";
                                //SendKeys.SendWait("{F1}");
                                sendKeystroke(c.p.MainWindowHandle, VK.F1);
                                Sleep(200, 20);
                            }
                            else if (Char.php < 50)
                            {
                                log += " > low health > pick";
                                //SendKeys.SendWait("{F4}");
                                sendKeystroke(c.p.MainWindowHandle, VK.F9);
                                Sleep(500, 20);
                                //SendKeys.SendWait("{F4}");
                                sendKeystroke(c.p.MainWindowHandle, VK.F9);
                                Sleep(500, 20);
                                //SendKeys.SendWait("{F4}");
                                sendKeystroke(c.p.MainWindowHandle, VK.F9);
                                Sleep(500, 20);
                            }

                            log += " > attack";
                            //SendKeys.SendWait("{F2}");
                            sendKeystroke(c.p.MainWindowHandle, VK.F2);
                            Sleep(100, 20);

                            SetAILog(log);
                        }
                    }
                    else
                    {
                        SetAILog("secondary char > assist | "+c.p.Id);
                        sendKeystroke(c.p.MainWindowHandle, VK.F5);
                        Sleep(200, 20);
                    }
                }
            }

            if (THP == THPmax)
                Sleep(1000, 30);

            return 1;
        }

        static void Sleep(int time, int rand)
        {
            Thread.Sleep(time + rnd.Next(rand));
        }

    }
}
