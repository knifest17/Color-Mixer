using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Assets.Scripts
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] SpriteRenderer desiredColorImage;
        [SerializeField] RectTransform resultScreen, resultBar;
        [SerializeField] TMPro.TMP_Text resultText;

        public void SetDesiredColor(Color color)
        {
            desiredColorImage.color = color;
        }

        public void ShowResult(bool show)
        {
            resultScreen.gameObject.SetActive(show);
            resultBar.localScale = new Vector3(0, 1, 1);
        }

        public void SetResultPercent(float value)
        {
            resultBar.DOScaleX(value, 3);
            StartCoroutine(ResultTextRoutine());
            IEnumerator ResultTextRoutine()
            {
                float t = 0;
                while (t <= 3)
                {
                    t += Time.deltaTime;
                    resultText.text = $"{Mathf.Ceil(resultBar.localScale.x * 100)}%";
                    print(value);
                    yield return null;
                }
            }
        }

        public void SetResultColor(Color color)
        {
            color.a = 1;
            resultBar.GetComponent<RawImage>().color = color;
        }
    }
}