namespace Sigma.Comunication
{
    public interface ISignalReceiver
    {
        Signal<dynamic>[] FindSignals(string Identifier);

        dynamic[] CallSignals(string Identifier, params object[] parameters);
    }
}
