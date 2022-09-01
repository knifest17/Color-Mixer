using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

namespace Assets.Scripts
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] Transform desiredColor;
        [SerializeField] SpriteRenderer desiredColorImage;
        [SerializeField] RectTransform resultScreen, resultBar;
        [SerializeField] TMPro.TMP_Text resultText;
        [SerializeField] Button mixBtn, nextBtn, restartBtn;

        [Header("Background Character")]
        [SerializeField] Transform developer;
        [SerializeField] Animator developerAnimator;
        [SerializeField] RuntimeAnimatorController idle, jump;
        [SerializeField] ParticleSystem confettiParticles;

        public event Action ResultShown;

        public void SetDesiredColor(Color color)
        {
            color.a = 1;
            StartCoroutine(SetColorRoutine());
            IEnumerator SetColorRoutine()
            {
                yield return new WaitForSeconds(0.5f);
                desiredColorImage.color = color;
            }
            desiredColor.DOScale(0, 0.5f);
            desiredColor.DOScale(1, 1).SetDelay(0.5f);
        }

        public void ShowResult(bool show)
        {
            resultScreen.gameObject.SetActive(show);
            resultBar.localScale = new Vector3(0, 1, 1);
            SetNextBtn(false);
            SetRestartBtn(false);
        }

        public void SetResultPercent(float value)
        {
            resultBar.DOScaleX(value, 5);
            StartCoroutine(ResultTextRoutine());
            IEnumerator ResultTextRoutine()
            {
                float t = 0;
                while (t <= 5)
                {
                    t += Time.deltaTime;
                    resultText.text = $"{Mathf.Ceil(resultBar.localScale.x * 100)}%";
                    print(value);
                    yield return null;
                }
                ResultShown?.Invoke();
            }
        }

        public void SetResultColor(Color color)
        {
            color.a = 1;
            resultBar.GetComponent<RawImage>().color = color;
        }

        public void SetMixBtn(bool active) => mixBtn.gameObject.SetActive(active);

        public void SetNextBtn(bool active) => nextBtn.gameObject.SetActive(active);

        public void SetRestartBtn(bool active) => restartBtn.gameObject.SetActive(active);

        public void SetWinBackground(bool active)
        {
            developerAnimator.runtimeAnimatorController = active ? jump : idle;
            if (active) confettiParticles.Play();
            else
            {
                developer.transform.DOLocalMoveY(0, 0.2f);
                confettiParticles.Stop();
            }
        }
    }
}