using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public int completedSceneNum = 4;
    public AudioClip LevelFinished;

    // Start is called before the first frame update
    void Start()
    {

    }

    IEnumerator playSoundThenLoad()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = LevelFinished;
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().GoToScene(completedSceneNum);
        MenuAudioManager.instance.gameObject.GetComponent<AudioSource>().Play();

    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            StartCoroutine(playSoundThenLoad());
        }
    }
}
