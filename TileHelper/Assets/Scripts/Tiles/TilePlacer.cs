using Tiles.Interfaces;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace Tiles
{
    public class TilePlacer : MonoBehaviour, ITilePlacer
    {
        private void Start()
        {
            PlaceTiles();
        }

        public void PlaceTiles()
        {
            var position = Vector3.zero;
            var tile = LoadTile();
            var tileRect = tile.GetComponent<SpriteRenderer>().size;

            for (var indexI = 0; indexI < 4; indexI++)
            {
                for (var indexJ = 0; indexJ < 4; indexJ++)
                {
                    Object.Instantiate(tile, position, Quaternion.identity);
                    position = new Vector3(position.x + tileRect.x / 2, position.y, position.z);
                }

                position = new Vector3(0, position.y + +tileRect.y / 2, position.z);
            }
        }

        public void ClearTiles()
        {
        }

        private GameObject LoadTile() => 
            Addressables.LoadAssetAsync<GameObject>("Tile").WaitForCompletion();
    }
}