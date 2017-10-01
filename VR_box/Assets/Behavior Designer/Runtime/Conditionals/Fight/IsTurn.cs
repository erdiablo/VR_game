using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    [TaskDescription("Returns success when a collision starts. This task will only receive the physics callback if it is being reevaluated (with a conditional abort or under a parallel task).")]
    [TaskCategory("Fight")]
    [HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=110")]
    public class IsTurn : Conditional
    {
        public SharedGameObject target;

        public override TaskStatus OnUpdate()
        {
            var position = Target();
            Vector3 napr = transform.position - target.Value.transform.position;
            var distance = napr.magnitude;
            var direction = napr / distance;
            Debug.Log(direction);
            Debug.Log(transform.forward);

            if ((System.Math.Round(Mathf.Abs(direction.x), 1) == System.Math.Round(Mathf.Abs(transform.forward.x), 1)) &&
                (System.Math.Round(Mathf.Abs(direction.y), 1) == System.Math.Round(Mathf.Abs(transform.forward.y), 1)) &&
                (System.Math.Round(Mathf.Abs(direction.z), 1) == System.Math.Round(Mathf.Abs(transform.forward.z), 1)))
                    return TaskStatus.Failure;
            return TaskStatus.Success;


        }
        private Vector3 Target()
        {
            return target.Value.transform.position;
        }

    }
}