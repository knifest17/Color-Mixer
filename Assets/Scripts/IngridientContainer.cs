using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class IngridientContainer : MonoBehaviour
    {
        [SerializeField] float space;
        [SerializeField] Transform container;

        public void SpawnIngridients(Ingredient[] ingredients)
        {
            for (int i = 0; i < ingredients.Length; i++)
            {
                float x = (i - ingredients.Length * 0.5f + 0.5f) * space;
                var spawnPoint = container.position + new Vector3(x, 0, 0);
                Instantiate(ingredients[i], spawnPoint, ingredients[i].transform.rotation, container);
            }
        }

        public void RestoreIngridient(Ingredient ingredient) => Instantiate(ingredient, container);

        public void Clear() => container.DestroyChildren();
    }
}