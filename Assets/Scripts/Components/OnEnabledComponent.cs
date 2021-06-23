using UnityEngine;
using UnityEngine.Events;

public class OnEnabledComponent : MonoBehaviour
{
    [Header("Events")]
    public UnityEvent OnComponentEnabled;

    private void OnEnable()
    {
        OnComponentEnabled.Invoke();
    }
}
