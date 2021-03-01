using System;

namespace rrlib.Entities
{
    public class Deployment
    {
        public string Id { get; set; }
        public string ReleaseId { get; set; }
        public virtual Release Release { get; set; }
        public string EnvironmentId { get; set; }
        public DateTime DeployedAt { get; set; }
    }
}