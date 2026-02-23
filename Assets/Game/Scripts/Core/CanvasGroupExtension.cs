using DG.Tweening;
using UnityEngine;

namespace Game.Core
{
    public static class CanvasGroupExtension
    {
        public static Tween SetActiveEffect(this CanvasGroup canvasGroup, bool active, float duration = 0.5f)
        {
            float fadeValue = active ? 1 : 0;

            canvasGroup.interactable = active;
            canvasGroup.blocksRaycasts = active;
            return canvasGroup.DOFade(fadeValue, duration).SetEase(Ease.OutQuad).SetUpdate(UpdateType.Normal, true);
        }
    }
}