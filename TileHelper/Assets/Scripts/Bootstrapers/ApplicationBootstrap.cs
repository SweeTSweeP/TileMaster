using System;
using UnityEngine;
using Wall;
using Zenject;

namespace Bootstrapers
{
    public class ApplicationBootstrap : MonoBehaviour
    {
        private IWallBuilder wallBuilder;
        
        [Inject]
        public void Construct(IWallBuilder wallBuilder)
        {
            this.wallBuilder = wallBuilder;
        }

        private void Awake()
        {
            wallBuilder.BuildWall();
        }
    }
}