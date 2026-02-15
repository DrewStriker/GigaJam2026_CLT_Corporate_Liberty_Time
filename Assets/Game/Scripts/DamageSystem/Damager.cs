using System;
using Game.Core;
using Game.Core.SimplePool;
using Game.Core.SimplePool.SfxPool;
using Game.Core.SimplePool.VfxPool;
using UnityEngine;
using Zenject;

namespace DamageSystem
{
    public class Damager : MonoBehaviour, IDamager
    {
        [SerializeField] private Bounds bounds = new(Vector3.forward * 0.56f, Vector3.one);
        private readonly DamageData damageData = new();
        private readonly Collider[] hits = new Collider[16];
        [Inject] private SfxPoolFacade sfxPoolFacade;
        [Inject] private VfxPoolFacade vfxPoolFacade;


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            var center = transform.position + transform.forward + bounds.center;
            var rotationMatrix = Matrix4x4.TRS(
                center,
                transform.rotation,
                Vector3.one);

            Gizmos.matrix = rotationMatrix;
            Gizmos.DrawWireCube(Vector3.zero, bounds.size);
        }

        public void DoDamage(int damage)
        {
            var hitCount = Physics.OverlapBoxNonAlloc(
                transform.position + transform.forward * 1f,
                bounds.size * 0.5f,
                hits,
                transform.rotation,
                Layers.Character,
                QueryTriggerInteraction.Ignore);

            if (hitCount == 0) return;
            for (var i = 0; i < hitCount; i++)
            {
                if (hits[i].gameObject == gameObject) continue;
                if (!hits[i].TryGetComponent(out IDamageable damageable)) continue;
                ConfiguraDamageData(hits[i], damage);
                vfxPoolFacade.Spawn(VfxType.Hit, damageData.AttackerPosition);
                sfxPoolFacade.Play(SfxType.Hit, damageData.AttackerPosition, 1, true);
                damageable.TakeDamage(damageData);
                OnHit.Invoke(hits[i]);
            }
        }

        public event Action<Collider> OnHit = delegate { };

        private void ConfiguraDamageData(Collider collider, int damage)
        {
            var position = collider.ClosestPoint(bounds.center);
            damageData.Configure(damage, position);
        }
    }
}