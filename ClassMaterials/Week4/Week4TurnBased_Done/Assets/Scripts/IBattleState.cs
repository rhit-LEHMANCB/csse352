using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattleState 
{
    //this will return what the next state should be
    void Handle(BattleFSM context);

    //this is just for our UI stuff
    void SetUpUI(GameObject prefab);
    void TearDownUI();
}
