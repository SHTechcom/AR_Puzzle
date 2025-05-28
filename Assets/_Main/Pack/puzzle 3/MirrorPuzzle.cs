using UnityEngine;

public class MirrorPuzzle : MonoBehaviour
{
    public Mirror currentMirror;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(mouseRay, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent<Mirror>(out var mirror))
                {
                    currentMirror.Deselect();
                    currentMirror = mirror;
                    currentMirror.Select();
                }
                else
                {
                    currentMirror.Deselect();
                    currentMirror = null;
                }
            }
        }
    }
}
