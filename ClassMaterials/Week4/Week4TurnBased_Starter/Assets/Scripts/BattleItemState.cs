using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleItemState : MonoBehaviour, IBattleState
{

    GameObject _myUI;
    Canvas _canvas;
    bool _isReady = false;

    //references for my transitions
    BattleFSM _fsm;
    BattleResolveState _resolve_state;

    void Start()
    {
        Debug.Log("Starting up Item State");

    }

    public void Handle(BattleFSM context)
    {

    }

    public void SetUpUI(GameObject prefab)
    {
        Debug.Log("Setting up ITEM UI");
        
    }

    public void TearDownUI()
    {
        Debug.Log("Tearing down my UI");
        
    }

    void DoItem()
    {
        Debug.Log("Using an Item");
       
    }

}