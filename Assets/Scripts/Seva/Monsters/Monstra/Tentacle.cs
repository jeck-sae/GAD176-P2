using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour
{
    public int length;
    public LineRenderer line;
    public Vector3[] segmentsPos;
    private Vector3[] segmentsVelocity;

    public Transform targetDir;
    public float segmentsDistance;
    public float smoothSpeed;
    public float trailSpeed;

    public float wiggle;
    public float wiggleMagnitude;
    public Transform wiggleDirection;

    private void Start()
    {
        line.positionCount = length;
        segmentsPos = new Vector3[length];
        segmentsVelocity = new Vector3[length];
    }
    private void Update()
    {
        wiggleDirection.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * wiggle) * wiggleMagnitude);
        segmentsPos[0] = targetDir.position;

        for (int i = 1; i < segmentsPos.Length; i++)
        {
            segmentsPos[i] = Vector3.SmoothDamp(segmentsPos[i], segmentsPos[i-1] + targetDir.up * segmentsDistance, ref segmentsVelocity[i], smoothSpeed + i / trailSpeed);
        }
        line.SetPositions(segmentsPos);
    }
}
