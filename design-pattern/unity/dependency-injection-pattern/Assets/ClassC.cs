using DependencyInjection;
using UnityEngine;

public class ClassC : MonoBehaviour
{
    ClassA classA;
    ClassB classB;

    [Inject] // Method injection supports multiple dependencies
    public void Init(ClassA classA, ClassB classB)
    {
        this.classA = classA;
        this.classB = classB;
    }

    private void Start()
    {
        classA.Initialize("Initialize ClassA from ClassC");
        classB.Initialize("Initialize ClassA from ClassC");
    }
}
