using System.Collections.Generic;
using Sigma.Comunication;
using Sigma.Logging;

namespace Sigma.API
{

    /// <summary>
    /// The base mod class. This class will be loaded by <see cref="SigmaLoader"/>
    /// With this class you can develop your own custom modification using the SigmaFramework
    /// </summary>
    /// 
    [Documented(false)]
    public abstract class BaseMod
    {
        private readonly static SigmaLogger Logger = new SigmaLogger(typeof(BaseMod));

        private SigmaConfiguration configuration;
        private SigmaLoader modLoader;
        private List<MethodCaller> callers;

        /// <summary>
        /// All necessary configurations to load properly the mod library
        /// </summary>
        /// 
        public SigmaConfiguration Configuration
        {
            get { return configuration; }
            set
            {
                if(configuration == null)
                {
                    configuration = value;
                    return;
                }
                Logger.LogFail("Configuration is set internally by the system.");
            }
        }

        /// <summary>
        /// The current ModLoader. It loaded this instance.
        /// </summary>
        ///
        public SigmaLoader ModLoader
        {
            get { return modLoader; }
            set
            {
                if(modLoader == null)
                {
                    modLoader = value;
                    return;
                }

                Logger.LogFail("ModLoader is set internally by the system.");
            }
        }

        /// <summary>
        /// All MethodCallers of this mod
        /// </summary>
        ///
        public List<MethodCaller> Callers
        {
            get { return callers; }
            set
            {
                if(callers == null)
                {
                    callers = value;
                    return;
                }

                Logger.LogFail("Callers are set internally by the system.");
            }
        }

        /// <summary>
        /// This function is called once ModManagerSystem is loaded.
        /// </summary>
        /// 
        public abstract void OnEnable();

        /// <summary>
        /// This function is called once ModManagerSystem is disabled.
        /// </summary>
        /// 
        public abstract void OnDisable();

        /// <summary>
        /// Register new type that will be handled by a MethodCaller
        /// </summary>
        /// 
        /// <param name="Listener">The listener object</param>
        /// <param name="Method">The method name used by MethodCaller</param>
        ///
        public virtual void RegisterInstance<E>(E Listener, string Method)
        {
            if(Listener == null)
            {
                Logger.LogCritical("Registered type cannot be null");
                return;
            }
            /*New MethodCaller that will handle the listener*/
            MethodCaller Caller = new MethodCaller(Listener, Method);
            if(Caller.isMethodValid())
            {
                Callers.Add(Caller);
                return;
            }
            Logger.LogError("Invalid caller.");
            Logger.LogError("MethodCaller is unable to access the MethodInfo from " + nameof(Listener));
        }

        /// <summary>
        /// Get all necessary stuff to load the Modification.
        /// </summary>
        /// 
        public virtual void Use(SigmaConfiguration Configuration, SigmaLoader ModLoader)
        {
            this.ModLoader = ModLoader;
            this.Configuration = Configuration;
            this.Callers = new List<MethodCaller>();
        }
    }
}
