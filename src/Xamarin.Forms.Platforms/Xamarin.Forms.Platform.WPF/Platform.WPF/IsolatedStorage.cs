namespace Xamarin.Forms.Platform.WPF
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class IsolatedStorageFile : Xamarin.Forms.IIsolatedStorageFile
    {
        System.IO.IsolatedStorage.IsolatedStorageFile file;

        public IsolatedStorageFile(System.IO.IsolatedStorage.IsolatedStorageFile file)
        {
            this.file = file;
        }

        public void CreateDirectory(string path)
        {
            file.CreateDirectory(path);
        }

        public bool DirectoryExists(string path)
        {
            return file.DirectoryExists(path);
        }

        public bool FileExists(string path)
        {
            return file.FileExists(path);
        }

        public DateTimeOffset GetLastWriteTime(string path)
        {
            return file.GetLastWriteTime(path);
        }

        public System.IO.Stream OpenFile(string path, FileMode mode, FileAccess access, FileShare share)
        {
            return file.OpenFile(path, (System.IO.FileMode)mode, (System.IO.FileAccess)access, (System.IO.FileShare)share);
        }

        public System.IO.Stream OpenFile(string path, FileMode mode, FileAccess access)
        {
            return file.OpenFile(path, (System.IO.FileMode)mode, (System.IO.FileAccess)access);
        }

        public Task CreateDirectoryAsync(string path)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetDirectoryExistsAsync(string path)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetFileExistsAsync(string path)
        {
            throw new NotImplementedException();
        }

        public Task<DateTimeOffset> GetLastWriteTimeAsync(string path)
        {
            throw new NotImplementedException();
        }

        public Task<Stream> OpenFileAsync(string path, Xamarin.Forms.FileMode mode, Xamarin.Forms.FileAccess access)
        {
            throw new NotImplementedException();
        }



      public  Task<Stream> OpenFileAsync(string path, Xamarin.Forms.FileMode mode, Xamarin.Forms.FileAccess access, Xamarin.Forms.FileShare share)
        {
            throw new NotImplementedException();
        }
    }
}
