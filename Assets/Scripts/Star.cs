using System.Collections.Generic;
using UnityEngine;

// This Class is attached to each star and contains its properties
public class Star : MonoBehaviour {
    // Star Properties:
    public string starName;
    public string isHabitable;
    public string threatLevel;
    public int numberOfConnections;
    // Pathfinding Properties: 
    public int starDistance;
    // Shortest distance from this star to the start [get the destination star and find the path that has the shortest distance to the start]
    public int shortestDistanceToStart = int.MaxValue;
    // Stores all the neighbouring stars positions for this individual star
    public List<Vector3> neighbouringStarsPositions = new List<Vector3>();
    // Stores all the neighbouring stars GameObjects for this individual star
    public List<GameObject> neighbouringStarsObjs = new List<GameObject>();

    void Start() {
        starName = StarManager.starNamesArray[Random.Range(0, StarManager.starNamesArray.Length)];
        // Won't generate an existing starName - unique name each time
        while(GameObject.Find(starName) == true) {
            starName = StarManager.starNamesArray[Random.Range(0, StarManager.starNamesArray.Length)];
        }
        isHabitable = StarManager.isHabitableArray[Random.Range(0, StarManager.isHabitableArray.Length)];
        threatLevel = StarManager.threatLevelArray[Random.Range(0, StarManager.threatLevelArray.Length)];
        // Decides how many connections a star can have and gives it a random number of connections between a range
        // Between 1 and 4 neighbouring connections
        switch (StarManager.maxConnectionNumber) {
            case 0:
                numberOfConnections = Random.Range(0, 0);
                break;
            case 1:
                numberOfConnections = Random.Range(1, 2);
                break;
            case 2:
                numberOfConnections = Random.Range(1, 3);
                break;
            case 3:
                numberOfConnections = Random.Range(1, 4);
                break;
            case 4:
                numberOfConnections = Random.Range(1, 5);
                break;
        }
        starDistance = Random.Range(1, 11);
        // Set chosen name of star to the GameObjects name inside the inspector - for UI purposes
        gameObject.name = starName;
        StarManager.numberOfStarConnectionsList.Add(numberOfConnections);
    }
    public void StoreNeighbourPositions() {
        // Set Max neighbouring List capacity: - 1 because of 0 indexing
        neighbouringStarsPositions.Capacity = numberOfConnections - 1;
        LineRenderer[] childLineRendererObjs = GetComponentsInChildren<LineRenderer>();
        foreach (LineRenderer currentLR in childLineRendererObjs) {
            neighbouringStarsPositions.Add(currentLR.GetPosition(1));
        }
    }
}