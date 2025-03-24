using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject playerPrefab;

    GameObject _player = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = Instantiate(playerPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        if (_player == null)
            _player = Instantiate(playerPrefab);
    }

    public GameObject GetPlayer()
    {
        return _player;
    }
}
