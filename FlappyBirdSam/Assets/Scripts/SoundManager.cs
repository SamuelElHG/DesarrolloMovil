using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField, Header("AudioSources")] private AudioSource targetSource;


    public void MuteVolume()
    {
        if (targetSource.volume > 0) {

            targetSource.volume = 0;
        }
        else
        {
            targetSource.volume = 1;
        }
    }
}
