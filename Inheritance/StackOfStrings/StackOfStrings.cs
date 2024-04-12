namespace StackOfStrings
{
    public class StackOfStrings : Stack<string>
    {
        public bool IsEmpty()
        {
            return this.Count == 0;
        }
        public void AddRange(IEnumerable<string> collection)
        {
            foreach (string item in collection)
            {
                this.Push(item);
            }
        }
    }
}
