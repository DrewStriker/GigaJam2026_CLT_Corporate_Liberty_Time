using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Health
{
    public class HealthItemUI : MonoBehaviour
    {
        [SerializeField] private Image imageFill;

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