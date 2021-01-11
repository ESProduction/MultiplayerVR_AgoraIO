using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elara.Networking.PlayerManagement
{
    public class VRPlayerManager : MonoBehaviourPunCallbacks
    {
        //TODO: RESEARCH https://www.youtube.com/watch?v=YvLbPC4TlX4

        public Camera playerCamera;
        float _exitTimer;

        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;


        private void Start()
        {
            //only activate vr camera if this is the photon view
            if (photonView.IsMine)
            {
                Camera.main.enabled = false;
                playerCamera.enabled = true;
            }
            else
            {
                Camera.main.enabled = true;
                playerCamera.enabled = false;
            }

            // #Important
            // used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
            if (photonView.IsMine)
            {
                VRPlayerManager.LocalPlayerInstance = this.gameObject;
            }
            // #Critical
            // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
            DontDestroyOnLoad(this.gameObject);

        }
        private void Update()
        {
            if (photonView.IsMine == false)
                return;

            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.up * h * Time.deltaTime * 45f);
            transform.Translate(new Vector3(0f, 0f, v * Time.deltaTime * 50f), Space.Self);

            if (Input.GetKey(KeyCode.Joystick1Button1))
            {
                _exitTimer += Time.deltaTime;
            }
            else
                _exitTimer -= Time.deltaTime;

            _exitTimer = Mathf.Clamp(_exitTimer, 0f, 1f);
            if(_exitTimer >= 1f)
                GameManager.Instance.LeaveRoom();

        }
    }
}
