using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulator : MonoBehaviour
{
    [SerializeField] float iterationCount;
    [SerializeField] float groundLevel = -10f;
    [SerializeField] float topLevel = 10f;
    public Transform lineHolder;
    public Transform pointHolder;
    [SerializeField] BoolVariable gamePaused;
    [SerializeField] FloatVariable gravity;

    List<Point> points;
    List<Line> lines;

    private void Start()
    {
        points = new List<Point>();
        lines = new List<Line>();

        foreach(Transform child in pointHolder)
        {
            points.Add(child.GetComponent<Point>());
        }
        foreach (Transform child in lineHolder)
        {
            lines.Add(child.GetComponent<Line>());
        }
        foreach (Line line in lines)
        {
            line.length = Vector2.Distance(line.pointA.position, line.pointB.position);
        }
    }

    void Update()
    {
        if(!gamePaused.value) Simulate();
    }

    void Simulate()
    {
        foreach (Point point in points)
        {
            if (point.grounded)
            {
                if (point.position.y > groundLevel) point.grounded = false;
                else continue;
            }
            if (!point.locked)
            {
                Vector2 posBeforeUpdate = point.position;
                point.position += point.position - point.lastPosition;
                point.position += Vector2.down * gravity.value * Time.deltaTime * Time.deltaTime;
                point.lastPosition = posBeforeUpdate;
                if (point.position.y < groundLevel)
                {
                    point.position = new Vector2(point.position.x, groundLevel);
                    point.grounded = true;
                }
                
                if (point.position.y > topLevel)
                {
                    point.position = new Vector2(point.position.x, topLevel);
                }
            }
        }

        for (int i = 0; i < iterationCount; i++)
        {
            foreach (Line line in lines)
            {
                Vector2 lineCenter = (line.pointA.position + line.pointB.position) / 2;
                Vector2 lineDir = (line.pointA.position - line.pointB.position).normalized;

                if (!line.pointA.locked) line.pointA.position = lineCenter + lineDir * line.length / 2;
                if (!line.pointB.locked) line.pointB.position = lineCenter - lineDir * line.length / 2;
                line.transform.position = lineCenter;
            }
        }
    }

    public void CutLine(Line line)
    {
        if (lines.Contains(line))
        {
            lines.Remove(line);
            Destroy(line.gameObject);
        }
    }
}
