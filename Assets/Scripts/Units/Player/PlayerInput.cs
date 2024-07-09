using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Unit unit;
    Vector2 m_movement;
    public float movementSpeed;

    protected bool m_isSprinting;
    protected bool m_isAttacking;

    protected void Awake()
    {
        unit = GetComponent<Unit>();
    }

    private void Update()
    {
        m_isSprinting = Input.GetKey(KeyCode.LeftControl);
    }
    private void FixedUpdate()
    {
        HandleInput();
    }
    protected void HandleInput()
    {
        //movement
        m_movement.x = Input.GetAxis("Horizontal");
        m_movement.y = Input.GetAxis("Vertical");

        float moveSpeed = movementSpeed;
        if (m_isSprinting)
            moveSpeed += PlayerStats.Instance.Dexterity.GetValue() / 10;

        unit.MoveBy(m_movement.normalized * moveSpeed * Time.fixedDeltaTime);

        //attacking
        if (Input.GetButton("Fire1"))
            unit.TryAttacking();

        if (Input.GetButton("Fire2"))
            unit.itemInHand?.TryUsing();

        //reloading
        if(Input.GetKeyDown(KeyCode.R))
            unit.weapon?.Reload();
    }

}
