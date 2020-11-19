using System.Collections.Generic;

namespace Sigma
{

    public abstract class BaseMod {

        private Configuration Configuration 
        {
            get { return Configuration; }
            set 
            {
                if(Configuration == null)
                {
                    Configuration = value;
                    return;
                }

                SigmaLogger.LogFail("Configuration is set internally by the system.");
            }

        }

        private SigmaLoader ModLoader
        {
            get { return ModLoader; }
            set
            {
                if(ModLoader == null)
                {
                    ModLoader = value;
                    return;
                }

                SigmaLogger.LogFail("ModLoader is set internally by the system.");
            }

        }

        private List<MethodCaller> Callers
        {
            get { return Callers; }
            set
            {
                if(Callers == null)
                {
                    Callers = value;
                    return;
                }

                SigmaLogger.LogFail("Callers are set internally by the system.");
            }

        }



        public void Register<E>(E listener, string method)
        {
            if(listener == null)
            {
                SigmaLogger.LogCritical("Registered type cannot be null");
                return;
            }

            MethodCaller Caller = new MethodCaller(listener, method);

            if(Caller.Validate())
            {
                GetModCallers().Add(Caller);
                return;
            }

            SigmaLogger.LogCritical("Invalid caller.");

        }

        public virtual void Use(Configuration Configuration, SigmaLoader ModLoader, List<MethodCaller> Callers)
        {
            this.ModLoader = ModLoader;
            this.Configuration = Configuration;
            this.Callers = Callers;
        }

        public virtual List<MethodCaller> GetModCallers()
        {
            return Callers;
        }

        public abstract void OnEnable();

        public abstract void OnDisable();

    }
}
