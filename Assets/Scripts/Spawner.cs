using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    /// <summary>
    /// 決定是否生成，條件讓derived class控制，例：與玩家的距離或特定時間範圍
    /// </summary>
    public bool IsSpawnable { get; protected set; }

    public abstract void Load();

    public abstract void Unload();

    private void OnEnable()
    {
        ServiceLocator.Get<ISpawnManager>().Register(this);
    }

    private void OnDisable()
    {
        ServiceLocator.Get<ISpawnManager>().Unregister(this);
    }
}
