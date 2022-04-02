using Data;
using UnityEngine;

namespace Wall
{
    public class WallSizeSetter : MonoBehaviour
    {
        [SerializeField] private float width;
        [SerializeField] private float height;
        
        private void Start()
        {
            TileProperties.Width = width;
            TileProperties.Height = height;

                var sprite = GetComponent<SpriteMask>().sprite;
            var aspectRatio = Screen.width / (float) Screen.height;

            var orthoPixelsY = Mathf.CeilToInt(Camera.main.orthographicSize * 2 * 256);
            var orthoPixelsX = Mathf.CeilToInt(orthoPixelsY * aspectRatio);

            var newScale = new Vector3(
                orthoPixelsX / (float) sprite.rect.width,
                orthoPixelsY / (float) sprite.rect.height, 
                1f);

            transform.localScale = newScale;
        }
    }
}