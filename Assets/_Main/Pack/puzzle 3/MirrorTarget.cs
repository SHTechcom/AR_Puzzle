using UnityEngine;

public class MirrorTarget : MonoBehaviour
{
    public bool isAvtive = false;
    public float threshhold = 1f;
    private Material mat;

    private void Awake()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            mat = new Material(renderer.material);
            renderer.material = mat;
        }
        else
        {
            Debug.LogError("Renderer not found on MirrorTarget.");
        }
    }

    private void OnEnable()
    {
        threshhold = 1f;
        mat.SetFloat("_Cutoff", threshhold);
        mat.color = new Color(1f, 0, 0, 0);
        isAvtive = false;
    }

    private void Update()
    {
        if (isAvtive)
        {
            threshhold -= Time.deltaTime;
            mat.SetFloat("_Cutoff", threshhold);
            if(threshhold <= 0)
            {
                Completed();
            }
        }
    }

    public void Active()
    {
        if (isAvtive) return;
        isAvtive = true;
    }

    public void Completed()
    {
        GameManager.Instance.NextGame();
    }
}
