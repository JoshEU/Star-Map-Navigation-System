using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This Class allow the user to toggle the UI ON/OFF using the 'Q' Key
public class ShowUI : MonoBehaviour {
    [SerializeField]
    private UIManager uiManagerScript;
    [SerializeField]
    private GameObject uiObj;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Q) && uiObj.activeSelf == false && uiManagerScript.isPaused == false) {     
            uiObj.SetActive(true);
        } else if(Input.GetKeyDown(KeyCode.Q) && uiObj.activeSelf == true && uiManagerScript.isPaused == false) {
            uiObj.SetActive(false);
        }
    }
}