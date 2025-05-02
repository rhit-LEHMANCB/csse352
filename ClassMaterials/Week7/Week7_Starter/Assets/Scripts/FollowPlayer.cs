using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player; // Reference to the player GameObject
    public NavMeshAgent agent; // Reference to the NavMeshAgent component

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.transform.position); // Set the destination of the agent to the player's position
    }
}
