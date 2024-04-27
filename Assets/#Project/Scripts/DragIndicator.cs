using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragIndicator : MonoBehaviour
{
    Vector3 fromPos = Vector3.zero;
    Vector3 targetPos = Vector3.zero;
    LineRenderer lr;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        lr.SetPosition(0, fromPos);
        lr.SetPosition(1, targetPos);
    }

    public void SetTarget(Vector3 fromPosition, Vector3 targetPosition)
    {
        fromPos = fromPosition;
        targetPos = targetPosition;
    }
}
