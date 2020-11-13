
namespace USML {

    public abstract class BaseMod {

        public Configuration Configuration { get; private set; } = null;

        public IModLoader ModLoader { get; private set; } = null;

        private ModHandler Handler { get; set; } = null;

        
        public virtual ModHandler GetHandler() 
        {
            return Handler;
        }
        
        public virtual void SetModHandler(ModHandler handler) 
        {
            if(Configuration != null) {
                throw new USMLException("Handler is internally set.");
            }
            Handler = handler;
        }

        public virtual void SetConfiguration(Configuration configuration) 
        {
            if(Configuration != null) 
            {
                throw new USMLException("Configuration is internally set.");
            }
            Configuration = configuration;
        }
    }
}
