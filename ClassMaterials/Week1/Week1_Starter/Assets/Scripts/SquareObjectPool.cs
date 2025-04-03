using UnityEngine;
using UnityEngine.Pool;

public class SquareObjectPool : MonoBehaviour
{

    public int maxPoolSize = 10;
    public int stackDefaultCapacity = 10;
    public GameObject prefab;

    private IObjectPool<Square> _pool;

    public IObjectPool<Square> Pool
    {
        get
        {
            if (_pool == null)
            {
                _pool = new ObjectPool<Square>(
                    CreatePooledItem,
                    OnTakeFromPool,
                    OnReturnedToPool,
                    OnDestroyPoolObject,
                    true,
                    stackDefaultCapacity,
                    maxPoolSize
                    );
            }
            return _pool;
        }
    }

    int id = 0;
    private Square CreatePooledItem()
    {
        GameObject go = GameObject.Instantiate(prefab, gameObject.transform);
        go.SetActive(false);
        go.name = "Square_" + id.ToString();
        id++;

        Square s = go.GetComponent<Square>();
        s.Pool = Pool;

        return s;
    }

    private void OnReturnedToPool(Square s)
    {
        s.gameObject.SetActive(false);
    }

    private void OnTakeFromPool(Square s)
    {
        s.gameObject.SetActive(true);
        s.ResetSquare();
    }

    private void OnDestroyPoolObject(Square s)
    {
        GameObject.Destroy(s.gameObject);
    }

}
