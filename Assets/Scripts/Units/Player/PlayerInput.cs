using UnityEngine;

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

        if (Input.GetKey(SprintKey))
        {
            modifier = 1f + PlayerStats.Instance.Dexterity.GetValue() / 10f;
        }
        else
        {
            modifier = 1f;
        }

        //attacking
        if (Input.GetButton("Fire1"))
            unit.TryAttacking();

        if (Input.GetButton("Fire2"))
            unit.itemInHand?.TryUsing();

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