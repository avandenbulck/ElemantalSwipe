using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    public LayerMask clickableLayer;

    Element elementClicked;
    Vector3 pointClickedOnElement;

    void Update()
    {
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Debug.Log("Touched screen!");
            Vector3 pointClicked = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            RaycastHit2D raycastHit = Physics2D.Raycast(pointClicked, Vector2.zero, clickableLayer);

            if (raycastHit.collider != null)
            {
                Debug.Log("Touched an object!");

                Element element = raycastHit.collider.GetComponent<Element>();

                if (element != null)
                {
                    Debug.Log("Clicked element!");
                    elementClicked = element;
                    pointClickedOnElement = pointClicked;
                }
            }
        }
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            Vector3 pointReleased = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

            //Instantiate(prefab, endPosition, Quaternion.identity);
            Vector2 direction = pointReleased - pointClickedOnElement;

            elementClicked.ShootProjectile(direction);
        }
    }
}
