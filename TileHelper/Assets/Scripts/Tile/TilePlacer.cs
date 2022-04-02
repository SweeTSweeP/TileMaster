using System;
using Data;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace Tile
{
    public class TilePlacer : MonoBehaviour
    {
        private GameObject parent;
        private GameObject tile;

        public event Action<float> AreaCalculated;

        private void Start()
        {
            tile = LoadTile();
        }

        private void PlaceTiles()
        {
            if (parent == null) parent = new GameObject("Tiles");
            var finalPosition = GetCameraStartPosition();
            var position = new Vector3(finalPosition.x * -2, finalPosition.y * -2, finalPosition.z);
            var rowCount = 0;

            while (position.y < finalPosition.y * 2)
            {
                PositionRow();
            }

            PositionRow();
            parent.transform.eulerAngles = new Vector3(0, 0, -TileProperties.AngleValue);
            
            //CalculateArea();

            void PositionRow()
            {
                var sprite = tile.GetComponent<SpriteRenderer>();

                while (position.x < finalPosition.x * 2)
                {
                    sprite = InstantiateTile(tile, position).GetComponent<SpriteRenderer>();

                    position = new Vector3(
                        sprite.bounds.max.x + sprite.bounds.size.x / 2 + TileProperties.SeamSize,
                        position.y,
                        position.z);
                }

                InstantiateTile(tile, position);

                rowCount++;
                position = new Vector3(
                    finalPosition.x * -2 + TileProperties.BiasValue * rowCount,
                    position.y + sprite.bounds.size.y + TileProperties.SeamSize,
                    position.z);
            }
        }

        private void CalculateArea()
        {
            var area = 0f;

            foreach (Transform child in parent.transform)
            {
                area += child.GetComponent<Tile>().CalculateArea();
            }
            
            AreaCalculated?.Invoke(area);
        }

        private GameObject InstantiateTile(GameObject tile, Vector3 position)
        {
            var spawnedTile = Instantiate(tile, position, Quaternion.identity, parent.transform);

            return spawnedTile;
        }

        private void ClearTiles()
        {
            if (parent != null)
            {
                parent.transform.eulerAngles = Vector3.zero;

                foreach (Transform child in parent.transform)
                {
                    Destroy(child.gameObject);
                }
            }
        }

        private Vector3 GetCameraStartPosition() =>
            Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        private GameObject LoadTile() =>
            Addressables.LoadAssetAsync<GameObject>("Tile").WaitForCompletion();

        public void BuildWall()
        {
            ClearTiles();
            PlaceTiles();
        }
    }
}
