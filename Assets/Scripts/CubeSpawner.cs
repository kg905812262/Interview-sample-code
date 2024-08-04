using UnityEngine;

public class CubeSpawner : Spawner
{
    private ICubeFactory cubeFactory;
    private GameObject spawnedObject;

    private void Awake()
    {
        IsSpawnable = true;
        cubeFactory = ServiceLocator.Get<ICubeFactory>();
    }

    public override void Load()
    {
        if (spawnedObject != null)
            Unload();

        spawnedObject = cubeFactory.Load();
        spawnedObject.transform.position = transform.position;
    }

    public override void Unload()
    {
        if (spawnedObject != null)
            cubeFactory.Unload(spawnedObject);
    }
}
