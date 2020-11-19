using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Sigma
{

    public sealed class Configuration : IEquatable<Configuration>, IValidator {

        public string DriveClassPath { get; private set; }

        public string Version { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public string FilePath { get; private set; }

        public Configuration(string FilePath) 
        {

            Dictionary<string, object> keys = Objects.ReadYAMLFile(FilePath);

            DriveClassPath = keys.GetValueOrDefault(USMLDefaults.CONFIG_DRIVERCLASS_KEY) as string;
            Version = keys.GetValueOrDefault(USMLDefaults.CONFIG_VERSION_KEY) as string;
            Name = keys.GetValueOrDefault(USMLDefaults.CONFIG_NAME_KEY) as string;
            Description = keys.GetValueOrDefault(USMLDefaults.CONFIG_DESCRIPTION_KEY) as string;
            this.FilePath = FilePath;
        
        }

        public string GetFullName() 
        {
            return string.Concat(Name, " ", Version);
        }

        public bool Equals([NotNull] Configuration other) 
        {
            if(other != null) {
                return other.Name.Equals(Name) && other.Version.Equals(Version) || (other.FilePath.Equals(other.FilePath));
            }
            throw new NullReferenceException("other cannot be null.");
        }

        public bool Validate()
        {
            if(string.IsNullOrEmpty(DriveClassPath) || string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Version))
            {
                return false;
            }
            return true;
        }


    }
}
