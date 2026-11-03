using UnityEngine;
using System;
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;
    private static bool initialized = false;

    public static T Instance => instance;
    public event Action OnInitialize;

    [SerializeField] protected bool DoNotDestroyOnLoad;

    protected virtual void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        instance = this as T;

        initialized = true;
        OnInitialize?.Invoke();


        if (DoNotDestroyOnLoad)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    protected virtual void OnDestroy()
    {
        initialized = false;
    }
}
