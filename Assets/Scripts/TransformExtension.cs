using UnityEngine;

namespace Assets.Scripts
{
    public static class TransformExtension
    {
        public static void DestroyChildren(this Transform t)
        {
            foreach (Transform child in t)
                Object.Destroy(child.gameObject);
        }
    }
}
