using System;
using System.Collections.Generic;
using System.Linq;
using eduLib.Core.Interfaces;

namespace eduLib.Infrastructure.Storage
{
    // Teknik 1: Generics (T terbatas pada class yang mengimplementasikan IEntity)
    public class FileRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly List<T> _dataStorage = new List<T>();

        // Teknik 2: Runtime Configuration
        public int MaxSizeMB { get; private set; }

        public FileRepository(int configSize)
        {
            // DbC: Precondition
            if (configSize <= 0)
                throw new ArgumentException("Ukuran maksimal file dalam konfigurasi harus lebih dari 0.");
            MaxSizeMB = configSize;
        }

        public void Upload(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (_dataStorage.Any(x => x.Id == item.Id))
                throw new InvalidOperationException("Data dengan ID ini sudah ada.");

            _dataStorage.Add(item);
        }

        public void Update(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            var existing = _dataStorage.FirstOrDefault(x => x.Id == item.Id);
            if (existing == null) throw new KeyNotFoundException("Data tidak ditemukan untuk diupdate.");

            // Hapus data lama, masukkan data baru (Update simulasi list)
            _dataStorage.Remove(existing);
            _dataStorage.Add(item);
        }

        public void Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("ID tidak boleh kosong.");

            var existing = _dataStorage.FirstOrDefault(x => x.Id == id);
            if (existing == null) throw new KeyNotFoundException("Data tidak ditemukan untuk dihapus.");

            _dataStorage.Remove(existing);
        }

        public T GetById(string id) => _dataStorage.FirstOrDefault(x => x.Id == id);
        public List<T> GetAll() => _dataStorage;
    }
}