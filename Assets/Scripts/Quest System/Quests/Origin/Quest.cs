using UnityEngine;
using UnityEngine.Search;

[CreateAssetMenu(fileName = "Quest", menuName = "Quests/Quest", order = 1)]
public class Quest :ScriptableObject{
   [TextArea]
   public string Text;

   [SearchContext("t:QuestsZone ", SearchViewFlags.GridView,"prefab")]
   public QuestsZone QuestsZone;
}
