using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMe : MonoBehaviour {
    [SerializeField]
    Vector3 newLocation;
    [SerializeField]
    float offset = 0.5f;
    [SerializeField]
    float speed = 5;
    [SerializeField]
    GameObject[] setOfLocations;

    private bool penInZone = false;
    private Vector3 startLocation;
    HapticPlugin_Test hapticDevice;
    HapticEffect_Test magnet;
    int currentLocationIndex = 0;

    // Start is called before the first frame update
    void Start() {
        magnet = GetComponentInChildren<HapticEffect_Test>();
        startLocation = transform.position;
        if (setOfLocations != null) {
            newLocation = setOfLocations[currentLocationIndex].transform.position;
        }
    }

    // Update is called once per frame
    void FixedUpdate() {
        penInZone = magnet.inTheZone[1];

        if(setOfLocations != null) {
            if(setOfLocations.Length >= 1) {
                if (penInZone) {
                    if(Vector3.Distance(transform.position, newLocation) > offset) {
                        moveTo();
                    } else{
                        currentLocationIndex += 1;
                        if (currentLocationIndex < setOfLocations.Length)
                            newLocation = setOfLocations[currentLocationIndex].transform.position;
                    }
                }

            }
        }


        if (Input.GetKey(KeyCode.Space)) resetPosition();

        DebugLogging();
    }

    void moveTo() {
        transform.position += Vector3.Normalize(newLocation - transform.position) * Time.deltaTime * speed;
    }

    void resetPosition() {
        transform.position = startLocation;
    }

    void DebugLogging() {
        Debug.Log(penInZone);
    }
}
