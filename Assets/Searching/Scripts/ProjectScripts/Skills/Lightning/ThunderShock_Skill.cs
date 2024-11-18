using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderShock_Skill : MonoBehaviour
{
    //set form the class that spawn this obj
    public Skill spawnedSkillClass;
    public Vector3 direction;

    //--------------------
    public float speed = 0;
    public float lifetime = 0;

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

        transform.position += direction * Time.deltaTime * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Try to get the IDamageable interface from the collided object
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        
        // If the interface is implemented, apply damage
        if (damageable != null && spawnedSkillClass != null)
        {
            damageable.TakeDamage(spawnedSkillClass.skillDamage);
            if(spawnedSkillClass.stunnDuration > 0)
            {
                damageable.GetStunned(spawnedSkillClass.stunnDuration);
            }           
        }
    }
}
