using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSlash : Skill
{
    [SerializeField] GameObject skillPref;
    public override void Activate()
    {
        if(skillPref == null)
        {
            return;
        }

        Vector3 mouseDir = (MockUpPlayer.instance.GetMousePosition() - MockUpPlayer.instance.transform.position).normalized;
        Shoot(mouseDir);
        Shoot(MockUpPlayer.instance.RotateVector(mouseDir, 30));
        Shoot(MockUpPlayer.instance.RotateVector(mouseDir, -30));

    }

    private void Shoot(Vector3 _direction)
    {
        GameObject skill = Instantiate(skillPref);
        skill.transform.position = MockUpPlayer.instance.transform.position;

        ShadowSlash_Skill shadowSlash_Skill = skill.GetComponent<ShadowSlash_Skill>();
        shadowSlash_Skill.spawnedSkillClass = this;
        shadowSlash_Skill.direction = _direction;
        
    }
}
