using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyConditions/Is EVA in angle?")]
[Help("Checks whether EVA can shoot to the other player.")]
public class IsEVAAngle : ConditionBase
{
    public override bool Check()
    {
        GameObject eva_player = GameObject.Find("EVA Player");
        GameObject angel_player = GameObject.Find("Angel Player");

        if (eva_player != null && angel_player != null)
        {
            return Vector3.Distance(eva_player.transform.position, angel_player.transform.position) < 35f;
        }
        else
        {
            return false;
        }
    }
}
