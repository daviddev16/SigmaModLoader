﻿using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;

namespace Sigma.IO
{
    /// <summary>
    /// Some utils for File manipulation.
    /// </summary>
    /// 
    [Documented(true)]
    public sealed class FileUtils
    {
        /// <summary>
        /// Read and parse a File as YAML File.
        /// </summary>
        /// 
        /// <param name="FilePath"></param>
        /// <returns>The Dictionary provided by the YAML deserialize.</returns>
        /// 
        public static Dictionary<string, object> ReadAsYAMLFile(string FilePath)
        {
            var deserializer = new Deserializer();
            FileStream stream = new FileStream(FilePath, FileMode.Open);
            return deserializer.Deserialize<Dictionary<string, object>>(new StreamReader(stream));
        }

        /// <summary>
        /// validate the file with the settings
        /// </summary>
        /// 
        /// <param name="FilePath">the File Path</param>
        /// <param name="RAF">Required as File</param>
        /// <param name="NeededExtension">The required extension</param>
        /// 
        /// <returns>if it's all qualificated</returns>
        /// 
        public static bool CheckFileOrDirectory(string FilePath, bool RAF, string NeededExtension)
        {
            if(!File.Exists(FilePath))
            {
                return false;
            }
            if(RAF)
            {
                if(RequiredAsFile(FilePath))
                {
                    if(Path.GetExtension(FilePath).Equals(NeededExtension))
                    {
                        return true;
                    }
                }
            }
            else
            {
                if(RequiredAsDirectory(FilePath))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Check if the FilePath is actually a File
        /// </summary>
        /// 
        /// <param name="FilePath">Relative file path</param>
        /// 
        /// <returns>true if it is a File</returns>
        /// 
        public static bool RequiredAsFile(string FilePath)
        {
            return !RequiredAsDirectory(FilePath);
        }

        /// <summary>
        /// Check if the FilePath is actually a Directory
        /// </summary>
        /// 
        /// <param name="FilePath">Relative file path</param>
        /// 
        /// <returns>true If it is a Directory</returns>
        /// 
        public static bool RequiredAsDirectory(string FilePath)
        {
            return File.GetAttributes(FilePath).HasFlag(FileAttributes.Directory);
        }

    }
}
