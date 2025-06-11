namespace Application.Interfaces.Localizer
{
    public interface ILocalizer
    {
        string this[string key] { get; }
    }
}
