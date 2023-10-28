using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServer.Attributes
{
    public class GetAttribute : Attribute
    {
    public string actionName;
        public GetAttribute(string _actionName)
        {
            actionName = _actionName;
        }
    }
}
