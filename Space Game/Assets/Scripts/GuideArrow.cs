using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideArrow : MonoBehaviour
{
    public Transform goalPos;
    public Transform playerPos;
    public AnimationCurve curveX, curveY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dirToGoal = (goalPos.position - playerPos.position).normalized;
        transform.up = dirToGoal;
        transform.localPosition = new Vector2(325 * curveX.Evaluate(dirToGoal.x), 175 * curveY.Evaluate(dirToGoal.y));
    }
}
