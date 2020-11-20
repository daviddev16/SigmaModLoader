using System.Reflection;

namespace Sigma
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
