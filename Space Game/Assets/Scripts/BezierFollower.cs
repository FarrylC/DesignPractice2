using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierFollower : MonoBehaviour
{
    //This script is responsible for making the asteroid rotates around their own orbit

    //Asteroid folowing bezier variables
    [SerializeField]
    //Put the bezier empty object as "routes";
    private Transform[] routes;
    private int routeToGo;
    private float tParam;
    private Vector2 asteroidPosition;
    public float speedModifier = 0.5f;
    private bool coroutineAllowed;


    // Start is called before the first frame update
    void Start()
    {
        //Asteroids folowing bezier curve starter
        routeToGo = 0;
        tParam = 0f;
        coroutineAllowed = true;
    }

    // Update is called once per frame
    void Update()
    {
        //move the asteroid acording to the bezier curve
        if (coroutineAllowed)
            StartCoroutine(GoByTheRoute(routeToGo));
    }

    private IEnumerator GoByTheRoute(int routeNumber)
    {
        //ensures that a new coroutine only starts after this one ends
        coroutineAllowed = false;

        //Vector2 variables that hold the bezier curve points position
        Vector2 p0 = routes[routeNumber].GetChild(0).position;
        Vector2 p1 = routes[routeNumber].GetChild(1).position;
        Vector2 p2 = routes[routeNumber].GetChild(2).position;
        Vector2 p3 = routes[routeNumber].GetChild(3).position;

        //calculates the movement of the asteroid inside the bezier curve
        while (tParam < 1)
        {
            //define the asteroid speed
            tParam += Time.deltaTime * (speedModifier / 1000);

            asteroidPosition = Mathf.Pow(1 - tParam, 3) * p0 +
                3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                Mathf.Pow(tParam, 3) * p3;

            transform.position = asteroidPosition;
            yield return new WaitForEndOfFrame();
        }

        //restart the coroutine parameters for the next one
        tParam = 0f;
        //go to the next bezier curve
        routeToGo += 1;

        //restart the coroutine system
        if (routeToGo > routes.Length - 1)
            routeToGo = 0;

        coroutineAllowed = true;
    }
}
