using UnityEngine;

public class HPMonitor : MonoBehaviour
{
    public GameObject HPbar;//this should be the object with a mask
    public int maxHP;
    public int currentHP;
    [SerializeField] float startingScale;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startingScale = HPbar.transform.localScale.x;
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHP();
    }

    void UpdateHP()
    {
        float hpFraction = (float) currentHP / maxHP;
        HPbar.transform.localScale =
            new Vector3(hpFraction * startingScale,
                        HPbar.transform.localScale.y,
                        HPbar.transform.localScale.z);
    }
}
