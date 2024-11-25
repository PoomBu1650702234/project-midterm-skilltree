using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popble : Skill
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

        Popble_Skill popble_Skill = skill.GetComponent<Popble_Skill>();
        popble_Skill.spawnedSkillClass = this;

        popble_Skill.direction = Vector3.zero;

    }
}
