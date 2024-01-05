using UnityEngine;

// This Class contains the Movement logic for the Cameras in the scene
public class BasicCameraMovement : MonoBehaviour {
    public float speed;
    public static Vector3 camRotation = Vector3.zero;

    private void Start() {
        camRotation = Vector3.zero;
    }
    void Update() {
        // Camera Movement Inputs:
        if (Input.GetKey(KeyCode.W)) {
            // Move Camera in forwards direction
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A)) {
            // Move Camera in left direction (inverse of right)
            transform.position += -transform.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S)) {
            // Move Camera in downwards direction (inverse of forward)
            transform.position += -transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D)) {
            // Move Camera in right direction
            transform.position += transform.right * speed * Time.deltaTime;
        }
        // Camera Look Inputs mapped to the mouses x and y axis
        camRotation.y += Input.GetAxis("MouseX");
        camRotation.x += -Input.GetAxis("MouseY");
        // Allows rotation of the camera using the mouse
        transform.localEulerAngles = camRotation * speed;
    }
}