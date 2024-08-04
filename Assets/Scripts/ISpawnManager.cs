public interface ISpawnManager : IService
{
    void Register(Spawner spawner);
    void Unregister(Spawner spawner);
}
