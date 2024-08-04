using System.Collections.Generic;
using UnityEngine;

public class MonoUpdater : MonoBehaviour
{
    private readonly HashSet<IUpdatable> updatables = new();

    public void Add(IUpdatable updatable)
    {
        updatables.Add(updatable);
    }

    public void Remove(IUpdatable updatable)
    {
        updatables.Remove(updatable);
    }

    void Update()
    {
        foreach (var updatable in updatables)
        {
            if (updatable.Enabled)
                updatable.Update();
        }
    }
}
