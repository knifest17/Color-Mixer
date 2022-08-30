using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] SpriteRenderer desiredColorImage;
        [SerializeField] RawImage colorResultImage;

        public void SetDesiredColor(Color color)
        {
            desiredColorImage.color = color;
        }

        public void SetResultColor(Color color)
        {
            color.a = 1;
            colorResultImage.color = color;
        }
    }
}