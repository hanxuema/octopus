using System;
using System.Collections.Generic; 
using Newtonsoft.Json;
using rrlib.Entities;
using System.IO;

namespace rrlib.Dataloader
{
    public class EnvironmentLoader : IDataLoader<rrlib.Entities.Environment>
    { 
        private readonly string _fileLocation;
        public EnvironmentLoader(string fileLocation )
        {
            _fileLocation = fileLocation; 
        }
 
        public List<rrlib.Entities.Environment> Loader()
        {
            try
            {
                var environments = JsonConvert.DeserializeObject<List<rrlib.Entities.Environment>>(File.ReadAllText(_fileLocation));
                return environments;
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }
    }
}