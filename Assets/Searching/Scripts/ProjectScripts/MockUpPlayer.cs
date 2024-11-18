using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MockUpPlayer : MonoBehaviour
{
    SkillTreeManager skillTreeManager;
    public static MockUpPlayer instance;
    public Skill[] skillSlots = new Skill[2];

    float hInput;
    float vInput;
    Vector3 InputAxis;
    [SerializeField]private float moveSpeed;
    void Awake()
    {
        if(instance != null)
        {
            instance = this;
        }
        instance = this;
    }
    void Start()
    {
        skillTreeManager = SkillTreeManager.skillTreeManagerInstance;
    }
    void Update()
    {
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");
        InputAxis = new Vector3(hInput,vInput,0);
        InputAxis.Normalize();
        Move();
        if(Input.GetKeyDown("1"))
        {
            if(skillSlots[0] != null)
            {
                skillTreeManager.SkillHandle(skillSlots[0],0);
            }
        }
        if(Input.GetKeyDown("2"))
        {
            
            if(skillSlots[1] != null)
            {
                skillTreeManager.SkillHandle(skillSlots[1],1);
            }
        }
    }

    private void Move()
    {
        transform.position += InputAxis * moveSpeed * Time.deltaTime;
    }
}
