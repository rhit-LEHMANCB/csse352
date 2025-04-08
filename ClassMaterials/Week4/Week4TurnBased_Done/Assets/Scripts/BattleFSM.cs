using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFSM : MonoBehaviour
{
    public GameObject stateUIPrefab;
    public int numTurns = 0;
    public bool verboseMessages = true;

    IBattleState currentState;

    //holds data about what happened this turn
    Queue<string> _messageQueue = new Queue<string>();

    private void Start()
    {
        //we should have one of each state as our componenents
        //but the states will make them if we dont
        //we'll start off in the MainState just to keep things easy
        currentState = GetComponent<BattleMainState>();
        if(currentState == null)
        {
            //make one if we dont have it already
            currentState = gameObject.AddComponent<BattleMainState>();
        }
    }

    private void Update()
    {
        Transition();
    }

    public void Transition()
    {
        currentState.Handle(this);
    }

    public void SetNextState(IBattleState state)
    {
        currentState = state;
    }

    //helper methods for other states to record messages
    public void EnqueueMessage(string message)
    {
        _messageQueue.Enqueue(message);
    }

    public Queue<string> GetQueue()
    {
        return _messageQueue;
    }

    public void ClearQueue()
    {
        _messageQueue.Clear();
    }

    public void SetAnimators(Animator friend, Animator enemy)
    {
        //just search for any UIsetter
        UISetter uiSetter = FindAnyObjectByType<UISetter>();
        friend.transform.position = uiSetter.GetFriendImageLocation();
        enemy.transform.position = uiSetter.GetEnemyImageLocation();
    }

}
