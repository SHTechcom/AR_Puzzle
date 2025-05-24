using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class JumpScareView : MonoBehaviour
{
    public Image bg;
    public Image icon;
    public AudioSource audioSource;

    private void OnEnable()
    {
        audioSource.PlayScheduled(0);
        bg.color = Color.red;
        icon.transform.localScale = Vector3.one;

        bg.DOColor(Color.white, .1f).SetLoops(-1);
        icon.transform.DOScale(Vector3.one * 1.5f, .1f).SetLoops(-1, LoopType.Yoyo);
        Invoke(nameof(Hide), 1f);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
