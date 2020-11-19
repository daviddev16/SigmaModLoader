using System;
using System.Collections.Generic;
using System.Reflection;

namespace Sigma
{

    public sealed class SigmaLoader {

        private ModManagerSystem ModManagerSystem { get; set; }

        public SigmaLoader(Bookshelf bookshelf) 
        {
            Init(bookshelf);
        }

        public SigmaLoader(string ModsPath)
        {
            Init(new Bookshelf(ModsPath));
        }

        private void Init(Bookshelf bookshelf)
        {
            ModManagerSystem = new ModManagerSystem();
            ProcessBookshelf(bookshelf);
        }


        public void Process(ref IBaseInspector baseInspector) 
        {
            try
            {
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
                    SigmaLogger.LogFail("Mod instance was null.");
                }
            }
            catch(Exception e)
            {
                SigmaLogger.LogError("Something goes wrong while processing.", e, false);
            }
        }

        public void ProcessBookshelf(Bookshelf Bookshelf) 
        {
            Bookshelf.AllSet(inspector => 
            {
                Process(ref inspector);
            });

        }
        
        public ModManagerSystem GetModManagerSystem() 
        {
            return ModManagerSystem;
        }
    }

}
