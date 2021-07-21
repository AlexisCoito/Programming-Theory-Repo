using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 10;
    [SerializeField] float speedImpulse = 10;
    [SerializeField] float jumpForce = 10;
    [SerializeField] float turnSpeed = 5;
    private float distToGround;
    Rigidbody playerRb;
    float horizontalInput;
    float verticalInput;
    bool impulseIsReady = true;
    bool ShiftPressed;
    bool spacePressed;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    private void Update()
    {
        // ABSTRACTION
        GetInput();

        if(transform.position.y < -10)
        {
            SceneManager.LoadScene(3);
        }
    }

    void FixedUpdate()
    {
        if (ShiftPressed)
        {
            ShiftPressed = false;
            // ABSTRACTION
            Impulse();
            
        }

        if (spacePressed)
        {
            spacePressed = false;
            // ABSTRACTION
            Jump();
        }
        transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * speed);
        transform.Rotate(Vector3.up * Time.deltaTime * horizontalInput * turnSpeed);
    }

   bool IsGrounded() 
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift) && impulseIsReady)
        {
            ShiftPressed = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && (IsGrounded()))
        {
            spacePressed = true;
            
        }
        
    }
    void Impulse()
    {
        playerRb.AddRelativeForce(Vector3.forward * Time.deltaTime * speedImpulse, ForceMode.Impulse);
        impulseIsReady = false;
        StartCoroutine("ImpulseCountdownRoutine");
    }

    void Jump()
    {
        playerRb.AddRelativeForce(Vector3.up * Time.deltaTime * jumpForce);
    }
    IEnumerator ImpulseCountdownRoutine()
    {
        yield return new WaitForSeconds(3);
        impulseIsReady = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //EndGame
            SceneManager.LoadScene(3);
            
        }

        if (other.CompareTag("Finish"))
        {
            SceneManager.LoadScene(2);
        }
    }
}
