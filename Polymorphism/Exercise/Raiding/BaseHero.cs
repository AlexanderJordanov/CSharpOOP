namespace Raiding
{
    public abstract class BaseHero
    {
        protected BaseHero(string name)
        {
            Name = name;
        }

        public string Name { get; protected set; }
        public int Power { get; protected set; }
        public abstract string CastAbility();
    }
}
