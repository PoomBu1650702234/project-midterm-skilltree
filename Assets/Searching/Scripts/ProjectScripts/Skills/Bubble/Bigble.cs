using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bigble : Skill
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

        Bigble_Skill bigble_Skill = skill.GetComponent<Bigble_Skill>();
        bigble_Skill.spawnedSkillClass = this;

        // Calculate the direction from the player to the mouse position
        Vector3 direction = (MockUpPlayer.instance.GetMousePosition() - MockUpPlayer.instance.transform.position).normalized;
        bigble_Skill.direction = direction;

    }
}
