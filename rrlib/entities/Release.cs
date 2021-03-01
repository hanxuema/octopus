using System;

namespace rrlib.Entities
{
    public class Release
    {
        public string Id { get; set; }
        public string ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public string Version { get; set; }
        public DateTime Created { get; set; }
    }

}