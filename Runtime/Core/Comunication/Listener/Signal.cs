namespace Sigma.Comunication
{
    public abstract class Signal<R>
    {

        public string Indentifier { get; private set; }

        public Signal(string Indentifier)
        {
            this.Indentifier = Indentifier;
        }

        public abstract R Send(object[] parameters);

    }
}
