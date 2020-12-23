namespace Sigma.Comunication
{
    public interface ISignalReceiver
    {
        Signal<object>[] FindSignals(string Identifier);

        object[] CallSignals(string Identifier, params object[] parameters);
    }
}
