using System.Linq;
using System;
using System.Collections.Generic;
using rrlib.Entities;

namespace rrlib
{
    public class ReleaseRetention
    {
        public int NumbersOfRetentionToKeep { get; set; }
        public List<Deployment> Deployments { get; set; }
        public List<Release> Releases { get; set; }
        public List<Project> Projects { get; set; }
        public List<rrlib.Entities.Environment> Environments { get; set; }
    }
}