using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Module_10.OOP.Part_1
{
    //Статичный класс для работы с .json-файлом и его текстом.
    static class FileManager
    {
        #region Fields
        //Переменные с путями к файлам с клиентской базой и историей изменений.
        private static string clientsBasePath = $@"{AppDomain.CurrentDomain.BaseDirectory}\Files\ClientsBase.json";
        private static string recordsBasePath = $@"{AppDomain.CurrentDomain.BaseDirectory}\Files\RecordsBase.json";
        #endregion

        #region Properties
        //Поля на чтение полей.
        public static string ClientsBasePath { get { return clientsBasePath; } }
        public static string RecordsBasePath { get { return recordsBasePath; } }
        #endregion

        #region Methods
        /// <summary>
        /// Метод создает файл, если его не существует.
        /// </summary>
        /// <param name="filePath"></param>
        public static void CreateFilesIfDoesntExist(string filePath)
        {
            if (!File.Exists(filePath)) File.Create(filePath);
        }

        /// <summary>
        /// Метод получает текст .json-файлов и возвращает его.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>Текст .json-файла.</returns>
        public static string GetFileText(string filePath)
        {
            string jsonText = File.ReadAllText(filePath);
            return jsonText;
        }

        /// <summary>
        /// Метод вносит изменения в файл, заменяя одну строку на другую.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="changedText"></param>
        /// <param name="originText"></param>
        public static void ChangeFileText(string filePath, string changedText, string originText, string recordID)
        {
            string[] fileStrings = File.ReadAllLines(filePath);
            int textStringStartIndex = 0;
            int textStringLastIndex = 0;

            for(int i = 0; i < fileStrings.Length; i++)
            {
                if (fileStrings[i].Contains(recordID) && fileStrings[i].Contains("ClientID"))
                {
                    textStringStartIndex = i;
                    textStringLastIndex = i + (int)Client.InfoFields.PassportSeriesNumber;
                    break;
                }
            }

            for (int q = textStringStartIndex; q <= textStringLastIndex; q++)
            {
                if (fileStrings[q].Contains(originText))
                {
                    fileStrings[q] = fileStrings[q].Replace(originText, changedText);
                    break;
                }
            }

            File.WriteAllLines(filePath, fileStrings);
        }

        /// <summary>
        /// Метод читает весь текст файла и перезаписывает его с новым текстом.
        /// </summary>
        /// <param name="AddedText"></param>
        /// <param name="filePath"></param>
        public static void AddNewText(string AddedText, string filePath)
        {
            string fileText = File.ReadAllText(filePath);
            fileText = FormatText(fileText, AddedText);
            File.WriteAllText(filePath, fileText);
        }

        /// <summary>
        /// Метод считывает все строки файла в лист.
        /// Удаляет текст о клиенте по его ID 
        /// вместе со знаками-разделителями.
        /// Перезаписывает файл.
        /// </summary>
        /// <param name="RecordID">ID клиента.</param>
        /// <param name="FilePath">Путь к файлу с базой клиентов из класса File Manager.</param>
        public static void DeleteText(string RecordID, string FilePath)
        {
            List<string> fileStrings = File.ReadAllLines(FilePath).ToList();
            int deletedStrings = 9;
            int textStringStartIndex = 0;
            int textStringLastIndex = 0;

            for (int i = 0; i < fileStrings.Count; i++)
            {
                if (fileStrings[i].Contains(RecordID) && fileStrings[i].Contains("ClientID"))
                {
                    textStringStartIndex = --i;
                    textStringLastIndex = textStringStartIndex + deletedStrings - 1;
                    break;
                }
            }

            if (fileStrings[textStringStartIndex].Contains("["))
            {
                ++textStringStartIndex;
            }

            if (fileStrings[textStringLastIndex].Contains("]"))
            {
                --textStringStartIndex;
            }

            fileStrings.RemoveRange(textStringStartIndex, deletedStrings);
            File.WriteAllLines(FilePath, fileStrings);
        }

        /// <summary>
        /// Метод форматирует добавляемый текст, добавляет его к тексту файла для перезаписи.
        /// </summary>
        /// <param name="FileText"></param>
        /// <param name="AddedText"></param>
        /// <returns>Текст файла с новым добавлением.</returns>
        private static string FormatText(string FileText, string AddedText)
        {
            AddedText = AddedText.Replace(",", ",\n\t");
            AddedText = AddedText.Replace("{", "{\n\t");
            AddedText = AddedText.Replace("}", "\n}");

            if (string.IsNullOrEmpty(FileText) == false)
            {
                FileText = FileText.Remove(FileText.IndexOf("["), 1);
                FileText = FileText.Replace("]", ",");
            }

            FileText += $"\n{AddedText}]";
            FileText = "[" + FileText;
            return FileText;
        }
        #endregion
    }
}
