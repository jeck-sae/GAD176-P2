using UnityEngine;
using static UnityEditor.Rendering.FilterWindow;

public class PlayerInput : MonoBehaviour
{
    Unit unit;

    protected bool m_isSprinting;
    protected bool m_isAttacking;

    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;
    public KeyCode SprintKey;
    float modifier;
    public Animator anim;

    protected void Awake()
    {
        unit = GetComponent<Unit>();
    }
    private void Update()
    {
        HandleInput();
    }
    protected void HandleInput()
    {
        //movement
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        if (movement.x > 0 || movement.y > 0 || movement.x < 0 || movement.y < 0)
        {
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }

        if (Input.GetKey(SprintKey))
        {
            modifier = 1f + PlayerStats.Instance.Dexterity.GetValue() / 10f;
        }
        else
        {
            modifier = 1f;
        }

        //attacking
        if (Input.GetMouseButton(1))
        {
            if (Input.GetMouseButtonDown(0))
            {
                unit.TryAttacking();
                anim.SetTrigger("Shot");
            }
            anim.SetBool("Aim", true);
        }
        else
        {
            anim.SetBool("Aim", false);
        }

        //reloading
        if (Input.GetKeyDown(KeyCode.R))
            unit.weapon?.Reload();
    }
    void FixedUpdate()
    {
        // Move the player
        rb.MovePosition(rb.position + movement * moveSpeed * modifier * Time.fixedDeltaTime);
    }
}