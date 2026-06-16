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

    public Action<int,int,int> OnDayPast;
    public Action<int, int, int> OnWeekPast;
    public Action<int, int, int> OnMonthPast;
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
            
            OnDayPast?.Invoke(m_MonthPast,m_weekPast,m_dayPast);
            m_dayPast++;
            m_elapse = 0;
        }

        if (m_dayPast >= 7)
        {
            //week past
           
            m_weekPast++;
            m_dayPast = 0;
            OnWeekPast?.Invoke(m_MonthPast, m_weekPast, m_dayPast);
        }
        else if (m_dayPast >= 30 )
        {
            
            //month past 
            m_MonthPast++;
            m_weekPast = 0;
            OnMonthPast?.Invoke(m_MonthPast, m_weekPast, m_dayPast);
        }



    }
}
