using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : Skill
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

        Flash_Skill flash_Skill = skill.GetComponent<Flash_Skill>();
        flash_Skill.spawnedSkillClass = this;

    }
}
