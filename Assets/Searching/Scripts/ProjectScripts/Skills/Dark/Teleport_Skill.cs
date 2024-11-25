using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport_Skill : MonoBehaviour
{
    private float lifeTime = 1;

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
