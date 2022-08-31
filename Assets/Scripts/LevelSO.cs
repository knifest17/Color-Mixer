using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "LevelSO", menuName = "LevelSO", order = 0)]
    public class LevelSO : ScriptableObject
    {
        [SerializeField] Color desiredColor;
        [SerializeField] Ingredient[] ingredients;
        [SerializeField] bool useColorGenerating;
        [SerializeField] Ingredient[] desiredIngredients;

        public Color DesiredColor  => useColorGenerating ? GetGeneratedColor() : desiredColor;
        public Ingredient[] Ingredients  => ingredients;

        Color GetGeneratedColor()
        {
            return ColorTools.CombineColors(desiredIngredients.Select(i => i.Color).ToArray());
        }
    }
}