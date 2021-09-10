using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public Point pointA, pointB;
    public float length;
    LineRenderer lineRenderer;
    BoxCollider cutCollider;
    Vector3[] renderPoints;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        cutCollider = GetComponent<BoxCollider>();
        renderPoints = new Vector3[2];
    }

    private void Update()
    {
        renderPoints[0] = pointA.position;
        renderPoints[1] = pointB.position;

        lineRenderer.SetPositions(renderPoints);

        Vector2 lineCenter = (pointA.position + pointB.position) / 2;
        transform.position = lineCenter;
    }
}
