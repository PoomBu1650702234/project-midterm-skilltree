using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillInteractPromt : MonoBehaviour
{
    public static SkillInteractPromt instance;
    public Skill skill;

    public TextMeshProUGUI promptText;
    public Button learnButton;
    public Button unLearnButton;
    public Button closeButton;

    public Button equipSLot1;
    public Button equipSLot2;

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
        // Adding listeners to buttons
        learnButton.onClick.AddListener(OnLearnButtonClick);
        unLearnButton.onClick.AddListener(OnUnlearnButtonClick);
        closeButton.onClick.AddListener(OnCloseButtonClick);
        equipSLot1.onClick.AddListener(OnEquipButton1Click);
        equipSLot2.onClick.AddListener(OnEquipButton2Click);

        Close();
    }

    private void OnEquipButton1Click()
    {
        if(skill.isLock == true || skill.isLearned == false)
        {
            return;
        }
        for (int i = 0; i < MockUpPlayer.instance.skillSlots.Length; i++)
        {
            if(skill == MockUpPlayer.instance.skillSlots[i])
            {
                //can't equip same skill
                return;
            }
        }
        SkillTreeManager.skillTreeManagerInstance.EquipSkill(skill,0);
        Close();

    }

    private void OnEquipButton2Click()
    {
        if(skill.isLock == true || skill.isLearned == false)
        {
            return;
        }
        for (int i = 0; i < MockUpPlayer.instance.skillSlots.Length; i++)
        {
            if(skill == MockUpPlayer.instance.skillSlots[i])
            {
                //can't equip same skill
                return;
            }
        }
        SkillTreeManager.skillTreeManagerInstance.EquipSkill(skill,1);
        Close();

    }
    // Methods to be called on button click
    private void OnLearnButtonClick()
    {
        if(skill.isLearned == true)
        {
            return;
        }
        SkillTreeManager.skillTreeManagerInstance.Learn(skill);
        Close();
    }

    private void OnUnlearnButtonClick()
    {
        if(skill.isLearned == false)
        {
            return;
        }
        Skill nextActiveAbleSkill = SkillTreeManager.skillTreeManagerInstance.GetNextActiveableSkill(skill);
        if(nextActiveAbleSkill != null)
        {
            //You have to UnLearn that skill before this skill
            return;
        }
        SkillTreeManager.skillTreeManagerInstance.UnLearn(skill);
        Close();
    }

    private void OnCloseButtonClick()
    {
        Close();
    }

    public void Open(Skill _skill)
    {
        gameObject.SetActive(true);
        this.skill = _skill;
        promptText.text = $"What do you want to do with {skill.SkillName} ?";
    }

    public void Close()
    {
        this.skill = null;
        gameObject.SetActive(false);
    }
}
