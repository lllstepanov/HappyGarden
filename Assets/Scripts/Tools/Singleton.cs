using UnityEngine;

internal class Singleton<T> : MonoBehaviour where T : Component
{
    internal static T Instance;

    internal virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
