using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDragger : MonoBehaviour
{
    public float forceAmount = 500;

    Rigidbody2D selectedRigidbody;
    Camera targetCamera;
    Vector2 originalScreenTargetPosition;
    Vector3 originalRigidbodyPos;
    float selectionDistance;

    // Start is called before the first frame update
    void Start()
    {
        targetCamera = FindObjectOfType<Camera>();
    }

    void Update()
    {
        if (!targetCamera)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            //Check if we are hovering over Rigidbody, if so, select it
            selectedRigidbody = GetRigidbodyFromMouseClick();
        }
        if (Input.GetMouseButtonUp(0) && selectedRigidbody)
        {
            //Release selected Rigidbody if there any
            selectedRigidbody = null;
        }
    }

    void FixedUpdate()
    {
        if (selectedRigidbody)
        {
            Vector3 currMousePos = targetCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 mousePositionOffset = new Vector2(currMousePos.x, currMousePos.y)- originalScreenTargetPosition;
            selectedRigidbody.velocity = ( (originalRigidbodyPos + mousePositionOffset) - selectedRigidbody.transform.position) * forceAmount * Time.deltaTime;
        }
    }

    Rigidbody2D GetRigidbodyFromMouseClick()
    {
        Vector3 mousePos = targetCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        if (hit)
        {
            if (hit.collider.CompareTag("Food"))
            {
                //selectionDistance = Vector2.Distance(ray.origin, hitInfo.point);
                originalScreenTargetPosition = targetCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, selectionDistance));
                originalRigidbodyPos = hit.collider.transform.position;
                print("Hit Food");
                return hit.collider.gameObject.GetComponent<Rigidbody2D>();
            }
        }

        return null;
    }
}
