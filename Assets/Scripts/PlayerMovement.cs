using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;

    PlayerInput playerInput;
    [SerializeField] float movementForce = 1f;
    [SerializeField] float jumpForce = 1f;

    public bool isGrounded;
    Vector2 movementInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        sr = GetComponent<SpriteRenderer>();

        playerInput = GetComponent<PlayerInput>();
        
        Physics2D.queriesStartInColliders = false;
        
        isGrounded = false;
    }


    // Update is called once per frame
    void Update()
    {
       // if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            //Jump();
        }

        //movementInput = Input.GetAxis("Horizontal");
        movementInput = playerInput.actions["Move"].ReadValue<Vector2>();

        if (movementInput.x != 0f)
        {
            if (movementInput.x > 0f)
            {
                sr.flipX = false;
            }
            else
            {
                sr.flipX = true;
            }
        }
    }

     void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position,Vector2.down);

        //Debug.Log($"Distance: {hit.distance}"); PARA VER DONDE ESTÁ
        if (hit.collider != null && hit.distance < 0.48f && rb.linearVelocityY < 0.01f) 
        {
            isGrounded = true;
        }

        rb.AddForce(Vector2.right * movementInput.x * movementForce);
    }

    //METODOS PROPIOS//
    public void Jump(InputAction.CallbackContext context)
    {
        /*
         * rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
         * isGrounded = false;
        */

        //Performed = Cuando la accion se ha completado 
        if (context.performed && isGrounded)
        {
            Debug.Log("Jump");
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            isGrounded = false;
        }
    }
}
