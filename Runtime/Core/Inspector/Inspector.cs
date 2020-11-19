using System;
using System.Reflection;
using System.IO;
using System.Diagnostics.CodeAnalysis;

namespace Sigma
{

    public sealed class Inspector : IBaseInspector {

        private string RootFolderPath = null;

        private string LibrariesFolderPath = null;

        private string ConfigurationFilePath = null;

        public string Name { get; private set; }

        public Configuration Configuration { get; private set; }

        public Inspector([NotNull] string rootPath)
        {
            RootFolderPath = Objects.RequireNotNull(rootPath);
            
            LibrariesFolderPath = Path.Combine(RootFolderPath, USMLDefaults.LIBRARIES_FOLDER);
            ConfigurationFilePath = Path.Combine(RootFolderPath, USMLDefaults.USMLCONFIG_FILE);

            Name = Path.GetFileName(RootFolderPath);
        }


        public bool Validate()
        {
            SigmaLogger.LogInformation("Reading folder \"" + Name + "\"...");

            if(CheckFiles())
            {
                Configuration = new Configuration(ConfigurationFilePath);
                if(Configuration.Validate())
                {
                    if(!HasAnyLibrary())
                    {
                        SigmaLogger.LogError("No library found.");
                        return false;
                    }
                }
                else
                {
                    SigmaLogger.LogError("Fail on loading the configuration file.");
                    return false;
                }
            }

            return true;
        }

        private bool CheckFiles() 
        {
            try {

                if(!Directory.Exists(RootFolderPath)) 
                {
                    SigmaLogger.LogError("The root folder doesn't exists.");
                    return false;
                }
                
                if(!Directory.Exists(LibrariesFolderPath)) 
                {
                    SigmaLogger.LogError("The libraries folder doesn't exists");
                    return false;
                }
                
                if(!File.Exists(ConfigurationFilePath)) 
                {
                    SigmaLogger.LogError("Configuration file doesn't exists.");
                    return false;
                }

            }
            catch(Exception e) 
            {
                SigmaLogger.LogError("Something goes wrong while processing.", e, false);
                return false;
            }
            
            return true;
        }

      
        private bool HasAnyLibrary() 
        {
            foreach(string file in Directory.GetFiles(LibrariesFolderPath)) 
            {
                if(Path.GetExtension(file).Equals(".dll", StringComparison.OrdinalIgnoreCase)) 
                {
                    return true;
                }
            }
           
            return false;
        }

        public Assembly LoadAssembly() 
        {
            string modLibraryPath = Directory.GetFiles(LibrariesFolderPath)[0];
            return Assembly.LoadFile(modLibraryPath);
        }

        public string GetRootPath()
        {
            return RootFolderPath;
        }

        public string GetConfigurationPath()
        {
            return ConfigurationFilePath;
        }

        public string GetLibraryPath()
        {
            return LibrariesFolderPath;
        }

        public Configuration GetConfiguration() {
            return Configuration;
        }
    }

}
