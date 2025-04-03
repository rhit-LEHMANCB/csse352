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
        Debug.Log("Setting up ITEM UI");
        if (_myUI == null)
        {
            _myUI = Instantiate(prefab, _canvas.transform);
            _myUI.gameObject.name = "Item UI";

            UISetter uiSetter = _myUI.GetComponent<UISetter>();
            uiSetter.AddButton("Item1", EventBus.EventType.Button1);
            uiSetter.AddButton("Item2", EventBus.EventType.Button2);
            uiSetter.AddButton("Item3", EventBus.EventType.Button3);
            uiSetter.AddButton("Item4", EventBus.EventType.Button4);

            uiSetter.SetTitle("Items");
        }
        else
        {
            _myUI.SetActive(true);
        }

        EventBus.Subscribe(EventBus.EventType.Button1, DoItem);
        EventBus.Subscribe(EventBus.EventType.Button2, DoItem);
        EventBus.Subscribe(EventBus.EventType.Button3, DoItem);
        EventBus.Subscribe(EventBus.EventType.Button4, DoItem);

        _isReady = true;
    }

    public void TearDownUI()
    {
        Debug.Log("Tearing down my UI");
        
        EventBus.Unsubscribe(EventBus.EventType.Button1, DoItem);
        EventBus.Unsubscribe(EventBus.EventType.Button2, DoItem);
        EventBus.Unsubscribe(EventBus.EventType.Button3, DoItem);
        EventBus.Unsubscribe(EventBus.EventType.Button4, DoItem);

        _myUI.SetActive(false);
        _isReady = false;
    }

    void DoItem()
    {
        Debug.Log("Using an Item");
        _fsm.AddMessageToQueue("Using an Item");
        TearDownUI();
        _fsm.SetNextState(_resolve_state);
    }

}