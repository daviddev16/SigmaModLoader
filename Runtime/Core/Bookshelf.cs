using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Sigma
{

    public sealed class Bookshelf : IValidator {

        public HashSet<IBaseInspector> Inspectors { get; private set; }

        public string ModsPath { get; private set; }

        public string[] ModPathArray { get; private set; }

        public Bookshelf([NotNull] string modsPath) 
        {
            ModsPath = Objects.RequireNotNull(modsPath);

            if(!Validate()) {
                SigmaLogger.LogError("Bookshelf could not be instantiated.");
                SigmaLogger.LogError("Please do NOT use any method from this class.");
                return;
            }

            SigmaLogger.LogInformation("Starting the BookShelf.");

            Inspectors = new HashSet<IBaseInspector>();
            ModPathArray = Directory.GetDirectories(ModsPath);
            
            foreach(string ModDirectory in ModPathArray) {

                IBaseInspector modInspector = new Inspector(ModDirectory);

                if(modInspector.Validate()) 
                {
                    if(AddInspector(ref modInspector)) {
                        continue;
                    }
                }

                SigmaLogger.LogWarning("\"" + modInspector.GetConfiguration().Name + "\" failed.");
            }
        }

        public bool AddInspector(ref IBaseInspector baseInspector) 
        {
            Objects.RequireNotNull(baseInspector);

            if(CheckDuplications(ref baseInspector)) 
            {
                SigmaLogger.LogWarning("Duplication detected: \"" + baseInspector.GetConfiguration().Name + "\".");
                SigmaLogger.LogWarning("Please delete the duplication.");
                return true;
            }

            Inspectors.Add(baseInspector);

            string FullName = baseInspector.GetConfiguration().GetFullName();
            SigmaLogger.LogInformation(string.Concat(FullName, " found."));
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
                SigmaLogger.LogError("Mod path must be a directory.");
                return false;
            }

            if(!Directory.Exists(ModsPath)) 
            {
                SigmaLogger.LogError("Directory not found.");
                return false;
            }
 
            return true;
        }
    }
}
