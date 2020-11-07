using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace USML {

    public class Configuration : IConfig {

        public string GetDriverClassPath() {
            return "";
        }

        public string GetFile() {
            return "file://";
        }

        public Dictionary<string, object> GetProperties() {
            return null;
        }

        public bool IsValid() {
            return false;
        }
    }
}
