using System.Reflection;

namespace Sigma
{

    public interface IBaseInspector : IValidator {

        string GetRootPath();

        string GetConfigurationPath();

        string GetLibraryPath();

        Configuration GetConfiguration();

        Assembly LoadAssembly();

    }

}
