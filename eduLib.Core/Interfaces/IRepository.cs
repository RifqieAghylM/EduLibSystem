using System.Collections.Generic;

namespace eduLib.Core.Interfaces
{
    // Teknik: Parameterization/Generics (Sekarang mewajibkan tipe T memiliki Id)
    public interface IRepository<T> where T : IEntity
    {
        void Upload(T item);
        void Update(T item);
        void Delete(string id);
        T GetById(string id);
        List<T> GetAll();
    }
}