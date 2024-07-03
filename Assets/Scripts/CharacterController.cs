using UnityEngine;

public class CharacterController : ManagedBehaviour
{
    Unit unit;
    Vector2 m_movement;
    public float movementSpeed;

    protected bool m_isAttacking;

    protected override void ManagedInitialize()
    {
        unit = GetComponent<Unit>();
    }

    public override void ManagedUpdate()
    {
        HandleInput();
    }

    protected void HandleInput()
    {
        //movement
        m_movement.x = Input.GetAxis("Horizontal");
        m_movement.y = Input.GetAxis("Vertical");

        unit.MoveBy(m_movement.normalized * movementSpeed * Time.deltaTime);

        //attacking
        if (GetAttackInput())
            unit.TryAttacking();

        //reloading
        if(Input.GetKeyDown(KeyCode.R))
            unit.weapon?.Reload();
    }

    protected bool GetAttackInput()
    {
        //this prevents the player from shooting while
        //picking up a weapon or interacting with other objects
        if (Input.GetButtonDown("Fire1") && PlayerCursor.instance.CurrentInteractable == null)
        {
            m_isAttacking = true;
            return true;
        }

        if(m_isAttacking && Input.GetButton("Fire1"))
            return true;

        if (Input.GetButtonUp("Fire1"))
            m_isAttacking = false;

        return false;
    }
}
