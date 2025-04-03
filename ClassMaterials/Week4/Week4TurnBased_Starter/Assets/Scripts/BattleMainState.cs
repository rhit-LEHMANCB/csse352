using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMainState : MonoBehaviour, IBattleState
{

    GameObject _myUI;
    Canvas _canvas;
    bool _isReady = false; //is ui set up?

    //references for my transitions
    BattleFSM _fsm;
    BattleItemState _itemState;
    BattleMoveState _moveState;

    void Start()
    {
        Debug.Log("Starting up Main State");
        _itemState = GetComponent<BattleItemState>();
        if (_itemState == null)
        {
            _itemState = gameObject.AddComponent<BattleItemState>();
        }
        _moveState = GetComponent<BattleMoveState>();
        if (_moveState == null)
        {
            _moveState = gameObject.AddComponent<BattleMoveState>();
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
        Debug.Log("Setting up Main UI");
        if (_myUI == null)
        {
            _myUI = Instantiate(prefab, _canvas.transform);
            _myUI.gameObject.name = "Main UI";

            UISetter uiSetter = _myUI.GetComponent<UISetter>();
            uiSetter.AddButton("Moves", EventBus.EventType.Button1);
            uiSetter.AddButton("Items", EventBus.EventType.Button2);
            uiSetter.SetTitle("Main Menu");
        }
        else
        {
            _myUI.SetActive(true);
        }

        EventBus.Subscribe(EventBus.EventType.Button1, DoMoves);
        EventBus.Subscribe(EventBus.EventType.Button2, DoItems);

        _fsm.numTurns++;

        _isReady = true;
    }

    public void TearDownUI()
    {
        Debug.Log("Tearing down my UI");
        // Destroy(_myUI);
        EventBus.Unsubscribe(EventBus.EventType.Button1, DoMoves);
        EventBus.Unsubscribe(EventBus.EventType.Button2, DoItems);
        _myUI.SetActive(false);
        _isReady = false;
    }

    void DoItems()
    {
        //this just demos how we can use info about the state to do different things when the button is pressed
        Debug.Log("ITEMS! Number of turns: " + _fsm.numTurns);
        TearDownUI();
        _fsm.SetNextState(_itemState);

    }

    void DoMoves()
    {
        Debug.Log("MOVES! Number of turns: " + _fsm.numTurns);
        TearDownUI();
        _fsm.SetNextState(_moveState);

    }
}
