using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;

namespace L2Helper
{
    public class Character
    {
        public string name;
        public bool main;
        public Process p;
        public BarValue cp;
        public BarValue hp;
        public BarValue mp;
        public bool AIrun;
        public Class clas = new Class("");

        public Character(Process _p)
        {
            p = _p;
            cp = new BarValue();
            hp = new BarValue();
            mp = new BarValue();
        }
    }

    public class BarValue {
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
        public ClassType type;
        public bool autoAttackPreferred;
        public List<Skill> buffSelf= new List<Skill>();
        public List<Skill> buffParty = new List<Skill>();
        public List<Skill> HealSingle = new List<Skill>();
        public List<Skill> HealParty = new List<Skill>();
        public List<Skill> Recharge = new List<Skill>();
        public List<Skill> DmgSkill = new List<Skill>();

        public Class(string _name) {
            name = _name;

            switch (name) {
                case "Warcryer":
                    type = ClassType.support;
                    autoAttackPreferred = true;
                    buffParty.Add(new Skill("MainBuff", 120, VK.F10));
                    break;
                case "BladeDancer":
                    type = ClassType.support;
                    autoAttackPreferred = true;
                    buffParty.Add(new Skill("Dance",120,VK.F10));
                    break;
                case "SwordSinger":
                    type = ClassType.support;
                    autoAttackPreferred = true;
                    buffParty.Add(new Skill("Sing", 120, VK.F10));
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
    public class Skill {
        public string name;
        public bool targetReq; //requires target
        public int cd; //cooldown
        public ushort ks; //keyboard shortcut
        public int cdLeft; // cooldown left
        public int cTime; // cast time
        public Skill(string _name, int _cd, ushort _ks, bool _targetReq = false) {
             name= _name;
             targetReq= _targetReq;
             cd= _cd;
             ks= _ks; 
        }
        public void Use() { }

    }
}
