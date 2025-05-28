using UnityEngine;

public class Mirror : MonoBehaviour
{
    public bool isSelected = false;
    public float rotationSpeed = 3f;

    private bool isDragging = false;
    private float lastMouseX;

    private void OnEnable()
    {
        float randomY = Random.Range(0f, 360f);
        transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, randomY, transform.localEulerAngles.z);
    }

    private void Update()
    {
        if (!isSelected) return;

        if (Input.GetMouseButtonDown(0))
        {
            lastMouseX = Input.mousePosition.x;
            isDragging = true;
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            float currentMouseX = Input.mousePosition.x;
            float deltaX = currentMouseX - lastMouseX;

            transform.Rotate(0f, deltaX * rotationSpeed * Time.deltaTime * -1, 0f, Space.World);

            lastMouseX = currentMouseX;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    public void Select()
    {
        isSelected = true;
    }

    public void Deselect()
    {
        isSelected = false;
        isDragging = false;
    }
}
