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
        _resolve_state = GetComponent<BattleResolveState>();
        if (_resolve_state == null)
        {
            _resolve_state = gameObject.AddComponent<BattleResolveState>();
        }

        _canvas = FindAnyObjectByType<Canvas>();
        _fsm = FindAnyObjectByType<BattleFSM>();
    }

    public void Handle(BattleFSM context)
    {
       if (!_isReady)
        {
            SetUpUI(context.stateUIPrefab);
        }

    }

    public void SetUpUI(GameObject prefab)
    {
        Debug.Log("Setting up MOVE UI");

        if (_myUI == null)
        {
            _myUI = Instantiate(prefab, _canvas.transform);
            _myUI.gameObject.name = "Move UI";
            UISetter uiSetter = _myUI.GetComponent<UISetter>();
            uiSetter.AddButton("Move1", EventBus.EventType.Button1);
            uiSetter.AddButton("Move2", EventBus.EventType.Button2);
            uiSetter.AddButton("Move3", EventBus.EventType.Button3);
            uiSetter.AddButton("Move4", EventBus.EventType.Button4);
            uiSetter.SetTitle("Moves");
        }
        else
        {
            _myUI.SetActive(true);
        }

        EventBus.Subscribe(EventBus.EventType.Button1, DoMove);
        EventBus.Subscribe(EventBus.EventType.Button2, DoMove);
        EventBus.Subscribe(EventBus.EventType.Button3, DoMove);
        EventBus.Subscribe(EventBus.EventType.Button4, DoMove);

        _isReady = true;
    }

    public void TearDownUI()
    {
        Debug.Log("Tearing down my UI");
        
        EventBus.Unsubscribe(EventBus.EventType.Button1, DoMove);
        EventBus.Unsubscribe(EventBus.EventType.Button2, DoMove);
        EventBus.Unsubscribe(EventBus.EventType.Button3, DoMove);
        EventBus.Unsubscribe(EventBus.EventType.Button4, DoMove);

        _myUI.SetActive(false);
        _isReady = false;
    }

    void DoMove()
    {
        Debug.Log("Using a Move");
        TearDownUI();
        _fsm.AddMessageToQueue("Using a Move");
        _fsm.SetNextState(_resolve_state);
    }

}