using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Data
{
    public static class ResourceLoader
    {
        public static GameObject LoadTile() =>
            Addressables.LoadAssetAsync<GameObject>("Tile").WaitForCompletion();
        
        public static GameObject GetWall() => 
            Addressables.LoadAssetAsync<GameObject>("Mask").WaitForCompletion();
    }
}