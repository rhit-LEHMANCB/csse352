using UnityEngine;

public class Player : MonoBehaviour
{

    public Monster friend;
    public Item[] items;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //set up some initial stuff for me
        friend = GetComponentInChildren<Monster>();
        if(friend == null)
        {
            //I have no monster, sad
            //lets make one
            friend = GameManager.Instance.PickRandomMonster();
            //move it over to me
            friend.gameObject.transform.parent = gameObject.transform;
        }
        //tell the GM to set up my friend's animations
        GameManager.Instance.SetAnimations(friend);

        //set up my items
        items = new Item[]
        {
            new Item("potion"),
            new Item("ball"),
            new Item("rock"),
            new Item("cheer")
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
