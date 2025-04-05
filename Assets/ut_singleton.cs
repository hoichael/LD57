using UnityEngine;

public class ut_singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T i { get; private set; }
    protected virtual void Awake()
    {
        if (i == null)
        {
            i = (T)FindFirstObjectByType(typeof(T));
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
