using UnityEngine;
using System.Collections; // Required for IEnumerator

// Controls player movement and rotation.
public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f; // Set player's movement speed.
    public float rotationSpeed = 120.0f; // Set player's rotation speed.
    public float jumpForce = 5.0f; // Set player's jump force.
    public float jumpCooldown = 1.0f; // Set the cooldown time between jumps.

    private Rigidbody rb; // Reference to player's Rigidbody.
    private Animator animator; // Reference to player's Animator.
    private bool canJump = true; // Flag to control jump cooldown.

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Access player's Rigidbody.
        animator = GetComponent<Animator>(); // Access player's Animator.
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Jump") && canJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            StartCoroutine(JumpCooldown());
        }

        // Update the isWalking parameter based on player movement input
        float moveVertical = Input.GetAxis("Vertical");
        bool isWalking = Mathf.Abs(moveVertical) > 0.1f; // Adjust threshold as needed
        //animator.SetBool("isWalking", isWalking);

        //Animations
        if(isWalking == false)
        {
            //idle
            animator.SetFloat("Speed", 0);
        }
        else if (!Input.GetKey(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.LeftShift))
        {
            //walk
            animator.SetFloat("Speed", 0.3f);
            speed = 1f;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            //run
            animator.SetFloat("Speed", 1);
            speed = 5f;
        }       
    }

    // Handle physics-based movement and rotation.
    private void FixedUpdate()
    {
        // Move player based on vertical input.
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = transform.forward * moveVertical * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

        // Rotate player based on horizontal input.
        float turn = Input.GetAxis("Horizontal") * rotationSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }

    private IEnumerator JumpCooldown()
    {
        canJump = false;
        yield return new WaitForSeconds(jumpCooldown);
        canJump = true;
    }
}
