using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Core
{

    public static class MaterialPropertyUtility
    {
        public static void DoFloat(this Renderer[] renderers, int propertyID, float targetValue, float duration = 1, Ease ease = Ease.Linear, float delay = 0, bool unscaledTime = false)
        {
            foreach (var renderer in renderers)
            {
                DoFloat(renderer, propertyID, targetValue, duration, ease, delay, unscaledTime);
            }
        }
        public static void DoFloat(this Renderer[] renderers, int propertyID, float targetValue, float duration, AnimationCurve ease, float delay = 0, bool unscaledTime = false)
        {
            foreach (var renderer in renderers)
            {
                DoFloat(renderer, propertyID, targetValue, duration, ease, delay, unscaledTime);
            }
        }
        public static Tween DoFloat(this Renderer renderer, int propertyID, float targetValue, float duration = 1, Ease ease = Ease.Linear, float delay = 0, bool unscaledTime = false)
        {
            var propertyBlock = new MaterialPropertyBlock();
            renderer.GetPropertyBlock(propertyBlock);

            float startValue = propertyBlock.GetFloat(propertyID);

            return DOTween.To(() => startValue, value =>
             {
                 startValue = value;
                 propertyBlock.SetFloat(propertyID, startValue);
                 renderer.SetPropertyBlock(propertyBlock);
             }, targetValue, duration)
                .SetDelay(delay).SetEase(ease).SetUpdate(unscaledTime);

        }
        public static Tween DoFloat(this Renderer renderer, int propertyID, float targetValue, float duration, AnimationCurve ease, float delay = 0, bool unscaledTime = false)
        {
            var propertyBlock = new MaterialPropertyBlock();
            renderer.GetPropertyBlock(propertyBlock);

            float startValue = propertyBlock.GetFloat(propertyID);

            return DOTween.To(() => startValue, value =>
            {
                startValue = value;
                propertyBlock.SetFloat(propertyID, startValue);
                renderer.SetPropertyBlock(propertyBlock);
            }, targetValue, duration)
                .SetDelay(delay).SetEase(ease).SetUpdate(unscaledTime);

        }
        public static Tween InterpolateInt(this Renderer renderer, int propertyID, int targetValue, float duration = 1, Ease ease = Ease.Linear, float delay = 0, bool unscaledTime = false)
        {
            var propertyBlock = new MaterialPropertyBlock();
            renderer.GetPropertyBlock(propertyBlock);
            int startValue = propertyBlock.GetInt(propertyID);

            return DOTween.To(() => startValue, value =>
            {
                startValue = value;
                propertyBlock.SetInt(propertyID, startValue);
                renderer.SetPropertyBlock(propertyBlock);
            }, targetValue, duration)
                .SetDelay(delay).SetEase(ease).SetUpdate(unscaledTime);

        }

        public static Tween DoVector(this Renderer renderer, int propertyID, Vector4 targetValue, float duration = 1, Ease ease = Ease.Linear, float delay = 0, bool unscaledTime = false)
        {
            var propertyBlock = new MaterialPropertyBlock();
            renderer.GetPropertyBlock(propertyBlock);
            Vector4 startValue = propertyBlock.GetVector(propertyID);

            return DOTween.To(() => startValue, value =>
             {
                 startValue = value;
                 propertyBlock.SetVector(propertyID, startValue);
                 renderer.SetPropertyBlock(propertyBlock);
             }, targetValue, duration)
                .SetDelay(delay).SetEase(ease).SetUpdate(unscaledTime);

        }

        public static Tween DoVector(this Renderer renderer, int propertyID, Vector4 targetValue, float duration, AnimationCurve ease, float delay = 0, bool unscaledTime = false)
        {
            var propertyBlock = new MaterialPropertyBlock();
            renderer.GetPropertyBlock(propertyBlock);
            Vector4 startValue = propertyBlock.GetVector(propertyID);

            return DOTween.To(() => startValue, value =>
            {
                startValue = value;
                propertyBlock.SetVector(propertyID, startValue);
                renderer.SetPropertyBlock(propertyBlock);
            }, targetValue, duration)
                .SetDelay(delay).SetEase(ease).SetUpdate(unscaledTime);

        }

        public static void DoColor(this Renderer[] renderers, int propertyID, Color targetValue, float duration = 1, Ease ease = Ease.Linear, float delay = 0, bool unscaledTime = false)
        {
            foreach (var renderer in renderers)
            {
                DoColor(renderer, propertyID, targetValue, duration, ease, delay, unscaledTime);
            }
        }

        public static Tween DoColor(this Renderer renderer, int propertyID, Color targetValue, float duration = 1, Ease ease = Ease.Linear, float delay = 0, bool unscaledTime = false)
        {
            var propertyBlock = new MaterialPropertyBlock();
            renderer.GetPropertyBlock(propertyBlock);
            Color startValue = propertyBlock.GetColor(propertyID);

            return DOTween.To(() => startValue, value =>
            {
                startValue = value;
                propertyBlock.SetColor(propertyID, startValue);
                renderer.SetPropertyBlock(propertyBlock);
            }, targetValue, duration)
                .SetDelay(delay).SetEase(ease).SetUpdate(unscaledTime);

        }

        public static Tween DoColor(this Renderer renderer, int propertyID, Color targetValue, float duration, AnimationCurve ease, float delay = 0, bool unscaledTime = false)
        {
            var propertyBlock = new MaterialPropertyBlock();
            renderer.GetPropertyBlock(propertyBlock);
            Color startValue = propertyBlock.GetColor(propertyID);

            return DOTween.To(() => startValue, value =>
            {
                startValue = value;
                propertyBlock.SetColor(propertyID, startValue);
                renderer.SetPropertyBlock(propertyBlock);
            }, targetValue, duration)
                .SetDelay(delay).SetEase(ease).SetUpdate(unscaledTime);

        }


        public static Tween DoFloat(this Image image, int propertyID, float targetValue, float duration, AnimationCurve ease, float delay = 0f, bool unscaledTime = false)
        {
            float startValue = image.material.GetFloat(propertyID);

            return DOTween.To(() => startValue, value =>
            {
                startValue = value;
                image.material.SetFloat(propertyID, startValue);
            }, targetValue, duration)
            .SetDelay(delay)
            .SetEase(ease)
            .SetUpdate(unscaledTime);
        }
        public static Tween DoFloat(this Image image, int propertyID, float targetValue, float duration, Ease ease, float delay = 0f, bool unscaledTime = false)
        {
            float startValue = image.material.GetFloat(propertyID);

            return DOTween.To(() => startValue, value =>
            {
                startValue = value;
                image.material.SetFloat(propertyID, startValue);
            }, targetValue, duration)
            .SetDelay(delay)
            .SetEase(ease)
            .SetUpdate(unscaledTime);
        }
    }

}
