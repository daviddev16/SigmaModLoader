using System;
using Sigma.Comunication;
using System.Diagnostics;
using Sigma.Logging;
using Sigma.Manager;

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

            Sequencer seq = new Sequencer("OnHelloWorld");

            modManager.AddSequencer(seq);

            Stopwatch watcher = new Stopwatch();
            watcher.Start();
            for(int i = 0; i < 1; i++)
            {
                foreach(string msg in modManager.CallSequencer("OnHelloWorld", 2))
                {
                    Logger.LogInformation(msg + " => from CustomMod");
                }
            }
            watcher.Stop();
            Console.WriteLine(watcher.ElapsedMilliseconds + "ms to complete.");
        }

    }
}
