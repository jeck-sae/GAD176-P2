using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tail : MonoBehaviour
{
    public int length;
    public LineRenderer line;
    public Vector3[] segmentsPos;
    private Vector3[] segmentsVelocity;

    public Transform targetDir;
    public float segmentsDistance;
    public float smoothSpeed;

    public float wiggle;
    public float wiggleMagnitude;
    public Transform wiggleDirection;

    public Transform[] bodyParts;

    private void Start()
    {
        line.positionCount = length;
        segmentsPos = new Vector3[length];
        segmentsVelocity = new Vector3[length];
        ResetPos();
    }
    private void Update()
    {
        wiggleDirection.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * wiggle) * wiggleMagnitude);
        
        segmentsPos[0] = targetDir.position;
        for (int i = 1; i < length; i++)
        {
            Vector3 targetPos = segmentsPos[i-1] + (segmentsPos[i] - segmentsPos[i-1]).normalized * segmentsDistance;
            segmentsPos[i] = Vector3.SmoothDamp(segmentsPos[i], targetPos, ref segmentsVelocity[i], smoothSpeed);
            bodyParts[i - 1].transform.position = segmentsPos[i];
        }
        line.SetPositions(segmentsPos);
    }
    public void ResetPos()
    {
        segmentsPos[0] = targetDir.position;
        for(int i = 1; i < length; i++)
        {
            segmentsPos[i] = segmentsPos[i - 1] + targetDir.up * segmentsDistance;
        }
        line.SetPositions(segmentsPos);
    }
}
