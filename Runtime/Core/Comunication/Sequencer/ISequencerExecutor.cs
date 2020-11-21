
namespace Sigma.Comunication
{
    public interface ISequencerExecutor
    {
        void CallSequencerQuietly(string name);

        void CallSequencerQuietly(string name, params object[] prms);

        object[] CallSequencer(Sequencer Sequencer, params object[] methodParameters);

        object[] CallSequencer(string name, object[] methodParameters);

        Sequencer[] GetSequencers();
    }
}
