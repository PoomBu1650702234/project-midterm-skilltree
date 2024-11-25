using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Lullaby : Skill
{
    public static Lullaby instance;
    [SerializeField] GameObject skillPref;
    [SerializeField] int skillCounts = 10;
    [SerializeField] float radius = 3f;

    void Awake()
    {
        if(instance != null)
        {
            instance = this;
        }
        instance = this;
        
    } 

    public override void Activate()
    {
        if(skillPref == null)
        {
            return;
        }

        for (int i = 0; i < skillCounts; i++)
        {
            float angle = (2 * Mathf.PI * i) / skillCounts; // Evenly distribute the angle

            float positionX = Mathf.Cos(angle) * radius; // Correct circular distribution
            float positionY = Mathf.Sin(angle) * radius;
            Vector3 skillPos = MockUpPlayer.instance.transform.position + new Vector3(positionX, positionY, 0);

            GameObject skill = Instantiate(skillPref);
            skill.transform.position = skillPos;

            Lullaby_Skill lullaby_Skill = skill.GetComponent<Lullaby_Skill>();
            lullaby_Skill.spawnedSkillClass = this;
            lullaby_Skill.direction = (skill.transform.position - MockUpPlayer.instance.transform.position).normalized;
        }

    }

    public void OutSourceActivate(Vector3 sourcePosition)
    {
        if(skillPref == null)
        {
            return;
        }

        for (int i = 0; i < skillCounts; i++)
        {
            float angle = (2 * Mathf.PI * i) / skillCounts; // Evenly distribute the angle

            float positionX = Mathf.Cos(angle) * radius; // Correct circular distribution
            float positionY = Mathf.Sin(angle) * radius;
            Vector3 skillPos = sourcePosition + new Vector3(positionX, positionY, 0);

            GameObject skill = Instantiate(skillPref);
            skill.transform.position = skillPos;

            Lullaby_Skill lullaby_Skill = skill.GetComponent<Lullaby_Skill>();
            lullaby_Skill.spawnedSkillClass = this;
            lullaby_Skill.direction = (skill.transform.position - MockUpPlayer.instance.transform.position).normalized;
        }

    }
}
