using UnityEngine;

public class Seek : AgentBehaviour
{
    //public GameObject food, water;

    public override Steering GetSteering()
    {
        Steering steering = new Steering();
        /*if (gameObject.GetComponent<MyAnky>().animator.GetBool("isEating"))
        {
            target = food;
        }
        else if (gameObject.GetComponent<MyAnky>().animator.GetBool("isDrinking"))
        {
            target = water;
        }*/
        steering.linear = target.transform.position - transform.position;
        steering.linear.Normalize();
        steering.linear = steering.linear * agent.maxAccel;
        return steering;
    }
}
