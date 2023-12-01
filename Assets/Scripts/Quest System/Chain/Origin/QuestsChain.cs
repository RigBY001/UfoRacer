using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestsChain", menuName = "QuestsChains/QuestsChain", order = 1)]
public class QuestsChain : ScriptableObject{
    public List<Quest> Quests;
}
