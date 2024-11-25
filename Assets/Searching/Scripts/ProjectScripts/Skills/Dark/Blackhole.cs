using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackhole : Skill
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

        Blackhole_Skill Blackhole_Skill = skill.GetComponent<Blackhole_Skill>();
        Blackhole_Skill.spawnedSkillClass = this;
    }
}
