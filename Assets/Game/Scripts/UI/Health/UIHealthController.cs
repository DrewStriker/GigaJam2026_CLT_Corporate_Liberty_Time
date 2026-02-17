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
        [SerializeField] private HealthUnitUI UiHealthUnitPrefab;
        [Inject] private IPlayableCharacter playableCharacter;
        private List<HealthUnitUI> healthUnits = new();

        private CharacterStats characterStats => playableCharacter.characterStats;

        [Inject]
        private void Construct(IPlayableCharacter playableCharacter)
        {
            this.playableCharacter = playableCharacter;
        }

        private void Awake()
        {
            healthUnits = GetComponentsInChildren<HealthUnitUI>().ToList();
        }

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
            FillImages(value);
        }

        private void AddHealthUiUnit()
        {
            var healthUnit = Instantiate(UiHealthUnitPrefab, transform);
            healthUnits.Add(healthUnit);
            healthUnit.Show();
        }


        private void FillImages(int healthValue)
        {
            for (var i = 0; i < healthUnits.Count; i++)
                if (i < healthValue)
                    healthUnits[i].Show();
                else
                    healthUnits[i].Hide();
        }
    }
}