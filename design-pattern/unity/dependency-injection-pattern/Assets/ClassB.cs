using UnityEngine;
using DependencyInjection;

public class ClassB : MonoBehaviour
{
    [Inject] ClassA classA;

    private void Start()
    {
        Debug.Log(classA.textA.ToString());
        classA.Initialize("Initialize ClassA from ClassB");
    }

    public void Initialize(string message)
    {
        Debug.Log(message);
    }
}
