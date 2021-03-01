using System;
using Newtonsoft.Json;
using rrlib;

namespace rrconsole
{
      class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            System.Console.WriteLine("Loading deployment data");
            var depLoader = new rrlib.Dataloader.DeploymentLoader("rrconsole/payload/Deployments.json");
            var deployments = depLoader.Loader();
            System.Console.WriteLine(JsonConvert.SerializeObject(deployments));
            System.Console.WriteLine("Finish loading deployment data");

            System.Console.WriteLine("Loading release data");
            var resLoader = new rrlib.Dataloader.ReleaseLoader("rrconsole/payload/Releases.json");
            var releases = resLoader.Loader();
            System.Console.WriteLine(JsonConvert.SerializeObject(releases));
            System.Console.WriteLine("Finish loading releases data");

            // System.Console.WriteLine("Loading deployment data");
            // var depLoader = new rrlib.Dataloader.DeploymentLoader("./payload/Deployments.json");
            // var deployments = depLoader.Loader();
            // System.Console.WriteLine(JsonConvert.SerializeObject(deployments));
            // System.Console.WriteLine("Finish loading deployment data");

            // System.Console.WriteLine("Loading deployment data");
            // var depLoader = new rrlib.Dataloader.DeploymentLoader("./payload/Deployments.json");
            // var deployments = depLoader.Loader();
            // System.Console.WriteLine(JsonConvert.SerializeObject(deployments));
            // System.Console.WriteLine("Finish loading deployment data");

            var rs = new rrlib.ReleaseRetention()
            {
                Releases = releases,
                NumbersOfRetentionToKeep = 2,
            };

            rs.CalculateReleaseRetention();
        }
    }
}
