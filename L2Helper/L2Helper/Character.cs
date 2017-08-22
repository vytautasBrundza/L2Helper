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
        public bool active;
        public Process p;
        public int hp;
        public int mhp;
        public int mp;
        public int mmp;
        public int php;
        public int pmp;
        public bool AIrun;

        public void SetHP(int _hp)
        {
            hp = _hp;
            if (mhp > 0)
                php = 100 * hp / mhp;
        }
        public void SetMP(int _mp)
        {
            mp = _mp;
            if (mmp > 0)
                pmp = 100 * mp / mmp;
        }

        public Character(Process _p)
        {
            p = _p;
        }
    }
}
