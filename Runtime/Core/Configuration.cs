using System;
using System.Diagnostics.CodeAnalysis;
using System.Json;

namespace USML {

    public sealed class Configuration : IEquatable<Configuration> {

        public string DriveClassPath { get; private set; }

        public string Version { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public string FilePath { get; private set; }

        public Configuration(JsonObject jsonObject, string file) 
        {
            Objects.RequireNotNull(jsonObject);

            DriveClassPath = Objects.GetString(ref jsonObject, USMLDefaults.CONFIG_DRIVERCLASS_KEY);
            Version = Objects.GetString(ref jsonObject, USMLDefaults.CONFIG_VERSION_KEY).Trim();
            Name = Objects.GetString(ref jsonObject, USMLDefaults.CONFIG_NAME_KEY).Trim();
            Description = Objects.GetString(ref jsonObject, USMLDefaults.CONFIG_DESCRIPTION_KEY);
            FilePath = file;
        
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
    }
}
