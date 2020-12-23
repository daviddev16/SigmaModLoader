using Sigma.Logging;
using Sigma.Manager;
using System;
using System.IO;
using UnityEngine;

namespace Sigma
{
    public class SigmaBehaviour : MonoBehaviour
    {
        private static readonly SigmaLogger Logger = new SigmaLogger(typeof(SigmaBehaviour));
        public static SigmaBehaviour Instance { get; private set; }

        [SerializeField]
        private string ModsFolder = "Mods";

        private SigmaLoader SigmaLoader { get; set; }
        public ModManagerSystem Manager;

        void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
                return;
            }
            Destroy(this);
        }

        void Start()
        {
            StartSigmaLoader();
        }

        private void StartSigmaLoader()
        {
            string ModsPath = Path.Combine(Path.GetFullPath("."), GetModsFolder());
            try 
            {
                Logger.LogInformation("Loading SigmaLoader...");
                SigmaLoader = new SigmaLoader(ModsPath);
                Manager = SigmaLoader.ModManagerSystem;
            }
            catch(Exception e)
            {
                Logger.LogError("SigmaLoader failed.", e, true);
            }
            Logger.LogInformation("Loaded.");
        }

        public string GetModsFolder()
        {
            return ModsFolder;
        }
    }

}
