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
    public sealed class ModManagerSystem : ISequencerExecutor, ISignalReceiver
    {

        private readonly static SigmaLogger Logger = new SigmaLogger(typeof(ModManagerSystem));

        private HashSet<BaseMod> ModSet { get; set; }

        private List<Sequencer> Sequencers { get; set; }

        private List<Signal<object>> Signals { get; set; }

        /// <summary>
        /// Is ModManagerSystem Loaded?
        /// </summary>
        public bool IsLoaded { get; set; } = false;

        public ModManagerSystem()
        {
            Sequencers = new List<Sequencer>();
            Signals = new List<Signal<dynamic>>();
            ModSet = new HashSet<BaseMod>();
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnShutdownEvent);
        }

        /// <summary>
        /// Execute an Sequence through the Reflection methods
        /// </summary>
        /// 
        /// <param name="Sequencer">The current Sequencer</param>
        /// <param name="methodParameters">All necessary parameters</param>
        /// 
        /// <returns>Return an array of all returned values</returns>
        /// 
        public object[] CallSequencer(Sequencer Sequencer, object[] methodParameters)
        {
            try
            {
                Objects.RequireNotNull(ref Sequencer, "Sequencer is null.");

                List<object> values = new List<object>();
                foreach(MethodCaller Caller in Sequencer.Callers)
                {
                    InvokationResult<object> invokationResult = Handlers.Call(Caller, ref methodParameters);
                    if(!invokationResult.Failed)
                    {
                        values.Add(invokationResult.Result);
                        continue;
                    }
                    Logger.LogError("Invokation failed.", invokationResult.Exception, false);
                }
                return values.ToArray();
            }
            catch(Exception e)
            {
                Logger.LogError("CallSequencer failed.", e, false);
            }

            return null;
        }

        /// <summary>
        /// find all sequences with the respective name and send as an <see cref="Action{Sequencer}"/>
        /// </summary>
        /// 
        /// <param name="ActionSequencer">Delegate Action</param>
        /// <param name="name">name of the sequencer</param>
        ///
        public void FindAllSequences(string name, Action<Sequencer> ActionSequencer)
        {
            Objects.RequireNotNull(ref name, "Name is null");
            Objects.RequireNotNull(ref name, "ActionSequencer is null");

            foreach(Sequencer Sequencer in Sequencers)
            {
                if(Sequencer.Name.Equals(name))
                {
                    ActionSequencer.Invoke(Sequencer);
                }
            }
        }

        /// <summary>
        /// Execute the sequencer without return anything.
        /// </summary>
        /// 
        /// <param name="name">name of the sequencer</param>
        ///
        public void CallSequencerQuietly(string name)
        {
            FindAllSequences(name, Seq =>
            {
                CallSequencer(Seq, null);
            });
        }

        /// <summary>
        /// Execute the sequencer without return anything.
        /// </summary>
        /// 
        /// <param name="name">name of the sequencer</param>
        /// <param name="parameters">the necessary parameters</param>
        ///
        public void CallSequencerQuietly(string name, params object[] parameters)
        {
            FindAllSequences(name, Seq =>
            {
                CallSequencer(Seq, parameters);
            });
        }

        /// <summary>
        /// Execute the sequencer returning all the returned values from the invokation.
        /// </summary>
        /// 
        /// <param name="name">name of the sequencer</param>
        /// <param name="parameters">the necessary parameters</param>
        ///
        public object[] CallSequencer(string name, params object[] parameters)
        {
            List<object> values = new List<object>();

            FindAllSequences(name, Seq =>
            {
                foreach(object obj in CallSequencer(Seq, parameters))
                {
                    values.Add(obj);
                }
            });

            return values.ToArray();
        }

        public Signal<object>[] FindSignals(string Identifier)
        {
            List<Signal<object>> foundSignals = new List<Signal<object>>();
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
        /// Add a new Sequencer to the Sequencers, this will help you to call any method<br/>
        /// from a especific(or not) listener.
        /// </summary>
        /// 
        ///<param name="seq">The actual instance of a Sequencer</param>
        ///
        public void AddSequencer(Sequencer seq)
        {
            seq.LoadSequence(this);
            Sequencers.Add(seq);
        }

        /// <summary>
        /// Get the Sequencers array.
        /// </summary>
        ///
        public Sequencer[] GetSequencers()
        {
            return Sequencers.ToArray();
        }

        /// <summary>
        /// iterate all Mods
        /// </summary>
        ///
        public void AllSet(Action<BaseMod> ActionMod)
        {
            foreach(BaseMod baseMod in ModSet)
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
                Sequencers.Clear();
                ModSet.Clear();
                Logger.LogInformation("ModManagerSystem Closed.");
            }
        }

        /// <summary>
        /// Disable all added mods and close the ModManagerSystem
        /// </summary>
        ///
        public void DisableAll()
        {
            AllSet(Mod => Mod.OnDisable());
            IsLoaded = false;
        }

        /// <summary>
        /// Enable all added mods and open the ModManagerSystem
        /// </summary>
        ///
        public void EnableAll()
        {
            AllSet(Mod => Mod.OnEnable());
            IsLoaded = true;
        }

        /// <summary>
        /// Add a new mod instance.
        /// </summary>
        ///
        public void InsertMod(ref BaseMod mod)
        {
            ModSet.Add(mod);
        }

        private void OnShutdownEvent(object sender, EventArgs args)
        {
            Dispose();
        }

     
    }
}
