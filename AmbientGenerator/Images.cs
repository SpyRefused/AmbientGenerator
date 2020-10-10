using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Docker.DotNet;
using Docker.DotNet.Models;

namespace AmbientGenerator
{
    public class Images
    {
        private readonly DockerClient _dockerClient;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dockerClient"></param>
        public Images(DockerClient dockerClient)
        {
            _dockerClient = dockerClient;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<IList<ImagesListResponse>> ListAll()
        {
            return _dockerClient.Images.ListImagesAsync(new ImagesListParameters()
            {
                All = true
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imagesCreateParameters"></param>
        /// <param name="authConfig"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        public Task Pull(ImagesCreateParameters imagesCreateParameters, AuthConfig authConfig, Progress<JSONMessage> progress)
        {
            return _dockerClient.Images.CreateImageAsync(imagesCreateParameters, authConfig, progress);
        }
    }
}
