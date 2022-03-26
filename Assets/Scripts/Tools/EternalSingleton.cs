using UnityEngine;

internal class EternalSingleton<T> : MonoBehaviour where T : Component
{
    internal static T Instance;

    internal virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}