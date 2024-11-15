using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_FireBall : MonoBehaviour
{
    private Rigidbody2D rd; 
    // Start is called before the first frame update
    void Start()
    {
        rd = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
