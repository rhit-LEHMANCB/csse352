using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleResolveState : MonoBehaviour, IBattleState
{

    GameObject _myUI;
    Canvas _canvas;
    bool _isReady = false;

    //unique variables to this state
    UISetter _myUIsetter;
    Queue<string> _messageQueue = new Queue<string>();

    //references for my transitions
    BattleFSM _fsm;
    BattleMainState _mainBattleState;

    void Start()
    {
        Debug.Log("Starting up Resolve State");
       

    }

    public void Handle(BattleFSM context)
    {
        
    }

    public void SetUpUI(GameObject prefab)
    {
        Debug.Log("Setting up Resolve UI");
       
    }

    public void TearDownUI()
    {
        Debug.Log("Tearing down my UI");
        
    }

    IEnumerator BattleTimer()
    {
       yield return null; 
    }

    public void AddMessageToQueue(string s)
    {
        _messageQueue.Enqueue(s);
    }

    void AddEnemyMessages()
    {
        //this should depend on the enemy and things, of course
        _messageQueue.Enqueue("Enemy used scratch!");
        _messageQueue.Enqueue("It missed!");
    }

}