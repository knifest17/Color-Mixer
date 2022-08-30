using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class SelectingManager : MonoBehaviour
    {
        [SerializeField] Blender blender;

        Camera cam;

        public event Action<Ingredient> IngredientSelected;

        void Awake() => cam = Camera.main;

        void Update()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit))
            {
                var ingredient = hit.transform.GetComponent<Ingredient>();
                if (ingredient is not null)
                {
                    IngredientSelected?.Invoke(ingredient);
                    blender.AddIngredient(ingredient);
                }
            }
        }
    }
}