using System;
using System.Collections.Generic;
using System.IO;

namespace USML {

    public interface IConfig {

        string GetFile();

        bool IsValid();

        string GetDriverClassPath();

        Dictionary<String, Object> GetProperties();


    }

}
