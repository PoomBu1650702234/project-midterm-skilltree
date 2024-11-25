using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Realm_Skill : MonoBehaviour
{
    //Just trigger Lullaby Skill
   
    //set form the class that spawn this obj
    public Skill spawnedSkillClass;
    public Vector3 direction;

    //--------------------
    public float speed = 20;
    public float lifetime = 0;

    private int spawnCount = 0;
    private int MaxSpawnCount = 10;
    
    private float delaySpawn = 0.5f;
    private float currentDelaySpawn = 0f;

    private void Update()
    {
        currentDelaySpawn += Time.deltaTime;
        lifetime += Time.deltaTime;
        if(spawnedSkillClass != null)
        {
            if(spawnedSkillClass.lifeTime <= lifetime)
            {
                Destroy(gameObject);
            }
            
        }

        if(spawnCount >= MaxSpawnCount)
        {
            Destroy(gameObject);
        }

        if(currentDelaySpawn >= delaySpawn)
        {
            currentDelaySpawn = 0f;
            spawnCount += 1;
            Lullaby.instance.OutSourceActivate(transform.position);
        }     
    }

}
