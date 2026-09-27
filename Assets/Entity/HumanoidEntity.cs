
public class HumanoidEntity : Entity
{
    public EntityStats entityStats;

    protected override void Die(Entity caster)
    {
        entityStats.entityStats.currentLife = 0;
    }
}
