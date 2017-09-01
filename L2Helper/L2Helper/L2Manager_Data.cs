using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace L2Helper
{
    public static partial class L2Manager
    {
        static void ReadDataJSON()
        {
            string JSONstring = File.OpenText(@"resources\data.json").ReadToEnd();
            var root = JsonConvert.DeserializeObject<JsonDataRoot>(JSONstring);

            classList = root.classes;
            classList.Insert(0,new Class(""));

            foreach (Buff b in root.buffs)
            {
                foreach (string cn in b.classList)
                {
                    if (b.self)
                    {
                        Class cl = classList.Find(c => c.name == cn);
                        if (cl != null) cl.buffSelf.Add(b);
                    }
                    else
                    {
                        Class cl = classList.Find(c => c.name == cn);
                        if (cl != null) cl.buffParty.Add(b);                      
                    }
                }
            }
            foreach (Buff b in root.heals)
            {
                foreach (string cn in b.classList)
                {
                    Class cl = classList.Find(c => c.name == cn);
                    if (cl != null) cl.heal.Add(b);
                }
            }
            foreach (Buff b in root.rechargess)
            {
                foreach (string cn in b.classList)
                {
                    Class cl = classList.Find(c => c.name == cn);
                    if (cl != null) cl.recharge.Add(b);
                }
            }
            foreach (Skill s in root.dmgskills)
            {
                foreach (string cn in s.classList)
                {
                    Class cl = classList.Find(c => c.name == cn);
                    if (cl != null) cl.dmgSkill.Add(s);
                }
            }
        }
    }

    public class JsonDataRoot {
        public List<Class> classes = new List<Class>();
        public List<Buff> buffs = new List<Buff>();
        public List<Buff> heals = new List<Buff>();
        public List<Buff> rechargess = new List<Buff>();
        public List<Skill> dmgskills = new List<Skill>();
    }
}
