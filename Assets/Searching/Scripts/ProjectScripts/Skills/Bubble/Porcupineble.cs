using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Porcupineble : Skill
{
    [SerializeField] GameObject skillPref;

    [SerializeField]private float radius = 5f;
    [SerializeField]private int bubbleCounts = 10;
    public override void Activate()
    {
        if(skillPref == null)
        {
            return;
        }

        for (int i = 0; i < bubbleCounts; i++)
        {
            float angle = (2 * Mathf.PI * i) / bubbleCounts; // Evenly distribute the angle

            float positionX = Mathf.Cos(angle) * radius; // Correct circular distribution
            float positionY = Mathf.Sin(angle) * radius;
            Vector3 skillPos = MockUpPlayer.instance.transform.position + new Vector3(positionX, positionY, 0);

            GameObject skill = Instantiate(skillPref);
            skill.transform.position = skillPos;

            Porcupineble_Skill porcupineble_Skill = skill.GetComponent<Porcupineble_Skill>();
            porcupineble_Skill.spawnedSkillClass = this;
        }
    }
}
