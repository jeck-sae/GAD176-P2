using UnityEngine;

public class AssignmentRequirements : MonoBehaviour
{
    [SerializeField] protected int serializeField;
    
    public Transform target;
    AssignmentRequirements r;
    
    void Start()
    {
        if(r == null)
        {
            r = GetComponent<AssignmentRequirements>();
            r.transform.position += Vector3.up;
        }

        var a = (Vector3.zero + Vector3.one).normalized.magnitude;
    }

    void Update()
    {
        Vector3 difference = target.position - transform.position;

        float magnitude = Mathf.Sqrt(
            difference.x * difference.x +
            difference.y * difference.y +
            difference.z * difference.z);

        Vector3 direction = new Vector3(
            difference.x / magnitude,
            difference.y / magnitude,
            difference.z / magnitude);

        transform.position += direction * Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, target.position);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (target.position - transform.position).normalized);

    }
}
