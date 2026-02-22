using Game.CollectableSystem;
using Game.InteractionSystem;
using Game.WeaponSystem;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
namespace Game.SpawnSystem
{
    public class WeaponSpawner : MonoBehaviour
    {
        [Header("Spawnning Area (form above the scene)")]
        public Vector2 spawningArea = new Vector2(20f, 20f);

        [SerializeField] private int maxWeapons;
        [SerializeField] private int maxGrabbables;
        [Range(0, 100)]
        [SerializeField] private int spawningChance;

        [Inject] private PlaceholderFactory<WeaponType, WeaponCollectable> weaponFactory;
        [Inject] private PlaceholderFactory<GrabbableType, GrabbableObject> grabbableFactory;

        private void Start()
        {
            SpawnWeapons();
            SpawnInteractables();
        }
        private void SpawnWeapons()
        {
            for (int i = 0; i < maxWeapons; i++)
            {
                if (Random.Range(0, 100) < spawningChance)
                {
                    var weapon = weaponFactory.Create((WeaponType)Random.Range(0, System.Enum.GetValues(typeof(WeaponType)).Length));
                    weapon.transform.position = SortRandomPosition();
                }
            }
        }

        private void SpawnInteractables()
        {
            for (int i = 0; i < maxGrabbables; i++)
            {
                if (Random.Range(0, 100) < spawningChance)
                {
                    var grabbable = grabbableFactory.Create((GrabbableType)Random.Range(0, System.Enum.GetValues(typeof(GrabbableType)).Length));
                    grabbable.transform.position = SortRandomPosition();
                }
            }
        }

        private Vector3 SortRandomPosition()
        {
            return SearchClosestPointOnNavMesh(GetRandomPointInRectangle());
        }

        private Vector3 SearchClosestPointOnNavMesh(Vector3 origin, float additiveRadius = 0f)
        {
            float searchRadius = 10f + additiveRadius;
            if (NavMesh.SamplePosition(origin, out NavMeshHit hit, searchRadius, NavMesh.AllAreas))
            {
                return hit.position;
            }

            else return SearchClosestPointOnNavMesh(origin, searchRadius);
        }

        private Vector3 GetRandomPointInRectangle()
        {
            Vector2 halfSize = spawningArea * 0.5f;

            float randomX = Random.Range(-halfSize.x, halfSize.x);
            float randomZ = Random.Range(-halfSize.y, halfSize.y);

            return transform.position + new Vector3(randomX, 0f, randomZ);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(Vector3.zero, new Vector3(spawningArea.x, 0f, spawningArea.y));
        }
    }
}
