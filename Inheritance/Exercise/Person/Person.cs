namespace Person
{
    public class Person
    {
        // 1. Add Fields
        private string name;
        private int age;

        // 2. Add Constructor
        public Person(string name, int age)
        {
            Age = age;
            Name = name;
        }

        // 3. Add Properties
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public virtual int Age
        {
            get { return age; }
            set
            {
                if (value < 0)
                {
                    age = 0;
                }
                else
                {
                    age = value;
                }
            }
        }

        // 4. Add Methods
        public override string ToString()
        {
            return $"Name: {Name}, Age: {Age}";
        }
    }
}
