using System.Collections;
using UnityEngine;

// This Class carries out the Explosion logic for the NPC Spaceships
public class NPCExplode : MonoBehaviour {
    [SerializeField]
    private AudioManager audioManagerScript;
    [SerializeField]
    private ParticleSystem boostPS;
    [SerializeField]
    private ParticleSystem explosionPS;
    public float selfDestructTimer = 20.0f;

	private void Awake() {
        // Finds the Gameobject with the AudioManager.cs script attached
        audioManagerScript = (AudioManager)GameObject.FindObjectOfType(typeof(AudioManager));
    }
    // Explodes the Ship once it either collides with a Star or its self-destruct timer has been reached in order to increase performance of the project
	private IEnumerator ExplodeShip() {
        boostPS.Stop();
        explosionPS.Play();
        yield return new WaitForSeconds(0.05f);
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(2.01f); // Wait for the Explosion VFX to end
        Destroy(gameObject);
    }
    private void Update() {
        // Decrement selfDestructTimer using Time
        selfDestructTimer -= Time.deltaTime;

        // Destroy NPC Spaceship when the selfDestructTimer reaches or 0 or below
        if (selfDestructTimer <= 0.0f) {
            StartCoroutine(ExplodeShip());
        }
    }
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Star")) {
            audioManagerScript.NPCExplosionAudio();
            StartCoroutine(ExplodeShip());
        }
    }
}