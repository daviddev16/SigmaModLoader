using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace USML {

    public class Bookshelf : IValidator {


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

            Inspectors = new HashSet<IBaseInspector>();
            ModPathArray = Directory.GetDirectories(ModsPath);
            
            foreach(string ModDirectory in ModPathArray) {

                IBaseInspector modInspector = new ContentInspector(ModDirectory);

                if(modInspector.Validate()) 
                {

                    Tracer.Here(this);

                    if(CheckDuplications(ref modInspector)) {

                        Tracer.Warning("Duplication detected => \"" + modInspector.GetConfiguration().Name + "\".");
                        Tracer.Warning("Please delete the duplication.");
                        continue;
                    }

                    Inspectors.Add(modInspector);

                    string FullName = modInspector.GetConfiguration().GetFullName();
                    Tracer.Log(string.Concat(FullName, " Added! [soon will be loaded properly]"));
                    
                    continue;
                }
                Tracer.Warning("\"" + ModDirectory + "\" failed.");
            }

        }
        
        public void All(Action<IBaseInspector> inspectorAction) 
        { 
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
            return Directory.Exists(ModsPath);
        }
    
    
    }


}
