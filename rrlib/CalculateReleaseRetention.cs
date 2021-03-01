using System.Linq;
using System;
using System.Collections.Generic;


namespace rrlib
{
    public static class ReleaseRetentionExtension
    {
        public static void CalculateReleaseRetention(this ReleaseRetention rr)
        {
            var releaseGroupByProject = rr.Releases.GroupBy(r => r.ProjectId)
                                                     .Select(group =>
                                                          new
                                                          {
                                                              project = group.Key,
                                                              releases = group
                                                                            .OrderByDescending(g => g.Created)
                                                                            .Take(rr.NumbersOfRetentionToKeep)
                                                          });

            Console.WriteLine(releaseGroupByProject);
        }
    }
}