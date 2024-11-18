using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallSpread : Skill
{
    [SerializeField] GameObject skillPref;
    public override void Activate()
    {
        if(skillPref == null)
        {
            return;
        }
        Vector3 mouseDir = (MockUpPlayer.instance.GetMousePosition() - MockUpPlayer.instance.transform.position).normalized;
        Shoot(mouseDir);
        Shoot(RotateVector(mouseDir, 30));
        Shoot(RotateVector(mouseDir, -30));

    }

    private void Shoot(Vector3 _direction)
    {
        GameObject skill = Instantiate(skillPref);
        skill.transform.position = MockUpPlayer.instance.transform.position;

        FireBall_Skill fireBall_Skill = skill.GetComponent<FireBall_Skill>();
        fireBall_Skill.spawnedSkillClass = this;
        fireBall_Skill.direction = _direction;
    }

    Vector3 RotateVector(Vector3 direction, float angle)
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
