using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour,IDamageable
{
    [SerializeField]private int hp;
    [SerializeField]private bool isStun = false;
    [SerializeField]private float stunnedDuration = 0;
    [SerializeField]private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(text != null)
        {
            text.text = $"HP : {hp}\n Stunned : {isStun}\n CC Duration {stunnedDuration}";
        }
        if(stunnedDuration <= 0)
        {
            stunnedDuration = 0;
            isStun = false;
        }
        if(stunnedDuration > 0)
        {
            isStun = true;
            stunnedDuration -= Time.deltaTime;
        }
    }

    public void TakeDamage(int damage)
    {
        print($"enemy take damage {damage}");
        
        hp -= damage;
        if(hp <= 0)
        {
            hp = 100;
        }
    }

    public void GetStunned(float duration)
    {
        print($"enemy get stun for {duration} sec");
        stunnedDuration += duration;
    }
}
