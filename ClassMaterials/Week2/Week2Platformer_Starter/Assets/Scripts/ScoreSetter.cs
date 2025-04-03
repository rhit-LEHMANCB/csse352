using UnityEngine;
using TMPro;

public class ScoreSetter : MonoBehaviour
{
    TMP_Text text;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
        UpdateScore(GameManager.Instance.KillCount);
        EventBus.Subscribe(EventBus.EventType.KillsUpdate, 
            () => UpdateScore(GameManager.Instance.KillCount));
    }

    public void UpdateScore(int s)
    {
        text.text = "Score: " + s.ToString();
    }
}
