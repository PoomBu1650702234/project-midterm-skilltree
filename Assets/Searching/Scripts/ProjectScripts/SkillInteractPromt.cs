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
