using System.Collections.Generic;
using System.Linq;
using Game.Characters;
using Game.StatsSystem;
using UnityEngine;
using Zenject;

namespace Game.UI.Health
{
    public class UIHealthController : MonoBehaviour
    {
        public PlayerController playableCharacter;
        private List<HealthItemUI> healthItemUIs = new();

        private CharacterStats characterStats => playableCharacter.characterStats;

        private void Awake()
        {
            healthItemUIs = GetComponentsInChildren<HealthItemUI>().ToList();
        }

        // [Inject]
        // private void Construct(IPlayableCharacter playableCharacter)
        // {
        //     print(this.playableCharacter.characterStats == null);
        //     this.playableCharacter = playableCharacter;
        //
        // }

        private void Start()
        {
            characterStats.HealthChanged += OnHealthChanged;
        }

        private void OnDestroy()
        {
            characterStats.HealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged(int value)
        {
            if (healthItemUIs.Count < value)
                AddHealthItem();
            FillImages(value);
        }

        private void AddHealthItem()
        {
            var healthItem = Instantiate(healthItemUIs[0], transform);
            healthItemUIs.Add(healthItem);
        }


        private void FillImages(int healthValue)
        {
            for (var i = 0; i < healthItemUIs.Count; i++)
                if (i < healthValue)
                    healthItemUIs[i].Show();
                else
                    healthItemUIs[i].Hide();
        }
    }
}