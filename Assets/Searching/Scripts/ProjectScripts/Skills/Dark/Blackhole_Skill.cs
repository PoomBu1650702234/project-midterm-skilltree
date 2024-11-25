using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Blackhole_Skill : MonoBehaviour
{
    public List<IDamageable> damageablesList;

    //set form the class that spawn this obj
    public Skill spawnedSkillClass;
    public Vector3 direction;

    //--------------------
    public float speed = 20;
    public float lifetime = 0;

    private float hitCd = 0.2f;
    private float pullStr = 20f;

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
                Enemy enemy = damageablesList[i] as Enemy;
                if(enemy != null)
                {
                    Rigidbody2D enemyRigidbody = enemy.GetComponent<Rigidbody2D>();
                    enemyRigidbody.AddForce((transform.position - enemy.transform.position).normalized * pullStr);
                }
            }
            hitCd = 0.2f;
        }

        if(spawnedSkillClass != null)
        {
            if(spawnedSkillClass.lifeTime <= lifetime)
            {
                Destroy(gameObject);
            }
            
        }

        
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

    private void OnDestroy()
    {
        for(int i = 0; i < damageablesList.Count ; i++)
        {
            DoDamage(damageablesList[i]);
            Enemy enemy = damageablesList[i] as Enemy;
            if(enemy != null)
            {
                Rigidbody2D enemyRigidbody = enemy.GetComponent<Rigidbody2D>();
                enemyRigidbody.velocity = Vector3.zero;
            }
        }
    }
}
