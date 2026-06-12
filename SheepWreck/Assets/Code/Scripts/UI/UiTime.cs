using TMPro;
using UnityEngine;

public class UiTime : MonoBehaviour
{
    [SerializeField] private TMP_Text m_timeText;

    private void Start()
    {
        m_timeText.text = $" Month:0 / Week : 0 / day: 0";
        TimeManager.Instance.OnDayPast += NotifyTimeChange;
        TimeManager.Instance.OnWeekPast += NotifyTimeChange;
        TimeManager.Instance.OnMonthPast += NotifyTimeChange;
    }

    private void OnDisable()
    {
        TimeManager.Instance.OnDayPast -= NotifyTimeChange;
        TimeManager.Instance.OnWeekPast -= NotifyTimeChange;
        TimeManager.Instance.OnMonthPast -= NotifyTimeChange;
    }

    private void NotifyTimeChange(int months,int weeks,int days)
    {
        m_timeText.text = $" Month:{months} / Week : {weeks} / day: {days}";
    }
}
