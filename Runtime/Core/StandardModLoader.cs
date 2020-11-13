using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace USML {

    public sealed class StandardModLoader : IModLoader {

        public Bookshelf Bookshelf { get; private set; }

        private IModLoader CurrentModLoader { get; set; }

        public StandardModLoader(ref Bookshelf bookshelf) 
        {
            this.CurrentModLoader = this;
            Bookshelf = bookshelf;
        }

        public static StandardModLoader Create(ref Bookshelf bookshelf) 
        {
            return new StandardModLoader(ref bookshelf);
        }

        public ModManagerSystem Process() 
        {

            Tracer.Here(this);

            ModManagerSystem managerSystem = new ModManagerSystem();
            
            Bookshelf.AllSet(Inspector => 
            {

                Action<Assembly> assemblyAction = (assembly) => 
                {

                    Configuration Config = Inspector.GetConfiguration();
                    BaseMod ModInstance = (BaseMod)assembly.CreateInstance(Config.DriveClassPath);

                    ModInstance.SetModHandler(ModHandler.CreateHandler(CurrentModLoader, ref ModInstance));
                    ModInstance.SetConfiguration(Inspector.GetConfiguration());

                    managerSystem.AddMod(ModInstance);

                };

                Exception ex = null;

                if(Load(ref Inspector, assemblyAction, out ex)) 
                {
                    Tracer.Log("Mod instance created [" + Inspector.GetConfiguration().DriveClassPath + "]");
                    return;
                }

                Tracer.Fail("Failed load. [at " + Inspector.GetConfiguration().GetFullName() + "] (" + ex.GetType().Name + ")");
                Tracer.Exception(ex);

            });

            return managerSystem;
        }

        public void Disable(ref BaseMod mod) 
        {

        }

        public void Enable(ref BaseMod mod) 
        {

        }

        public bool Load(ref IBaseInspector inspector, Action<Assembly> action, out Exception e) 
        {
            try {
                Assembly assembly = inspector.LoadAssembly();
                action.Invoke(assembly);
                e = null;
            
                return true;

            }catch(Exception ex) {
                e = ex;
                return false;
            }
        }

    }

}
