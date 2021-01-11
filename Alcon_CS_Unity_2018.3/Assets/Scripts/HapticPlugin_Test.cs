using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine;

public class HapticPlugin_Test : HapticPlugin {
    #region DLL_Imports
    [DllImport("OHToUnityBridge")] public static extern void setProxyPosition(string configName, double[] position3);
    #endregion

    [SerializeField]
    double[] l = { 0.0, 0.0, 0.0 };
    [SerializeField]
    double[] t = { 0.0, 0.0, 0.0 };
    [SerializeField]
    float clampvalue = 0.5f;
    [SerializeField]
    float rate = 1;
    [SerializeField]
    Vector3 position3 = new Vector3(0,0,0);
    [SerializeField]
    double[] velocity3 = { 0, 0, 0 };


    [SerializeField]
    float timeItTakesToMove = 5.0f;
    [SerializeField]
   

    private void Update() {
        //l[1] = Mathf.Clamp(Mathf.Sin(Time.time) * rate, clampvalue * -1, clampvalue);
        //setForce(configName, l,t);

        if (hHD < 0)
            return;


        if (Input.GetKey(KeyCode.A))
        {
            double[] set = { 0.0, 1.0, 0.0 };
            ApplyForce(set);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveTo();
        }
        else
        {
            double[] set = { 0.0, 0.0, 0.0 };
            ApplyForce(set);
        }
        //setProxyPosition(configName, pos3);

        Debug.Log(stylusPositionRaw);
    }



    void moveTo() {
        Vector3 deltaD = position3 - transform.position;
        Vector3 vel = deltaD / timeItTakesToMove;
        double[] forceToApply = { vel.x / Time.deltaTime, vel.y / Time.deltaTime, vel.z / Time.deltaTime };
        setForce(configName, forceToApply, t);
    }

    void ApplyForce(double[] force) {
        
        setForce(configName, force, t);
    }
}
