using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionConeBlinker : MonoBehaviour
{
    // Start is called before the first frame update
    SpriteRenderer sr;
    private Carrier carrierScript;
    private GameObject Carrier;
    public Color visionIdleColor;
    public Color visionAlertColor;
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();

        carrierScript = GetComponentInParent<Carrier>();

        
    }

    // Update is called once per frame
    void Update()
    {
        SetColor();
    }
    void SetColor()
    {
        if(this.carrierScript.isPlayerInSight)
        {
            this.sr.color = visionAlertColor;
        }
        else
        {
            this.sr.color = visionIdleColor;
        }
    }
}
