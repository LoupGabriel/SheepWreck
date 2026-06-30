using UnityEngine;

public class SheepStateMachine 
{
   public SheepState m_currentSheepState {  get;  set; }


    public void Initialize(SheepState startingState,Animator animator)
    {
        m_currentSheepState = startingState;
        m_currentSheepState.Enter(animator);
    }

    public void ChangeState(SheepState newState,Animator animator)
    {

        m_currentSheepState.Exit();
        m_currentSheepState = newState;
        m_currentSheepState.Enter(animator);

    }
}
