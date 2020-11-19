using System;
using System.Collections.Generic;
using System.Reflection;

namespace Sigma
{

    public sealed class StandardModLoader {

        private ModManagerSystem ModManagerSystem { get; set; }

        public StandardModLoader(Bookshelf bookshelf, bool Process = true) 
        {
            ModManagerSystem = new ModManagerSystem();

            if(Process)
            {
                ProcessBookshelf(ref bookshelf);
            }
        }

        public StandardModLoader(string ModsPath, bool Process = true)
        {
            ModManagerSystem = new ModManagerSystem();

            Bookshelf bookshelf = new Bookshelf(ModsPath);
            if(Process)
            {
                ProcessBookshelf(ref bookshelf);
            }
        }


        public StandardModLoader Process(ref IBaseInspector baseInspector) 
        {
            try {

                Assembly assembly = baseInspector.LoadAssembly();
                Configuration Config = baseInspector.GetConfiguration();

                BaseMod ModInstance = (BaseMod)assembly.CreateInstance(Config.DriveClassPath);
                if(ModInstance != null)
                {
                    ModInstance.Use(Config, this, new List<MethodCaller>());
                    ModInstance.OnEnable();
                    ModManagerSystem.Add(ModInstance);
                }
                else
                {
                    Logger.LogFail("Mod instance was null.");
                }
            }
            catch(Exception e) 
            {
                Logger.LogError("Something goes wrong while processing.", e, false);
                throw;
            }

            return this;
        }

        public StandardModLoader ProcessBookshelf(ref Bookshelf Bookshelf) 
        {
            Bookshelf.AllSet(inspector => 
            {
                Process(ref inspector);
            });

            return this;
        }
        
        public ModManagerSystem GetModManagerSystem() 
        {
            return ModManagerSystem;
        }
    }

}
