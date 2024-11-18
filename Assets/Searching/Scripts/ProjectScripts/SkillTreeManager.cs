using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public event Action OnSkillSlotsChange;

    public MockUpPlayer player;

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
        foreach (Skill skill in skillPref)
        {
            SkillTree rootSkill = new SkillTree(skill);
            skillTreeList.Add(rootSkill);
        }
        OnSkillTreeChange?.Invoke();
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
            //Check if player have parent of this skill equip in Slots
            for (int i = 0; i < player.skillSlots.Length; i++)
            {
                if(player.skillSlots[i] == skill.parentSkill && skill.parentSkill != null)
                {
                    AdjustSkillSlotsOfPlayer(i,skill);
                }
            }
        }
        OnSkillTreeChange?.Invoke();
    }

    public void UnLearn(Skill skill)
    {
        if(skill.isLearned == true && skill.isLock == false)
        {
            //For root skill
            if(skill.parentSkill == null)
            {
                skill.isLearned = false;
                OnSkillTreeChange?.Invoke();
                //Remove Skill form slot if this unlearn skill is equip
                for (int i = 0; i < player.skillSlots.Length; i++)
                {
                    if(player.skillSlots[i] == skill)
                    {
                        player.skillSlots[i] = null;
                    }

                }
                OnSkillSlotsChange?.Invoke();
                return;
            }
            //Unlock same depth skill
            foreach (Skill _skill in skill.parentSkill.childSkill)
            {
                _skill.isLearned = false;
                _skill.isLock = false;
            }
            //Remove Skill form slot if this unlearn skill is equip
            for (int i = 0; i < player.skillSlots.Length; i++)
            {
                if(player.skillSlots[i] == skill)
                {
                    AdjustSkillSlotsOfPlayer(i, skill);
                }

            }
            OnSkillSlotsChange?.Invoke();
        }
        OnSkillTreeChange?.Invoke();
    }

    public void AdjustSkillSlotsOfPlayer(int slot,Skill skill)
    {
        if(skill.isChainReaction == true)
        {
            Skill rootSkill = GetRootSkill(skill);
            player.skillSlots[slot] = rootSkill;
        }
        else if(skill.isChainReaction == false)
        {
            Skill rootSkill = GetRootSkill(skill);
            Skill bestSkill = GetBestVersionOfSkill(rootSkill);
            player.skillSlots[slot] = bestSkill;
        }
        OnSkillSlotsChange?.Invoke();
    }

    public void EquipSkill(Skill skill , int slot)
    {
        if(skill.isChainReaction == false)
        {
           Skill rootSkill = GetRootSkill(skill);
           Skill bestVersion = GetBestVersionOfSkill(rootSkill);
           player.skillSlots[slot] = bestVersion;
        }
        else if(skill.isChainReaction == true)
        {
            Skill rootSkill = GetRootSkill(skill);
            player.skillSlots[slot] = rootSkill;
        }
        OnSkillSlotsChange?.Invoke();
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
        for(int i = 0 ; i < player.skillSlots.Length ; i++)
        {
            player.skillSlots[i] = null;
        }
        for(int i = 0; i < skillTreeList.Count ; i++)
        {
            ResetSkillTree(skillTreeList[i].rootSkill);
        }
        OnSkillTreeChange?.Invoke();
        OnSkillSlotsChange?.Invoke();
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

    
    public void SkillHandle(Skill skill,int slot)
    {
        if(skill.isChainReaction == false)
        {
           skill.Activate();
        }
        else if(skill.isChainReaction == true)
        {
            skill.Activate();
            Skill nextSkill = GetNextActiveableSkill(skill);
            if(nextSkill != null)
            {
                player.skillSlots[slot] = nextSkill;
            }
            else if(nextSkill == null)
            {
                player.skillSlots[slot] = GetRootSkill(skill);
            }
            
        }
        OnSkillSlotsChange?.Invoke();
    }

    public Skill GetBestVersionOfSkill(Skill skill)
    {
        Skill bestVersion = null;
        Skill previousSkill = null;
        Skill currentSkill = skill;

        while (bestVersion == null)
        {
            previousSkill = currentSkill;
            currentSkill = GetNextActiveableSkill(currentSkill);
            if(currentSkill == null)
            {
                bestVersion = previousSkill;
                return bestVersion;
            }
        }
        return null;
    }

    public void UpdateSkillInfo()
    {
        OnSkillTreeChange?.Invoke();
        OnSkillSlotsChange?.Invoke();
    }
    

}
