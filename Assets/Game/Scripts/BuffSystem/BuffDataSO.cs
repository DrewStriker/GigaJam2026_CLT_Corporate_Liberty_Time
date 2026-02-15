using Cysharp.Threading.Tasks;
using Game.StatsSystem;
using UnityEngine;

namespace Game.Scripts.BuffSystem
{
    [CreateAssetMenu(menuName = "Character/buff")]
    public class BuffDataSO : ScriptableObject
    {
        [field: SerializeField]
        [field: Min(0)]
        public float Duration { get; private set; }

        [field: SerializeField]
        [field: Min(0)]
        public int Damage { get; private set; }

        [field: SerializeField]
        [field: Min(0)]
        public int Health { get; private set; }

        [field: SerializeField]
        [field: Range(0, 1000)]
        public float MoveSpeed { get; private set; }

        public async void ApplyBuff(CharacterStats stats)
        {
            stats.IncreaseHealth(Health);
            AddBuff(stats);
            if (Duration == 0) return;
            await UniTask.Delay((int)(Duration * 1000));
            RemoveBuff(stats);
        }

        private void AddBuff(CharacterStats stats)
        {
            stats.IncreaseMoveSpeed(+MoveSpeed);
            stats.IncreaseDamage(+Damage);
        }

        private void RemoveBuff(CharacterStats stats)
        {
            stats.IncreaseMoveSpeed(-MoveSpeed);
            stats.IncreaseDamage(-Damage);
        }
    }
}