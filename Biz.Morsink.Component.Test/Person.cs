namespace Biz.Morsink.Component.Test
{
    public class Person : IContainerAware
    {
        private IContainer container;
        
        public string FirstName { get; }
        public string LastName { get; }

        public Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public void SetContainer(IContainer container)
        {
            this.container = container;
        }
    }

}