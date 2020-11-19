using System.Collections.Generic;

namespace USML {

    public abstract class BaseMod {

        public Configuration Configuration { get; private set; } = null;

        public StandardModLoader ModLoader { get; private set; } = null;

        private List<MethodCaller> Callers { get; set; } = null;

       

        public virtual List<MethodCaller> GetModCallers()
        {
            return Callers;
        }

        public void Register<E>(E listener, string method)
        {
            if(listener == null)
            {
                Logger.LogCritical("Registered type cannot be null");
                return;
            }

            MethodCaller Caller = new MethodCaller(listener, method);
            //validate
            GetModCallers().Add(Caller);
        }

        public virtual void Use(Configuration Configuration, StandardModLoader ModLoader, List<MethodCaller> Callers)
        {
            this.ModLoader = ModLoader;
            this.Configuration = Configuration;
            this.Callers = Callers;
        }

        public abstract void OnEnable();

        public abstract void OnDisable();

    }
}
