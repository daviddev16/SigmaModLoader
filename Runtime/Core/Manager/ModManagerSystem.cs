using Sigma.Reflections;
using System;
using System.Collections.Generic;
using Sigma.Comunication;
using Sigma.Logging;
using Sigma.API;
using Sigma.Utils;

namespace Sigma.Manager
{
    /// <summary>
    /// this class is responsible for managing mods
    /// </summary>
    /// 
    [Documented(false)]
    public sealed class ModManagerSystem : ISignalReceiver
    {

        private readonly static SigmaLogger Logger = new SigmaLogger(typeof(ModManagerSystem));

        private List<BaseMod> LoadedMods { get; set; }

        private List<Signal<dynamic>> Signals { get; set; }

        /// <summary>
        /// Is ModManagerSystem Loaded?
        /// </summary>
        public bool IsLoaded { get; set; } = false;

        public ModManagerSystem()
        {
            Signals = new List<Signal<dynamic>>();
            LoadedMods = new List<BaseMod>();
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnShutdownEvent);
        }

        public List<object> Handle(string methodName, params object[] parameters)
        {
            if(ContainsMethodCaller(methodName))
            {
                List<object> returnList = new List<object>();
                AllSet(LoadedMod =>
                {
                    foreach(MethodCaller caller in LoadedMod.Callers)
                    {
                        InvokationResult<object> invokationResult = Handlers.Call(caller, parameters);
                        if(invokationResult.Failed)
                        {
                            Logger.LogError(string.Format("Failed at invokation: {0}.", methodName), invokationResult.Exception, true);
                            return;
                        }
                        returnList.Add(invokationResult.Result);
                    }
                });
                return returnList;
            }
            return null;
        }

        public bool ContainsMethodCaller(string methodName)
        {
            foreach(BaseMod loadedMod in LoadedMods)
            {
                foreach(MethodCaller caller in loadedMod.Callers)
                {
                    if(caller.MethodName.Equals(methodName))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public Signal<dynamic>[] FindSignals(string Identifier)
        {
            List<Signal<dynamic>> foundSignals = new List<Signal<dynamic>>();
            foreach(var signal in Signals)
            {
                if(signal.Equals(Identifier))
                {
                    foundSignals.Add(signal);
                }
            }

            return foundSignals.ToArray();
        }

        public object[] CallSignals(string Identifier, params object[] parameters)
        {
            List<object> results = new List<object>();
            foreach(Signal<object> signals in FindSignals(Identifier))
            {
                object dynamicSend = signals.Send(parameters);
                results.Add(dynamicSend);
            }

            return results.ToArray();
        }

        public void AddSignal(Signal<object> signal)
        {
            Signals.Add(signal);
        }

        /// <summary>
        /// iterate all Mods
        /// </summary>
        ///
        public void AllSet(Action<BaseMod> ActionMod)
        {
            foreach(BaseMod baseMod in LoadedMods)
            {
                ActionMod.Invoke(baseMod);
            }
        }

        /// <summary>
        /// Disable and clean up everything.
        /// </summary>
        ///
        public void Dispose()
        {
            if(IsLoaded)
            {
                DisableAll();
                LoadedMods.Clear();
                Logger.LogInformation("ModManagerSystem Closed.");
            }
        }

        /// <summary>
        /// Disable all added mods and close the ModManagerSystem
        /// </summary>
        ///
        public void DisableAll()
        {
            AllSet(Mod => DisableMod(Mod));
            IsLoaded = false;
        }

        /// <summary>
        /// Enable all added mods and open the ModManagerSystem
        /// </summary>
        ///
        public void EnableAll()
        {
            AllSet(Mod => EnableMod(Mod));
            IsLoaded = true;
        }

        /// <summary>
        /// Add a new mod instance.
        /// </summary>
        ///
        public void AddMod(ref BaseMod mod)
        {
            LoadedMods.Add(mod);
        }

        public void DisableMod(BaseMod baseMod)
        {
            if(baseMod != null)
            {
                baseMod.OnDisable();
                LoadedMods.Remove(baseMod);
            }
        }

        public void EnableMod(BaseMod baseMod)
        {
            if(baseMod != null)
            {
                baseMod.OnEnable();
            }
        }

        private void OnShutdownEvent(object sender, EventArgs args)
        {
            Dispose();
        }

     
    }
}
