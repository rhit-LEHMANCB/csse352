using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
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
        Debug.Log("Starting up Resolve State");
       _mainBattleState = GetComponent<BattleMainState>();
        if (_mainBattleState == null)
        {
            _mainBattleState = gameObject.AddComponent<BattleMainState>();
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
        Debug.Log("Setting up Resolve UI");
        if (_myUI == null)
        {
            _myUI = Instantiate(prefab, _canvas.transform);
            _myUI.gameObject.name = "Resolve UI";
            _myUIsetter = _myUI.GetComponent<UISetter>();
            _myUIsetter.AddPanel();
            _myUIsetter.SetPanelText("I'm gonna fight you");
            _myUIsetter.SetTitle("Resolve");
        }
        else
        {
            _myUI.SetActive(true);
        }

        _isReady = true;

        StartCoroutine(BattleTimer());
    }

    public void TearDownUI()
    {
        Debug.Log("Tearing down my UI");
        _isReady = false;
        _myUI.SetActive(false);

    }

    public float textDelay = 3f;
    IEnumerator BattleTimer()
    {
        AddEnemyMessages();
        float time = 2f;
        Queue<string> messages = _fsm.GetQueue();

        while (time > 0 || messages.Count > 0)
        {
            string s = "default";
            if (messages.Count > 0)
            {
                s = messages.Dequeue();
            }
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
        _fsm.AddMessageToQueue("Enemy used scratch!");
        _fsm.AddMessageToQueue("It missed!");
    }

}