using System.Json;
using System;
using System.Reflection;
using System.IO;
using System.Diagnostics.CodeAnalysis;

namespace USML {

    public class ContentInspector : IBaseInspector {

        private string RootFolderPath = null;

        private string LibrariesFolderPath = null;

        private string ConfigurationFilePath = null;

        public string Name { get; private set; }

        public Configuration Configuration { get; private set; }

        public ContentInspector([NotNull] string rootPath)
        {
            Tracer.Here(this);
            RootFolderPath = Objects.RequireNotNull(rootPath);
            
            LibrariesFolderPath = Path.Combine(RootFolderPath, USMLDefaults.LIBRARIES_FOLDER);
            ConfigurationFilePath = Path.Combine(RootFolderPath, USMLDefaults.USMLCONFIG_FILE);
            
            Name = Path.GetFileName(RootFolderPath);
        }


        public bool Validate()
        {
            Tracer.Log("Inspecting folder \"" + Name + "\"...");
            
            if(CheckFiles() && CheckConfiguration() && HasAnyLibrary()){
                Tracer.Log("Modification found!");
                return true;
            }
            Tracer.Warning("Something goes wrong while the inspection of \"" + Name + "\"");
            return false;
        }

        private bool CheckFiles() {
            try {

                if(!Directory.Exists(RootFolderPath)) 
                {
                    Tracer.Fail("The root folder doesn't exists.");
                    return false;
                }
                
                if(!Directory.Exists(LibrariesFolderPath)) 
                {
                    Tracer.Fail("The libraries folder doesn't exists");
                    return false;
                }
                
                if(!File.Exists(ConfigurationFilePath)) 
                {
                    Tracer.Fail("Configuration file doesn't exists.");
                    return false;
                }

            }
            catch(Exception e) {
                Tracer.Throw("Something goes wrong", e.Message);
                return false;
            }

            Tracer.Log("all necessary directories/files have been found.");
            return true;
        }

        private bool CheckConfiguration()
        {
            try {
                string configFileContent = File.ReadAllText(ConfigurationFilePath);
                JsonObject jsonConfig = (JsonObject)JsonObject.Parse(configFileContent);

                if(!ValidateKeys(ref jsonConfig, USMLDefaults.CONFIG_DRIVERCLASS_KEY,
                    USMLDefaults.CONFIG_NAME_KEY, USMLDefaults.CONFIG_VERSION_KEY)) 
                {
                    Tracer.Fail("Missing or Invalid essentials keys at \"" + USMLDefaults.USMLCONFIG_FILE + "\"");
                    return false;
                }

                Configuration = new Configuration(jsonConfig, ConfigurationFilePath);
                Tracer.Log("Essentials keys found!");

            }
            catch(Exception e) {
                Tracer.Fail("Failed to inspect the config file \"" + USMLDefaults.USMLCONFIG_FILE + "\"");
                Tracer.Exception(e, false);
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
                    Tracer.Log("Some library was found.");
                    return true;
                }
            }
            Tracer.Fail("No library found.");
            return false;
        }


        private bool ValidateKeys([NotNull] ref JsonObject obj, params string[] keys) 
        {
            foreach (string key in keys) {
                if(!CheckKey(ref obj, key)) {
                    return false;
                }
            }
            return true;
        }

        private bool CheckKey([NotNull] ref JsonObject obj, [NotNull] string key) 
        {
            if(!obj.ContainsKey(key)) {
                return false;
            }
            if(obj.TryGetValue(key, out JsonValue value)) 
            {
                if(value.JsonType == JsonType.String) {
                    if(value.ToString().Length == 0) {
                        return false;
                    }
                }
                else 
                {
                    return false;
                }
            }
            return true;
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
