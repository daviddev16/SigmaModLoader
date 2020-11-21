using Sigma.API;
using Sigma.Logging;
using Sigma.Manager;
using Sigma.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Sigma
{

    public sealed class SigmaLoader : IValidator, IDisposable
    {

        private readonly static SigmaLogger Logger = new SigmaLogger(typeof(SigmaLoader));

        public ModManagerSystem ModManagerSystem { get; private set; }

        public HashSet<IBaseInspector> Inspectors { get; private set; }

        private string[] ModPathArray { get; set; }

        public string ModsPath { get; private set; }


        public SigmaLoader(string ModsPath)
        {
            this.ModsPath = Objects.RequireNotNull(ref ModsPath, "Mods path is null.");
            ModManagerSystem = new ModManagerSystem();
            Load(this.ModsPath);
        }

        public void Reload()
        {

            if(ModManagerSystem.IsLoaded)
            {
                ModManagerSystem.Dispose();
            }

            if(ModsPath != null)
            {
                Load(ModsPath);
                return;
            }
        }

        private void Load(string modsPath)
        {
            if(!Validate())
            {
                Logger.LogError("SigmaLoader could not be instantiated.");
                Logger.LogError("Please do NOT use any method from this class.");
                return;
            }

            Logger.LogInformation("Loading...");

            Inspectors = new HashSet<IBaseInspector>();
            ModPathArray = Directory.GetDirectories(modsPath);

            foreach(string ModDirectory in ModPathArray)
            {
                IBaseInspector modInspector = new Inspector(ModDirectory);

                if(modInspector.Validate())
                {
                    if(AddInspector(ref modInspector))
                    {
                        continue;
                    }
                }
                Logger.LogWarning("\"" + modInspector.GetConfiguration().Name + "\" failed.");
            }

            ProcessAll();
            Dispose();
        }

        private void Process(ref IBaseInspector baseInspector)
        {
            try
            {
                Assembly assembly = baseInspector.LoadAssembly();
                SigmaConfiguration Config = baseInspector.GetConfiguration();

                BaseMod ModInstance = (BaseMod)assembly.CreateInstance(Config.DriveClassPath);

                if(ModInstance != null)
                {
                    ModInstance.Use(Config, this);
                    ModManagerSystem.InsertMod(ref ModInstance);
                }
                else
                {
                    Logger.LogFail("Mod instance was null.");
                }
            }
            catch(Exception e)
            {
                Logger.LogError("Something goes wrong while processing.", e, false);
            }
        }

        private bool AddInspector(ref IBaseInspector baseInspector)
        {
            Objects.RequireNotNull(ref baseInspector, "BaseInspector is null.");

            if(CheckDuplications(ref baseInspector))
            {
                Logger.LogWarning("Duplication detected: \"" + baseInspector.GetConfiguration().Name + "\".");
                Logger.LogWarning("Please delete the duplication.");
                return true;
            }

            Inspectors.Add(baseInspector);

            string FullName = baseInspector.GetConfiguration().GetFullName();
            Logger.LogInformation(string.Concat(FullName, " found."));
            return true;

        }

        private void AllSet(Action<IBaseInspector> inspectorAction)
        {
            foreach(IBaseInspector inspectors in Inspectors)
            {
                inspectorAction.Invoke(inspectors);
            }
        }

        private bool CheckDuplications(ref IBaseInspector inspector)
        {
            foreach(IBaseInspector others in Inspectors)
            {
                if(others.GetConfiguration().Equals(inspector.GetConfiguration()))
                {
                    return true;
                }
            }

            return false;
        }

        private void ProcessAll()
        {
            AllSet(inspector =>
            {
                Process(ref inspector);
            });
            Logger.LogTitle("SUCCESSFUL!");
            ModManagerSystem.EnableAll();
            Dispose();
        }

        public bool Validate()
        {
            if(!File.GetAttributes(ModsPath).HasFlag(FileAttributes.Directory))
            {
                Logger.LogError("Mod path must be a directory.");
                return false;
            }

            if(!Directory.Exists(ModsPath))
            {
                Logger.LogError("Directory not found.");
                return false;
            }

            return true;
        }

        public void Dispose()
        {
            Inspectors.Clear();
        }
    }

}

