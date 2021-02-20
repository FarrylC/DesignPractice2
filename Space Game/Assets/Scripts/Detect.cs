using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Detect : MonoBehaviour
{
    public Image detect;

    void Update()
    {
        Vector3 detectPos = Camera.main.WorldToScreenPoint(this.transform.position);

        detect.transform.position = detectPos;

        transform.up = transform.position;
    }
}
