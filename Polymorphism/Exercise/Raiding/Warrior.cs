namespace Raiding
{
    public class Warrior : BaseHero
    {
        public Warrior(string name) : base(name)
        {
            this.Power = 100;
        }

        public override string CastAbility()
        {
            return $"{this.GetType().Name} - {this.Name} hit for {Power} damage";
        }
    }
}
