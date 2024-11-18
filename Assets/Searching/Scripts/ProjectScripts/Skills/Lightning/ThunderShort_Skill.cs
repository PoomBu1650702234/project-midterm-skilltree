using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderShort_Skill : MonoBehaviour
{
    //set form the class that spawn this obj
    public Skill spawnedSkillClass;
    public Vector3 direction;

    //--------------------
    public float speed = 0;
    public float lifetime = 0;

    private bool haveTarget = false;
    private bool alreadyDoDamage = false;
    private IDamageable target = null;
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

        if(haveTarget == true && alreadyDoDamage == false)
        {
            if(target != null)
            {
                for(int i = 0; i < 5 ; i++)
                {
                    StartCoroutine(DamageCouroutine(i));
                }
                alreadyDoDamage = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(haveTarget == true)
        {
            return;
        }
        // Try to get the IDamageable interface from the collided object
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        
        // If the interface is implemented, apply damage
        if (damageable != null && spawnedSkillClass != null)
        {
            gameObject.transform.position = collision.gameObject.transform.position;
            target = damageable;
            haveTarget = true;
        }
    }

    private IEnumerator DamageCouroutine(float second)
    {
        yield return new WaitForSeconds(second);
        target.TakeDamage(spawnedSkillClass.skillDamage);
    }
}
