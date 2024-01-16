using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 10.0f;
    public float dashSpeed = 50.0f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 5.0f;

    public CooldownBar slashCooldown;

    private CharacterController characterController;


    private Rigidbody rb;
    private bool canDash = true;
    private bool isDashing = false;
    private bool hasDashedRecently = false;
    private Collider playerCollider;
    private PlayerShooting playerShooting;

    private void Start()
    {
        playerShooting = GetComponent<PlayerShooting>();
        slashCooldown.SetMaxHealth(dashCooldown);
        slashCooldown.SetHealth(dashCooldown);
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Make the character face the mouse
        RotateTowardsMouse();

        // Dash on right-click
        if (Input.GetMouseButtonDown(1) && canDash)
        {
            StartCoroutine(Dash());
        }

        if (isDashing)
        {
            playerCollider.isTrigger = true; // Set collider to trigger mode during dash
        }
        else
        {
            playerCollider.isTrigger = false; // Reset to default after dash ends
        }

        // Only proceed with WASD movement if not dashing
        if (!isDashing)
        {
            Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            //Vector3 move = transform.TransformDirection(moveInput) * moveSpeed;
            characterController.Move(moveInput * moveSpeed * Time.deltaTime);
            //Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            //transform.position += moveInput * moveSpeed * Time.deltaTime;
            //Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            //Vector3 moveVelocity = moveInput.normalized * moveSpeed;
            //rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);
        }

        if (transform.position.y != 1.32f)
        {
            transform.position = new Vector3(transform.position.x, 1.32f, transform.position.z);
        }
    }

    private void RotateTowardsMouse()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        hasDashedRecently = true;

        //Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        //float rayLength;
        //if (groundPlane.Raycast(cameraRay, out rayLength))
        //{
        //    Vector3 pointToLook = cameraRay.GetPoint(rayLength);
        //    Vector3 dashDirection = (pointToLook - transform.position).normalized;

        //    rb.AddForce(dashDirection * dashSpeed, ForceMode.VelocityChange);
        //}

        float startTime = Time.time;

        while (Time.time < startTime + dashDuration)
        {
            transform.position += transform.forward * dashSpeed * Time.deltaTime;
            yield return null;
        }

        SoundManager.Instance.PlaySoundOnce("Slash");

        //yield return new WaitForSeconds(dashDuration);

        //rb.velocity = Vector3.zero;

        isDashing = false;

        StartCoroutine(DashCooldown());

        yield return new WaitForSeconds(1f);
        hasDashedRecently = false;
    }


    private IEnumerator DashCooldown()
    {
        float remainingCooldown = 0f;

        while (remainingCooldown < dashCooldown)
        {
            remainingCooldown += Time.deltaTime;
            slashCooldown.SetHealth(remainingCooldown);
            yield return null;
        }

        canDash = true;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("Hit " + hit.gameObject.name);
        InteractiveObject interactiveObject = hit.gameObject.GetComponent<InteractiveObject>();
        if (hasDashedRecently && interactiveObject != null)
        {
            interactiveObject.TakeDamage(5);
            hasDashedRecently = false;
            return;
        }

        EnemyHealth enemyObject = hit.gameObject.GetComponent<EnemyHealth>();
        if (hasDashedRecently && enemyObject != null)
        {
            enemyObject.TakeDamage(5);
            if (enemyObject.currenthp <= 0)
            {
                playerShooting.currentProjectileType = enemyObject.gameObject.GetComponent<EnemyShooting>().selectedProjectileType;
                int randomValue;
                if (playerShooting.currentProjectileType.GetComponent<ShotgunProjectile>() != null)
                {
                    randomValue = Random.Range(6, 12);
                    playerShooting.ammoBar.projectileIcon = AmmoBar.ProjectileType.Shotgun;
                } else if (playerShooting.currentProjectileType.GetComponent<RicochetProjectile>() != null)
                {
                    randomValue = Random.Range(8, 14);
                    playerShooting.ammoBar.projectileIcon = AmmoBar.ProjectileType.Ricochet;
                } else
                {
                    randomValue = Random.Range(12, 18);
                    playerShooting.ammoBar.projectileIcon = AmmoBar.ProjectileType.Standard;
                }

                playerShooting.ammo = randomValue;
                playerShooting.ammoBar.SetMaxAmmo(randomValue);
            }
            hasDashedRecently = false;
            return;
        }

    }

}
