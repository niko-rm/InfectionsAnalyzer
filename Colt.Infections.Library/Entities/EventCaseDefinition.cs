using System;
using System.Collections.Generic;

namespace Colt.Infections.Library.Entities
{
    public partial class EventCaseDefinition
    {
        public Guid UidCase { get; set; }
        public int IdVirus { get; set; }
        public DateTime DateEvent { get; set; }
        public string Location { get; set; }
        public int Infected { get; set; }
        public int Death { get; set; }

        public virtual VirusDefinition IdVirusNavigation { get; set; }
    }
}
