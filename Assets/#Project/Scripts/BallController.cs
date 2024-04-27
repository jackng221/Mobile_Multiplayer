using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallController : MonoBehaviour
{
    Camera cam;
    Rigidbody rb;
    Ray debugRay;
    [SerializeField] DragIndicator dragIndicatorPrefab;
    DragIndicator dragIndicatorObj;

    Coroutine dragCoroutine;
    Vector3 dragPos = Vector3.zero;
    public float shootForceMultiplier = 100f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    private void Start()
    {
        dragIndicatorObj = Instantiate(dragIndicatorPrefab);
        dragIndicatorObj.gameObject.SetActive(false);
    }

    private void OnMouseDrag()
    {
        dragCoroutine = StartCoroutine( UpdateDragPosition() );
        dragIndicatorObj.gameObject.SetActive(true);

        Debug.Log("OnDrag");
    }
    private void OnMouseUp()
    {
        if (dragCoroutine != null)
        {
            ShootBall();
            StopCoroutine(dragCoroutine);
            dragCoroutine = null;
            dragIndicatorObj.gameObject.SetActive(false);
        }
        Debug.Log("OnExit");
    }

    IEnumerator UpdateDragPosition()
    {
        // initiate updating drag position on mouse drag

        bool locked = true;
        while (locked)
        {
            GetDragPos();

            dragIndicatorObj.SetTarget(transform.position, dragPos);
            //Debug.Log("Set target");

            yield return null;
        }
        // infinite loop broken by OnMouseUp
    }

    void ShootBall()
    {
        rb.velocity = Vector3.zero;
        rb.AddForce((transform.position -  dragPos) * shootForceMultiplier);
        Debug.Log("Shoot: " + (transform.position - dragPos) * shootForceMultiplier);
    }

    void GetDragPos()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        debugRay = ray;

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            dragPos = hit.point;
            dragPos = new Vector3(dragPos.x, transform.position.y, dragPos.z); // set drag position to be on the same y-level -> no up/down force
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (dragCoroutine != null)
        {
            //Gizmos.DrawSphere(dragPos, 1);
            Gizmos.DrawRay(debugRay);
        }
    }
}
