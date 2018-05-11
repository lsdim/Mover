using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Mover
{
    class IniFiles
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="aPath">Path to the ini file</param>
        public IniFiles(string aPath)
        {
            path = aPath;
        }

        /// <summary>
        /// Read string from ini file
        /// </summary>
        /// <param name="aSection">Section</param>
        /// <param name="aKey">Key</param>
        /// <param name="defaultVal">Default value</param>
        /// <returns>Value or Default value</returns>
        public string ReadString(string aSection, string aKey, string defaultVal)
        {
            //Для получения значения
            StringBuilder buffer = new StringBuilder(SIZE);

            //Получить значение в buffer
            GetPrivateString(aSection, aKey, null, buffer, SIZE, path);

            //Вернуть полученное значение
            string rez = buffer.ToString() == "" ? defaultVal : buffer.ToString();
            return rez;
        }
        /// <summary>
        ///Read integer from ini file
        /// </summary>
        /// <param name="aSection">Section</param>
        /// <param name="aKey">Key</param>
        /// <param name="defaultVal">Default value</param>
        /// <returns>Value or Default value</returns>
        public int ReadInteger(string aSection, string aKey, int defaultVal)
        {
            //Для получения значения
            StringBuilder buffer = new StringBuilder(SIZE);

            //Получить значение в buffer
            GetPrivateString(aSection, aKey, null, buffer, SIZE, path);

            //Вернуть полученное значение

            if (int.TryParse(buffer.ToString(), out int rez))
                return rez;
            else
                return defaultVal;  
        }

        public bool ReadBool(string aSection, string aKey, bool defaultVal)
        {
            //Для получения значения
            StringBuilder buffer = new StringBuilder(SIZE);

            //Получить значение в buffer
            GetPrivateString(aSection, aKey, null, buffer, SIZE, path);

            //Вернуть полученное значение

            if (int.TryParse(buffer.ToString(), out int rez))
                return Convert.ToBoolean(rez);
            else
                return defaultVal;
        }


        //Пишет значение в INI-файл (по указанным секции и ключу) 
        /// <summary>
        /// Write string into ini file
        /// </summary>
        /// <param name="aSection">Section</param>
        /// <param name="aKey">Key</param>
        /// <param name="aValue">Value</param>
        public void WriteString(string aSection, string aKey, string aValue)
        {
            //Записать значение в INI-файл
            WritePrivateString(aSection, aKey, aValue, path);
        }


        //Возвращает или устанавливает путь к INI файлу
        public string Path { get { return path; } }

        //Поля класса
        private const int SIZE = 1024; //Максимальный размер (для чтения значения из файла)
        private string path = null; //Для хранения пути к INI-файлу

        //Импорт функции GetPrivateProfileString (для чтения значений) из библиотеки kernel32.dll
        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileString")]
        private static extern int GetPrivateString(string section, string key, string def, StringBuilder buffer, int size, string path);

        //Импорт функции WritePrivateProfileString (для записи значений) из библиотеки kernel32.dll
        [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileString")]
        private static extern int WritePrivateString(string section, string key, string str, string path);

    }
}
