using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Fight
{
    [TaskDescription("Fight.")]
    [TaskCategory("Fight")]
    [HelpURL("http://www.opsive.com/assets/BehaviorDesigner/Movement/documentation.php?id=1")]


    public class  Turn: Action
    {

    public SharedGameObject target;
    private Animator anim;

    public override TaskStatus OnUpdate()
    {
        anim = GetComponent<Animator>();
        var position = target.Value.transform.position;
            Vector3 napr = transform.position - target.Value.transform.position;
            var distance = napr.magnitude;
        var direction = napr / distance;
            if ((System.Math.Round(Mathf.Abs(direction.x), 1) == System.Math.Round(Mathf.Abs(transform.forward.x), 1)) &&
                (System.Math.Round(Mathf.Abs(direction.y), 1) == System.Math.Round(Mathf.Abs(transform.forward.y), 1)) &&
                (System.Math.Round(Mathf.Abs(direction.z), 1) == System.Math.Round(Mathf.Abs(transform.forward.z), 1)))
            {
                anim.SetFloat("VelY", 0);
                return TaskStatus.Success;
            }
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(position - transform.position), 1);
        anim.SetFloat("VelX", 1);
        anim.SetFloat("VelY", -1);
        return TaskStatus.Running;

        }

    }
}