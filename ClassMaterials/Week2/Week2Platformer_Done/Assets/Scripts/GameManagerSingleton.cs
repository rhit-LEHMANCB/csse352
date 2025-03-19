using UnityEngine;

public class GameManagerSingleton : Singleton<GameManagerSingleton>
{

    [SerializeField] GameObject playerPrefab;

    GameObject _player = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = Instantiate(playerPrefab);
        _killCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (_player == null)
            if (_lives > 0)//intermediate thing to before we implemented PlayerDeath()
                _player = Instantiate(playerPrefab);
        */
    }

    public GameObject GetPlayer()
    {
        return _player;
    }

    [SerializeField] int _killCount;
    [SerializeField] int _lives = 3;

    public void IncrementKillCount()
    {
        _killCount += 1;
        Debug.LogFormat("Killed {0} total so far", _killCount);
    }

    public void PlayerDeath()
    {
        _player.GetComponent<PlayerInfo>().Die();
        _lives -= 1;
        if (_lives > 0)
        {
            _player = Instantiate(playerPrefab);
        }
        else
        {
            Debug.LogFormat("Game Over. Score = {0}", _killCount);
        }
    }

}
