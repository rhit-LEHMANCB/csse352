using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleResolveState : MonoBehaviour, IBattleState
{

    GameObject _myUI;
    Canvas _canvas;
    bool _isReady = false;
    public bool verboseMessages = true;

    //unique variables to this state
    UISetter _myUIsetter;

    //references for my transitions
    BattleFSM _fsm;
    BattleMainState _mainBattleState;

    void Start()
    {
        if (verboseMessages) Debug.Log("Starting up Resolve State");
        //set up my possible transitions
        _mainBattleState = GetComponent<BattleMainState>();
        if (_mainBattleState == null)
        {
            //there isnt one we need to make it
            _mainBattleState = gameObject.AddComponent<BattleMainState>();
        }

        _canvas = FindAnyObjectByType<Canvas>();
        _fsm = FindAnyObjectByType<BattleFSM>();
        verboseMessages = _fsm.verboseMessages;

    }

    public void Handle(BattleFSM context)
    {
        if (!_isReady)
        {
            //AI chooses what it will do
            //AI make choices here while the player is planning
            //should this be a new state? Probably.
            //under this regime the Enemy will always go after the player
            GameManager.Instance.enemy.AIChooseMove();

            //deal with the UI
            this.SetUpUI(context.stateUIPrefab);
            //AddEnemyMessages();
        }

    }

    public void SetUpUI(GameObject prefab)
    {
        if (verboseMessages) Debug.Log("Setting up Resolve UI");
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
        if (verboseMessages) Debug.Log("Tearing down my UI");
        //Destroy(_my_UI);
        _myUI.SetActive(false);
        //make sure we know that we aren't ready to do things yet
        _isReady = false;
    }

    public float textDelay = 3f;
    IEnumerator BattleTimer()
    {
        float time = 2f;//minimum time the window will stay open
        Queue<string> messages = _fsm.GetQueue();
        while (time > 0 || messages.Count > 0)
        {
            string s = "default string...";
            if (messages.Count > 0)
                s = messages.Dequeue();
            _myUIsetter.SetPanelText(s);
            time -= textDelay;
            yield return new WaitForSeconds(textDelay);
        }
        TearDownUI();
        _fsm.SetNextState(_mainBattleState);
    }

    void AddEnemyMessages()
    {
        //this should depend on the enemy and things, of course
        _fsm.EnqueueMessage("Enemy used scratch!");
        _fsm.EnqueueMessage("It missed!");
    }

}