using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Blender : MonoBehaviour
    {
        [SerializeField] Transform ingredientContainer;

        List<Ingredient> ingredientList = new List<Ingredient>();

        public event Action<Color> Mixed;
        public event Action<Ingredient> IngrediendAdded;

        public void AddIngredient(Ingredient ingredient)
        {
            ingredient.transform.SetParent(ingredientContainer);
            ingredient.transform.localPosition = Vector3.zero;
            ingredientList.Add(ingredient);
            IngrediendAdded?.Invoke(ingredient);
        }

        public void Mix()
        {
            var color = ColorTools.CombineColors(ingredientList.Select(i => i.Color).ToArray());
            Mixed?.Invoke(color);
        }

        public void Clear()
        {
            ingredientContainer.DestroyChildren();
        }
    }
}