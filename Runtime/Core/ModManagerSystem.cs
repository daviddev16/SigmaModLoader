using System.Collections.Generic;

namespace USML {

    public sealed class ModManagerSystem {

        public HashSet<BaseMod> Mods { get; private set; }

        public bool AddMod(BaseMod mod) 
        {
            return Mods.Add(mod);
        }

    }
}
