using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    public DynamicJoystick joystick;

    private Player player;
    private float angle = 5;
    private Animator anim;

    void Start()
    {
        player = gameObject.GetComponent<Player>();
        anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (joystick.Direction.sqrMagnitude > 0.01f && !player.IsTalking)
        {
            Vector2 direction = joystick.Direction;
            angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

            if (angle > -45f && angle <= 45f)
            {
                anim.SetInteger("WalkDir", 3);
            }
            else if (angle > 45f && angle <= 135f)
            {
                anim.SetInteger("WalkDir", 2);
            }
            else if (angle > 135f || angle <= -135f)
            {
                anim.SetInteger("WalkDir", 1);
            }
            else if (angle > -135f && angle <= -45f)
            {
                anim.SetInteger("WalkDir", 4);
            }
        }
        else
        {
            anim.SetInteger("WalkDir", 0);
        }
    }
}
