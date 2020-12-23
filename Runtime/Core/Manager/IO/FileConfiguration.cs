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
        public Dictionary<string, object> Mapping { get; private set; }

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

        public string GetValueAsString(string key)
        {
            if(Mapping.ContainsKey(key))
            {
                object outValue = null;
                if(Mapping.TryGetValue(key, out outValue))
                {
                    return (outValue as string);
                }
            }
            return null;
        }

        /// <summary>
        /// Load the file as an YAML dictionary
        /// </summary>
        /// <param name="FilePath">the file path</param>
        /// 
        private void Load(string FilePath)
        {
            Mapping = FileUtils.ReadAsYAMLFile(FilePath);
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
    }
}
