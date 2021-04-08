using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    #region Fields

    public GameObject[] SystemPrefab;
    private List<GameObject> _instancedSystemPrefabs;
    private List<AsyncOperation> _loadOperations;
    private int _currentLevelIndex = 0;
    private int _levelToLoad = 0;

    #endregion




    void Start()
    {
        DontDestroyOnLoad(gameObject);
        _loadOperations = new List<AsyncOperation>();
        _instancedSystemPrefabs = new List<GameObject>();
        InstantiateSystemPrefabs();
        UIManager.Instance.playMenuPlayClicked.AddListener(PlayClick);
        UIManager.Instance.playMenuDebugClicked.AddListener(DebugClick);
        UIManager.Instance.debugMenuBackClicked.AddListener(BackClick);
        UIManager.Instance.endMenuExitClicked.AddListener(ExitClick);
        UIManager.Instance.endMenuReturnClicked.AddListener(ReturnClick);
    }

    

    void Update()
    {
        
    }

    private void InstantiateSystemPrefabs()
    {
        GameObject prefabInstance;
        for (int i = 0; i < SystemPrefab.Length; i++)
        {
            prefabInstance = Instantiate(SystemPrefab[i]);
            _instancedSystemPrefabs.Add(prefabInstance);
        }
    }

    #region Loading and Unloading Scenes

    public void LoadLevel(int LevelIndex)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(LevelIndex, LoadSceneMode.Additive);
        if (asyncOperation == null)
        {

        }
        else
        {
            _currentLevelIndex = LevelIndex;
        }
        asyncOperation.completed += OnLoadOperationComplete;
        _loadOperations.Add(asyncOperation);
    }


    public void OnLoadOperationComplete(AsyncOperation ao)
    {
        if (_loadOperations.Contains(ao))
        {


            _loadOperations.Remove(ao);
        }
        UIManager.Instance.SetBootCameraActive(false);
        UIManager.Instance.SetPlayMenuActive(false);
        UIManager.Instance.SetEndMenuActive(false);
        UIManager.Instance.SetMainMenuActive(true);
    }
    public void UnLoadLevel(int LevelIndex, bool won)
    {

        UIManager.Instance.SetBootCameraActive(true);
        UIManager.Instance.SetPlayMenuActive(false);
        UIManager.Instance.SetMainMenuActive(false);
        UIManager.Instance.SetEndMenuActive(true);
        UIManager.Instance.SetEndMenuStatement(won);
        AsyncOperation asyncOperation = SceneManager.UnloadSceneAsync(LevelIndex);
        if (asyncOperation == null)
        {

        }
        else
        {
            asyncOperation.completed += OnUnloadOperationComplete;
        }
    }

    public void OnUnloadOperationComplete(AsyncOperation ao)
    {
        if(_loadOperations.Contains(ao))
        {
            _loadOperations.Remove(ao);
        }
    }

    #endregion

    #region SceneManagement

    public void PlayClick()
    {
        LoadLevel(1);

    }

    public void DebugClick()
    {
        UIManager.Instance.SetPlayMenuActive(false);
        UIManager.Instance.SetDebugMenuActive(true);
        UIManager.Instance.SetEndMenuActive(false);
        UIManager.Instance.SetMainMenuActive(false);
    }

    public void BackClick()
    {
        UIManager.Instance.SetDebugMenuActive(false);
        UIManager.Instance.SetPlayMenuActive(true);
        UIManager.Instance.SetEndMenuActive(false);
        UIManager.Instance.SetMainMenuActive(false);
    }

    public void ExitClick()
    {
        Application.Quit();
    }

    public void ReturnClick()
    {
        UIManager.Instance.SetEndMenuActive(false);
        UIManager.Instance.SetPlayMenuActive(true);
        UIManager.Instance.SetMainMenuActive(false);
        UIManager.Instance.SetMainMenuActive(false);
    }

    #endregion
}
