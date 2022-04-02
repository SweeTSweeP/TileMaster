using Data;
using UnityEngine;

namespace Tile
{
    public class Tile : MonoBehaviour
    {
        public float CalculateArea()
        {
            var texture = GetComponent<SpriteRenderer>().sprite.texture;

            var count = 0;
            
            for (var indexI = 0; indexI < texture.width; indexI++)
            {
                for (var indexJ = 0; indexJ < texture.height; indexJ++)
                {
                    if (texture.GetPixel(indexI, indexJ).a > 0) count++;
                }
            }

            return (count * TileProperties.Height * TileProperties.Width) / (texture.height * texture.width);
        }
    }
}