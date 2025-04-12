using UnityEngine;

public class PlayerMovementInputController : MonoBehaviour
{
    [Header("Movement axis")]
    [SerializeField] string HorizontalAxis = "Horizontal";
    [SerializeField] string VerticalAxis = "Vertical";

    [Header("Actions keys")]
    [SerializeField] KeyCode JumpKey = KeyCode.Space;
    [SerializeField] KeyCode CrouchKey = KeyCode.LeftControl;
    [SerializeField] bool HoldToCrouch = true;
    [SerializeField] KeyCode RunKey = KeyCode.LeftShift;
    [SerializeField] bool HoldToRun = true;

    [Header("Camera movement control")]
    [SerializeField] string VerticalCameraAxis = "Mouse X";
    [SerializeField] bool InvertVerticalAxis = false;
    [SerializeField] string HorizontalCameraAxis = "Mouse Y";
    [SerializeField] bool InvertHorizonalAxis = false;
    [SerializeField] [Range(0, 10)] float CameraSens = 2;
    [SerializeField] KeyCode activateCameraMove = KeyCode.Mouse1;
    [SerializeField] bool PressToMoveCamera = false;

    [Header("Zoom config")]
    [SerializeField] string ZoomAxis = "Mouse ScrollWheel";
    [SerializeField] bool InvertZoom = false;
    [SerializeField][Range(1, 10)] float ZoomSens = 10;



    [Header("Input values")]

    public float horizontalAxisValue;
    public float verticalAxisValue;

    [Space(10)]
    public bool jump;
    public bool crouch;
    public bool run;

    [Space(10)]
    public bool resetPostion;

    [Space(10)]
    public float horizontalCameraAxisValue;
    public float verticalCameraAxisValue;
    public bool cameraMovemntActive;
    public float zoomAxisValue;


    private void Update()
    {
        // Obtenemos valores de ejes verticales y horizontales

        if(HorizontalAxis != "")horizontalAxisValue = Input.GetAxis(HorizontalAxis);
        if(VerticalAxis   != "") verticalAxisValue = Input.GetAxis(VerticalAxis);

        // Salto
        jump = Input.GetKey(JumpKey);
        
        // Agachado
        if (HoldToCrouch) crouch = Input.GetKey(CrouchKey);
        else
        {
            if (Input.GetKeyDown(CrouchKey)) crouch = !crouch;
        }

        // Correr
        if (HoldToRun) run = Input.GetKey(RunKey);
        else
        {
            if (Input.GetKeyDown(RunKey)) run = !run;
        }

        // Camera control axis
        horizontalCameraAxisValue = Input.GetAxis(HorizontalCameraAxis) * (InvertHorizonalAxis? -1 : 1) * CameraSens;
        verticalCameraAxisValue = Input.GetAxis(VerticalCameraAxis) * (InvertVerticalAxis? -1 : 1) * CameraSens;

        // Para deteccion de tecla de mover camara
        if (PressToMoveCamera) cameraMovemntActive = Input.GetKeyDown(activateCameraMove);
        else cameraMovemntActive = true;

        // Para valores del zoom
        zoomAxisValue = Input.GetAxis(ZoomAxis) * (InvertZoom? -1 : -1) * (ZoomSens * 100);



    }

}
