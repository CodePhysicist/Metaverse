using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public struct CharacterCreatorMessage : NetworkMessage
{
    public string name;
    public string character;
}
