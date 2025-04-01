using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMoveState : MonoBehaviour, IBattleState
{

    GameObject _myUI;
    Canvas _canvas;
    bool _isReady = false;
    public bool verboseMessages = true;

    //references for my transitions
    BattleFSM _fsm;
    BattleResolveState _resolveState;

    void Start()
    {
        if (verboseMessages) Debug.Log("Starting up Main State");
        //set up my possible transitions
        _resolveState = GetComponent<BattleResolveState>();
        if (_resolveState == null)
        {
            //there isnt one we need to make it
            _resolveState = gameObject.AddComponent<BattleResolveState>();
        }

        _canvas = FindAnyObjectByType<Canvas>();
        _fsm = FindAnyObjectByType<BattleFSM>();
        verboseMessages = _fsm.verboseMessages;

    }

    public void Handle(BattleFSM context)
    {
        if (!_isReady)
            this.SetUpUI(context.stateUIPrefab);

    }

    public void SetUpUI(GameObject prefab)
    {
        if (verboseMessages) Debug.Log("Setting up MOVE UI");
        if (_myUI == null)
        {
            _myUI = Instantiate(prefab, _canvas.transform);
            _myUI.gameObject.name = "Move UI";

            //edit the UI elements
            UISetter uiSetter = _myUI.GetComponent<UISetter>();
            uiSetter.SetTitle("MOVE MENU");
            /* 
             * Basic version we started with
            uiSetter.AddButton("Move 1", EventBus.EventType.Button1);
            uiSetter.AddButton("Move 2", EventBus.EventType.Button2);
            uiSetter.AddButton("Move 3", EventBus.EventType.Button3);
            uiSetter.AddButton("Move 4", EventBus.EventType.Button4);
            */
            //after we added the Player part of the game
            uiSetter.AddButton(GameManager.Instance.player.GetFriendMoveByIndex(0).name, EventBus.EventType.Button1);
            uiSetter.AddButton(GameManager.Instance.player.GetFriendMoveByIndex(1).name, EventBus.EventType.Button2);
            uiSetter.AddButton(GameManager.Instance.player.GetFriendMoveByIndex(2).name, EventBus.EventType.Button3);
            uiSetter.AddButton(GameManager.Instance.player.GetFriendMoveByIndex(3).name, EventBus.EventType.Button4);
        }
        else
        {
            _myUI.gameObject.SetActive(true);
        }
        //register for listeners on the buttons
        EventBus.Subscribe(EventBus.EventType.Button1, DoMove0);
        EventBus.Subscribe(EventBus.EventType.Button2, DoMove1);
        EventBus.Subscribe(EventBus.EventType.Button3, DoMove2);
        EventBus.Subscribe(EventBus.EventType.Button4, DoMove3);

        //next state will stay here until we hit a button
        _fsm.SetNextState(this);
        //ready to go now
        _isReady = true;
    }

    public void TearDownUI()
    {
        if (verboseMessages) Debug.Log("Tearing down my UI");
        //Destroy(_my_UI);
        _myUI.SetActive(false);
        //make sure we dont respond to someone else's buttons
        EventBus.Unsubscribe(EventBus.EventType.Button1, DoMove0);
        EventBus.Unsubscribe(EventBus.EventType.Button2, DoMove1);
        EventBus.Unsubscribe(EventBus.EventType.Button3, DoMove2);
        EventBus.Unsubscribe(EventBus.EventType.Button4, DoMove3);
        //make sure we know that we aren't ready to do things yet
        _isReady = false;
    }

    void DoMove()
    {
        if (verboseMessages) Debug.Log("Using a Move");
        _fsm.EnqueueMessage("Using a move...");
        //set next state here
        _fsm.SetNextState(_resolveState);
        //tear down my UI
        TearDownUI();
    }

    void ResolveMoveByIndex(int i)
    {
        //get the move
        Move move = GameManager.Instance.player.GetFriendMoveByIndex(i);
        //make it's effect happen
        move.Effect(GameManager.Instance.player.friend, GameManager.Instance.enemy);

        _fsm.EnqueueMessage("done");
        //set next state here
        _fsm.SetNextState(_resolveState);
        //tear down my UI
        TearDownUI();

    }

    //these are set up this way, instead of using anonymous delegates
    //so we can unsubscribe completely when the state transitions
    void DoMove0()
    {
        if (verboseMessages) Debug.Log("Using Move0");
        //get the move
        ResolveMoveByIndex(0);
    }

    void DoMove1()
    {
        if (verboseMessages) Debug.Log("Using Move1");
        //get the move
        ResolveMoveByIndex(1);
    }

    void DoMove2()
    {
        if (verboseMessages) Debug.Log("Using Move2");
        //get the move
        ResolveMoveByIndex(2);
    }

    void DoMove3()
    {
        if (verboseMessages) Debug.Log("Using Move3");
        //get the move
        ResolveMoveByIndex(3);
    }

}