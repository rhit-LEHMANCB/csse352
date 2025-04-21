using UnityEngine;

public class Player : MonoBehaviour
{

    public Monster friend;
    public Item[] items;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        friend = GetComponentInChildren<Monster>();
        if (friend == null)
        {
            friend = Monster.MakeNewMonster("Sriram", 1, MoveManager.GetRandomMoves(), 0, -10);
            friend.gameObject.transform.parent = this.transform;
        }

        items = new Item[]
        {
            new Item("Potion"),
            new Item("Super Potion"),
            new Item("Hyper Potion"),
            new Item("Max Potion"),
            new Item("Revive"),
            new Item("Max Revive"),
            new Item("Elixir"),
            new Item("Max Elixir")
        };
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
