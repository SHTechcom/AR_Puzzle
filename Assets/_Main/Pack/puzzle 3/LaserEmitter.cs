using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class LaserEmitter : MonoBehaviour
{
    public float maxDistance = 100f;
    public int maxReflections = 10;
    public LayerMask reflectionMask; // Chỉ layer có gương
    public LineRenderer lineRenderer;
    public Transform[] points;

    private void OnEnable()
    {
        lineRenderer.positionCount = 0;
    }

    void Update()
    {
        Vector3 direction = (points[1].position - points[0].position).normalized;
        CastLaser(points[0].position, direction);
    }

    void CastLaser(Vector3 startPosition, Vector3 direction)
    {
        List<Vector3> points = new List<Vector3>();
        points.Add(startPosition);

        Vector3 currentPosition = startPosition;
        Vector3 currentDirection = direction;

        for (int i = 0; i < maxReflections; i++)
        {
            Ray ray = new Ray(currentPosition, currentDirection);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxDistance, reflectionMask))
            {
                if(hit.collider.CompareTag("mirror"))
                {
                    points.Add(hit.point);

                    // Tính hướng phản xạ theo góc tới và mặt gương
                    Vector3 incoming = currentDirection;
                    Vector3 normal = hit.normal;
                    Vector3 reflected = Vector3.Reflect(incoming, normal);

                    // Debug góc (tuỳ chọn)
                    float angleIn = Vector3.Angle(-incoming, normal);
                    float angleOut = Vector3.Angle(reflected, normal);
                    Debug.Log($"Góc tới: {angleIn:F2}°, Góc phản xạ: {angleOut:F2}°");

                    currentPosition = hit.point;
                    currentDirection = reflected;
                }
                if (hit.collider.CompareTag("back mirror"))
                {
                    // Nếu không phải gương, kết thúc tại điểm va chạm
                    points.Add(hit.point);
                    break;
                }
                if (hit.collider.CompareTag("Target"))
                {
                    // Nếu không phải gương, kết thúc tại điểm va chạm
                    points.Add(hit.point);
                    if(hit.collider.TryGetComponent<MirrorTarget>(out MirrorTarget mirrorTarget))
                    {
                        mirrorTarget.Active();
                    }
                    break;
                }
                if (hit.collider.CompareTag("UnTag"))
                {
                    // Nếu không phải gương, kết thúc tại điểm va chạm
                    points.Add(hit.point);
                    if (hit.collider.TryGetComponent<MirrorTarget>(out MirrorTarget mirrorTarget))
                    {
                        mirrorTarget.Active();
                    }
                    break;
                }
            }
            else
            {
                points.Add(currentPosition + currentDirection * maxDistance);
                break;
            }
        }

        // Cập nhật LineRenderer
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }
}
