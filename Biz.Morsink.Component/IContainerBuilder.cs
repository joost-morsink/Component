namespace Biz.Morsink.Component
{
    public interface IContainerBuilder
    {
        IContainerBuilder Add(object component);
        IContainer Build();
    }

}
