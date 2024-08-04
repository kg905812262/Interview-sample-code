using System;
using System.Collections.Generic;
using UnityEngine;

public static class ServiceLocator
{
    private static Dictionary<Type, IService> services = new();

    public static void Provide<T>(T service) where T : IService
    {
        if (!services.TryAdd(typeof(T), service))
            Debug.LogError($"Type of service {typeof(T)} already exists.");
    }

    public static T Get<T>() where T : IService
    {
        if (services.TryGetValue(typeof(T), out var service))
            return (T)service;
        else
            throw new ArgumentException($"No service of type {typeof(T)} is provided.");
    }
}
