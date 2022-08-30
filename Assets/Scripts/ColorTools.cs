using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public static class ColorTools
    {
        public static Color CombineColors(params Color[] aColors)
        {
            Color result = new Color(0, 0, 0, 0);
            foreach (Color c in aColors)
            {
                result += c;
            }
            result /= aColors.Length;
            return result;
        }

        public static float CompareColors(Color a, Color b)
        {
            float k = 1f / 3f;
            float red = (1f - Mathf.Abs(a.r - b.r)) * k;
            float green = (1f - Mathf.Abs(a.g - b.g)) * k;
            float blue = (1f - Mathf.Abs(a.b - b.b)) * k;
            return red + green + blue;
        }
    }
}
