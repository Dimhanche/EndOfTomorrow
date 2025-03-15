using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "NodeCompetencePoint", menuName = "Leveling/NodeCompetencePoint")]
public class SONodeCompetencePoint : ScriptableObject
{
    public SONodeCompetencePoint soNodeRequired;
    public SONodeCompetencePoint[] nextNodes;
    public bool isUnlocked;
    public int competencePointCost;
}
