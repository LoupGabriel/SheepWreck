using UnityEditor.EditorTools;
using UnityEngine;

public abstract class GameEvent : ScriptableObject
{
   

    public abstract bool canTrigger(GameContext context);

    public abstract void Trigger(GameContext context);

    
}



public class GameContext
{
    public bool isTraveling;

    public UiEvent ui;
}