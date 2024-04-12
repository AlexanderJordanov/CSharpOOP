namespace Raiding
{
    public class Rogue : BaseHero
    {
        public Rogue(string name) : base(name)
        {
            this.Power = 80;
        }

        public override string CastAbility()
        {
            return $"{this.GetType().Name} - {this.Name} hit for {Power} damage";
        }
    }
}
