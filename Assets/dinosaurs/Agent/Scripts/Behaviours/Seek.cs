public class Seek : AgentBehaviour
{
    public override Steering GetSteering()
    {
        if (target != null)
        {
            Steering steering = new Steering();
            steering.linear = target.transform.position - transform.position;
            steering.linear.Normalize();
            steering.linear = steering.linear * agent.maxAccel;
            return steering;
        }
        return new Steering();
    }
}
