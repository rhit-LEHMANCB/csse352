using UnityEngine;

public class BushSpawner : MonoBehaviour
{
    public GameObject bushPrefab;
    public int bushNum = 20;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < bushNum; i++)
        {
            if (bushPrefab == null)
            {
                GameObject GO = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                GO.transform.SetParent(transform);
                GO.AddComponent<Bush>();
            }
            else
            {
                Instantiate(bushPrefab, transform);
            }
        }
    }
}
