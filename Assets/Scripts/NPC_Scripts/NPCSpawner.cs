using System.Collections;
using UnityEngine;

// This Class contains the logic for spawning in the NPC's in a random location out of the pre-defined spawning parameters
// Spawning occurs after a set time-interval
public class NPCSpawner : MonoBehaviour {
    [SerializeField]
    private GameObject[] spaceShipObjectsArray;
    [SerializeField]
    private GameObject redAsteroidObj;
    public float shipSpawnTimer = 15.0f;
    public bool canSpawnSpaceship = true;
    public float asteroidSpawnTimer = 20.0f;
    public bool canSpawnAsteroid = true;

    void Start() {
        canSpawnSpaceship = true;
        canSpawnAsteroid = true;
    }
    private IEnumerator SpawnNPCShip() {
        float randSpawnX = Random.Range(-39.25f, -27.528f);
        float randSpawnY = Random.Range(-3.118f, 5.11f);
        float randSpawnZ = Random.Range(-2.507f, 2.505f);
        int randIndex = Random.Range(0, 3);
        // Checks if an NPC Ship can spawn
        if (canSpawnSpaceship) {
            Vector3 randSpawnPos = new Vector3(randSpawnX, randSpawnY, randSpawnZ);
            Instantiate(spaceShipObjectsArray[randIndex], randSpawnPos, new Quaternion(0, 90, 0, 90f));
        }
        yield return null;
    }
    private IEnumerator SpawnNPCAsteroid() {
        float randSpawnX = Random.Range(149.07f, 160.92f);
        float randSpawnY = Random.Range(-2.37f, 4.35f);
        float randSpawnZ = Random.Range(103.8f, 128.09f);
        // Checks if an NPC Asteroid can spawn
        if (canSpawnAsteroid) {
            Vector3 randSpawnPos = new Vector3(randSpawnX, randSpawnY, randSpawnZ);
            Instantiate(redAsteroidObj, randSpawnPos, new Quaternion(0, 90, 0, 90f));
        }
        yield return null;
    }
    void Update() {
        // Decrement shipSpawnTimer using Time
        shipSpawnTimer -= Time.deltaTime;
        // Decrement asteroidSpawnTimer using Time
        asteroidSpawnTimer -= Time.deltaTime;
        // Spawn NPC Spaceship when the timer reaches or 0 or below
        if (shipSpawnTimer <= 0.0f) {
            StartCoroutine(SpawnNPCShip());
            shipSpawnTimer = 15;
        }
        // Spawn NPC RedAsteroid when the timer reaches or 0 or below
        if (asteroidSpawnTimer <= 0.0f) {
            StartCoroutine(SpawnNPCAsteroid());
            asteroidSpawnTimer = 20;
        }
    }
}