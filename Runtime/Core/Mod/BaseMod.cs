﻿using System.Collections.Generic;

namespace Sigma
{

    /// <summary>
    /// The base mod class. This class will be loaded by <see cref="SigmaLoader"/>
    /// With this class you can develop your own custom modification using the SigmaFramework
    /// </summary>
    /// 
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
        /// Register new type that will be handled by a MethodCaller
        /// </summary>
        /// 
        /// <param name="listener">The listener object</param>
        /// <param name="method">The method name used by MethodCaller</param>
        ///
        public void Register<E>(E listener, string method)
        {
            if(listener == null)
            {
                Logger.LogCritical("Registered type cannot be null");
                return;
            }

            /*New MethodCaller that will handle the listener*/
            MethodCaller Caller = new MethodCaller(listener, method);

            if(Caller.Validate())
            {
                Callers.Add(Caller);
                return;
            }

            Logger.LogError("Invalid caller.");
            Logger.LogError("MethodCaller is unable to access the MethodInfo from " + nameof(listener));

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

    }
}