using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator<T> : MonoBehaviour where T : class
{
    private static List<T> services;

    public static void Provide(T service)
    {
        if (services.Contains(service)) return;

        services.Add(service);
    }

    public static T GetService()
    {
        foreach (var service in services)
        {
            if (service is T)
            {
                return service;
            }
        }

        return null;
    }
}
