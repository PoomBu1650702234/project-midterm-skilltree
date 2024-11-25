using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;

public class Skill : MonoBehaviour 
{
    public string SkillName = null;
    public Sprite skillIcon;
    public Skill parentSkill = null;
    public List<Skill> childSkill = null; 

    public bool isLearned = false;
    public bool isChainReaction = false;
    public bool isLock = false; // for disable other tree

    public int skillDamage = 0;
    public float stunnDuration = 0f;
    public float lifeTime = 10f;
    
    void Start()
    {
        if(parentSkill != null)
        {
            isChainReaction = parentSkill.isChainReaction;
        }
    }
    public virtual void Activate()
    {
        Debug.Log(SkillName);
    }    

}
