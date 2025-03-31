using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMoveState : MonoBehaviour, IBattleState
{

    GameObject _myUI;
    Canvas _canvas;
    bool _isReady = false;

    //references for my transitions
    BattleFSM _fsm;
    BattleResolveState _resolve_state;

    void Start()
    {
        Debug.Log("Starting up Move State");
        

    }

    public void Handle(BattleFSM context)
    {
       

    }

    public void SetUpUI(GameObject prefab)
    {
        Debug.Log("Setting up MOVE UI");
       
    }

    public void TearDownUI()
    {
        Debug.Log("Tearing down my UI");
        
    }

    void DoMove()
    {
        Debug.Log("Using a Move");
        
    }

}