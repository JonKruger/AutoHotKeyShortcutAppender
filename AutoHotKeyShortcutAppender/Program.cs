using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace AutoHotKeyShortcutAppender
{
    class Program
    {
        static void Main(string[] args)
        {
            var shortcut = args[0];
            var output = args[1];

            string script = null;
            using (var fileStream = new FileStream(ConfigurationManager.AppSettings["AutoHotKeyScriptPath"], FileMode.Open, FileAccess.Read))
            {
                using (var streamReader = new StreamReader(fileStream))
                {
                    script = streamReader.ReadToEnd();
                    script += string.Format(@"
:*:{0}::{1}", shortcut, output);
                }
            }

            using (var fileStream = new FileStream(ConfigurationManager.AppSettings["AutoHotKeyScriptPath"], FileMode.Open, FileAccess.Write))
            {
                using (var writer = new StreamWriter(fileStream))
                {
                    writer.Write(script);
                }
            }

            Process.Start(ConfigurationManager.AppSettings["AutoHotKeyScriptPath"]);
        }
    }
}
