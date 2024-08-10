using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Interactable : MonoBehaviour
{
    public float InteractionDistance = 5f;

    Transform player;
    void Awake()
    {
        player = PlayerManager.Instance?.player;

        Initialize();
    }

    protected virtual void Initialize() { }


    public virtual void Interact()
    {
        //Can be overwriten
        Debug.Log("Interacting with " + transform.name);
    }

    //radius for editor
    /*void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, InteractionDistance);
    }*/


    //Check for interaction
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && !Input.GetMouseButton(1))
        {
            float distance = Vector2.Distance(player.position, transform.position);
            if (distance <= InteractionDistance)
            {
                Interact();
            }
        }
    }
}
