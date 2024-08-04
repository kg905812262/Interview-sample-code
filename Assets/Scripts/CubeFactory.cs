using UnityEngine;
using UnityEngine.Pool;

public class CubeFactory : ICubeFactory, IService
{
    private readonly IObjectPool<GameObject> pool;

    public CubeFactory(IObjectPool<GameObject> pool)
    {
        this.pool = pool ?? throw new System.ArgumentNullException(nameof(pool));
    }

    public GameObject Load() => pool.Get();

    public void Unload(GameObject cube) => pool.Release(cube);
}
