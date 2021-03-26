using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierPath : MonoBehaviour
{
    //Create a new empty object and apply this script
    //Now create four empty child objects inside the main object
    //The main object will be used as a route to the asteroids
    //The four child will be used as a guide for the route drawn the bezier curves


    //Asteroid bezier gizmo variables
    [SerializeField]
    private Transform[] controlPoint;
    private Vector2 gizmosPosition;

    //Drawn the orbital asteroids gizmos on the screen
    private void OnDrawGizmos()
    {
        
        for (float t = 0; t <= 1; t += 0.05f)
        {

            gizmosPosition = Mathf.Pow(1 - t, 3) * controlPoint[0].position +
                3 * Mathf.Pow(1 - t, 2) * t * controlPoint[1].position +
                3 * (1 - t) * Mathf.Pow(t, 2) * controlPoint[2].position +
                Mathf.Pow(t, 3) * controlPoint[3].position;

            Gizmos.DrawWireSphere(gizmosPosition, 0.25f);
        }

        Gizmos.DrawLine(new Vector2(controlPoint[0].position.x, controlPoint[0].position.y),
            new Vector2(controlPoint[1].position.x, controlPoint[1].position.y));

        Gizmos.DrawLine(new Vector2(controlPoint[2].position.x, controlPoint[2].position.y),
            new Vector2(controlPoint[3].position.x, controlPoint[3].position.y));
    }


}
