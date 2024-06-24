using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtHandler.Interface
{
    public interface IConfigProvider
    {
        string GetConfigValue(string key);
    }
}
