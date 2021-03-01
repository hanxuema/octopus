using System;
using System.Collections.Generic; 
using Newtonsoft.Json;
using rrlib.Entities;
using System.IO;

namespace rrlib.Dataloader
{
    public class ReleaseLoader : IDataLoader<Release>
    { 
        private readonly string _fileLocation;
        public ReleaseLoader(string fileLocation )
        {
            _fileLocation = fileLocation; 
        }
 
        public List<Release> Loader()
        {
            try
            {
                var releases = JsonConvert.DeserializeObject<List<Release>>(File.ReadAllText(_fileLocation));
                return releases;
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }
    }
}