using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour {

    [SerializeField]
    GameObject target;
    [SerializeField]
    float speed = 50.0f;
    [SerializeField]
    HapticPlugin hapticPen; 

    bool entered = false;
    HapticEffect_Test effect;
    Vector3 enterOffset;
    Quaternion q;

    Vector3 axisVector;
    float theta;

    private void Start() {
        effect = GetComponentInChildren<HapticEffect_Test>();
        enterOffset = Vector3.zero;
        theta = 0;
        axisVector = Vector3.zero;
    }

    Quaternion lookAtRotation() {
        Vector3 relativePos = target.transform.position - transform.position;
        return Quaternion.LookRotation(relativePos, Vector3.up);
    }

    // Update is called once per frame
    void FixedUpdate() {

        if (effect.inTheZone[0]) {
             enterOffset = Vector3.Normalize(target.transform.position- transform.position);
            if(!entered) { // Pen entered eye
                entered = true;
                theta = Mathf.Acos(Vector3.Dot(transform.forward, enterOffset));
                axisVector = Vector3.Cross(enterOffset,transform.forward);
                float w = Mathf.Cos(theta / 2);
                axisVector = Mathf.Sin(theta / 2) * axisVector;
                q = new Quaternion(axisVector.x, axisVector.y, axisVector.z, w);
            }

            Vector3 targetForward = q * enterOffset;
            Vector3 newForward = Vector3.Slerp(transform.forward, targetForward, speed);
            transform.LookAt(transform.position + newForward);

            
        }
        else { // Pen exitted eye
            entered = false;
            q = Quaternion.identity;
        }


        
       
    }
}
