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
        //set up my possible transitions
        _itemState = GetComponent<BattleItemState>();
        if (_itemState == null)
        {
            //there isnt one we need to make it
            _itemState = gameObject.AddComponent<BattleItemState>();
        }

        _moveState = GetComponent<BattleMoveState>();
        if (_moveState == null)
        {
            //there isnt one we need to make it
           _moveState = gameObject.AddComponent<BattleMoveState>();
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
        Debug.Log("Setting up Main UI");
        _fsm.numTurns += 1;//increment the turn counter when we start the MainState Each time
        if (_myUI == null)
        {
            _myUI = Instantiate(prefab, _canvas.transform);
            _myUI.gameObject.name = "Main UI";

            //edit the UI elements
            UISetter uiSetter = _myUI.GetComponent<UISetter>();
            uiSetter.SetTitle("MAIN MENU");
            uiSetter.AddButton("Items", EventBus.EventType.Button1);
            uiSetter.AddButton("Moves", EventBus.EventType.Button2);
        }
        else {
            _myUI.gameObject.SetActive(true);
        }

        //register for listeners on the buttons
        EventBus.Subscribe(EventBus.EventType.Button1, DoItems);
        EventBus.Subscribe(EventBus.EventType.Button2, DoMoves);

        //next state will stay here until we hit a button
        _fsm.SetNextState(this);
        //ready to go now
        _isReady = true;
    }

    public void TearDownUI()
    {
        Debug.Log("Tearing down my UI");
        //Destroy(_my_UI);
        //alternatively we just activate and deactivate after we make it the first time
        _myUI.SetActive(false);
        //make sure we dont respond to someone else's buttons
        EventBus.Unsubscribe(EventBus.EventType.Button1, DoItems);
        EventBus.Unsubscribe(EventBus.EventType.Button2, DoMoves);
        //make sure we know that we aren't ready to do things yet
        _isReady = false;
    }

    void DoItems()
    {
        //this just demos how we can use info about the state to do different things when the button is pressed
        Debug.Log("ITEMS! Number of turns: " + _fsm.numTurns);
        //set next state here
        _fsm.SetNextState(_itemState);
        //tear down my UI
        TearDownUI();
    }

    void DoMoves()
    {
        Debug.Log("MOVES! Number of turns: " + _fsm.numTurns);
        //set next state here
        _fsm.SetNextState(_moveState);
        //tear down my UI
        TearDownUI();
    }
}
