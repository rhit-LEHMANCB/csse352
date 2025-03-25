using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public void Die()
    {
        Debug.Log("Oof, oww, my bones.");
        //inform listeners that we died
        EventBus.Publish(EventBus.EventType.PlayerDie);
        Destroy(gameObject);
    }
}
