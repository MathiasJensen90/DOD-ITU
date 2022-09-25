using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public partial class MoveSeekersAndTargetsSystem  : SystemBase
{
    protected override void OnUpdate()
    {
        float dt = Time.DeltaTime;
        var seeksersJob = new SeekersMoveJob
        {
            dt = dt
        }.Schedule();
        var targetsJob = new TargetsMoveJob
        {
            dt = dt
        }.Schedule();
        
        seeksersJob.Complete();
        targetsJob.Complete();
    }
}



public partial struct SeekersMoveJob : IJobEntity
{
    public float dt;
    public void Execute(ref Translation trans, ref SeekerData target)
    {
        trans.Value += target.direction * dt;
    }
}

public partial struct TargetsMoveJob : IJobEntity
{
    public float dt;
    public void Execute(ref Translation trans, ref TargetData target)
    {
        trans.Value += target.direction * dt;
    }
}
