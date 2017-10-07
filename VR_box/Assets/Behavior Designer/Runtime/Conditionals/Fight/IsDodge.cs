using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    [TaskDescription("Returns success when a collision starts. This task will only receive the physics callback if it is being reevaluated (with a conditional abort or under a parallel task).")]
    [TaskCategory("Fight")]

    public class IsDodge : Conditional
    {

        public SharedGameObject bone;
        public SharedGameObject target;
        public SharedFloat arriveDistance = 0.1f;

        public override TaskStatus OnUpdate()
        {
            var position = target.Value.transform.position;
            Vector3 napr = bone.Value.transform.position - target.Value.transform.position;
            var distance = napr.magnitude;

            if (distance < arriveDistance.Value)
                return TaskStatus.Success;
            return TaskStatus.Failure;


        }


    }
}