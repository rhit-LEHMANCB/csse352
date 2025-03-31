using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMainState : MonoBehaviour, IBattleState
{

    GameObject _myUI;
    Canvas _canvas;
    bool _isReady = false;

    //references for my transitions
    BattleFSM _fsm;
    BattleItemState _itemState;
    BattleMoveState _moveState;

    void Start()
    {
        Debug.Log("Starting up Main State");
        

    }

    public void Handle(BattleFSM context)
    {
        
    }

    public void SetUpUI(GameObject prefab)
    {
        Debug.Log("Setting up Main UI");
       
    }

    public void TearDownUI()
    {
        Debug.Log("Tearing down my UI");
        
    }

    void DoItems()
    {
        //this just demos how we can use info about the state to do different things when the button is pressed
        Debug.Log("ITEMS! Number of turns: " + _fsm.numTurns);
        
    }

    void DoMoves()
    {
        Debug.Log("MOVES! Number of turns: " + _fsm.numTurns);
       
    }
}
