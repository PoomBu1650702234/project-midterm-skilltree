using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombble_Skill : MonoBehaviour
{
    //set form the class that spawn this obj
    public Skill spawnedSkillClass;
    public Vector3 direction;

    //--------------------
    public float speed = 20;
    public float lifetime = 0;

    public GameObject explosionPref;
    public bool isExplosionPref = false;
    public int bubbleCounts = 5;
    public int radius = 3;
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
        transform.right = direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Try to get the IDamageable interface from the collided object
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        
        // If the interface is implemented, apply damage
        if (damageable != null && spawnedSkillClass != null )
        {
            damageable.TakeDamage(spawnedSkillClass.skillDamage);
            if(spawnedSkillClass.stunnDuration > 0)
            {
                damageable.GetStunned(spawnedSkillClass.stunnDuration);
            }
            if(isExplosionPref == false && explosionPref != null)
            {
                for (int i = 0; i < bubbleCounts; i++)
                {
                    float angle = (2 * Mathf.PI * i) / bubbleCounts; // Evenly distribute the angle

                    float positionX = Mathf.Cos(angle) * radius; // Correct circular distribution
                    float positionY = Mathf.Sin(angle) * radius;
                    Vector3 skillPos = collision.transform.position + new Vector3(positionX, positionY, 0);

                    GameObject skill = Instantiate(explosionPref);
                    skill.transform.position = skillPos;

                    Bombble_Skill bombble_Skill = skill.GetComponent<Bombble_Skill>();
                    bombble_Skill.isExplosionPref = true;
                    bombble_Skill.spawnedSkillClass = spawnedSkillClass;
                    bombble_Skill.direction = (bombble_Skill.transform.position - collision.transform.position).normalized;
                }
                Destroy(gameObject); 
            }
            if(isExplosionPref == true)
            {
                Destroy(gameObject); 
            }
            
            
        }
    }
}
