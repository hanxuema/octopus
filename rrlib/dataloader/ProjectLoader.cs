using System;
using System.Collections.Generic; 
using Newtonsoft.Json;
using rrlib.Entities;
using System.IO;

namespace rrlib.Dataloader
{
    public class ProjectLoader : IDataLoader<Project>
    { 
        private readonly string _fileLocation;
        public ProjectLoader(string fileLocation )
        {
            _fileLocation = fileLocation; 
        }

        //@"./payload/Project.json"
        public List<Project> Loader()
        {
            try
            {
                var projects = JsonConvert.DeserializeObject<List<Project>>(File.ReadAllText(_fileLocation));
                return projects;
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }
    }
}