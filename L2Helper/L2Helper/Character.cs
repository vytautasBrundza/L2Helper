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
            Class c = L2Manager.classList.Find(cl => cl.name == _name);
            if (c != null)
            {
                this.type = c.type;
                this.autoAttackPreferred = c.autoAttackPreferred;
                foreach (Buff b in c.buffParty)
                {
                    this.buffParty.Add(b.Clone());
                }
                foreach (Buff b in c.buffSelf)
                {
                    this.buffSelf.Add(b.Clone());
                }
                foreach (Buff b in c.heal)
                {
                    this.heal.Add(b.Clone());
                }
                foreach (Buff b in c.recharge)
                {
                    this.recharge.Add(b.Clone());
                }
                foreach (Skill s in c.dmgSkill)
                {
                    this.dmgSkill.Add(s.Clone());
                }
            }
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
    }

    public enum UseCase
    {
        standart,
        special,
        lowHp,
        lowMp
    }

    public class Skill
    {
        public List<string> classList = new List<string>();
        public string name;
        public bool targetReq; //requires target
        public int cd; //cooldown
        public ushort ks; //keyboard shortcut
        public DateTime cdrTime; // cooldown reset time
        public ushort cTime; // cast time
        public byte minLVL;
        public bool toggle = false;
        public UseCase use = UseCase.standart;

        public Skill(string _name, int _cd, ushort _ks, UseCase _use, ushort _ct = 0, bool _targetReq = false, bool _toggle = false)
        {
            name = _name;
            targetReq = _targetReq;
            cd = _cd;
            ks = _ks;
            cTime = _ct;
            use = _use;
            toggle = _toggle;
            cdrTime = DateTime.Now;
        }

        public Skill()
        {
            cdrTime = DateTime.Now;
        }

        public Skill Clone()
        {
            return new Skill(name, cd, ks, use, cTime, targetReq, toggle);
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
                return true;
            }
            return false;
        }
    }

    public class Buff : Skill
    {
        public int duration;
        public DateTime durationEnds;
        public bool party;
        public bool self;

        public Buff(string _name, int _cd, ushort _ks, UseCase _use, int _duration, ushort _ct = 0, bool _targetReq = false, bool _toggle = false)
        {
            name = _name;
            targetReq = _targetReq;
            cd = _cd;
            ks = _ks;
            cTime = _ct;
            use = _use;
            toggle = _toggle;
            cdrTime = DateTime.Now;
            durationEnds = DateTime.Now;
            duration = _duration;
        }

        public new bool Use(Character c)
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

        public new Buff Clone()
        {
            return new Buff(name, cd, ks, use, duration, cTime, targetReq, toggle);
        }
    }
}
