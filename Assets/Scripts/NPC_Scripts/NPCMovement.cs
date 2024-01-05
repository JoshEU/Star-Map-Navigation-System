using UnityEngine;

// This Class contains the logic for both the NPC Spaceship's movement and the NPC Flying Asteroid's movement
public class NPCMovement : MonoBehaviour {
    public float speed = 100.0f;

    void Update() {
        if (gameObject.CompareTag("NPCSpaceship")) {
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.right * speed * Time.deltaTime, ForceMode.Force);
        }
        else if (gameObject.CompareTag("NPCFlyingAsteroid")) {
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.left * speed * Time.deltaTime, ForceMode.Force);
        }
    }
}