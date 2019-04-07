using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    public LayerMask clickableLayer;
    public float maxSwipeDistance;

    Element lastElementClicked;
    Vector3 pointClickedOnLastElement;

    void Update()
    {
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Vector3 pointClicked = GetPositionOfTouch();
            RaycastHit2D raycastHit = Physics2D.Raycast(pointClicked, Vector2.zero, clickableLayer);

            if (raycastHit.collider != null)
            {
                Element elementClicked = raycastHit.collider.GetComponent<Element>();

                if (elementClicked != null)
                {
                    lastElementClicked = elementClicked;
                    pointClickedOnLastElement = pointClicked;
                }
            }
        }
        if ((Input.touchCount > 0)  && lastElementClicked != null)
        {
            Vector3 pointReleased = GetPositionOfTouch();
            Vector2 swipeVector = pointReleased - pointClickedOnLastElement;

            if(Input.GetTouch(0).phase == TouchPhase.Ended || swipeVector.magnitude > maxSwipeDistance)
            {
                lastElementClicked.ShootProjectile(swipeVector);
                lastElementClicked = null;
            }     
        }
    }

    public Vector3 GetPositionOfTouch()
    {
        return Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
    }
}
