using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace L2Helper
{
    public class Character
    {
        public string name;
        public byte lvl;
        public bool main;
        public Process p;
        public BarValue cp;
        public BarValue hp;
        public BarValue mp;
        public bool AIrun;
        public Class clas = new Class("");
        public DateTime busyUntil = DateTime.Now;

        public Character(Process _p)
        {
            p = _p;
            cp = new BarValue();
            hp = new BarValue();
            mp = new BarValue();
        }

        public void SetClass(string newClassName)
        {

            if (newClassName != clas.name)
            {
                clas = new Class(newClassName);
            }
        }
    }

    public class BarValue
    {
        public int val;
        public int max;
        public int p;
        public void SetValue(int _val)
        {
            val = _val;
            if (max > 0)
                p = 100 * val / max;
        }
    }

    public class Class
    {
        public string name;
        public ClassType type = ClassType.dmg;
        public bool autoAttackPreferred = true;
        public List<Buff> buffSelf = new List<Buff>();
        public List<Buff> buffParty = new List<Buff>();
        public List<Buff> heal = new List<Buff>();
        public List<Buff> recharge = new List<Buff>();
        public List<Skill> dmgSkill = new List<Skill>();
        public DateTime busyUntil = DateTime.Now;

        public Class(string _name)
        {
            name = _name;
            /*
            switch (name)
            {
                case "Warcryer":
                    type = ClassType.support;
                    autoAttackPreferred = true;
                    buffParty.Add(new Buff("MainBuff", 20000, VK.F10, 1200000, 20000));
                    break;
                case "BladeDancer":
                    type = ClassType.support;
                    autoAttackPreferred = true;
                    buffParty.Add(new Buff("Dance", 5000, VK.F10, 122000, 10000));
                    break;
                case "SwordSinger":
                    type = ClassType.support;
                    autoAttackPreferred = true;
                    buffParty.Add(new Buff("Sing", 5000, VK.F10, 122000, 10000));
                    break;
                case "Tank":
                    type = ClassType.tank;
                    autoAttackPreferred = true;
                    break;
                case "":
                default:
                    type = ClassType.dmg;
                    autoAttackPreferred = true;
                    break;
            }
            */
        }

        public override string ToString()
        {
            return name;
        }
    }

    public enum ClassType
    {
        dmg,
        tank,
        support,
        nuker
    };

    public class Skill
    {
        public List<string> classList = new List<string>();
        public string name;
        public bool targetReq; //requires target
        public int cd; //cooldown
        public ushort ks; //keyboard shortcut
        public DateTime cdrTime; // cooldown reset time
        public int cTime; // cast time
        public byte minLVL;
        public Skill(string _name, int _cd, ushort _ks, int _ct = 0, bool _targetReq = false)
        {
            name = _name;
            targetReq = _targetReq;
            cd = _cd;
            ks = _ks;
            cTime = _ct;
            cdrTime = DateTime.Now;
        }
        public Skill()
        {
            cdrTime = DateTime.Now;
        }
        public void Use(Character c)
        {
            if (cdrTime < DateTime.Now)
            {
                L2Manager.SendKeystroke(c.p.MainWindowHandle, this.ks);
                cdrTime = DateTime.Now.AddMilliseconds(this.cd);
                if (cTime > 0)
                {
                    c.busyUntil = DateTime.Now.AddMilliseconds(this.cd);
                }
            }
        }
    }

    public class Buff : Skill
    {
        public int duration;
        public DateTime durationEnds;
        public bool party;
        public bool self;
        public Buff(string _name, int _cd, ushort _ks, int _duration, int _ct = 0, bool _targetReq = false)
        {
            name = _name;
            targetReq = _targetReq;
            cd = _cd;
            ks = _ks;
            cTime = _ct;
            cdrTime = DateTime.Now;
            durationEnds = DateTime.Now;
            duration = _duration;
        }

        public bool Use(Character c)
        {
            if (cdrTime < DateTime.Now)
            {
                L2Manager.SendKeystroke(c.p.MainWindowHandle, this.ks);
                cdrTime = DateTime.Now.AddMilliseconds(this.cd);
                if (cTime > 0)
                {
                    c.busyUntil = DateTime.Now.AddMilliseconds(this.cd);
                }
                durationEnds = DateTime.Now.AddMilliseconds(duration);
                return true;
            }
            return false;
        }
    }
}
