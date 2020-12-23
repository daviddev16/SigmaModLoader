using Sigma.Logging;
using Sigma.Utils;
using System;
using System.IO;

namespace Sigma.Manager
{
    public sealed class Inspector : IBaseInspector
    {
        private readonly static SigmaLogger Logger = new SigmaLogger(typeof(Inspector));

        private string RootFolderPath = null;

        private string LibrariesFolderPath = null;

        private string ConfigurationFilePath = null;

        public string Name { get; private set; }

        public SigmaConfiguration Configuration { get; private set; }

        public Inspector(string rootPath)
        {
            RootFolderPath = Objects.RequireNotNull(ref rootPath, "Root path is invalid.");

            LibrariesFolderPath = Path.Combine(RootFolderPath, SigmaConstants.LIBRARIES_FOLDER);
            ConfigurationFilePath = Path.Combine(RootFolderPath, SigmaConstants.SIGMACONFIG_FILE);
           
            Name = Path.GetFileName(RootFolderPath);
        }


        public bool Validate()
        {
            Logger.LogInformation("Reading folder \"" + Name + "\"...");
            if(CheckFiles())
            {
                Configuration = new SigmaConfiguration(ConfigurationFilePath);
                if(Configuration.Validate())
                {
                    if(!HasAnyLibrary())
                    {
                        Logger.LogError("No library found.");
                        return false;
                    }
                }
                else
                {
                    Logger.LogError("Fail on loading the configuration file.");
                    return false;
                }
            }
            return true;
        }

        private bool CheckFiles()
        {
            try
            {
                if(!Directory.Exists(RootFolderPath))
                {
                    Logger.LogError("The root folder doesn't exists.");
                    return false;
                }
                if(!Directory.Exists(LibrariesFolderPath))
                {
                    Logger.LogError("The libraries folder doesn't exists");
                    return false;
                }
                if(!File.Exists(ConfigurationFilePath))
                {
                    Logger.LogError("Configuration file doesn't exists.");
                    return false;
                }
            }
            catch(Exception e)
            {
                Logger.LogError("Something goes wrong while processing.", e, false);
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

        public string GetModLibrary()
        {
            return Directory.GetFiles(LibrariesFolderPath)[0];
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

        public SigmaConfiguration GetConfiguration()
        {
            return Configuration;
        }
    }

}
