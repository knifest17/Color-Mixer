using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] RawImage colorResultImage;

        public void SetResultColor(Color color)
        {
            color.a = 1;
            colorResultImage.color = color;
        }
    }
}