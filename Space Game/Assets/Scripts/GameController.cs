using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    private static GameController instance;

    public GameObject player;
    int detectCount = 0;
    public GameObject redBorder;

    [HideInInspector] public int currentLevel = 1;
    public Text levelDisplay;
    public List<string> levelNames = new List<string>();

    private void Awake()
    {
        // Only have one instance of the game controller at all times
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateLevelDisplay();
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
        // If there is not red border, do nothing
        if (!redBorder)
            return;

        // If the player is not detected by any enemies, hide the red border
        if (detectCount == 0)
            redBorder.SetActive(false);

        // If the player is detected by one or more enemies, show the red border
        else
            redBorder.SetActive(true);
    }

    public void UpdateLevelDisplay()
    {
        // If there is no display label, do nothing
        if (levelDisplay == null)
            return;

        // Show the name of the current level
        levelDisplay.text = levelNames[currentLevel - 1];
    }

    public void GoToScene(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }
}
