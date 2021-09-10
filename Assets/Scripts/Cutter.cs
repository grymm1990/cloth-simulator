using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutter : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] Simulator sim;
    [SerializeField] float radius;

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.SphereCast(ray, radius, out RaycastHit hitInfo))
            {
                Line line = hitInfo.collider.GetComponent<Line>();
                if (line != null)
                {
                    sim.CutLine(line);
                }
            }
        }
    }
}
