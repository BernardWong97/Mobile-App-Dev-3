using System;
using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class MenuRope : MonoBehaviour
{
    public GameObject ball;
    public GameObject plane;

    public LineRenderer lineRenderer;

    private void Update()
    {
        CreateRope();
    }

    private void CreateRope()
    {
        Vector3 planePosition = plane.transform.position;
        Vector3 ballPosition = ball.transform.position;
        
        lineRenderer.enabled = true;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, ballPosition);
        lineRenderer.SetPosition(1, planePosition);
    }
}