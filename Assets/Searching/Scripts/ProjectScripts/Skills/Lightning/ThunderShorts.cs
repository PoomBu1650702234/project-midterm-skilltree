using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderShorts : Skill
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

        ThunderShort_Skill thunderShort_Skill = skill.GetComponent<ThunderShort_Skill>();
        thunderShort_Skill.spawnedSkillClass = this;

    }
}
