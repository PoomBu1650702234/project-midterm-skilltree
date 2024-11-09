using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;


public class SkillTreeManager : MonoBehaviour
{
    public static SkillTreeManager skillTreeManagerInstance;
    
    public GameObject textUIPref;
    public List<Skill> skillPref = new List<Skill>(); 
    public List<SkillTree> skillTreeList = new List<SkillTree>(); 

    public event Action OnSkillTreeChange;

    void Awake()
    {
        if (skillTreeManagerInstance != null)
        {
            skillTreeManagerInstance = this;
        }
        skillTreeManagerInstance = this; 
    }

    void Start()
    {
        //Create Moc up Skill Trees by drah and drop in inspector
       
        //add root skill to Skill Tree list
        //all child skill is add manualy in inspector
        skillTreeList.Add(new SkillTree(skillPref[0]));
        PrintSkillTreeHierarchy(skillTreeList[0].rootSkill,"");
        
        OnSkillTreeChange?.Invoke();
    }

    void Update()
    {
        
    }

    public Skill GetSkillInTree(Skill rootSkill , Skill desireSkill)
    {
        //print(rootSkill.SkillName);
        if (rootSkill == desireSkill)
        {
            return rootSkill;
        }

        // Loop through each child skill to search recursively
        for (int i = 0; i < rootSkill.childSkill.Count; i++)
        {
            Skill foundSkill = GetSkillInTree(rootSkill.childSkill[i], desireSkill);
            if (foundSkill != null) // if found in the child, return it
            {
                return foundSkill;
            }
        }

        // If the skill was not found in any path, return null
        return null;
    }

    public void Learn(Skill skill)
    {
        if(skill.isLearned == false && skill.isLock == false)
        {
            skill.isLearned = true;
            LockOtherSkillPath(skill);
        }
        OnSkillTreeChange?.Invoke();
    }

    public void UnLearn(Skill skill)
    {
        if(skill.isLearned == true && skill.isLock == false)
        {
            skill.isLearned = false;
            //Unlock same depth skill
            foreach (Skill _skill in skill.parentSkill.childSkill)
            {
                skill.isLearned = false;
                skill.isLock = false;
            }

        }
        OnSkillTreeChange?.Invoke();
    }

    public void ResetSkillTree(Skill _rootSkill)
    {
        _rootSkill.isLearned = false;
        _rootSkill.isLock = false;
        for (int i = 0; i < _rootSkill.childSkill.Count; i++)
        {
            ResetSkillTree(_rootSkill.childSkill[i]);
        }
        OnSkillTreeChange?.Invoke();
    }

    public void ResetAllSkillTree()
    {
        for(int i = 0; i < skillTreeList.Count ; i++)
        {
            ResetSkillTree(skillTreeList[i].rootSkill);
        }
        OnSkillTreeChange?.Invoke();
    }

    private void LockOtherSkillPath(Skill onlyAvalableSkill)
    {
        if(onlyAvalableSkill.parentSkill == null)
        {
            OnSkillTreeChange?.Invoke();
            return;
        }
        foreach (Skill skill in onlyAvalableSkill.parentSkill.childSkill)
        {
            if(skill != onlyAvalableSkill)
            {
                skill.isLock = true;
                skill.isLearned = false;
            }
        }
    }

    public void PrintSkillTreeNoIndent(Skill _rootSkill)
    {
        Debug.Log($"Skill: {_rootSkill.SkillName} isLearned: {_rootSkill.isLearned} isLocked: {_rootSkill.isLock}");
        for (int i = 0; i < _rootSkill.childSkill.Count; i++)
        {
            PrintSkillTreeNoIndent(_rootSkill.childSkill[i]);
        }
    }

    public void PrintSkillTreeHierarchy(Skill _rootSkill,string indent)
    {
        Debug.Log($"{indent} Skill: {_rootSkill.SkillName} isLearned: {_rootSkill.isLearned} isLocked: {_rootSkill.isLock}");
        for (int i = 0; i < _rootSkill.childSkill.Count; i++)
        {
            PrintSkillTreeHierarchy(_rootSkill.childSkill[i], indent + "------------");
        }
    }

    public List<Skill> GetTrackBackSkillToParentList(List<Skill> skillList)
    {
        Skill parentSkill = skillList[skillList.Count - 1].parentSkill;

        if (parentSkill == null)
        {
            return skillList;
        }

        skillList.Add(parentSkill);
        return GetTrackBackSkillToParentList(skillList);
    }

    public Skill GetRootSkill(Skill skill)
    {
        Skill parentSkill = skill.parentSkill;

        if (parentSkill == null)
        {
            return skill;
        }else
        {
            return GetRootSkill(parentSkill);
        }
    }

    public Skill GetNextActiveableSkill(Skill skill)
    {
        foreach (Skill _skill in skill.childSkill)
        {
            if(_skill.isLearned == true && _skill.isLock == false)
            {
                return _skill;
            }
        }
        return null;
    }

    public void UpdateSkillInfo()
    {
        OnSkillTreeChange?.Invoke();
    }
    

}
