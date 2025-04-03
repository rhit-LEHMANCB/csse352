using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEditorInternal;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject livesText;
    [SerializeField] GameObject killCountText;
    GameObject _player = null;
    private int _lives;
    private int _killCount;
    public int KillCount {
        get => _killCount;
        set
        {
            _killCount = value;
            //ScoreSetter scoreSetter = FindAnyObjectByType<ScoreSetter>();
            //if (scoreSetter != null)
            //{
            //    scoreSetter.UpdateScore(KillCount);
            //}
            EventBus.Publish(EventBus.EventType.KillsUpdate);
        }
    }

    public int Lives
    {
        get => _lives;
        set
        {
            _lives = value;
            //LivesSetter livesSetter = FindAnyObjectByType<LivesSetter>();
            //if (livesSetter != null)
            //{
            //    livesSetter.UpdateLives(Lives);
            //}
            EventBus.Publish(EventBus.EventType.PlayerDie);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = Instantiate(playerPrefab);
        Lives = 3;
        KillCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_player == null && _lives > 0)
        {
            _player = Instantiate(playerPrefab);
        }
    }

    public PlayerInfo GetPlayer()
    {
        if (_player == null)
        {
            return null;
        }
        return _player.GetComponent<PlayerInfo>();
    }

    public void OnGoombaKilled()
    {
        KillCount++;
        Debug.Log("Goomba killed! Total: " + _killCount);
    }

    public void OnPlayerKilled()
    {
        Lives--;
        if (Lives <= 0)
        {
            Debug.Log("Game Over");
        }
    }
    [SerializeField] string[] levels = { "level1", "level2" };
    private int _sceneIndex = 0;
    public void LoadNextLevel()
    {
        _sceneIndex++;
        if (_sceneIndex >= levels.Length)
        {
            _sceneIndex = 0;
        }
        SceneManager.LoadScene(levels[_sceneIndex]);
        StartCoroutine(SpawnPlayerCoroutine());
    }

    IEnumerator SpawnPlayerCoroutine()
    {
        while (_player != null)
        {
            yield return null;
        }
        _player = Instantiate(playerPrefab);
    }

    void SetUpListeners()
    {
        EventBus.Subscribe(EventBus.EventType.PlayerDie, OnPlayerKilled);
        EventBus.Subscribe(EventBus.EventType.LoadLevelButton, LoadNextLevel);
    }

    void CleanUpListeners()
    {
        EventBus.Unsubscribe(EventBus.EventType.PlayerDie, OnPlayerKilled);
        EventBus.Unsubscribe(EventBus.EventType.LoadLevelButton, LoadNextLevel);
    }
}
