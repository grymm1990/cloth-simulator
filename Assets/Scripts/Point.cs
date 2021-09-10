using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public Vector2 position, lastPosition;
    public bool locked;
    public bool grounded;

    private void Awake()
    {
        position = transform.position;
        lastPosition = position;
    }

    private void Update()
    {
        transform.position = position;
    }
}
