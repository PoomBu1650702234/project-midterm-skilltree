using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Lightning : Skill
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

        Lightning_Skill lightning_Skill = skill.GetComponent<Lightning_Skill>();
        lightning_Skill.spawnedSkillClass = this;

        // Calculate the direction from the player to the mouse position
        Vector3 direction = (MockUpPlayer.instance.GetMousePosition() - MockUpPlayer.instance.transform.position).normalized;
        lightning_Skill.direction = direction;

    }

}
