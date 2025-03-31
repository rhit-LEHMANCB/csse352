using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleResolveState : MonoBehaviour, IBattleState
{

    GameObject _myUI;
    Canvas _canvas;
    bool _isReady = false;

    //unique variables to this state
    UISetter _myUIsetter;
    Queue<string> _messageQueue = new Queue<string>();

    //references for my transitions
    BattleFSM _fsm;
    BattleMainState _mainBattleState;

    void Start()
    {
        Debug.Log("Starting up Resolve State");
        //set up my possible transitions
        _mainBattleState = GetComponent<BattleMainState>();
        if (_mainBattleState == null)
        {
            //there isnt one we need to make it
            _mainBattleState = gameObject.AddComponent<BattleMainState>();
        }

        _canvas = FindAnyObjectByType<Canvas>();
        _fsm = FindAnyObjectByType<BattleFSM>();

    }

    public void Handle(BattleFSM context)
    {
        if (!_isReady)
        {
            this.SetUpUI(context.stateUIPrefab);
            AddEnemyMessages();
        }

    }

    public void SetUpUI(GameObject prefab)
    {
        Debug.Log("Setting up Resolve UI");
        if (_myUI == null)
        {
            _myUI = Instantiate(prefab, _canvas.transform);
            _myUI.gameObject.name = "Battle Resolve UI";

            //edit the UI elements
            UISetter uiSetter = _myUI.GetComponent<UISetter>();
            uiSetter.SetTitle("BATTLE TURN RESOLUTION");
            uiSetter.AddPanel();
            //reference for the coroutine
            _myUIsetter = uiSetter;
        }
        else
        {
            _myUI.gameObject.SetActive(true);
        }

        StartCoroutine(BattleTimer());

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
        //make sure we know that we aren't ready to do things yet
        _isReady = false;
    }

    IEnumerator BattleTimer()
    {
        float time = 2f;//minimum time the window will stay open
        float delay = 1.5f;
        while (time > 0 || _messageQueue.Count > 0)
        {
            //Debug.LogFormat("Printing things... {0}", time);
            string s = "default string...";
            if (_messageQueue.Count > 0)
                s = _messageQueue.Dequeue();
            _myUIsetter.SetPanelText(s);
            time -= delay;
            yield return new WaitForSeconds(delay);
        }
        TearDownUI();
        _fsm.SetNextState(_mainBattleState);
    }

    public void AddMessageToQueue(string s)
    {
        _messageQueue.Enqueue(s);
    }

    void AddEnemyMessages()
    {
        //this should depend on the enemy and things, of course
        _messageQueue.Enqueue("Enemy used scratch!");
        _messageQueue.Enqueue("It missed!");
    }

}