using UnityEngine;

public class ButtonLoader : MonoBehaviour
{
    public void OnClickLoad()
    {
        GameManager.Instance.LoadNextLevel();
    }
}
