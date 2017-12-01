using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{

    public AudioSource source;
    public AudioClip clip;

    private void Start()
    {
        if(GetComponent<AudioSource>() == null)
        {
            Debug.Log("Error, attatch Audio source to gameobject");
        }
        source.clip = clip;
    }
}
