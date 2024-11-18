using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallBig : Skill
{
    [SerializeField] GameObject skillPref;
    public override void Activate()
    {
        if(skillPref == null)
        {
            return;
        }

        GameObject skill = Instantiate(skillPref);
        skill.transform.position = MockUpPlayer.instance.transform.position;

        FireBallBig_Skill fireBallBig_Skill = skill.GetComponent<FireBallBig_Skill>();
        fireBallBig_Skill.spawnedSkillClass = this;

        // Calculate the direction from the player to the mouse position
        Vector3 direction = (MockUpPlayer.instance.GetMousePosition() - MockUpPlayer.instance.transform.position).normalized;
        fireBallBig_Skill.direction = direction;

    }
}
