using System.Json;
using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;

namespace USML {

    public class ModContentLoader : IBaseLoader {

        public static Tracer TRACER = new Tracer();

        private string RootFolderPath = null;

        private string LibrariesFolderPath = null;

        private string ConfigurationFilePath = null;

        public string Name;

        public ModContentLoader(string rootPath)
        {
            RootFolderPath = rootPath;
            LibrariesFolderPath = Path.Combine(RootFolderPath, USMLDefaults.LIBRARIES_FOLDER);
            ConfigurationFilePath = Path.Combine(RootFolderPath, USMLDefaults.USMLCONFIG_FILE);
            
            Name = Path.GetFileName(RootFolderPath);

            TRACER.SetHolder(this);
            TRACER.SetOptionalLabel(Name);
        }

        public void Validate()
        {
            TRACER.Log("Reading folder \"" + Name + "\"...");
            
            if(ExistsNeededFiles())
            {
                TRACER.Log("Stage 1 OK!");
                if(CheckConfiguration()) 
                {
                    TRACER.Log("Stage 2 OK!");
                }
            }
        
        }

        private bool CheckConfiguration()
        {
            try {
                string configFileContent = File.ReadAllText(ConfigurationFilePath);
                JsonObject jsonConfig = (JsonObject)JsonObject.Parse(configFileContent);

                if(!ValidateKeys(jsonConfig, USMLDefaults.CONFIG_DRIVERCLASS_KEY,
                    USMLDefaults.CONFIG_NAME_KEY, USMLDefaults.CONFIG_VERSION_KEY)) 
                {
                    TRACER.Fail("Missing or Invalid essentials keys at \"" + USMLDefaults.USMLCONFIG_FILE + "\"");
                    return false;
                }

            }
            catch(Exception e) {
                TRACER.Throw("Something goes wrong !", e.Message);
                return false;
            }

            return true;
        }

        private bool ValidateKeys(JsonObject obj, params string[] keys) 
        {
            foreach (string key in keys) {
                if(!CheckKey(obj, key)) {
                    return false;
                }
            }
            return true;
        }

        private bool CheckKey(JsonObject obj, string key) 
        {
            if(!obj.ContainsKey(key)) {
                return false;
            }
            if(obj.TryGetValue(key, out JsonValue value)) {
            
                if(value.JsonType == JsonType.String) {
                    if(value.ToString().Length == 0) {
                        return false;
                    
                    }
                }
                else {
                    return false;
                }
            }
            
            return true;
        }

        private bool ExistsNeededFiles()
        {
            try {

                if(!Directory.Exists(RootFolderPath)){
                    TRACER.Fail("The root folder doesn't exists.");
                    return false;
                }
                if(!Directory.Exists(LibrariesFolderPath)){
                    TRACER.Fail("The libraries folder doesn't exists");
                    return false;
                }
                if(!File.Exists(ConfigurationFilePath)){
                    TRACER.Fail("Configuration file doesn't exists.");
                    return false;
                }
            
            } 
            catch(Exception e) {
                TRACER.Throw("Something goes wrong", e.Message);
                return false;
            }

            return true;
        }

        public void LoadAssembly(Action<Assembly> assemblyResult)
        {
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

    }

}
