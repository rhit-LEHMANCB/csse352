using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleResolveState : MonoBehaviour, IBattleState
{

    GameObject _myUI;
    Canvas _canvas;
    bool _isReady = false;
    public bool verboseMessages = true;

    //unique variables to this state
    UISetter _myUIsetter;

    //references for my transitions
    BattleFSM _fsm;
    BattleMainState _mainBattleState;

    void Start()
    {
        if (verboseMessages) Debug.Log("Starting up Resolve State");
        

    }

    public void Handle(BattleFSM context)
    {
        

    }

    public void SetUpUI(GameObject prefab)
    {
        if (verboseMessages) Debug.Log("Setting up Resolve UI");
        
    }

    public void TearDownUI()
    {
        if (verboseMessages) Debug.Log("Tearing down my UI");
       
    }

    public float textDelay = 3f;
    IEnumerator BattleTimer()
    {
        yield return null;
    }

    void AddEnemyMessages()
    {
        //this should depend on the enemy and things, of course
        _fsm.EnqueueMessage("Enemy used scratch!");
        _fsm.EnqueueMessage("It missed!");
    }

}