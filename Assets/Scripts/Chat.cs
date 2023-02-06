using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;

public class Chat : MonoBehaviour
{
    public TMP_InputField chatInput;
    public TMP_Text chatTextBox;

    public void SendChat(string nameInGame)
    {
        if (Input.GetKeyDown(KeyCode.Return) && chatInput.text != "")
        {
            string content = $"<color=\"yellow\">{nameInGame}</color>:{chatInput.text}";

            chatInput.text = "";
            ChatMessage chatMessageObject = new ChatMessage()
            {
                message = content
            };
            NetworkClient.Send<ChatMessage>(chatMessageObject);
        }
    }

    public bool IsFocus()
    {
        return chatInput.isFocused;
    }

    public void SetChatText(string message)
    {
        int lineCount = chatTextBox.text.Split('\n').Length;
        if (lineCount > 5)
            chatTextBox.text = chatTextBox.text.Substring(chatTextBox.text.IndexOf("\n") + 1);
        chatTextBox.text += $"\n{message}";

    }
}
