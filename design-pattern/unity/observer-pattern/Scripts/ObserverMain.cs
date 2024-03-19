using UnityEngine;
using UnityEngine.UI;

public class ObserverMain : MonoBehaviour
{
    [SerializeField] Button button;
    private int points = 10;

    private void Start()
    {
        button.onClick.AddListener(Handle);
    }

    private void Handle()
    {
        points++;
        ObserverCollectable.OnCollectedEvent(points);
    }
}
