using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lullaby : Skill
{
    [SerializeField] GameObject skillPref;
    public override void Activate()
    {
        if(skillPref == null)
        {
            return;
        }

        /*GameObject skill = Instantiate(skillPref);
        skill.transform.position = MockUpPlayer.instance.transform.position;

        FireBall_Skill fireBall_Skill = skill.GetComponent<FireBall_Skill>();
        fireBall_Skill.spawnedSkillClass = this;

        // Calculate the direction from the player to the mouse position
        Vector3 direction = (MockUpPlayer.instance.GetMousePosition() - MockUpPlayer.instance.transform.position).normalized;
        fireBall_Skill.direction = direction;*/

    }
}
