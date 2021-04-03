using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MenuAudioManager.instance.gameObject.GetComponent<AudioSource>().Pause();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
