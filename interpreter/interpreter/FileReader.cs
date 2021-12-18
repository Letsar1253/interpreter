using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interpreter
{
    /// <summary>
    /// Класс для чтения файла, путь чтения ...
    /// </summary>
    class FileReader
    {
        private StreamReader streamReader;
        //Возможно будет задаваться динамически, пока хардкод
        private string path = "..\\..\\..\\FileToRead\\FileToRead.txt";
        //Строка из файла
        private string str;

        /// <summary>
        /// Прочитать файл
        /// </summary>
        public void ReadFile()
        {
            //Получаем текущий каталог и создаем путь для нашего файла
            var exePath = AppDomain.CurrentDomain.BaseDirectory;
            var path = Path.Combine(exePath, this.path);

            //Открываем наш файл и читаем полностью
            streamReader = new StreamReader(path);
            str = streamReader.ReadToEnd();
        }

        /// <summary>
        /// Получить файл в виде строки
        /// </summary>
        /// <returns></returns>
        public string GetString()
        {
            return str;
        }
    }
}
