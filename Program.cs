using System;
using System.Collections.Generic;
using System.IO;
using USML;

namespace USMLCore {

    public class Program {
    
        static void Main(string[] args) {

            string modList = @"C:\Users\redst\OneDrive\Área de Trabalho\modList";

            Bookshelf bookshelf = new Bookshelf(modList);
            StandardModLoader modLoader = StandardModLoader.Create(ref bookshelf);
            ModManagerSystem managerSystem = modLoader.Process();


            File.WriteAllText("logs.txt", Tracer.GetSingleTrace().ToString());
        }
    
    }
}
