using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lullaby_Skill : MonoBehaviour
{
    //set form the class that spawn this obj
    public Skill spawnedSkillClass;
    public Vector3 direction;

    //--------------------
    public float speed = 20;
    public float lifetime = 0;

    public IDamageable foundDamageAble = null;


    private void Update()
    {
        lifetime += Time.deltaTime;
        if(spawnedSkillClass != null)
        {
            if(spawnedSkillClass.lifeTime <= lifetime)
            {
                Destroy(gameObject);
            }
            
        }
        if(foundDamageAble == null)
        {
            transform.position += direction * Time.deltaTime * speed;
        }

        if (foundDamageAble != null && spawnedSkillClass != null)
        {
            Enemy enemy = foundDamageAble as Enemy;
            transform.position += (enemy.transform.position - transform.position).normalized * speed * Time.deltaTime;
            
            if(Vector3.Distance(transform.position, enemy.transform.position) < 0.5f)
            {
                foundDamageAble.TakeDamage(spawnedSkillClass.skillDamage);
                if(spawnedSkillClass.stunnDuration > 0)
                {
                    foundDamageAble.GetStunned(spawnedSkillClass.stunnDuration);
                }
                Destroy(gameObject);
            }
            
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Try to get the IDamageable interface from the collided object
        
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if(damageable != null && foundDamageAble == null)
        {
            foundDamageAble = damageable;
        }

    }
}
