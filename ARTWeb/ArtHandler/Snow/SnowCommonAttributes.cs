using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtHandler.Snow
{
    internal abstract class SnowCommonAttributes
    {
        public int state;
        public int urgency;
        public int priority;
        public int impact;
        public string assignment_group;
        public string assigned_to;
    }
}