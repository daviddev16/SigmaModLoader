using System;
using System.IO;
using USML;

namespace USMLCore {

    public class Program {
    
        static void Main(string[] args) {

            string modList = @"C:\Users\redst\OneDrive\Área de Trabalho\modList";

            StandardModLoader ModLoader = new StandardModLoader(modList, true);
            ModManagerSystem modManager = ModLoader.GetModManagerSystem();
            modManager.AddSequence(new Sequence("CustomCall"));

            foreach(string r in modManager.CallSequence("CustomCall", "ola!"))
            {
                Logger.LogInformation(r.GetType().Name);
                Logger.LogInformation(r + " from the mod dev retuned");
            }
        }
    
    }
}
