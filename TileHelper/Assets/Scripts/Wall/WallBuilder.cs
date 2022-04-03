using Data;
using UnityEngine;

namespace Wall
{
    public class WallBuilder : IWallBuilder
    {
        public void BuildWall()
        {
            var mask = ResourceLoader.GetWall();
            
            Object.Instantiate(mask, Vector3.zero, Quaternion.identity);

            var sprite = mask.GetComponent<SpriteMask>().sprite;
            var aspectRatio = Screen.width / (float) Screen.height;

            var orthoPixelsY = Mathf.CeilToInt(Camera.main.orthographicSize * 2 * 256);
            var orthoPixelsX = Mathf.CeilToInt(orthoPixelsY * aspectRatio);

            var newScale = new Vector3(
                orthoPixelsX / sprite.rect.width,
                orthoPixelsY / sprite.rect.height, 
                1f);

            mask.transform.localScale = newScale;
        }
    }
}