using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UpdateFolderFiles
{
    static class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args == null || args.Length != 2)
                {
                    Console.WriteLine("Argumentos inválidos. Informe primeiramente a pasta de origem e depois a pasta de destino!");
                    Console.ReadKey();
                    return;
                }

                var path1 = args[0];
                var path2 = args[1];

                var dir1 = new DirectoryInfo(path1);
                var dir2 = new DirectoryInfo(path2);

                UpdateFiles(dir1, dir2, "dll");
                UpdateFiles(dir1, dir2, "pdb");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadKey();
            }
        }

        private static void UpdateFiles(DirectoryInfo dir1, DirectoryInfo dir2, string extension)
        {
            var list1 = dir1.GetFiles($"*.{extension}", SearchOption.TopDirectoryOnly);
            var list2 = dir2.GetFiles($"*.{extension}", SearchOption.TopDirectoryOnly);

            var myFileCompare = new FileCompare();

            var queryList1Only = (from file in list1
                                  select file).Except(list2, myFileCompare);

            foreach (var file in queryList1Only)
            {
                var destinationFile = Path.Combine(dir2.FullName, file.Name);
                File.Copy(file.FullName, destinationFile, true);
            }
        }
    }

    class FileCompare : IEqualityComparer<FileInfo>
    {
        public FileCompare() { }

        public bool Equals(FileInfo x, FileInfo y)
        {
            if (x.Name == y.Name)
            {
                if (x.LastWriteTime >= y.LastWriteTime)
                {
                    return true;
                }

                return false;
            }
            else
            {
                return false;
            }

        }

        public int GetHashCode(FileInfo obj)
        {
            var s = $"{obj.Name}";
            return s.GetHashCode();
        }
    }
}
