using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StartModule : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public TMP_Text cTextBox;
    private string character = "Man";

    // Start is called before the first frame update
    public void StartHost()
    {
        if (string.IsNullOrWhiteSpace(nameInputField.text))
            return;

        PlayerPrefs.SetString("Name", nameInputField.text);
        PlayerPrefs.SetString("Character", character);
        PlayerPrefs.SetString("Connection", "Host");
        SceneManager.LoadScene("Metaverse", LoadSceneMode.Single);
    }

    public void StartClient()
    {
        if (string.IsNullOrWhiteSpace(nameInputField.text))
            return;

        PlayerPrefs.SetString("Name", nameInputField.text);
        PlayerPrefs.SetString("Character", character);
        PlayerPrefs.SetString("Connection", "Client");
        SceneManager.LoadScene("Metaverse", LoadSceneMode.Single);
    }

    public void ChangeCharacter()
    {
        if(character == "Man")
        {
            character = "Girl";
            cTextBox.SetText(character);
        }
        else if(character == "Girl")
        {
            character = "Boss";
            cTextBox.SetText(character);
        }
        else
        {
            character = "Man";
            cTextBox.SetText(character);
        }
    }
}
