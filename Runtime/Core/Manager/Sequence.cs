using System;
using System.Collections.Generic;

namespace Sigma
{ 

    public class Sequence
    {

        public string Name { get; private set; } = null;
        public MethodCaller[] Callers { get; private set; } = null;

        public Sequence(string Name)
        {
            this.Name = Name;
        }

        public void LoadSequence(ModManagerSystem ManagerSystem)
        {
            List<MethodCaller> Callers = new List<MethodCaller>();

            ManagerSystem.AllSet(Mod =>
            {
                foreach(MethodCaller caller in Mod.GetModCallers())
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
