using UnityEngine;

public class SheepStateMachine 
{
   public SheepState m_currentSheepState {  get;  set; }


    public void Initialize(SheepState startingState)
    {
        m_currentSheepState = startingState;
        m_currentSheepState.Enter();
    }

    public void ChangeState(SheepState newState)
    {

        m_currentSheepState.Exit();
        m_currentSheepState = newState;
        m_currentSheepState.Enter();

    }
}
