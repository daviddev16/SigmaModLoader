
using System;
using System.Reflection;

namespace USML {

    public interface IModLoader {

        void Disable(ref BaseMod mod);

        void Enable(ref BaseMod mod);

        bool Load(ref IBaseInspector inspector, Action<Assembly> action, out Exception e);

    }

}
