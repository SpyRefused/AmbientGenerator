using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Docker.DotNet;
using Docker.DotNet.Models;

namespace AmbientGenerator
{
    public class Containers
    {
        private readonly DockerClient _dockerClient;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dockerClient"></param>
        public Containers(DockerClient dockerClient)
        {
            _dockerClient = dockerClient;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<IList<ContainerListResponse>> ListAll()
        {
            return _dockerClient.Containers.ListContainersAsync(new ContainersListParameters()
            {
                All = true
            });
        }
    }
}
