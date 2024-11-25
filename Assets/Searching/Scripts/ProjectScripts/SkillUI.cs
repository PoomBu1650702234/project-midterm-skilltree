using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    SkillTreeManager skillTreeManagerInstance;
    
    [SerializeField] private Skill skill;
    void Start()
    {
        skillTreeManagerInstance = SkillTreeManager.skillTreeManagerInstance;

        // Assuming OnSkillTreeChange is an event in SkillTreeManager, subscribe to it.
        skillTreeManagerInstance.OnSkillTreeChange += UiUpdate;
        Button button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
        
    }

    void UiUpdate()
    {
        //print("Gott update signal");
        if(skill == null)
        {
            return;
        }

        ClearUI();
        GameObject textObj = Instantiate(skillTreeManagerInstance.textUIPref);
        textObj.transform.SetParent(transform , false);
        TextMeshProUGUI textUI = textObj.GetComponent<TextMeshProUGUI>();
        textUI.text = $"Skill: {skill.SkillName} \n Lock = {skill.isLock} \n Learned = {skill.isLearned} \n Parent = {skill.parentSkill?.SkillName}";

        GameObject imageObj = Instantiate(skillTreeManagerInstance.imageUIPref);
        imageObj.transform.SetParent(transform , false);
        Image skillimage = imageObj.GetComponent<Image>();

        if(skill.skillIcon != null)
        {
            skillimage.sprite = skill.skillIcon;
        }
        
        if(skill.isLearned == true)
        {
            Image image = gameObject.GetComponent<Image>();
            image.color = Color.green;
        }
        else if(skill.isLearned == false && skill.isLock == false)
        {
            Image image = gameObject.GetComponent<Image>();
            image.color = Color.grey;
        }
        else
        {
            Image image = gameObject.GetComponent<Image>();
            image.color = Color.red;
        }
    }

    void ClearUI()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    void OnButtonClick()
    {
        if(skill.isLock == true)
        {
            return;
        }

        if(skill.parentSkill != null && skill.parentSkill.isLearned == true && skill.parentSkill.isLock == false)
        {
            SkillInteractPromt.instance.Open(skill);
        }
        else if(skill.parentSkill == null && skill.isLearned == false)
        {
            SkillInteractPromt.instance.Open(skill);
        }
        else if(skill.isLearned == true)
        {
            SkillInteractPromt.instance.Open(skill);
        }
        
    }

    
}
