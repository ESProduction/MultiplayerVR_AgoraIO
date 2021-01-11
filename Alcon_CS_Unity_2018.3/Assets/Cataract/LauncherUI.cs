using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LauncherUI : MonoBehaviour
{
    [Tooltip("The Ui Panel to let the user enter name, connect and play")]
    [SerializeField]
    private GameObject controlPanel;
    [Tooltip("The UI Label to inform the user that the connection is in progress")]
    [SerializeField]
    private GameObject progressPanel;

    #region Private Constants
    // Store the PlayerPref Key to avoid typos
    const string playerNamePrefKey = "PlayerName";
    #endregion

    public InputField inputField_playerName;

    #region MonoBehaviour CallBacks
    void Start()
    {
        string defaultName = string.Empty;
        if (inputField_playerName != null)
        {
            if (PlayerPrefs.HasKey(playerNamePrefKey))
            {
                defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                inputField_playerName.text = defaultName;
            }
        }


        PhotonNetwork.NickName = defaultName;
    }
    #endregion


    #region Public Methods

    public void UpdatePanelVisibility(bool controlPanelActive, bool progressPanelActive)
    {
        controlPanel.SetActive(controlPanelActive);
        progressPanel.SetActive(progressPanelActive);
    }

    /// <summary>
    /// Sets the name of the player, and save it in the PlayerPrefs for future sessions.
    /// </summary>
    /// <param name="value">The name of the Player</param>
    public void SetPlayerName(string value)
    {
        // #Important
        if (string.IsNullOrEmpty(value))
        {
            Debug.LogError("Player Name is null or empty");
            return;
        }
        PhotonNetwork.NickName = value;
        PlayerPrefs.SetString(playerNamePrefKey, value);
    }
    #endregion
}
