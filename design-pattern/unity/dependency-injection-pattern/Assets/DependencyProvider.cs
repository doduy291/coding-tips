using UnityEngine;
using DependencyInjection;

public class DependencyProvider : MonoBehaviour, IDependencyProvider
{
    [Provide]
    public ClassA ProviderClassA()
    {
        return new ClassA();
    }

    [Provide]
    public ClassB ProviderClassB()
    {
        return new ClassB();
    }
}
