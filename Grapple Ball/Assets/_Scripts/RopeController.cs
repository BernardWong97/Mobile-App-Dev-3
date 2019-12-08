using System;
using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class RopeController : MonoBehaviour
{
    public GameObject ball;

    private SpringJoint2D _rope;
    public int maxRopeFrameCount;
    private int ropeFrameCounnt;

    public LineRenderer lineRenderer;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootRope();
        }
    }

    private void LateUpdate()
    {
        if (_rope != null)
        {
            lineRenderer.enabled = true;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, ball.transform.position);
            lineRenderer.SetPosition(1, _rope.connectedAnchor);
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    private void ShootRope()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 position = ball.transform.position;
        Vector3 direction = mousePosition - position;

        RaycastHit2D hit = Physics2D.Raycast(position, direction);

        if (hit.collider != null)
        {
            Debug.Log("HIT");
            SpringJoint2D newRope = ball.AddComponent<SpringJoint2D>();
            newRope.enableCollision = false;
            newRope.frequency = .2f;
            newRope.connectedAnchor = hit.point;
            newRope.enabled = true;
            
            GameObject.DestroyImmediate(_rope);
            _rope = newRope;
            ropeFrameCounnt = 0;
        }
    }
}