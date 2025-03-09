using UnityEngine;

public class ColorChanger : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RandomizeColor();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void RandomizeColor()
    {

        //pick a random color
        Color color = new Color(Random.Range(0f, 1f),
                                Random.Range(0f, 1f),
                                Random.Range(0f, 1f));
        SetColor(color);
    }

    void SetColor(Color c)
    {
        //Get the SpriteRenderer component
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        //set the color on the Sprite Renderer
        sr.color = c;

    }
}
