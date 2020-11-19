
using USML;

namespace Sigma {

    public class Program {
    
        static void Main(string[] args) {

            string modList = @"C:\Users\redst\OneDrive\Área de Trabalho\modList";

            SigmaLoader ModLoader = new SigmaLoader(modList, true);
            ModManagerSystem modManager = ModLoader.GetModManagerSystem();
            modManager.AddSequence(new Sequence("OnHelloWorld"));

            foreach(string msg in modManager.CallSequence("OnHelloWorld"))
            {
                SigmaLogger.LogInformation(msg + " => from CustomMod");
            }
        }
    
    }
}
