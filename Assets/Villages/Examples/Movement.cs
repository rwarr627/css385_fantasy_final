using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    private CharacterController characterController;

    public float horizontalSpeed = 30.0f;
    public float verticalSpeed = 30.0f;
    public float rotateSpeed = 3.0F;

    private float yaw = 0;
    private float pitch = 0;

    public float yawMin = -20f;
    public float yawMax = 45f;

    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        // moveDirection = transform.TransformDirection(Vector3.forward); // new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        // moveDirection *= speed;

        // transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);

        float dt = Time.deltaTime;
        float dy =  0;

        // ascend
        if( Input.GetKey( KeyCode.LeftShift ) )
        {
            dy = verticalSpeed * dt;
        }
        // decend
        if( Input.GetKey( KeyCode.LeftControl ) )
        {
            dy = -verticalSpeed * dt;
        }

        // Rotate around the character
        pitch += Input.GetAxis( "Mouse X" ) * rotateSpeed;
        yaw += Input.GetAxis( "Mouse Y" ) * rotateSpeed;
        yaw = Mathf.Clamp( yaw, yawMin, yawMax );
        transform.localRotation = Quaternion.Euler( yaw, pitch, 0 );

        float dx = Input.GetAxis( "Horizontal" ) * dt * horizontalSpeed;
        float dz = Input.GetAxis( "Vertical" ) * dt * horizontalSpeed;

        // characterController.Move( transform.TransformDirection( new Vector3( dx,
        //     dy, dz ) ) );
        characterController.Move( new Vector3( dx, dy, dz ) );
    }
}
