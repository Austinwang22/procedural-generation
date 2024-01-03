using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class FPSController : MonoBehaviour
{
    public float walkSpeed = 1.0f;

    public float jumpSpeed = 5.0f;

    public float sensitivity = 2.0f;

    public float gravity = 9.8f;

    public Camera mainCamera;
    public CharacterController characterController;

    float verticalVelocity = 0f;

    float rotationX = 0f;

    float lookLimit = 45f;
    
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    
    void Update()
    {
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        bool grounded = characterController.isGrounded;

        if (grounded) {
            verticalVelocity = 0f;
            if (Input.GetButton("Jump")) {
                verticalVelocity = jumpSpeed;
            }
        }
        else {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        characterController.Move(new Vector3(0f, verticalVelocity * Time.deltaTime, 0f));

        if (Input.GetKey(KeyCode.W)) {
            characterController.Move(forward * walkSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S)) {
            characterController.Move(-forward * walkSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D)) {
            characterController.Move(right * walkSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A)) {
            characterController.Move(-right * walkSpeed * Time.deltaTime);
        }

        rotationX += -Input.GetAxis("Mouse Y") * sensitivity;
        rotationX = Mathf.Clamp(rotationX, -lookLimit, lookLimit);
        mainCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * sensitivity, 0);
    }
}
