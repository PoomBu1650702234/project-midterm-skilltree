using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombble : Skill
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

        Bombble_Skill bombble_Skill = skill.GetComponent<Bombble_Skill>();
        bombble_Skill.spawnedSkillClass = this;

        // Calculate the direction from the player to the mouse position
        Vector3 direction = (MockUpPlayer.instance.GetMousePosition() - MockUpPlayer.instance.transform.position).normalized;
        bombble_Skill.direction = direction;
        bombble_Skill.isExplosionPref = false;

    }
}
