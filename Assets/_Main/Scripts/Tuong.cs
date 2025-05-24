using DG.Tweening;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(Tuong))]
public class TuongEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Next Status"))
        {
            Tuong tuong = (Tuong)target;
            tuong.NextStatus();
        }
    }
}
#endif

public class Tuong : Singleton<Tuong>
{
    public int statusId;
    private float scale = 7.561336f;
    public MeshRenderer meshRenderer;

    public void Init()
    {
        ResetStatus();
        gameObject.SetActive(true);
    }

    public void ResetStatus()
    {
        statusId = 0;
        OnAnimator(statusId);
    }

    public void NextStatus()
    {
        statusId++;
        OnAnimator(statusId);
    }

    private void OnAnimator(int layerIndex)
    {
        if (statusId == 0)
        {
            // Do something for status 0
            transform.localScale = Vector3.one * scale;
            meshRenderer.material.color = Color.white;
        }
        else if (statusId == 1)
        {
            // Do something for status 1
            transform.DOShakeScale(.2f, .2f).OnComplete(() =>
            {
                transform.localScale = Vector3.one * scale;
                meshRenderer.material.color = Color.yellow;
            });
        }
        else if (statusId == 2)
        {
            // Do something for status 2
            transform.DOShakeScale(.2f, .2f).OnComplete(() =>
            {
                transform.localScale = Vector3.one * scale;
                meshRenderer.material.color = Color.red;
            });
        }
    }
}
