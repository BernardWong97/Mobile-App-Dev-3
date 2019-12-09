using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class RopeController : MonoBehaviour
{
    public GameObject ropeHingeAnchor;
    public DistanceJoint2D ropeJoint;
    private bool ropeAttached;
    private Vector2 playerPosition;
    private Rigidbody2D ropeHingeAnchorRb;
    private SpriteRenderer ropeHingeAnchorSprite;
    public LineRenderer ropeRenderer;
    public LayerMask ropeLayerMask;
    private float ropeMaxCastDistance = 3f;
    private List<Vector2> ropePositions = new List<Vector2>();
    private bool distanceSet;
    private Vector2 direction;
    private KeyCode keyCode;


    private void Awake()
    {
        ropeJoint.enabled = false;
        playerPosition = transform.position;
        ropeHingeAnchorRb = ropeHingeAnchor.GetComponent<Rigidbody2D>();
        ropeHingeAnchorSprite = ropeHingeAnchor.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        playerPosition = gameObject.transform.position;
        ShootRope();
        UpdateRopePositions();
    }
    
    private void ShootRope()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            direction = Vector2.up;
            keyCode = KeyCode.O;
            RayCastRope();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            direction = Vector2.one;
            keyCode = KeyCode.P;
            RayCastRope();
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            direction = new Vector2(-1, 1);
            keyCode = KeyCode.I;
            RayCastRope();
        }

        if (Input.GetKeyUp(keyCode))
        {
            ResetRope();
        }
    }

    private void RayCastRope()
    {
        ropeRenderer.enabled = true;

        RaycastHit2D hit = Physics2D.Raycast(playerPosition, direction, ropeMaxCastDistance, ropeLayerMask);
            
        if (hit.collider != null)
        {
            ropeAttached = true;
            if (!ropePositions.Contains(hit.point))
            {
                // Jump slightly to distance the player a little from the ground after grappling to something.
                transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 2f), ForceMode2D.Impulse);
                ropePositions.Add(hit.point);
                ropeJoint.distance = Vector2.Distance(playerPosition, hit.point);
                ropeJoint.enabled = true;
                ropeHingeAnchorSprite.enabled = true;
            }
        }
        else
        {
            ropeRenderer.enabled = false;
            ropeAttached = false;
            ropeJoint.enabled = false;
        }
    }
    
    private void ResetRope()
    {
        ropeJoint.enabled = false;
        ropeAttached = false;
        ropeRenderer.positionCount = 2;
        ropeRenderer.SetPosition(0, gameObject.transform.position);
        ropeRenderer.SetPosition(1, gameObject.transform.position);
        ropePositions.Clear();
        ropeHingeAnchorSprite.enabled = false;
    }
    
    private void UpdateRopePositions()
    {
        if (!ropeAttached)
            return;

        ropeRenderer.positionCount = ropePositions.Count + 1;
        
        for (var i = ropeRenderer.positionCount - 1; i >= 0; i--)
        {
            if (i != ropeRenderer.positionCount - 1) // if not the Last point of line renderer
            {
                ropeRenderer.SetPosition(i, ropePositions[i]);
                
                if (i == ropePositions.Count - 1 || ropePositions.Count == 1)
                {
                    var ropePosition = ropePositions[ropePositions.Count - 1];
                    if (ropePositions.Count == 1)
                    {
                        ropeHingeAnchorRb.transform.position = ropePosition;
                        if (!distanceSet)
                        {
                            ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                            distanceSet = true;
                        }
                    }
                    else
                    {
                        ropeHingeAnchorRb.transform.position = ropePosition;
                        if (!distanceSet)
                        {
                            ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                            distanceSet = true;
                        }
                    }
                }
                else if (i - 1 == ropePositions.IndexOf(ropePositions.Last()))
                {
                    var ropePosition = ropePositions.Last();
                    ropeHingeAnchorRb.transform.position = ropePosition;
                    if (!distanceSet)
                    {
                        ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                        distanceSet = true;
                    }
                }
            }
            else
            {
                ropeRenderer.SetPosition(i, transform.position);
            }
        }
    }
    
}    