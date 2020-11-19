
namespace USML
{
    public interface SeqExecutor
    {

        void CallSequenceQuietly(string name);

        void CallSequenceQuietly(string name, params object[] prms);

        object[] CallSequence(Sequence Sequence, params object[] methodParameters);

        object[] CallSequence(string name, object[] methodParameters);

        Sequence[] GetSequences();

    }
}
