using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MockUpPlayer : MonoBehaviour
{
    SkillTreeManager skillTreeManager;
    public static MockUpPlayer instance;
    public Skill[] skillSlots = new Skill[2];

    float hInput;
    float vInput;
    Vector3 InputAxis;
    [SerializeField]private float moveSpeed;
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
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");
        InputAxis = new Vector3(hInput,vInput,0);
        InputAxis.Normalize();
        Move();
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

    private void Move()
    {
        transform.position += InputAxis * moveSpeed * Time.deltaTime;
    }

    public Vector3 GetMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Mathf.Abs(Camera.main.transform.position.z); // Set depth to the camera's distance
        
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        return mouseWorldPosition;
    }

    public Vector3 RotateVector(Vector3 direction, float angle)
    {
        float radians = angle * Mathf.Deg2Rad;  // Convert angle to radians
        float cosAngle = Mathf.Cos(radians);
        float sinAngle = Mathf.Sin(radians);

        // Apply the rotation matrix for 2D space
        float x = direction.x * cosAngle - direction.y * sinAngle;
        float y = direction.x * sinAngle + direction.y * cosAngle;

        return new Vector3(x, y, 0);  // Return the rotated vector
    }
}
