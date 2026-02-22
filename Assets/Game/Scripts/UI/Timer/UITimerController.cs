using TMPro;
using UnityEngine;
using Zenject;
using Game.TimeSystem;
using UnityEngine.UI;

namespace Game.UI
{
    public class UITimerController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private TextMeshProUGUI extraTimeText;
        [SerializeField] private Image imageClockFill;
        [SerializeField] private int startHour = 9;
        [Inject] private ITimerManager timerManager;

        private float totalGameMinutes = 8f * 60f;

        private void Start()
        {
            imageClockFill.fillAmount = 0;
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
            timerText.SetText(GetFormattedTime());
            UpdateClockFill();
        }


        private void UpdateExtraTime()
        {
            extraTimeText.SetText($"+{timerManager.ExtraTime}h");
        }


        private string GetFormattedTime()
        {
            float startHourOffset = startHour * 60f;
            var currentMinutes = startHourOffset + (timerManager.AbsNormalizedTime * totalGameMinutes);

            var hours = Mathf.FloorToInt(currentMinutes / 60f);
            var minutes = Mathf.FloorToInt(currentMinutes % 60f);

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
            var color = text.color;
            color.a = alpha;
            text.color = color;
        }

        private void UpdateClockFill()
        {
            imageClockFill.fillAmount = timerManager.Timer.RelativeNormalizedProgress;
        }
    }
}