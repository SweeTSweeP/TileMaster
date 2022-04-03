using System;
using Cysharp.Threading.Tasks;

namespace Tile
{
    public interface ITilePlacer
    {
        void Setup();
        UniTaskVoid PlaceWallTiles(float seamSize = 0, float angleValue = 0, float biasValue = 0);
        event Action<float> AreaCalculated;
    }
}