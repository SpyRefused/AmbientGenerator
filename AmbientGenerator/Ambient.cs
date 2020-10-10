using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Docker.DotNet;
using Docker.DotNet.Models;

namespace AmbientGenerator
{
    class Ambient
    {
        private static DockerClient _dockerClient;
        private static Images _images;
        private static Containers _containers;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            _dockerClient = new DockerClientConfiguration().CreateClient();
            _images = new Images(_dockerClient);
            _containers = new Containers(_dockerClient);

            MainAsync().GetAwaiter().GetResult();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static async Task MainAsync()
        {
            try
            {
                Console.WriteLine("Welcome Ambient Generator");

                var images = await _images.ListAll();
                var containers = await _containers.ListAll();

                if (!containers.Any(x => x.Names.Any(c => c.Contains("/ambientGenerator"))))
                {
                    await _dockerClient.Containers.CreateContainerAsync(new CreateContainerParameters()
                    {
                        Image = "jimmywhitaker/music-transformer:0.1",
                        Name = "ambientGenerator",
                        HostConfig = new HostConfig
                        {
                            Binds = new List<string>
                            {
                                Directory.GetCurrentDirectory() + "/data:/data",
                            }
                        },
                    });
                }

                var ambientGenerator = containers.FirstOrDefault(x => x.Image.Contains("jimmywhitaker/music-transformer"));

                if (ambientGenerator != null)
                {
                    var state  = ambientGenerator.State;
                    if (state != "running")
                    {
                        var starting = _dockerClient.Containers.StartContainerAsync(ambientGenerator.ID, new ContainerStartParameters());

                    }
                }
                    
                var mids = Directory.GetFiles(
                    Directory.GetCurrentDirectory() + "/data",
                    "*.mid",
                    SearchOption.AllDirectories);

                if (mids.Length > 0)
                {
                    foreach (var mid in mids)
                    {
                        var path = Path.GetDirectoryName(mid);



                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           


        }
    }
}
