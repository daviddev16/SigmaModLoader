using System;
using System.Diagnostics;

namespace Sigma
{

    public class Program
    {

        private readonly static SigmaLogger Logger = new SigmaLogger(typeof(Program));

        static void Main(string[] args)
        {

            string modList = @"C:\Users\redst\OneDrive\Área de Trabalho\modList";

            SigmaLoader ModLoader = new SigmaLoader(modList);
            ModManagerSystem modManager = ModLoader.ModManagerSystem;

            Sequencer OnHelloWorldSequence = new Sequencer("OnHelloWorld");

            modManager.AddSequencer(new Sequencer("OnHelloWorld"));


            Stopwatch watcher = new Stopwatch();
            watcher.Start();
            for(int i = 0; i < 1; i++)
            {
                foreach(string msg in modManager.CallSequencer("OnHelloWorld", 2, 4))
                {
                    Logger.LogInformation(msg + " => from CustomMod");
                }
            }
            watcher.Stop();
            Console.WriteLine(watcher.ElapsedMilliseconds + "ms to complete.");
        }

    }
}
