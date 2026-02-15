using System;
using DG.Tweening;
using Game.CollectableSystem;
using Game.Core;
using UnityEngine;

namespace Game.WeaponSystem
{
    public class WeaponController : CollectableObject<WeaponType>
    {
        private const float floatingHight = 0.5f;
        private const float floatingDuration =1;
        private Renderer renderer;
        Tween moveTween;
        Tween colorTween;
        private void Awake()
        {
            renderer = GetComponentInChildren<Renderer>();
        }

        private void OnEnable()
        {
            StartEffect();
        }

        private void OnDisable()
        {
            StopEffect();
        }

        public override void Collect(ICollector<WeaponType> collector)
        {
            base.Collect(collector);
            StopEffect();
        }

        private void StartEffect()
        {
            StopEffect();
           moveTween = transform.DOMoveY(transform.position.y+floatingHight,floatingDuration)
                .SetLoops(-1,LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
           colorTween = renderer.DoColor(
                    ShaderProperties.OverlayColor,new Color(1,1,1,0.3f),
                    floatingDuration/3)
                .SetLoops(-1,LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }

        private void StopEffect()
        {
            moveTween?.Kill();
            colorTween?.Kill();
        }
        
        
    }
}