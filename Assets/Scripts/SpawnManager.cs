using System;
using System.Collections.Generic;

public class SpawnManager : ISpawnManager, IService, IUpdatable
{
    public bool Enabled { get; private set; } = true;

    private readonly List<Spawner> loadingSpawners = new();
    private readonly List<Spawner> loadedSpawners = new();
    private readonly MonoUpdater monoUpdater;

    public SpawnManager(MonoUpdater monoUpdater, bool enabled)
    {
        if (monoUpdater == null)
            throw new ArgumentNullException(nameof(monoUpdater));

        this.monoUpdater = monoUpdater;
        monoUpdater.Add(this);
        Enabled = enabled;
    }

    ~SpawnManager()
    {
        monoUpdater.Remove(this);
    }

    public void Register(Spawner spawner)
    {
        if (!loadingSpawners.Contains(spawner) && !loadedSpawners.Contains(spawner))
            loadingSpawners.Add(spawner);
    }

    public void Unregister(Spawner spawner)
    {
        loadingSpawners.Remove(spawner);
        loadedSpawners.Remove(spawner);
    }

    public void Update()
    {
        if (loadingSpawners.Count > 0)
        {
            Spawner spawner = null;
            foreach (var item in loadingSpawners)
            {
                if (item.IsSpawnable)
                {
                    spawner = item;
                    break;
                }
            }

            if (spawner != null)
            {
                spawner.Load();
                loadingSpawners.Remove(spawner);
                loadedSpawners.Add(spawner);
            }
        }

        if (loadedSpawners.Count > 0)
        {
            Spawner spawner = null;
            foreach (var item in loadedSpawners)
            {
                if (!item.IsSpawnable)
                {
                    spawner = item;
                    break;
                }
            }

            if (spawner != null)
            {
                spawner.Unload();
                loadingSpawners.Add(spawner);
                loadedSpawners.Remove(spawner);
            }
        }
    }
}
