using UnityEngine;

/// <summary>
/// Manages coroutines and ensures only one instance of CoroutineManager exists.
/// </summary>
public class CoroutineManager : MonoBehaviour
{
    private static CoroutineManager instance;

    private void Awake()
    {
        // Ensure only one instance of CoroutineHandler exists
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static CoroutineManager Instance
    {
        get { return instance; }
    }
}
