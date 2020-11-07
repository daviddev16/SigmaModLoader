using System;
using System.IO;
using USML;

namespace USMLCore {

    public class Program {
    
        static void Main(string[] args) {

            string modTemplatePath = @"C:\Users\redst\OneDrive\Área de Trabalho\model";
            ModContentLoader loader = new ModContentLoader(modTemplatePath);
            Console.WriteLine("Loading: "+loader.Name);
            loader.Validate();

            File.WriteAllText("logs.txt", ModContentLoader.TRACER.ToString());
        }
    
    }
}
