using DG.Tweening;
using UnityEngine;

public class Box : MonoBehaviour
{
    public bool isFlipping;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            OnClick();
        }
    }

    public void OnClick()
    {
        OnFlip();
    }

    public void OnFlip()
    {
        if (isFlipping) return;
        isFlipping = true;
        transform.DOLocalRotate(new Vector3(90, 0, 0), 0.5f, RotateMode.LocalAxisAdd).OnComplete(() =>
        {
            isFlipping = false;
        });
    }
}
