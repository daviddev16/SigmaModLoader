using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace USML {

    public sealed class Bookshelf : IValidator {

        public HashSet<IBaseInspector> Inspectors { get; private set; }

        public string ModsPath { get; private set; }

        public string[] ModPathArray { get; private set; }

        public Bookshelf([NotNull] string modsPath) 
        {
            Tracer.Here(this);
            ModsPath = Objects.RequireNotNull(modsPath);

            if(!Validate()) {
                Tracer.Fail("Bookshelf could not be instantiated.");
                Tracer.Fail("Please do NOT use any method from this class.");
                return;
            }

            Tracer.Log("Starting the BookShelf.");

            Inspectors = new HashSet<IBaseInspector>();
            ModPathArray = Directory.GetDirectories(ModsPath);
            
            foreach(string ModDirectory in ModPathArray) {

                IBaseInspector modInspector = new ContentInspector(ModDirectory);

                if(modInspector.Validate()) 
                {
                    if(AddInspector(ref modInspector)) {
                        continue;
                    }
                }

                Tracer.Warning("\"" + modInspector.GetConfiguration().Name + "\" failed.");
            }
        }

        public bool AddInspector(ref IBaseInspector baseInspector) 
        {
            Objects.RequireNotNull(baseInspector);
            Tracer.Here(this);

            if(CheckDuplications(ref baseInspector)) 
            {
                Tracer.Warning("Duplication detected: \"" + baseInspector.GetConfiguration().Name + "\".");
                Tracer.Warning("Please delete the duplication.");
                return true;
            }

            Inspectors.Add(baseInspector);

            string FullName = baseInspector.GetConfiguration().GetFullName();
            Tracer.Log(string.Concat(FullName, " added."));
            return true;

        }
        
        public void AllSet(Action<IBaseInspector> inspectorAction) 
        {
            foreach(IBaseInspector inspectors in Inspectors) {
                inspectorAction.Invoke(inspectors);
            }
        }

        private bool CheckDuplications(ref IBaseInspector inspector) 
        {
            foreach(IBaseInspector others in Inspectors) {
                if(others.GetConfiguration().Equals(inspector.GetConfiguration())) 
                {
                    return true;
                }
            }

            return false;
        }

        public bool Validate() 
        {
            if(!File.GetAttributes(ModsPath).HasFlag(FileAttributes.Directory)) 
            {
                Tracer.Fatal("Mod path must be a directory.");
                return false;
            }

            if(!Directory.Exists(ModsPath)) 
            {
                Tracer.Fail("Directory not found.");
                return false;
            }
 
            return true;
        }
    }
}
