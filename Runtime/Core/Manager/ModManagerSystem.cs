using System;
using System.Collections.Generic;

namespace Sigma
{

    public sealed class ModManagerSystem : ModSet, SeqExecutor {


        private List<Sequence> Sequences { get; set; }

        public ModManagerSystem() : base() 
        {
            Sequences = new List<Sequence>();
        }

        public void AddSequence(Sequence seq)
        {
            seq.LoadSequence(this);
            Sequences.Add(seq);
        }

        public void FindAllSequences(string name, Action<Sequence> Consumer)
        {
            foreach(Sequence Sequence in Sequences)
            {
                if(Sequence.Name.Equals(name))
                {
                    Consumer.Invoke(Sequence);
                }
            }
        }

        public void CallSequenceQuietly(string name)
        {
            FindAllSequences(name, Seq =>
            {
                CallSequence(Seq, null);
            });
        }

        public void CallSequenceQuietly(string name, params object[] prms)
        {
            FindAllSequences(name, Seq =>
            {
                CallSequence(Seq, prms);
            });
        }

        public object[] CallSequence(string name, params object[] methodParameters)
        {
            List<object> values = new List<object>();

            FindAllSequences(name, Seq =>
            {
                foreach(object o in CallSequence(Seq, methodParameters))
                {
                    values.Add(o);
                }
            });

            return values.ToArray();
        }

        public object[] CallSequence(Sequence Sequence, object[] methodParameters)
        {
            List<object> values = new List<object>();
            foreach(MethodCaller Caller in Sequence.Callers)
            {
                object returnValue = ReflectionHandlers.HandleMethodCaller(Caller, ref methodParameters);
                values.Add(returnValue);
            }
            return values.ToArray();
        }

        public Sequence[] GetSequences()
        {
            return Sequences.ToArray();
        }

      
    }
}
