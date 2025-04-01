using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleItemState : MonoBehaviour, IBattleState
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
        if (verboseMessages) Debug.Log("Setting up ITEM UI");
        if (_myUI == null)
        {
            _myUI = Instantiate(prefab, _canvas.transform);
            _myUI.gameObject.name = "Item UI";

            //edit the UI elements
            UISetter uiSetter = _myUI.GetComponent<UISetter>();
            uiSetter.SetTitle("ITEM MENU");
            uiSetter.AddButton("Item 1", EventBus.EventType.Button1);
            uiSetter.AddButton("Item 2", EventBus.EventType.Button2);
            uiSetter.AddButton("Item 3", EventBus.EventType.Button3);
            uiSetter.AddButton("Item 4", EventBus.EventType.Button4);
        }
        else
        {
            _myUI.gameObject.SetActive(true);
        }
        //register for listeners on the buttons
        //I'm just using the same method here for all of them, but in reality we could use different ones
        EventBus.Subscribe(EventBus.EventType.Button1, DoItem);
        EventBus.Subscribe(EventBus.EventType.Button2, DoItem);
        EventBus.Subscribe(EventBus.EventType.Button3, DoItem);
        EventBus.Subscribe(EventBus.EventType.Button4, DoItem);

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
        EventBus.Unsubscribe(EventBus.EventType.Button1, DoItem);
        EventBus.Unsubscribe(EventBus.EventType.Button2, DoItem);
        EventBus.Unsubscribe(EventBus.EventType.Button3, DoItem);
        EventBus.Unsubscribe(EventBus.EventType.Button4, DoItem);
        //make sure we know that we aren't ready to do things yet
        _isReady = false;
    }

    void DoItem()
    {
        if (verboseMessages) Debug.Log("Using an Item");
        _fsm.EnqueueMessage("Using an item...");
        //set next state here
        _fsm.SetNextState(_resolveState);
        //tear down my UI
        TearDownUI();
    }

}