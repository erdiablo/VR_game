using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    [TaskDescription("Returns success when a collision starts. This task will only receive the physics callback if it is being reevaluated (with a conditional abort or under a parallel task).")]
    [TaskCategory("Fight")]
    [HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=110")]
    public class IsTurn : Conditional
    {
        public SharedGameObject target;
        private Vector3 a;
        public override TaskStatus OnUpdate()
        {
            a = new Vector3(0, 1, 0);
            var position = target.Value.transform.position;
            Vector3 proec_target = Vector3.ProjectOnPlane(transform.position, a);
            Vector3 proec_object = Vector3.ProjectOnPlane(target.Value.transform.position, a);
            Vector3 forvard = Vector3.ProjectOnPlane(transform.forward, a);
            Vector3 napr = proec_object - proec_target;
            var distance = napr.magnitude;

            var direction = napr / distance;
            if ((direction - forvard).magnitude < 0.7)
            {
                return TaskStatus.Failure;
            }
          
            
            return TaskStatus.Success;


        }
    }
}