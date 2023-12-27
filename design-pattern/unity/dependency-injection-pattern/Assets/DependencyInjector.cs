using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEngine;

namespace DependencyInjection
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Property)]
    public sealed class InjectAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class ProvideAttribute : Attribute { }

    public interface IDependencyProvider { }

    [DefaultExecutionOrder(-1000)] // Make class execute first
    public class DependencyInjector : MonoBehaviour
    {
        const BindingFlags k_bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        readonly Dictionary<Type, object> registry = new();

        void Awake()
        {
            var monoBehaviours = FindMonoBehaviours();

            // Find all modules implementing IDependencyProvider and register the dependencies they provide
            var providers = monoBehaviours.OfType<IDependencyProvider>();
            foreach (var provider in providers)
            {
                RegisterProvider(provider);
            }

            // Find all injectable objects and inject their dependencies
            var injectables = monoBehaviours.Where(IsInjectable);
            foreach (var injectable in injectables)
            {
                Inject(injectable);
            }
        }

        void Inject(object instance)
        {
            var type = instance.GetType();

            // Inject into fields
            #region Field Injection
            var injectableFields = type.GetFields(k_bindingFlags)
                .Where(member => Attribute.IsDefined(member, typeof(InjectAttribute)));

            foreach (var injectableField in injectableFields)
            {
                var fieldType = injectableField.FieldType;
                var resolvedInstance = Resolve(fieldType);
                if (resolvedInstance == null)
                {
                    throw new Exception($"Failed to inject dependency into field '{injectableField.Name}' of class '{type.Name}'.");
                }
                injectableField.SetValue(instance, resolvedInstance);
                Debug.Log($"Field:::: Injected {fieldType.Name} into {type.Name}");
            }
            #endregion

            // Inject into methods
            #region Method Injection
            var injectableMethods = type.GetMethods(k_bindingFlags)
                .Where(member => Attribute.IsDefined(member, typeof(InjectAttribute)));

            foreach (var injectableMethod in injectableMethods)
            {
                var requiredParameters = injectableMethod.GetParameters()
                    .Select(parameter => parameter.ParameterType)
                    .ToArray();
                var resolvedInstances = requiredParameters.Select(Resolve).ToArray();
                if (resolvedInstances.Any(resolvedInstance => resolvedInstance == null))
                {
                    throw new Exception($"Failed to inject dependencies into method '{injectableMethod.Name}' of class '{type.Name}'.");
                }

                injectableMethod.Invoke(instance, resolvedInstances);
                Debug.Log($"Method:::: Injected {type.Name}.{injectableMethod.Name}");
            }
            #endregion

            // Inject into properties
            #region Properties Injection
            var injectableProperties = type.GetProperties(k_bindingFlags)
                .Where(member => Attribute.IsDefined(member, typeof(InjectAttribute)));
            foreach (var injectableProperty in injectableProperties)
            {
                var propertyType = injectableProperty.PropertyType;
                var resolvedInstance = Resolve(propertyType);
                if (resolvedInstance == null)
                {
                    throw new Exception($"Failed to inject dependency into property '{injectableProperty.Name}' of class '{type.Name}'.");
                }

                injectableProperty.SetValue(instance, resolvedInstance);
            }
            #endregion
        }

        void RegisterProvider(IDependencyProvider provider)
        {
            var methods = provider.GetType().GetMethods(k_bindingFlags);

            foreach (var method in methods)
            {
                if (!Attribute.IsDefined(method, typeof(ProvideAttribute))) continue;

                var returnType = method.ReturnType;
                var providedInstance = method.Invoke(provider, null);
                if (providedInstance != null)
                {
                    registry.Add(returnType, providedInstance);
                    Debug.Log($"Registered {returnType.Name} from {provider.GetType().Name}");
                }
                else
                {
                    throw new Exception($"Provider method '{method.Name}' in class '{provider.GetType().Name}' returned null when providing type '{returnType.Name}'.");
                }
            }

        }

        object Resolve(Type type)
        {
            registry.TryGetValue(type, out var resolvedInstance);
            return resolvedInstance;
        }

        static MonoBehaviour[] FindMonoBehaviours()
        {
            return FindObjectsOfType<MonoBehaviour>();
        }

        static bool IsInjectable(MonoBehaviour obj)
        {
            var members = obj.GetType().GetMembers(k_bindingFlags);
            return members.Any(member => Attribute.IsDefined(member, typeof(InjectAttribute)));
        }
    }
}
