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

    private Square CreatePooledItem()
    {
        //todo
        return null;
    }

    private void OnReturnedToPool(Square s)
    {
        //todo
    }

    private void OnTakeFromPool(Square s)
    {
        //todo
    }

    private void OnDestroyPoolObject(Square s)
    {
        GameObject.Destroy(s.gameObject);
    }

}
