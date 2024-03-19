using UnityEngine;
using UnityEngine.UI;

public class ObserverChild : MonoBehaviour
{
    [SerializeField] Text text;

    private void Start()
    {
        ObserverCollectable.CollectedEvent += SetPoint;
    }

    private void SetPoint(int point)
    {
        text.text = point.ToString();
    }
}
