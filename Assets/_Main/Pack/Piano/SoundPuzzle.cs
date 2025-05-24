using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPuzzle : MonoBehaviour
{
    private int[] sttSounds = { 0, 0, 0, 1, 1, 0, 0 };
    public List<int> sounds;
    public bool isCompleted;
    public AudioClip[] audioClips;
    public AudioSource audioSource;

    private void OnEnable()
    {
        Init();
    }

    private void Update()
    {
        if (isCompleted) return;
        if (Input.GetMouseButtonDown(0))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent<Node>(out Node node))
                {
                    node.OnClick();
                    sounds.Add(node.id);
                }

                if (hit.collider.CompareTag("Tuong"))
                {
                    CheckComplete();
                }
            }
        }
    }

    public void Init()
    {
        isCompleted = false;
        sounds.Clear();
        StartCoroutine(TMPSounds());
    }

    public IEnumerator TMPSounds()
    {
        for (int i = 0; i < audioClips.Length; i++)
        {
            audioSource.clip = audioClips[i];
            audioSource.PlayScheduled(0);
            yield return new WaitForSeconds(1f);
        }
    }

    public void CheckComplete()
    {
        if (sounds.Count != sttSounds.Length)
        {
            //false
            sounds.Clear();
            Tuong.Instance.NextStatus();
            if(Tuong.Instance.statusId >= 2)
            {
                Lose();
            }
            return;
        }
        for (int i = 0; i < sounds.Count; i++)
        {
            if (sounds[i] != sttSounds[i])
            {
                //false
                sounds.Clear();
                Tuong.Instance.NextStatus();
                if (Tuong.Instance.statusId >= 2)
                {
                    Lose();
                }
                return;
            }
        }
        Win();
    }

    public void Win()
    {
        isCompleted = true;
        Tuong.Instance.ResetStatus();
        GameManager.Instance.NextGame();
    }

    private void Lose()
    {
        GameManager.Instance.Lose();
    }
}
