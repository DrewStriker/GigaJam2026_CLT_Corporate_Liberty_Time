using System;
using Game.Core.SimplePool.SfxPool;
using Game.Core.SimplePool.VfxPool;
using UnityEngine;
using Zenject;

namespace DamageSystem
{
    public class Damager : MonoBehaviour, IDamager
    {
        [SerializeField] private LayerMask targetLayers; 
        [field: SerializeField] public Bounds Bounds { get; set; } = new(Vector3.up, Vector3.one);
        private readonly DamageData damageData = new();
        private readonly Collider[] hits = new Collider[16];
        [Inject] private SfxPoolFacade sfxPoolFacade;
        [Inject] private VfxPoolFacade vfxPoolFacade;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            var center = transform.position + transform.TransformDirection(Bounds.center);
            var rotationMatrix = Matrix4x4.TRS(
                center,
                transform.rotation,
                Vector3.one);

            Gizmos.matrix = rotationMatrix;
            Gizmos.DrawWireCube(Vector3.zero, Bounds.size);
        }

        public void DoDamage(int damage)
        {
            var center = transform.position + transform.TransformDirection(Bounds.center);
            var hitCount = Physics.OverlapBoxNonAlloc(
                center,
                Bounds.size * 0.5f,
                hits,
                transform.rotation,
                targetLayers,
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
            var position = collider.ClosestPoint(Bounds.center);
            damageData.Configure(damage, position);
        }
    }
}