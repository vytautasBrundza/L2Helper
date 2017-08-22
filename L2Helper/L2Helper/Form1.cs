using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace L2Helper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            L2Manager.form = this;
            RefreshProcess();
            button5.Text = "RunOnActive:" + L2Manager.runOnActive;
        }

        private void RefreshProcess()
        {
            listBox1.Items.Clear();
            L2Manager.get_process();

            List<Character> CharsToRemove = new List<Character>();
            foreach (Character c in L2Manager.Chars)
            {
                if (!L2Manager.processList.Any(p => p.Id == c.p.Id))
                    CharsToRemove.Add(c);
            }
            foreach (Character c in CharsToRemove)
            {
                L2Manager.Chars.Remove(c);
            }
            foreach (Process p in L2Manager.processList)
            {
                listBox1.Items.Add(p.Id);
                if (!L2Manager.Chars.Any(c => c.p.Id == p.Id))
                    L2Manager.Chars.Add(new Character(p));
            }
            if (L2Manager.charInUse == null && L2Manager.Chars.Count > 0)
                L2Manager.charInUse = L2Manager.Chars[0];
            UpdateLoopStart();
        }

        private void ClickRefresh(object sender, EventArgs e)
        {
            RefreshProcess();
        }

        async void UpdateLoopStart()
        {
            await Task.Run(() => UpdateLoop());
        }

        async void UpdateLoop()
        {
            while (true)
            {
                int t = await Task.Run(() => AppUpdate());

                SetCharBars(L2Manager.charInUse, L2Manager.THP, L2Manager.THPmax);
                Thread.Sleep(100);
            }
        }

        delegate void SetCharBarsCallback(Character c, int thp, int mthp);

        public void SetCharBars(Character c, int thp, int mthp)
        {
            try
            {
                if (this.HPBar.InvokeRequired)
                {
                    SetCharBarsCallback d = new SetCharBarsCallback(SetCharBars);
                    this.Invoke(d, new object[] { c, thp, mthp });
                }
                else
                {
                    this.THPBar.Maximum = mthp;
                    if (thp <= mthp)
                        this.THPBar.Value = thp;
                    this.THPlabel.Text = thp + "/" + mthp;

                    this.HPBar.Maximum = c.mhp;
                    if (c.hp <= c.mhp)
                        this.HPBar.Value = c.hp;
                    this.HPlabel.Text = c.hp + "/" + c.mhp + "/" + c.php + "%";

                    this.MPBar.Maximum = c.mmp;
                    if (c.mp <= c.mmp)
                        this.MPBar.Value = c.mp;
                    this.MPlabel.Text = c.mp + "/" + c.mmp + "/" + c.pmp + "%";

                }
            }
            catch { }
        }

        int AppUpdate()
        {
            foreach (Character c in L2Manager.Chars)
            {
                L2Manager.charInUse = c;
                L2Manager.get_HP();
                L2Manager.get_MHP();
                L2Manager.get_MP();
                L2Manager.get_MMP();
                if (c.active)
                {
                    L2Manager.get_THP();
                }
            }
            return 1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //L2Manager.ActivateProcessWindow(L2Manager.charInUse.pid);
            //L2Manager.SendKeyToWindow("{F1}");
            L2Manager.sendKeystroke(L2Manager.activeProcess.MainWindowHandle, 0x70);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //L2Manager.ActivateProcessWindow(L2Manager.charInUse.pid);
            //L2Manager.SendKeyToWindow("{F2}");
            L2Manager.sendKeystroke(L2Manager.activeProcess.MainWindowHandle, 0x71);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            L2Manager.AILoopStart();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            L2Manager.AILoopStop();
        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                L2Manager.activeProcess = L2Manager.processList.Find(x => x.Id == (int)listBox1.SelectedItem);
                foreach (Character c in L2Manager.Chars)
                {
                    if (c.p.Id == L2Manager.activeProcess.Id)
                        c.active = true;
                    else
                        c.active = false;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AItextBox.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            L2Manager.runOnActive = !L2Manager.runOnActive;
            button5.Text = "RunOnActive:" + L2Manager.runOnActive;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            L2Manager.AILoopStartAll();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            L2Manager.AILoopStopAll();
        }
    }

}
