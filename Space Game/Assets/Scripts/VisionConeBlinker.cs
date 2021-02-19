using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionConeBlinker : MonoBehaviour
{
    // Start is called before the first frame update
    SpriteRenderer sr;
    public Color visionIdleColor;
    public Color visionAlertColor;
    public bool test;
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();

        
    }

    // Update is called once per frame
    void Update()
    {
        SetColor();
    }
    void SetColor()
    {
    

        if(test)
        {
            sr.color = visionAlertColor;
        }
        else
        {
            sr.color = visionIdleColor;
        }
        
    }
}
