using System;
using System.Collections.Generic; 
using Newtonsoft.Json;
using rrlib.Entities;
using System.IO;

namespace rrlib.Dataloader
{
    public class DeploymentLoader : IDataLoader<Deployment>
    { 
        private readonly string _fileLocation;
        public DeploymentLoader(string fileLocation )
        {
            _fileLocation = fileLocation; 
        }

        //@"./payload/Deployments.json"
        public List<Deployment> Loader()
        {
            try
            {
                var deployments = JsonConvert.DeserializeObject<List<Deployment>>(File.ReadAllText(_fileLocation));
                return deployments;
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }
    }
}