using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porcupineble_Skill : MonoBehaviour
{
    public List<IDamageable> damageablesList;

    //set form the class that spawn this obj
    public Skill spawnedSkillClass;
    public Vector3 direction;

    //--------------------
    public float speed = 20;
    public float lifetime = 0;

    private float hitCd = 0.5f;

    void Start()
    {
        damageablesList = new List<IDamageable>();
    }

    private void Update()
    {
        hitCd -= Time.deltaTime;
        lifetime += Time.deltaTime;
        if(hitCd <= 0f)
        {
            for(int i = 0; i < damageablesList.Count ; i++)
            {
                DoDamage(damageablesList[i]);
            }
            hitCd = 0.5f;
        }

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
        
        if(damageable != null)
        {
            damageablesList.Add(damageable);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Try to get the IDamageable interface from the collided object
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        
        if(damageable != null)
        {
            if(damageablesList.Contains(damageable))
            {
                damageablesList.Remove(damageable);
            }
        }
    }

    public void DoDamage(IDamageable damageable)
    {
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
