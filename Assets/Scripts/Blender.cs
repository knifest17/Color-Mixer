using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts
{
    public class Blender : MonoBehaviour
    {
        [SerializeField] Transform ingredientContainer;
        [SerializeField] float jumpDuration, jumpPower;

        List<Ingredient> ingredientList = new List<Ingredient>();

        public event Action<Color> Mixed;
        public event Action<Ingredient> IngrediendAdded;

        public void AddIngredient(Ingredient ingredient)
        {
            var ingrTrans = ingredient.transform;
            ingrTrans.SetParent(ingredientContainer);
            ingrTrans.DOLocalJump(new Vector3(0.05f, 0.2f, 0), jumpPower, 1, jumpDuration);
            StartCoroutine(OnMovingEndRoutine());
            IEnumerator OnMovingEndRoutine()
            {
                yield return new WaitForSeconds(jumpDuration);
                ingredient.gameObject.AddComponent<Rigidbody>();
                ingredientList.Add(ingredient);
                IngrediendAdded?.Invoke(ingredient);
            }
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