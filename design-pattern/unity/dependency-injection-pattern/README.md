## Features

- **Automatic Dependency Injection**: Automatically injects dependencies into your Unity MonoBehaviours.
- **Custom Attributes**: Use `[Inject]` and `[Provide]` attributes to denote injectable members and providers.
- **Method Injection**: Supports method injection for more complex and multiple initialization.
- **Field Injection**: Simplify your code with direct field injection.
- **Property Injection**: Supports property injection.

## Usage

### Defining Injectable Fields, Methods and Properties

### Creating Providers

Implement IDependencyProvider and use the `[Provide]` attribute on methods to define how dependencies are created.

```csharp
using DependencyInjection;
using UnityEngine;

public class Provider : MonoBehaviour, IDependencyProvider {
    [Provide]
    public ClassA ProvideServiceA() {
        return new ClassA();
    }

    // Other provides...
}
```

Use the `[Inject]` attribute on fields, methods, or properties to mark them as targets for injection.

```csharp
using DependencyInjection;
using UnityEngine;

public class ClassB : MonoBehaviour {
    [Inject] ClassA classA;

    private void Start() {
        classA.DoSomeThing();
    }
}
```

### Example of Using Multiple Dependencies

```csharp
using DependencyInjection;
using UnityEngine;

public class ClassC : MonoBehaviour {
    ClassA classA;
    ClassB classB;

    [Inject] // Method injection supports multiple dependencies
    public void Init(ClassA classA, ClassB classB) {
        this.classA = classA;
        this.classB = classB;
    }

    private void Start() {
        classA.DoSomeThing();
        classB.DoSomeThing();
    }
}
```

## Setup

- Include the Dependency Injection System: Add the provided DependencyInjection namespace and its classes to your project.
- Add the Injector Component: Attach the Injector MonoBehaviour to a GameObject in your scene.
- Define Providers: Create provider MonoBehaviours and attach them to GameObjects.
- Mark Providers: Use [Provide] in your MonoBehaviours to provide a dependency of a particular type.
- Mark Dependencies: Use [Inject] in your MonoBehaviours to satifsy dependencies.

## Referrence

- [**Watch the original tutorial video here**](https://youtu.be/PJcBJ60C970)
- https://github.com/adammyhre/Unity-Dependency-Injection-Lite/tree/master
