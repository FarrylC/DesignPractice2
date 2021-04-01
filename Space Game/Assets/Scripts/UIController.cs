using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject blackOutSquare;
    public GameObject crack;

    public PlayerHealth playerHealth;


    // Update is called once per frame
    void Update()
    {
        //start coroutine if player has no health
        if (playerHealth.health == 0) 
        {
            crack.SetActive(true);
            StartCoroutine(FadeBlackOutSquare());
        }

        //stop coroutine if player has health
        if (playerHealth.health != 0)
        {
            StopCoroutine(FadeBlackOutSquare());
        }
    }
    

    //Fade out the screen and restart the scene
    public IEnumerator FadeBlackOutSquare(bool fadeToBLack = true, int fadeSpeed = 1)
    {
        Color objectColor = blackOutSquare.GetComponent<Image>().color;
        float fadeAmount;

        /*if (fadeToBLack)
        {
            while (blackOutSquare.GetComponent<Image>().color.a < 1)
            {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent<Image>().color = objectColor;
                yield return null;
            }

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            fadeToBLack = false;
        }
        else
        {
            while (blackOutSquare.GetComponent<Image>().color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }*/
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        fadeToBLack = false;

        yield return null;
    }
}
