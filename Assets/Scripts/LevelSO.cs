using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "LevelSO", menuName = "LevelSO", order = 0)]
    public class LevelSO : ScriptableObject
    {
        [SerializeField] Color desiredColor;
        [SerializeField] Ingredient[] ingredients;

        public Color DesiredColor  => desiredColor;
        public Ingredient[] Ingredients  => ingredients;
    }
}