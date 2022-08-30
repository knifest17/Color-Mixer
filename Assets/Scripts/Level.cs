using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Level : MonoBehaviour
    {
        [SerializeField] Color desiredColor;

        public Color DesiredColor => desiredColor;
    }
}