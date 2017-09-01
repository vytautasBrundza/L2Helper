using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

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
            /*statCheckBox.Checked = L2Manager.DoStatCheck;
            pickCheckBox.Checked = L2Manager.PickDrop;*/
            statCheckBox.Checked = (bool)Properties.Settings.Default["DoStatCheck"];
            pickCheckBox.Checked = (bool)Properties.Settings.Default["PickDrop"];
            
            L2Manager.Init();
            foreach (Class c in L2Manager.classList) {
                classDropdown.Items.Add(c);
            }
        }

        private void RefreshProcess()
        {
            listBox1.Items.Clear();
            L2Manager.GetProcess();

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
                await Task.Run(() => AppUpdate());

                SetCharBars(L2Manager.charInUse, L2Manager.THP);
                Thread.Sleep(100);
            }
        }

        delegate void SetCharBarsCallback(Character c, BarValue thp);

        public void SetCharBars(Character c, BarValue thp)
        {
            try
            {
                if (this.HPBar.InvokeRequired)
                {
                    SetCharBarsCallback d = new SetCharBarsCallback(SetCharBars);
                    this.Invoke(d, new object[] { c, thp});
                }
                else
                {
                    this.THPBar.Maximum = thp.max;
                    if (thp.val <= thp.max)
                        this.THPBar.Value = thp.val;
                    this.THPlabel.Text = thp.val + "/" + thp.max;

                    this.HPBar.Maximum = c.hp.max;
                    if (c.hp.val <= c.hp.max)
                        this.HPBar.Value = c.hp.val;
                    this.HPlabel.Text = c.hp.val + "/" + c.hp.max + "/" + c.hp.p + "%";

                    this.MPBar.Maximum = c.mp.max;
                    if (c.mp.val <= c.mp.max)
                        this.MPBar.Value = c.mp.val;
                    this.MPlabel.Text = c.mp.val + "/" + c.mp.max + "/" + c.mp.p + "%";

                }
            }
            catch { }
        }

        void AppUpdate()
        {
            try
            {
                foreach (Character c in L2Manager.Chars)
                {
                    L2Manager.charInUse = c;
                    L2Manager.GetHP();
                    L2Manager.GetMHP();
                    L2Manager.GetMP();
                    L2Manager.GetMMP();
                    if (c == L2Manager.selected)
                    {
                        L2Manager.GetTHP();
                    }
                }
            }
            catch { }
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
                Character ch = L2Manager.Chars.Find(x => x.p.Id == L2Manager.activeProcess.Id);
                L2Manager.selected = ch;
                mainCheckBox.Checked = ch.main;
                classDropdown.SelectedItem = classDropdown.Items[classDropdown.FindString(ch.clas.name)];
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

        private void CharMainToggle(object sender, EventArgs e)
        {
            L2Manager.selected.main = mainCheckBox.Checked;
        }

        private void DoStatCheckToggle(object sender, EventArgs e)
        {
            L2Manager.DoStatCheck = statCheckBox.Checked;
            Properties.Settings.Default.DoStatCheck = L2Manager.DoStatCheck;
            Properties.Settings.Default.Save();
        }
        private void pickDoStatCheckToggle(object sender, EventArgs e)
        {
            L2Manager.PickDrop = pickCheckBox.Checked;
            Properties.Settings.Default.PickDrop = L2Manager.PickDrop;
            Properties.Settings.Default.Save();
        }

        private void classSelected(object sender, EventArgs e)
        {
            L2Manager.selected.SetClass(classDropdown.SelectedItem.ToString());
        }

        private void ToFront(object sender, EventArgs e)
        {
            L2Manager.ActivateProcessWindow(L2Manager.selected.p);  
        }

        private void ImportJSON(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }

}
