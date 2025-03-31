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
        Debug.Log("Starting up Main State");
        //set up my possible transitions
        _resolve_state = GetComponent<BattleResolveState>();
        if (_resolve_state == null)
        {
            //there isnt one we need to make it
            _resolve_state = gameObject.AddComponent<BattleResolveState>();
        }

        _canvas = FindAnyObjectByType<Canvas>();
        _fsm = FindAnyObjectByType<BattleFSM>();

    }

    public void Handle(BattleFSM context)
    {
        if (!_isReady)
            this.SetUpUI(context.stateUIPrefab);

    }

    public void SetUpUI(GameObject prefab)
    {
        Debug.Log("Setting up MOVE UI");
        if (_myUI == null)
        {
            _myUI = Instantiate(prefab, _canvas.transform);
            _myUI.gameObject.name = "Move UI";

            //edit the UI elements
            UISetter uiSetter = _myUI.GetComponent<UISetter>();
            uiSetter.SetTitle("MOVE MENU");
            uiSetter.AddButton("Move 1", EventBus.EventType.Button1);
            uiSetter.AddButton("Move 2", EventBus.EventType.Button2);
            uiSetter.AddButton("Move 3", EventBus.EventType.Button3);
            uiSetter.AddButton("Move 4", EventBus.EventType.Button4);
        }
        else
        {
            _myUI.gameObject.SetActive(true);
        }
        //register for listeners on the buttons
        EventBus.Subscribe(EventBus.EventType.Button1, DoMove);
        EventBus.Subscribe(EventBus.EventType.Button2, DoMove);
        EventBus.Subscribe(EventBus.EventType.Button3, DoMove);
        EventBus.Subscribe(EventBus.EventType.Button4, DoMove);

        //next state will stay here until we hit a button
        _fsm.SetNextState(this);
        //ready to go now
        _isReady = true;
    }

    public void TearDownUI()
    {
        Debug.Log("Tearing down my UI");
        //Destroy(_my_UI);
        _myUI.SetActive(false);
        //make sure we dont respond to someone else's buttons
        EventBus.Unsubscribe(EventBus.EventType.Button1, DoMove);
        EventBus.Unsubscribe(EventBus.EventType.Button2, DoMove);
        EventBus.Unsubscribe(EventBus.EventType.Button3, DoMove);
        EventBus.Unsubscribe(EventBus.EventType.Button4, DoMove);
        //make sure we know that we aren't ready to do things yet
        _isReady = false;
    }

    void DoMove()
    {
        Debug.Log("Using a Move");
        _resolve_state.AddMessageToQueue("Using a move...");
        //set next state here
        _fsm.SetNextState(_resolve_state);
        //tear down my UI
        TearDownUI();
    }

}