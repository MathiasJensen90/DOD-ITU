using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public partial class MoveSeekersAndTargetsSystem  : SystemBase
{
    protected override void OnUpdate()
    {
        float dt = SystemAPI.Time.DeltaTime; 
        new SeekersMoveJob
        {
            dt = dt
        }.Schedule();
       new TargetsMoveJob
        {
            dt = dt
        }.Schedule();
       
        Dependency.Complete();
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
