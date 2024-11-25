using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Realm : Skill
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

        Realm_Skill realm_Skill = skill.GetComponent<Realm_Skill>();
        realm_Skill.spawnedSkillClass = this;
    }
}
