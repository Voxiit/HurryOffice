using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Attributes
    //Components / Objects
    private CharacterController _playerController;

    [SerializeField]
    private Camera _playerCamera;

    private Transform _playerTransform;

    [SerializeField]
    private Transform _groundCheck;

    //Variables
    [SerializeField]
    private bool _playerCanMove = false;

    [SerializeField]
    private float _playerSpeed = 6f;

    [SerializeField]
    private float _playerJumpHeight = 3f;

    [SerializeField]
    private float _mouseSensitivity = 100f;

    private float _xRotation = 0f;

    private Vector3 _playerVelocity;

    [SerializeField]
    private float _gravity = -25f;

    [SerializeField]
    private float _groundDistanceDetection = 0.4f;

    [SerializeField]
    private LayerMask _groundLayerMask;

    private bool _playerIsOnTheFloor;

    //Functions
    void Start()
    {
        _playerTransform = this.GetComponent<Transform>();
        _playerController = this.GetComponent<CharacterController>();

        //We will lock the cursor for the sake of my sanity
        Cursor.lockState = CursorLockMode.Locked;

        //Subscribe to the coutdown manager action
        CoutdownManager.ChangePlayerMoveStatus += SetPlayerCanMove;
        Grabber.PlayerWin += PlayerWin;
    }

    //Update functions
    void Update()
    {
        if(_playerCanMove)
        {
            UpdateRotation();
            UpdatePosition();
        }
    }

    private void UpdateRotation()
    {
        //Get mouse state
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

        //For the horizontal rotation, we update the player object
        _playerTransform.Rotate(Vector3.up * mouseX);

        //For the vertical roation, we update the camera object
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
        _playerCamera.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
    }

    private void UpdatePosition()
    {
        PositionInput();
        PositionVelocity();
    }

    //Update position function
    private void PositionInput()
    {
        //Get keyboard state
        float xDirection = Input.GetAxis("Horizontal");
        float zDirection = Input.GetAxis("Vertical");

        //Calculate movement
        Vector3 xMovement = transform.right * xDirection;
        Vector3 zMovement = transform.forward * zDirection;
        Vector3 playerMovement = xMovement + zMovement;
        playerMovement = playerMovement * _playerSpeed * Time.deltaTime;

        //Apply movement
        _playerController.Move(playerMovement);
    }

    private void PositionVelocity()
    {
        //Let's check if we're on the floor
        _playerIsOnTheFloor = Physics.CheckSphere(_groundCheck.position, _groundDistanceDetection, _groundLayerMask);

        if(_playerIsOnTheFloor && _playerVelocity.y < 0f)
        {
            _playerVelocity.y = -5f;
        }


        //Check if we want to jump
        if(Input.GetButton("Jump") && _playerIsOnTheFloor)
        {
            _playerVelocity.y = Mathf.Sqrt(_playerJumpHeight * -2f * _gravity);
        }


        //Update with gravity
        _playerVelocity.y += _gravity * Time.deltaTime;
        _playerController.Move(_playerVelocity * Time.deltaTime);
    }

    //Change playerStatus
    private void SetPlayerCanMove(bool playerCanMove)
    {
        _playerCanMove = playerCanMove;
    }

    //Win function
    private void PlayerWin()
    {
        _playerCanMove = false;
    }
}
