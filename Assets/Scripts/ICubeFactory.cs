using UnityEngine;

public interface ICubeFactory : IService
{
    GameObject Load();
    void Unload(GameObject cube);
}
