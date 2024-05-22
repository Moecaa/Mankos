using System.Collections;
using UnityEngine;
using TMPro;
using Photon.VR;
using PlayFab.ClientModels;
using PlayFab;

public class NameManager : MonoBehaviour
{
    [Header("made by joshh, please give credits")]
    public bool isChar;
    public bool isBack;
    public bool isEnter;
    [Header("Only fill in the string if isChar is on")]
    public string charKey;
    public TextMeshPro nameText;
    public int minNameLength = 3;
    public int maxNameLength = 15;
    public string HandTag = "HandTag";
    private void Start()
    {
        nameText.text = PlayerPrefs.GetString("username");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(HandTag))
        {
            if (isChar)
            {
                if (nameText.text.Length >= maxNameLength)
                {
                    Debug.Log("Max limt reached!");
                }
                else
                {
                    nameText.text = nameText.text + charKey;
                }
            }

            if (isBack)
            {
                nameText.text = nameText.text.Remove(nameText.text.Length - 1);
            }
            if (isEnter)
            {
                if (nameText.text.Length < minNameLength)
                {
                    Debug.Log("You don't have enough characters");
                }
                else
                {
                    PhotonVRManager.SetUsername(nameText.text);
                    PlayerPrefs.SetString("username", nameText.text);
                    string NameChangeTo = nameText.text;

                    var request = new UpdateUserTitleDisplayNameRequest()
                    {
                        DisplayName = NameChangeTo,
                    };
                    PlayFabClientAPI.UpdateUserTitleDisplayName(request, UpdatedName, ErrorUpdatingName);
                }
            }
        }
    }
    void UpdatedName(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Updated Playfab Display Name");
    }

    void ErrorUpdatingName(PlayFabError error)
    {
        Debug.Log("Error updating Playfab Display Name!");
    }
}
