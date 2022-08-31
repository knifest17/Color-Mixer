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
        [SerializeField] Transform ingredientContainer, containerCollider, fluid, floor;
        [SerializeField] Transform lid, lidClose, lidOpen;
        [SerializeField] float openDuration, jumpDuration, jumpPower, mixDuration;
        [SerializeField] Vector3 jumpRotation;

        List<Ingredient> ingredientList = new List<Ingredient>();
        bool lidOpened = false, addingIngrediet = false;

        public event Action MixStarted;
        public event Action<Color> Mixed;
        public event Action<Ingredient> IngrediendAdded;

        public void AddIngredient(Ingredient ingredient)
        {
            StartCoroutine(OnMovingEndRoutine());
            IEnumerator OnMovingEndRoutine()
            {
                if (!lidOpened)
                {
                    lid.DOKill();
                    lid.DOJump(lidOpen.position, 0.1f, 1, openDuration);
                    lid.DORotateQuaternion(lidOpen.rotation, openDuration);
                    yield return new WaitForSeconds(openDuration);
                    lidOpened = true;
                }
                var ingrTrans = ingredient.transform;
                ingrTrans.SetParent(ingredientContainer);
                ingrTrans.DOLocalJump(new Vector3(0.05f, 0.1f, 0), jumpPower, 1, jumpDuration);
                ingrTrans.DOLocalRotate(jumpRotation, jumpDuration);
                addingIngrediet = true;
                yield return new WaitForSeconds(jumpDuration);
                ingredient.gameObject.AddComponent<Rigidbody>();
                ingredientList.Add(ingredient);
                IngrediendAdded?.Invoke(ingredient);
                yield return new WaitForSeconds(0.2f);
                transform.DOShakeRotation(0.2f, 5 / Mathf.Sqrt(ingredientList.Count), 15);
                addingIngrediet = false;
                yield return new WaitForSeconds(0.5f);
                if (lidOpened && !addingIngrediet)
                {
                    lid.DOLocalJump(lidClose.localPosition, 0.1f, 1, openDuration);
                    lid.DORotateQuaternion(lidClose.rotation, openDuration);
                    lidOpened = false;
                }
            }
        }

        public void Mix()
        {
            StartCoroutine(MixingRoutine());
            IEnumerator MixingRoutine()
            {
                MixStarted?.Invoke();
                //floor.DOLocalMoveY(0.1f, mixDuration + 0.5f);
                //containerCollider.DOLocalRotate(new Vector3(0, 360, 0), mixDuration, RotateMode.FastBeyond360);
                transform.DOShakeRotation(mixDuration, 10);
                var color = ColorTools.CombineColors(ingredientList.Select(i => i.Color).ToArray());
                fluid.DOScaleY(0.3f, mixDuration);
                foreach (Ingredient i in ingredientList)
                    i.transform.DOScale(0.05f, mixDuration);
                fluid.gameObject.GetComponent<MeshRenderer>().material.color = color;
                yield return new WaitForSeconds(mixDuration);
                Mixed?.Invoke(color);
            }
        }

        public void Clear()
        {
            ingredientContainer.DestroyChildren();
            ingredientList.Clear();
            Vector3 floorPos = floor.localPosition;
            floorPos.y = 0;
            floor.localPosition = floorPos;
            var fluidScale = fluid.localScale;
            fluidScale.y = 0;
            fluid.localScale = fluidScale;
        }
    }
}