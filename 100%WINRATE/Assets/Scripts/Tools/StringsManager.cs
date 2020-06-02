using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringsManager : Singleton<StringsManager>
{
    public string photon;
    public string duel;
    public string player;
    public string duelPlayer;
    public string projectile;
    public string loot;
    public string playerEntry;
    public string poolParent;
    public string musicGameobject;

    [Header("Duel")]
    public string roundProperty;
    public string playerIDProperty;
    public string scoreProperty;
    public string isPlayerFirst;
    public string playersInRoom;
    public string first;
    public string second;
}
