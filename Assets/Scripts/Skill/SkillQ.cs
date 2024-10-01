using UnityEngine;

public class SkillQ : SkillMove
{
    protected override void Update()
    {
        base.Update();
        if(moveDistance>12)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null && collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
