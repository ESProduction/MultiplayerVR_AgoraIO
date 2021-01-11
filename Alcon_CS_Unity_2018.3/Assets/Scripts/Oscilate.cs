using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscilate : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void FixedUpdate() {
        transform.position = new Vector3(Mathf.Clamp(Mathf.Sin(Time.time), -2.0f, 2.0f), transform.position.y, transform.position.z);
    }
}
