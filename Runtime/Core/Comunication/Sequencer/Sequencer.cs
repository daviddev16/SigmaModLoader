using Sigma.Manager;
using Sigma.Utils;
using System.Collections.Generic;

namespace Sigma.Comunication
{
    public class Sequencer
    {

        public MethodCaller[] Callers { get; private set; } = null;

        public string Name { get; private set; }

        public Sequencer(string name)
        {
            Objects.RequireNotNull(ref name, "Sequencer name cannot be null.");
            this.Name = name;
        }

        public void LoadSequence(ModManagerSystem ManagerSystem)
        {
            List<MethodCaller> Callers = new List<MethodCaller>();

            ManagerSystem.AllSet(Mod =>
            {
                foreach(MethodCaller caller in Mod.Callers)
                {
                    if(Name.Equals(caller.GetMethodName()))
                    {
                        Callers.Add(caller);
                    }
                }
            });
            this.Callers = Callers.ToArray();
        }

    }
}
