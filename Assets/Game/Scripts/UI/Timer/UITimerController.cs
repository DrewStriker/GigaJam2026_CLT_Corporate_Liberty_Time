using TMPro;
using UnityEngine;
using Zenject;
using Game.TimeSystem;
namespace Game.UI
{
    public class UITimerController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private TextMeshProUGUI extraTimeText;
        [Inject] private ITimerManager timerManager;

        private float totalGameMinutes = 17f * 60f;

        private void Start()
        {
            timerManager.MainTimeExpired += OnMainTimeExpired;
            timerManager.ExtraTimeExpired += OnExtraTimeExpired;
            HideExtraTime();
        }

        private void OnExtraTimeExpired()
        {
            Debug.Log("Timer expired");
        }

        private void OnMainTimeExpired()
        {
            ShowExtraTime();
        }

        private void Update()
        {
            UpdateTimer();
        }

        private void UpdateTimer()
        {
            timerText.text = GetFormattedTime();
        }

        private void UpdateExtraTime()
        {
            extraTimeText.text = $"+{timerManager.ExtraTime}h";
        }

        private string GetFormattedTime()
        {
            float currentMinutes = timerManager.AbsNormalizedTime * totalGameMinutes;

            int hours = Mathf.FloorToInt(currentMinutes / 60f);
            int minutes = Mathf.FloorToInt(currentMinutes % 60f);

            return $"{hours:00}:{minutes:00}";
        }

        private void HideExtraTime()
        {
            SetTextAlpha(extraTimeText, 0);
        }
        private void ShowExtraTime()
        {
            SetTextAlpha(extraTimeText, 1);
            UpdateExtraTime();
        }

        private void SetTextAlpha(TextMeshProUGUI text, float alpha)
        {
            Color color = text.color;
            color.a = alpha; 
            text.color = color;
        }
    }
}
