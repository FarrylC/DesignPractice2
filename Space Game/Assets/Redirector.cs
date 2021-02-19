using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Redirector : MonoBehaviour
{
    // Either drag in via Inspector
    [SerializeField] private Carrier carrierScript;

    // or get at runtime if you are always sure about the hierachy
    private void Awake()
    {
        carrierScript = transform.parent.GetComponent<Carrier>();
    }

    // and now call this from the AnimationEvent
    public void DoIt(string message)
    {
        carrierScript.AnimEventAlertBlinkOffc(message);
    }
}
