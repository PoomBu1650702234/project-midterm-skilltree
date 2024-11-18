using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderShoot : Skill
{
    [SerializeField] GameObject skillPref;
    public override void Activate()
    {
        if(skillPref == null)
        {
            return;
        }

        for(int i = 0; i < 4 ; i++)
        {
            StartCoroutine(DamageCouroutine(i));
        }
    }

    private IEnumerator DamageCouroutine(float second)
    {
        yield return new WaitForSeconds(second);
        SpawnLightning();
        
    }

    private void SpawnLightning()
    {
        GameObject skill = Instantiate(skillPref);
        skill.transform.position = MockUpPlayer.instance.transform.position + new Vector3(Random.Range(5, -5), Random.Range(5, -5),0);
        skill.transform.rotation = Quaternion.Euler(
            0, 
            0, 
            Random.Range(-360f, 360f)
        );

        ThunderShoot_Skill thunderShoot_Skill = skill.GetComponent<ThunderShoot_Skill>();
        thunderShoot_Skill.spawnedSkillClass = this;
    }
}
