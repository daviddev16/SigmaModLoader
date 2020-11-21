using Sigma.Utils;
using System.Reflection;

namespace Sigma.Manager
{
    public interface IBaseInspector : IValidator
    {
        string GetRootPath();

        string GetConfigurationPath();

        string GetLibraryPath();

        SigmaConfiguration GetConfiguration();

        Assembly LoadAssembly();
    }

}
