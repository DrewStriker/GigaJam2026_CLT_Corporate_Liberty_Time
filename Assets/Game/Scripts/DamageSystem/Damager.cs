using System;
using Game.Core;
using Game.Core.SimplePool;
using Game.Core.SimplePool.Game.Core.SimplePool;
using Game.Core.SimplePool.VfxPool;
using UnityEngine;
using Zenject;

namespace DamageSystem
{

    public class Damager : MonoBehaviour, IDamager
    {
        [SerializeField] private Bounds bounds = new Bounds(Vector3.forward*0.56f, Vector3.one);
        [SerializeField] private PoolObject hitEffectPrefab;
        private DamageData damageData = new();
        private Collider[] hits = new Collider[16];
        public event Action<Collider> OnHit = delegate { };
        [Inject] private VfxPoolFacade vfxPoolFacade;
        public void DoDamage(int damage)
        {
            int hitCount = Physics.OverlapBoxNonAlloc(
                transform.position + transform.forward * 1f,
                bounds.size * 0.5f,
                hits,
                transform.rotation,
                Layers.Character,
                QueryTriggerInteraction.Ignore);

            if (hitCount == 0) return;
            for (int i = 0; i < hitCount; i++)
            {
                if(hits[i].gameObject == this.gameObject) continue;
                if(!hits[i].TryGetComponent(out IDamageable damageable)) continue;
                ConfiguraDamageData(hits[i], damage);
                //TODO: refact
                vfxPoolFacade.Spawn(VfxType.Hit, damageData.AttackerPosition);
                
                
                damageable.TakeDamage(damageData);
                OnHit.Invoke(hits[i]);
            }
            

        }

        private void ConfiguraDamageData(Collider collider, int damage)
        {
            var position = collider.ClosestPoint(bounds.center);
            damageData.Configure(damage,position);
        }
        
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Vector3 center = transform.position + transform.forward + bounds.center;
            Matrix4x4 rotationMatrix = Matrix4x4.TRS(
                center,
                transform.rotation,
                Vector3.one);

            Gizmos.matrix = rotationMatrix;
            Gizmos.DrawWireCube(Vector3.zero, bounds.size);
        }


 
    }
}