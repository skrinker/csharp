using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebServer.Helpers.TemplateEngine
{
    public class TemplateEngineMethods
    {

        public string SubstituteString(string input, string replacement) 
        {
            string pattern = @"@\{([a-zA-Z]+)\}";
            return Regex.Replace(input, pattern, replacement);
        }
    }
}
