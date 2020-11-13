using System;
using System.IO;
using System.Reflection;

namespace USML {

    public interface IBaseInspector : IValidator {

        string GetRootPath();

        string GetConfigurationPath();

        string GetLibraryPath();

        Configuration GetConfiguration();

        Assembly LoadAssembly();

    }

}
