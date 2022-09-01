using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts
{
    public class IngridientContainer : MonoBehaviour
    {
        [SerializeField] float space, restoreDuration;
        [SerializeField] Transform container, jumpStart;

        public void SpawnIngridients(Ingredient[] ingredients)
        {
            for (int i = 0; i < ingredients.Length; i++)
            {
                float x = (i - ingredients.Length * 0.5f + 0.5f) * space;
                var spawnPoint = container.position + new Vector3(x, 0, 0);
                Instantiate(ingredients[i], spawnPoint, ingredients[i].transform.rotation, container);
            }
        }

        public void RestoreIngridient(Ingredient ingredient, Vector3 position)
        {
            var newIngr = Instantiate(ingredient, container).transform;
            newIngr.localPosition = jumpStart.localPosition;
            newIngr.DOLocalJump(position, 0.5f, 1, restoreDuration);
        }

        public void Clear() => container.DestroyChildren();
    }
}