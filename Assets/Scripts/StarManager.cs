using System.Collections.Generic;
using UnityEngine;

// This Class is for managing star property variables - ones that will pop up on the StarDescriptionPanel
public class StarManager : MonoBehaviour {
    // 50 Star Names
    public static string[] starNamesArray = { "The Sun", "Mercury", "Venus", "Earth", "Mars", "Jupiter", "Saturn", "Uranus", "Neptune", "Pluto",
    "Elysium", "Banshee", "Nemo", "Corel", "Kabal", "Tamsa", "Vesper", "Vendetta", "Vanguard", "Vulture",
    "Orion", "Virgil", "Virgo", "Hades", "Ferron", "Pyro", "Magnus", "Nexus", "Helios", "Osiris",
    "Indra", "Horus", "Terra", "Odin", "Khabari", "Pallas", "Baker", "Sol", "Kallis", "Kilian",
    "Drecon", "Pranilla", "Scareus", "Hol", "Keper", "Ibelius", "Kenoda", "Elos", "Etopia", "Utopia",
    };
    public static string[] isHabitableArray = { "Yes", "No" };
    public static string[] threatLevelArray = { "Low", "Medium", "High", "Very High", "Intense", "Extreme" };
    public GameObject[] starObjectsArray;
    public List<Vector3> starPositionsList;
    public static List<int> numberOfStarConnectionsList = new List<int>();
    public static int maxConnectionNumber = 3;
}