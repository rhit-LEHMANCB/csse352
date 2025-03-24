using UnityEngine;

public class ButtonLoader : MonoBehaviour
{
    public void OnClickLoad()
    {
        GameManagerSingleton.Instance.LoadNextLevel();
    }
}
