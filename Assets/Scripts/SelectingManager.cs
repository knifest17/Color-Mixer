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
#if UNITY_ANDROID
            if (Input.touchCount <= 0) return;
            Vector2 point = Input.GetTouch(0).position;
#else
            if (!Input.GetMouseButtonDown(0)) return;
            Vector2 point = Input.mousePosition;
#endif
            var ray = cam.ScreenPointToRay(point);
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