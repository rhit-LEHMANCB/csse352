using UnityEngine;

public class Player : MonoBehaviour
{

    public Monster friend;
    public Item[] items;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    public Move GetFriendMoveByIndex(int i)
    {
        return friend.moves[i];
    }

    public Item GetItemByIndex(int i)
    {
        return items[i];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
