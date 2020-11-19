using System;
using System.Collections.Generic;

namespace USML
{
    public abstract class ModSet
    {
        private HashSet<BaseMod> Set { get; set; }

        public ModSet()
        {
            Set = new HashSet<BaseMod>();
        }

        public void AllSet(Action<BaseMod> Consumer)
        {
            foreach(BaseMod baseMod in Set)
            {
                Consumer.Invoke(baseMod);
            }
        }

        public bool Add(BaseMod Mod)
        {
            return Set.Add(Mod);
        }

        public bool Remove(BaseMod Mod)
        {
            return Set.Remove(Mod);
        } 

    }
}
