using UnityEngine;

public class BoxPuzzle : MonoBehaviour
{
    public bool isCompleted = false;
    public Box[] boxxes;
    public int[] angles = { 0, 90, 180, 270, 360 };

    public void Init()
    {

    }

    public void Play()
    {
        isCompleted = false;
    }

    public void CheckCompleted()
    {
        for (int i = 0; i < boxxes.Length; i++)
        {
            if(boxxes[i].transform.localRotation != boxxes[i + 1].transform.localRotation)
            {
                //false
                isCompleted = false;
                return;
            }
        }
        isCompleted = true;
    }
}
