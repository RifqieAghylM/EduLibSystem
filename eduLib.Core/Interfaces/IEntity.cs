namespace eduLib.Core.Interfaces
{
    // Interface bantuan agar tipe Generics (T) wajib memiliki Id
    public interface IEntity
    {
        string Id { get; set; }
    }
}