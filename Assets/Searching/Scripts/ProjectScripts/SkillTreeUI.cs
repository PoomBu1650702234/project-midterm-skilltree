    using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeUI : MonoBehaviour
{
    SkillTreeManager skillTreeManagerInstance;
    
    [SerializeField] private Skill skill;
    void Start()
    {
        skillTreeManagerInstance = SkillTreeManager.skillTreeManagerInstance;

        // Assuming OnSkillTreeChange is an event in SkillTreeManager, subscribe to it.
        skillTreeManagerInstance.OnSkillTreeChange += UiUpdate;
        
    }

    void UiUpdate()
    {
        print("Gott update signal");
        if(skill == null)
        {
            return;
        }

        ClearUI();
        GameObject textObj = Instantiate(skillTreeManagerInstance.textUIPref);
        textObj.transform.SetParent(transform , false);
        TextMeshProUGUI textUI = textObj.GetComponent<TextMeshProUGUI>();
        textUI.text = $"Skill: {skill.SkillName}\n Cost: {skill.skillPointCost} \n Lock = {skill.isLock} \n Learned = {skill.isLearned} \n Parent = {skill.parentSkill?.SkillName}";
        
    }

    void ClearUI()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    
}
