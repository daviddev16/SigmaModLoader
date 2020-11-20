using Sigma.IO;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Sigma
{
    public sealed class SigmaConfiguration : FileConfiguration, IEquatable<SigmaConfiguration>, IValidator
    {

        public string DriveClassPath { get; private set; }
        public string Version { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public string RootFolder { get; private set; }

        public SigmaConfiguration(string FilePath) : base(FilePath)
        {
            SetupAllKeys();
        }

        private void SetupAllKeys()
        {
            DriveClassPath = GetValue(SigmaConstants.CONFIG_DRIVERCLASS_KEY) as string;
            Version = GetValue(SigmaConstants.CONFIG_VERSION_KEY) as string;
            Name = GetValue(SigmaConstants.CONFIG_NAME_KEY) as string;
            Description = GetValue(SigmaConstants.CONFIG_DESCRIPTION_KEY) as string;

            RootFolder = Path.GetDirectoryName(FilePath);
        }

        public bool Equals([NotNull] SigmaConfiguration other)
        {
            if(other != null)
            {
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

        public string GetFullName()
        {
            return string.Concat(Name, " ", Version);
        }
    }
}
