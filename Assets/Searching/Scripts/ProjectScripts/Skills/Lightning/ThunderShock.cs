using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderShock : Skill
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

        ThunderShock_Skill thunderShock_Skill = skill.GetComponent<ThunderShock_Skill>();
        thunderShock_Skill.spawnedSkillClass = this;

    }
}
