using System;
using System.Collections.Generic;

namespace Colt.Infections.Library.Entities
{
    public partial class VirusDefinition
    {
        public VirusDefinition()
        {
            EventCaseDefinition = new HashSet<EventCaseDefinition>();
        }

        public int IdVirus { get; set; }
        public string VirusName { get; set; }
        public string VirusCode { get; set; }

        public virtual ICollection<EventCaseDefinition> EventCaseDefinition { get; set; }
    }
}
