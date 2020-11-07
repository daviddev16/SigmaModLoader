using System;
using System.IO;
using System.Reflection;

namespace USML {

    public interface IBaseLoader {

        string GetRootPath();

        string GetConfigurationPath();

        string GetLibraryPath();

        void Validate();

        void LoadAssembly(Action<Assembly> assemblyResult);

    }

}
