using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Boombox : MonoBehaviour
{
    public AudioSource source;
    public AudioClip startAudio, loopAudio;

    public async void TurnOn()
    {
        source.loop = false;
        source.clip = startAudio;
        source.Play();

        await Task.Delay((int)(startAudio.length * 1000));

        source.clip = loopAudio;
        source.loop = true;
        source.Play();
    }
}
