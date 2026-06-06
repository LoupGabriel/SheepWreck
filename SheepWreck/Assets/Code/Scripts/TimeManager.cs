using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

    public static TimeManager Instance {  get; private set; }
    private float m_elapse = 0f;
    private static float m_dayDuration = 10f; // 
  

    private int m_dayPast = 0;
    private int m_weekPast = 0;
    private int m_MonthPast = 0;

    public Action OnDayPast;
    public Action OnWeekPast;
    public Action OnMonthPast;
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        m_elapse += Time.deltaTime;

        
        if(m_elapse >= m_dayDuration)
        {

            // a day past
            Debug.Log("A day past");
            OnDayPast?.Invoke();
            m_dayPast++;
            m_elapse = 0;
        }

        if (m_dayPast / 7 == 1)
        {
            //week past
            Debug.Log("A week past");
            m_weekPast++;
            m_dayPast = 0;
            OnWeekPast?.Invoke();
        }
        else if (m_dayPast / 30 == 1)
        {
            Debug.Log("A Month past");
            //month past 
            m_MonthPast++;
            m_weekPast = 0;
            OnMonthPast?.Invoke();
        }



    }
}
