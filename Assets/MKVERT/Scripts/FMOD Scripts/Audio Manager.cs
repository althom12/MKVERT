using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance {get; private set;}

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found more than one Audio Manager in scene");
        }

        instance = this;
    }


    public void PlayOneShot(EventReference sound, Vector3 worldPOS)
    {
        RuntimeManager.PlayOneShot(sound, worldPOS);
    }

    


}
