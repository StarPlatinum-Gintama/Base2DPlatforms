using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{

    Rigidbody2D rb;
    PlayerInput input;

    //Queremos modificar algo de un componente del GO
    //1. Definir una variable del tipo de componente aquí
    SpriteRenderer sr;

    [SerializeField] float movementForce = 10.0f;
    [SerializeField] float jumpForce = 1.0f;

    Vector2 movementInput;

    bool isGrounded = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();

        //No colisionar con mi propio collider
        Physics2D.queriesStartInColliders = false;

        //2. Recuperar el componente en el start
        sr = GetComponent<SpriteRenderer>();


    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded) {
            Debug.Log("Jump");
            isGrounded = false;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    // Update is called once per frame
    void Update()
    {   
        movementInput = input.actions["Move"].ReadValue<Vector2>();

        if(movementInput.x != 0f)
        {
            if (movementInput.x > 0f) 
            {
                //3. Modificar algún atributo o usar algún método
                sr.flipX = false;
            }
            else
            {
                sr.flipX = true;
            }
        }
    }

    private void FixedUpdate()
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);

        //Debug.Log($"Distance: {hit.distance}");

        if(hit.collider != null && hit.distance < 0.48f && rb.linearVelocityY < 0.1f)
        {
            //Debug.Log($"Distance: {hit.distance}");
            isGrounded = true;
        }

        rb.AddForce(Vector2.right * movementInput.x * movementForce);

    }
}
