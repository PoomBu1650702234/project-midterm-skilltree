using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MockUpPlayer : MonoBehaviour
{
    SkillTreeManager skillTreeManager;
    public static MockUpPlayer instance;
    public Skill[] skillSlots = new Skill[2];

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
}
