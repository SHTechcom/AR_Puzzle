using System;
using UnityEngine;

public class Node : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;
    public int id;

    private void Reset()
    {
        audioSource = GetComponent<AudioSource>();
        //if(!Int32.TryParse(gameObject.name, out id))
        //{
        //    id = -1;
        //}
    }

    private void OnEnable()
    {
        audioSource.clip = audioClip;
    }

    public void OnClick()
    {
        audioSource.PlayScheduled(0);
    }
}
