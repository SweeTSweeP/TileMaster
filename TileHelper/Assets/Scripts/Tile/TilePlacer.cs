using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using Data;
using UnityEngine;
using Object = UnityEngine.Object;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace Tile
{
    public class TilePlacer : ITilePlacer
    {
        private const int CountCoefficient = 3;
        
        private GameObject parent;
        private GameObject tile;
        private Vector3 finalPosition;
        private int rowCount;

        public event Action<float> AreaCalculated;

        public void Setup()
        {
            if (tile == null) tile = ResourceLoader.LoadTile();
            if (parent == null) parent = new GameObject("Tiles");
            finalPosition = GetCameraTopRightCoordinates();
        }

        private void PlaceTiles(float seamSize, float angleValue, float biasValue)
        {
            rowCount = 0;

            var position = new Vector3(
                finalPosition.x * -CountCoefficient, 
                finalPosition.y * -CountCoefficient,
                finalPosition.z);

            while (position.y < finalPosition.y * CountCoefficient) PositionRow();

            parent.transform.eulerAngles = new Vector3(0, 0, -angleValue);

            void PositionRow()
            {
                var sprite = tile.GetComponent<SpriteRenderer>();

                while (position.x < finalPosition.x * CountCoefficient)
                {
                    sprite = InstantiateTile(position).GetComponent<SpriteRenderer>();

                    position = new Vector3(
                        sprite.bounds.max.x + sprite.bounds.size.x / 2 + seamSize,
                        position.y,
                        position.z);
                }

                rowCount++;
                position = new Vector3(
                    finalPosition.x * -CountCoefficient + biasValue * rowCount,
                    position.y + sprite.bounds.size.y + seamSize,
                    position.z);
            }
        }

        private void CalculateArea()
        {
            var area = parent.transform
                .Cast<Transform>()
                .Where(child => child.GetComponent<SpriteRenderer>().isVisible)
                .Select(child => TileProperties.Width * TileProperties.Height).Sum();

            AreaCalculated?.Invoke(area);
        }

        private GameObject InstantiateTile(Vector3 position)
        {
            var spawnedTile = Object.Instantiate(tile, position, Quaternion.identity, parent.transform);
            
            return spawnedTile;
        }

        private void ClearTiles()
        {
            if (parent == null) return;
            
            parent.transform.eulerAngles = Vector3.zero;

            foreach (Transform child in parent.transform) Object.Destroy(child.gameObject);
        }

        private Vector3 GetCameraTopRightCoordinates() =>
            Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        public async UniTaskVoid PlaceWallTiles(float seamSize = 0, float angleValue = 0, float biasValue = 0)
        {
            ClearTiles();
            PlaceTiles(seamSize, angleValue, biasValue);

            await UniTask.NextFrame();
            
            CalculateArea();
        }
    }
}
