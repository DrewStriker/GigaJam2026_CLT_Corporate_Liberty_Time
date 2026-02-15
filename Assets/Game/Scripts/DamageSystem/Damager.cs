using System;
using Game.Core;
using UnityEngine;

namespace DamageSystem
{

    public class Damager : MonoBehaviour, IDamager
    {
        [SerializeField] private Bounds bounds = new Bounds(Vector3.forward*0.56f, Vector3.one);
        private DamageData damageData = new();
        private Collider[] hits = new Collider[16];
        public event Action<Collider> OnHit = delegate { };
        
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
            damageData.Configure(damage, transform.position);
            
            for (int i = 0; i < hitCount; i++)
            {
                if(hits[i].gameObject == this.gameObject) continue;
                if(!hits[i].TryGetComponent(out IDamageable damageable)) continue;
                damageable.TakeDamage(damageData);
                OnHit.Invoke(hits[i]);
            }
            

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