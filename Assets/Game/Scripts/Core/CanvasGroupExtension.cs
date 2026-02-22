using DG.Tweening;
using UnityEngine;

namespace Game.Core
{
    public static class CanvasGroupExtension
    {
        public static void SetActiveEffect(this CanvasGroup canvasGroup, bool active, float duration = 0.5f)
        {
            float fadeValue = active ? 1 : 0;

            canvasGroup.DOFade(fadeValue, duration).SetEase(Ease.OutQuad).SetUpdate(true);
            canvasGroup.interactable = active;
            canvasGroup.blocksRaycasts = active;
        }
    }
}