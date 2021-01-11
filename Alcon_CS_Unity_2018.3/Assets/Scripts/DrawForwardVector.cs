using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawForwardVector : MonoBehaviour {


    // Update is called once per frame
    void Update() {
        Debug.DrawRay(transform.position, transform.forward * 10, Color.red);
    }
}
