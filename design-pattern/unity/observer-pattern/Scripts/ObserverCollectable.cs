using UnityEngine;
using System;

public class ObserverCollectable : MonoBehaviour
{
    //public delegate void ObserverEvent(int points);
    //public static event ObserverEvent CollectedEvent;
    // or
    public static event Action<int> CollectedEvent;
    public static void OnCollectedEvent(int points)
    {
        CollectedEvent?.Invoke(points);
    }

    public static event Action CollectedNoParamEvent;
    public static void OnCollectedNoParamEvent()
    {
        CollectedNoParamEvent?.Invoke();
    }
}
