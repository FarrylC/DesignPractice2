using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject player;
    int detectCount = 0;
    public GameObject redBorder;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleRedBorder();
    }

    public static Vector2 GetPlayerInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    public void IncrementDetectCount()
    {
        detectCount++;
    }

    public void DecrementDetectCount()
    {
        detectCount--;

        if (detectCount < 0)
            detectCount = 0;
    }

    public void HandleRedBorder()
    {
        print(detectCount);
        // If the player is not detected by any enemies, hide the red border
        if (detectCount == 0)
            redBorder.SetActive(false);

        // If the player is detected by one or more enemies, show the red border
        else
            redBorder.SetActive(true);
    }
}
