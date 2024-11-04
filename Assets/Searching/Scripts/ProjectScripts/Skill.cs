using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour 
{
    public string SkillName = null;
    public Skill parentSkill = null;
    public List<Skill> childSkill = null; 

    public bool isLearned = false;
    public bool isChainReaction = false;
    public bool isLock = false; // for disable other tree
    public int skillPointCost = 0;

    public virtual void Activate()
    {
        Debug.Log(SkillName);
    }    

}
