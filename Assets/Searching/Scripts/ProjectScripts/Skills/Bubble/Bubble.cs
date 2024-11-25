using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : Skill
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

        Bubble_Skill bubble_Skill = skill.GetComponent<Bubble_Skill>();
        bubble_Skill.spawnedSkillClass = this;

        // Calculate the direction from the player to the mouse position
        Vector3 direction = (MockUpPlayer.instance.GetMousePosition() - MockUpPlayer.instance.transform.position).normalized;
        bubble_Skill.direction = direction;

    }
}
