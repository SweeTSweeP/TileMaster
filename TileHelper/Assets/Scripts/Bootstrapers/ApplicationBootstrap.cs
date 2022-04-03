using System;
using Tile;
using UnityEngine;
using Wall;
using Zenject;

namespace Bootstrapers
{
    public class ApplicationBootstrap : MonoBehaviour
    {
        private IWallBuilder wallBuilder;
        private ITilePlacer tilePlacer;
        
        [Inject]
        public void Construct(IWallBuilder wallBuilder, ITilePlacer tilePlacer)
        {
            this.wallBuilder = wallBuilder;
            this.tilePlacer = tilePlacer;
        }

        private void Awake()
        {
            wallBuilder.BuildWall();
            tilePlacer.Setup();
        }
    }
}