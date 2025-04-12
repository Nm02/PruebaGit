using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody))]
public class BallGamePlayerController : MonoBehaviour
{
    //Variables publicas (Se las puede ver desde el editor de Unity) 
    [Header("Settings")]
    [SerializeField] float MaxSpeed = 50;
    [SerializeField] float groundCheckDistance = 0.3f;
    [SerializeField] float speed = 4;
    [SerializeField] float jumpForce = 1;
                               
    [Header("Camera config")]
    [SerializeField] Camera PlayerCamera;

    [Header("Sound effects")]
    [SerializeField] AudioClip jumpSound;

    [Header("Input controller")]
    [SerializeField] PlayerMovementInputController inputController;

    //Variables privadas (No se las puede ver desde el editor de Unity)
    Rigidbody rb; 
    bool isGroundead;

    AudioSource src;
    Vector3 lastCheckPointPostion;
    
    //Todo lo que este aqui se va a hacer 1 sola vez al inicio
    void Start()
    {
        if (PlayerCamera == null) PlayerCamera = Camera.main;

        //Busco el rigydbody de la pelota
        rb = GetComponent<Rigidbody>();
        src = GetComponent<AudioSource>();

        // Guardo la posicion inicial como el primer checkpoint
        SaveCheckPoint();
    }

    private void Update()
    {
        // Detecto si el jugador esta tocando el suelo
        isGroundead = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);

    }

    void FixedUpdate()//Lo que escriba dentro de este "void" se repetira constantemente en mi juego
    {
        movement();
    }

    //Creo un "void" o PROCEDIMIENTO que controla unicamnete el movimiento del personaje  
    void movement()
    {
        if (inputController.jump && isGroundead)//Pregunta si aprito el espacio y si estoy tocando el suelo
        {
            if (jumpSound != null) src.PlayOneShot(jumpSound);

            Vector3 movimiento = new Vector3(0, jumpForce, 0);
            rb.AddForce(movimiento, ForceMode.Impulse);//Agrego una fuerza para arriba como si fuera un salto
            isGroundead = false;//Como salto dejo de tocar el suelo, entonces tando suelo es falso(para que no pueda saltar denuevo hasta tocar el suelo denuevo
        }
        else
        {
            // Para detectar para donde apunta la camara
            Vector3 cameraFoward = PlayerCamera.transform.forward;
            Vector3 cameraRight = PlayerCamera.transform.right;

            // Normalizamos para evitar diferencias de velocidad en las diagonales
            cameraFoward.y = 0;
            cameraRight.y = 0;
            cameraFoward.Normalize();
            cameraRight.Normalize();

            // Obtenemos los inputs
            float horizontal = inputController.horizontalAxisValue;//Vale 1 si estoy tocanto la letra para ir a la izquiera, -1 si aprieta la de la derecha y 0 si no aprieto nada
            float vertical = inputController.verticalAxisValue;//Lo mismo pero para arriba y abajo, si aprieto la letra para ir para adelante vale 1, para atras -1 y 0 si no aprieto ninguna
            Vector3 movimiento = (cameraFoward * vertical + cameraRight * horizontal) * speed * Time.deltaTime;


            //Agrego una fuerza o un impulso a mi pelota
            rb.AddForce(movimiento * speed);


            // Limitar la velocidad maxima
            if (rb.velocity.sqrMagnitude > MaxSpeed * MaxSpeed)                     // linearvelocity for Unity6
            {
                rb.velocity = rb.velocity.normalized * MaxSpeed;
            }
        }
    }

    public void ResetToCheckPoint()
    {
        // Para congelar el movimiento de la pelota
        rb.isKinematic = true;
        rb.isKinematic = false;

        // Actualizo la posicion de la pelota
        transform.position = lastCheckPointPostion;

    }

    public void SaveCheckPoint()
    {
        lastCheckPointPostion = transform.position;
    }



    public void TeleportTo(Transform newPlace)
    {
        // Para congelar el movimiento de la pelota
        rb.isKinematic = true;
        rb.isKinematic = false;

        // Actualizo la posicion de la pelota
        transform.position = newPlace.position;
    }

    void OnDrawGizmosSelected()
    {
        // Para dibujar la linea de ground check distance en la escena
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
    }

}
