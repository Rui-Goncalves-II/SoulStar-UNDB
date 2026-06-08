using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float gravity = -9.81f;
    public float mouseSensitivity = 2f;

    private CharacterController controller;
    private float verticalVelocity;
    private float cameraPitch = 0f;
    private Transform cam;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked; // trava o mouse na janela
    }

    void Update()
    {
        // --- Olhar com o mouse ---
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        transform.Rotate(Vector3.up * mouseX);
        cameraPitch = Mathf.Clamp(cameraPitch - mouseY, -90f, 90f);
        cam.localEulerAngles = new Vector3(cameraPitch, 0f, 0f);

        // --- Movimento WASD ---
        float x = Input.GetAxis("Horizontal"); // A / D
        float z = Input.GetAxis("Vertical");   // W / S
        Vector3 move = transform.right * x + transform.forward * z;

        // --- Gravidade ---
        if (controller.isGrounded && verticalVelocity < 0)
            verticalVelocity = -2f; // "cola" no chão sem acumular queda
        else
            verticalVelocity += gravity * Time.deltaTime;

        Vector3 velocity = move * speed + Vector3.up * verticalVelocity;
        controller.Move(velocity * Time.deltaTime);
    }
}