namespace Sigma.Comunication
{
    public interface ISignalReceiver
    {
        public Signal<dynamic>[] FindSignals(string Identifier);

        public dynamic[] CallSignals(string Identifier, params object[] parameters);
    }
}
