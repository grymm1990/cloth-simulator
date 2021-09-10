using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class MeshEditor : EditorWindow
{

    [MenuItem("Window/Mesh Editor")]
    public static void ShowWindow()
    {
        GetWindow<MeshEditor>("Mesh Editor");
    }

    public Simulator sim;
    public GameObject linePrefab;
    int height;
    int width;

    [Obsolete]
    void OnGUI()
    {
        sim = (Simulator)EditorGUILayout.ObjectField("Simulator", sim, typeof(Simulator));
        linePrefab = (GameObject)EditorGUILayout.ObjectField("Line Prefab", linePrefab, typeof(GameObject));

        if (GUILayout.Button("Connect Selected"))
        {
            Connect(Selection.gameObjects[0].GetComponent<Point>(), Selection.gameObjects[1].GetComponent<Point>());
        }

        width = EditorGUILayout.IntField("Mesh Width", width);
        height = EditorGUILayout.IntField("Mesh Height", height);

        if (GUILayout.Button("Build Mesh"))
        {
            BuildMesh();
        }
    }

    void Connect(Point a, Point b)
    {
        if (a == null || b == null)
        {
            Debug.Log("Error with selection.");
            return;
        }
        GameObject newLine = GameObject.Instantiate(linePrefab, sim.lineHolder);
        Line line = newLine.GetComponent<Line>();
        line.pointA = a;
        line.pointB = b;
        Vector2 lineCenter = (line.pointA.transform.position + line.pointB.transform.position) / 2;
        newLine.transform.position = lineCenter;
    }

    void BuildMesh()
    {
        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                if (i < width - 1)
                {
                    Connect(sim.pointHolder.GetChild(i + j * width).GetComponent<Point>(), 
                        sim.pointHolder.GetChild(i + j * width + 1).GetComponent<Point>());
                }
                if (j < height - 1)
                {
                    Connect(sim.pointHolder.GetChild(i + j * width).GetComponent<Point>(),
                        sim.pointHolder.GetChild(i + (j + 1) * width).GetComponent<Point>());
                }
            }
        }
    }
}
