using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace L2Helper
{
    public static partial class L2Manager
    {
        static Random rnd = new Random();
        static int lastTHP;
        static bool AIrunning = false;
        public static bool runOnActive = true;
        public static Character selected;
        public static bool DoStatCheck = false;
        public static bool PickDrop = false;
        public static List<Class> classList = new List<Class>();

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
                    form.Invoke(d, new object[] { });
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
            foreach (Character c in Chars)
            {
                if (c.AIrun && (c.hp.val > 0 || !DoStatCheck))
                {
                    if (c.main)
                    {
                        if (runOnActive)
                        {
                            ActivateProcessWindow(c.p);
                            string log = "char " + Char.name + " hp " + Char.hp.p + "% THP " + THP + "/" + THP.max;

                            if (c.busyUntil < DateTime.Now)
                            {
                                if (THP.val == 0 && lastTHP > 0)
                                {
                                    lastTHP = 0;
                                    SendKeystroke(c.p.MainWindowHandle, VK.ESC);
                                }
                                log += " > clear target";
                                Sleep(100, 20);

                                if ((THP.val == 0 || THP.val == THP.max) && (Char.hp.p > 50 || !DoStatCheck))
                                {
                                    log += " > next target";
                                    SendKeystroke(c.p.MainWindowHandle, VK.F1);
                                    Sleep(200, 20);
                                }
                                else if (PickDrop)
                                {
                                    log += " > picking";
                                    SendKeystroke(c.p.MainWindowHandle, VK.F9);
                                    Sleep(500, 20);
                                    SendKeystroke(c.p.MainWindowHandle, VK.F9);
                                    Sleep(500, 20);
                                    SendKeystroke(c.p.MainWindowHandle, VK.F9);
                                    Sleep(500, 20);
                                }

                                log += " > attack";
                                SendKeystroke(c.p.MainWindowHandle, VK.F2);
                                Sleep(100, 20);

                                foreach (Buff b in c.clas.buffParty) {
                                    if (b.durationEnds < DateTime.Now)
                                    {
                                        if (b.Use(c))
                                            log += "> buff " + b.name;
                                    }
                                }
                                foreach (Skill s in c.clas.dmgSkill)
                                {
                                    if (s.Use(c))
                                        log += "> skill " + s.name;
                                }
                            } else
                            {
                                log += " > busy";
                            }
                            SetAILog(log);

                        }
                    }
                    else
                    {
                        if (c.busyUntil < DateTime.Now)
                        {
                            SetAILog("secondary char > assist | " + c.p.Id);
                            SendKeystroke(c.p.MainWindowHandle, VK.F11);
                            Sleep(200, 20);
                            foreach (Buff b in c.clas.buffParty)
                            {
                                if (b.durationEnds < DateTime.Now)
                                {
                                    b.Use(c);
                                }
                            }
                        }
                        else {
                            SetAILog("secondary char > busy | " + c.p.Id);
                        }
                    }
                }
            }

            if (THP.val == THP.max)
                Sleep(1000, 30);

            return 1;
        }
    }
}
