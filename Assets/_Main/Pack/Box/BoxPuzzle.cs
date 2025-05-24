using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoxPuzzle : MonoBehaviour
{
    public bool isCompleted = false;
    public Box[] boxxes;
    public int[] angles = { 0, 90, 180, 270, 360 };

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
                if (hit.collider.TryGetComponent<Box>(out Box box))
                {
                    box.OnFlip(CheckCompleted);
                }
            }
        }
    }

    public void Init()
    {
        List<int> resAngles = angles.ToList();
        for (int i = 0; i < boxxes.Length; i++)
        {
            int angle = angles[UnityEngine.Random.Range(0, angles.Length)];
            boxxes[i].Init(angle);
            resAngles.Remove(angle);
        }

        Play();
    }

    public void Play()
    {
        isCompleted = false;
    }

    public void CheckCompleted()
    {
        for (int i = 0; i < boxxes.Length; i++)
        {
            if (boxxes[i].targetAngle != 0)
            {
                //false
                isCompleted = false;
                return;
            }
        }
        isCompleted = true;
        GameManager.Instance.NextGame();
    }
}
