using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviour
{
    [SerializeField]
    private string persistentSceneName = "Persistent";

    [SerializeField]
    private string initialSceneName = "Spawner Sample";

    private void Awake()
    {
        var operation = SceneManager.LoadSceneAsync(persistentSceneName, LoadSceneMode.Additive);
        operation.completed += OnLoadPersistentSceneCompleted;
    }

    private void OnLoadPersistentSceneCompleted(AsyncOperation obj)
    {
        var monoUpdater = FindAnyObjectByType<MonoUpdater>();
        if (monoUpdater != null)
        {
            InitializeServiceLocator(monoUpdater);
            var operation = SceneManager.LoadSceneAsync(initialSceneName, LoadSceneMode.Additive);
            operation.completed += OnLoadInitialSceneCompleted;
        }
        else
        {
            Debug.LogError("Cannot find any instance of MonoUpdater.");
        }
    }

    private void OnLoadInitialSceneCompleted(AsyncOperation obj)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(initialSceneName));
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }

    private static void InitializeServiceLocator(MonoUpdater monoUpdater)
    {
        // SpawnManager
        SpawnManager spawnManager = new(monoUpdater, true);
        ServiceLocator.Provide<ISpawnManager>(spawnManager);

        // CubeFactory
        ObjectPool<GameObject> cubePool = new(() => GameObject.CreatePrimitive(PrimitiveType.Cube), go => go.SetActive(true), go => go.SetActive(false), go => Destroy(go));
        CubeFactory cubeFactory = new(cubePool);
        ServiceLocator.Provide<ICubeFactory>(cubeFactory);
    }
}
