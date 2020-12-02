using Sigma.Logging;
using Sigma.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Sigma.IO
{
    /// <summary>
    /// Custom FileConfiguration built-in to easy create custom yaml files<br/>
    /// Can be used by mods to adding custom properties for the mod
    /// </summary>
    /// 
    [Documented(true)]
    public class FileConfiguration
    {
        private readonly static SigmaLogger Logger = new SigmaLogger(typeof(FileConfiguration));

        /// <summary>
        /// The file path
        /// </summary>
        public string FilePath { get; private set; }

        /// <summary>
        /// The Dictionary parsed from the YAML deserializer.
        /// </summary>
        public Dictionary<string, object> Keys { get; private set; }

        public FileConfiguration(string FilePath)
        {
            if(!FileUtils.CheckFileOrDirectory(FilePath, true, ".yml"))
            {
                Logger.LogError("Invalid file format.");
                return;
            }
            try
            {
                this.FilePath = Objects.RequireNotNull(ref FilePath, "File path is null.");
                Reload();
            }
            catch(Exception e)
            {
                Logger.LogError("Failed on loading \"" + Path.GetFileName(FilePath) + "\"", e, false);
            }
        }

        /// <summary>
        /// Load the file as an YAML dictionary
        /// </summary>
        /// <param name="FilePath">the file path</param>
        /// 
        private void Load(string FilePath)
        {
            Keys = FileUtils.ReadAsYAMLFile(FilePath);
        }

        /// <summary>
        /// Reload the file and update the dictionary
        /// </summary>
        public void Reload()
        {
            if(FilePath != null)
            {
                Load(FilePath);
            }
        }

        /// <summary>
        /// Get an int value from a respective key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>the actual int value</returns>
        /// 
        public int GetInt(string key)
        {
            return ((int)GetValue(key));
        }

        /// <summary>
        /// Get a string value from a respective key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>the actual string value</returns>
        /// 
        public string GetString(string key)
        {
            return ((string)GetValue(key));
        }

        private object GetValue(string key)
        {
            Objects.RequireNotNull(ref key, "The key cannot be null.");
            object OutObject = null;
            if(Keys.TryGetValue(key, out OutObject))
            {
                return OutObject;
            }
            return null;
        }

    }
}
