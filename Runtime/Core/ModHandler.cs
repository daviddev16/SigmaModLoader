using System;
using System.Collections.Generic;
using System.Text;

namespace USML {

    public sealed class ModHandler {

        public bool IsEnabled { get; set; } = false;

        public IModLoader ModLoader { get; private set; }

        public BaseMod Handled { get; private set; }

        public ModHandler(in IModLoader modLoader, ref BaseMod mod) 
        {
            ModLoader = modLoader;
            Handled = mod;
        }

        public static ModHandler CreateHandler(in IModLoader modLoader, ref BaseMod mod) 
        {
            return new ModHandler(in modLoader, ref mod);
        }

    }

}
