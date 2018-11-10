namespace Biz.Morsink.Component
{
    public interface IFlexibleContainer : IContainer
    {
        bool Add(object component);
        bool Remove(object component);
    }

}
