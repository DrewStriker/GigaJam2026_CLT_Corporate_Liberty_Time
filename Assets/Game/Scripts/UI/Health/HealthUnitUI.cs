using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Health
{
    public class HealthUnitUI : MonoBehaviour
    {
        [SerializeField] private Image imageFill;


        private void OnEnable()
        {
            Show();
        }

        public void Show()
        {
            imageFill.enabled = true;
        }

        public void Hide()
        {
            imageFill.enabled = false;
        }
    }
}