using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;


public class SkillTreeManager : MonoBehaviour
{
    public static SkillTreeManager skillTreeManagerInstance;
    
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

    public Skill GetSkillInTree(Skill rootSkill , string desireSkillName)
    {
        //print(rootSkill.SkillName);
        if (rootSkill.SkillName == desireSkillName)
        {
            return rootSkill;
        }

        // Loop through each child skill to search recursively
        for (int i = 0; i < rootSkill.childSkill.Count; i++)
        {
            Skill foundSkill = GetSkillInTree(rootSkill.childSkill[i], desireSkillName);
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
        
    }

    public void UnLearn(Skill skill)
    {

    }

    public void ResetSkillTree(SkillTree skillTree)
    {

    }

    private void LockOtherSkillPath(Skill onlyAvalableSkill)
    {

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

}
