
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] private List<GameEvent> m_allEvents;
    [SerializeField] private float m_eventChance = 0.2f;//module with time too travel
    private GameContext m_context;
    public static EventManager Instance {  get; private set; }

    private void Awake()
    {
        Instance = this;
        
    }

    private void Start()
    {
        m_context = new GameContext();
    }

    /// <summary>
    /// Make a list of valid events and trigger one at random 
    /// </summary>
    public void TryTriggerEvent()
    {
        if(UnityEngine.Random.value > m_eventChance) { return; }
        List<GameEvent> valid = new();


        //look in all event witch one can be trigger
        foreach(var e in m_allEvents)
        {
            if (e.canTrigger(m_context))
            {
                valid.Add(e);
            }
        }

        if(valid.Count == 0)
        {
            return;
        }

        GameEvent choosen = valid[Random.Range(0, valid.Count)];
        choosen.Trigger(m_context);
    }


    /// <summary>
    /// Set the context at true if the player travel
    /// </summary>
    /// <param name="isTraveling"></param>
    public void SetContext(bool isTraveling)
    {
        if (m_context == null)
            m_context = new GameContext();

        m_context.isTraveling = isTraveling;
        m_context.ui = UiEvent.Instance;
    }

}
