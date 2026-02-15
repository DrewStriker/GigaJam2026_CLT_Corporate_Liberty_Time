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

        private void Awake()
        {
            renderer = GetComponentInChildren<Renderer>();
        }

        private void OnEnable()
        {
           transform.DOMoveY(transform.position.y+floatingHight,floatingDuration)
               .SetLoops(-1,LoopType.Yoyo)
               .SetEase(Ease.InOutSine);
           renderer.DoColor(
                   ShaderProperties.OverlayColor,Color.white,
                   floatingDuration/3)
               .SetLoops(-1,LoopType.Yoyo)
               .SetEase(Ease.InOutSine);
        }
        
        
    }
}