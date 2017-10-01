using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Fight
{
    [TaskDescription("Fight.")]
    [TaskCategory("Fight")]
    [HelpURL("http://www.opsive.com/assets/BehaviorDesigner/Movement/documentation.php?id=1")]


    public class  Turn: Action
    {
    [Tooltip("The speed of the agent")]
    public SharedFloat speed;
    [Tooltip("The agent has arrived when the magnitude is less than this value")]
    public SharedFloat arriveDistance = 0.1f;
    [Tooltip("Should the agent be looking at the target position?")]
    public SharedBool lookAtTarget = true;
    [Tooltip("Max rotation delta if lookAtTarget is enabled")]
    public SharedFloat maxLookAtRotationDelta;
    [Tooltip("The GameObject that the agent is moving towards")]
    public SharedGameObject target;
    [Tooltip("If target is null then use the target position")]
    public SharedVector3 targetPosition;
    Quaternion a, b;
    Vector3 a1, b1;
    private Animator anim;

    public override TaskStatus OnUpdate()
    {
        anim = GetComponent<Animator>();
        var position = Target();

        a = transform.rotation;
        if (a==b)
        {
            return TaskStatus.Success;
        }
        b = a;
        if (lookAtTarget.Value)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(position - transform.position), maxLookAtRotationDelta.Value);
        }
        return TaskStatus.Running;
    }

    private Vector3 Target()
    {
        if (target == null || target.Value == null)
        {
            return targetPosition.Value;
        }
        return target.Value.transform.position;
    }

    // Reset the public variables
    public override void OnReset()
    {
        arriveDistance = 0.1f;
        lookAtTarget = true;
    }
    }
}