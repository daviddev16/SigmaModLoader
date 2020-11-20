using System.Collections.Generic;

namespace Sigma
{
    public class Sequencer
    {

        public string Name { get; private set; } = null;
        public MethodCaller[] Callers { get; private set; } = null;

        public Sequencer(string Name)
        {
            Objects.RequireNotNull(ref Name, "Sequecer name cannot be null.");
            this.Name = Name;
        }

        public void LoadSequence(ModManagerSystem ManagerSystem)
        {
            List<MethodCaller> Callers = new List<MethodCaller>();

            ManagerSystem.AllSet(Mod =>
            {
                foreach(MethodCaller caller in Mod.Callers)
                {
                    if(caller.GetMethodName().Equals(Name))
                    {
                        Callers.Add(caller);
                    }
                }
            });
            this.Callers = Callers.ToArray();
        }
    }
}
