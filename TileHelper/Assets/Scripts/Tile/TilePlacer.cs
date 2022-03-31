using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Tile
{
    public class TilePlacer : MonoBehaviour
    {
        private void Start()
        {
            PlaceTiles();
        }

        private void PlaceTiles()
        {
            var finalPosition = GetCameraStartPosition();
            var position = finalPosition * -1;
            var tile = LoadTile();
            var sprite = tile.GetComponent<SpriteRenderer>();

            while (position.y < finalPosition.y)
            {
                position = PositionRow();
            }

            PositionRow();
            
            Vector3 PositionRow()
            {
                while (position.x < finalPosition.x)
                {
                    Instantiate(tile, position, Quaternion.identity);

                    position = new Vector3(position.x + sprite.bounds.size.x, position.y, position.z);
                }

                Instantiate(tile, position, Quaternion.identity);

                position = new Vector3(finalPosition.x * -1, position.y + sprite.bounds.size.y, position.z);
                return position;
            }
        }

        

        private Vector3 GetCameraStartPosition() => 
            Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        private GameObject LoadTile()
        {
            return Addressables.LoadAssetAsync<GameObject>("Tile").WaitForCompletion();
        }
    }
}
