using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

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
            ingredient.IsAddedToMixer = true;
            if(ingredientList.Count >= 5)
            {
                var ingr = ingredientList[ingredientList.Count - 5];
                Destroy(ingr.GetComponent<Rigidbody>());
                Destroy(ingr.GetComponent<Collider>());
            }
            StartCoroutine(OnMovingEndRoutine());
            IEnumerator OnMovingEndRoutine()
            {
                addingIngrediet = true;
                if (!lidOpened)
                {
                    lid.DOKill();
                    lid.DOLocalJump(lidOpen.localPosition, 0.1f, 1, openDuration);
                    lid.DOLocalRotateQuaternion(lidOpen.localRotation, openDuration);
                    yield return new WaitForSeconds(openDuration);
                    lidOpened = true;
                }
                var ingrTrans = ingredient.transform;
                ingrTrans.SetParent(ingredientContainer);
                ingrTrans.DOLocalJump(new Vector3(0, 0.1f, 0), jumpPower, 1, jumpDuration);
                ingrTrans.DOLocalRotate(jumpRotation, jumpDuration);
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
                    lid.DOLocalRotateQuaternion(lidClose.localRotation, openDuration);
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
                containerCollider.DOLocalRotate(new Vector3(0, 360, 0), mixDuration, RotateMode.FastBeyond360);
                transform.DOShakeRotation(mixDuration, 10);
                Camera.main.transform.DOShakePosition(mixDuration, 0.01f);
                var color = ColorTools.CombineColors(ingredientList.Select(i => i.Color).ToArray());
                fluid.gameObject.SetActive(true);
                fluid.DOScaleY(0.3f, mixDuration);
                float d = 0;
                float incr = 1f / ingredientList.Count;
                foreach (Ingredient i in ingredientList)
                {
                    i.transform.DOScale(0, mixDuration - d).SetDelay(d += incr);
                    i.transform.DOLocalMoveY(-0.1f, mixDuration - d).SetDelay(d += incr);
                }
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
            fluid.gameObject.SetActive(false);
        }
    }
}