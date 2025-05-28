using System;
using UnityEngine;

public class Box : MonoBehaviour
{
    public bool isFlipping;
    public int targetAngle;
    private Action callback;
    public int id;

    public void Init(int angle)
    {
        if (angle >= 360) angle = 0;
        targetAngle = angle;
        transform.localRotation = Quaternion.Euler(angle, 0, 0);
    }

    public void OnFlip(Action callback)
    {
        if (isFlipping) return;
        isFlipping = true;
        this.callback = callback;
        targetAngle += 90;
    }

    private void FixedUpdate()
    {
        if (isFlipping)
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(targetAngle, 0, 0), 10f);
            if(transform.localRotation == Quaternion.Euler(targetAngle, 0, 0))
            {
                transform.localRotation = Quaternion.Euler(targetAngle, 0, 0);
                if(targetAngle >= 360)
                {
                    targetAngle = 0;
                }
                callback?.Invoke();
                isFlipping = false;
            }
        }
    }
}
