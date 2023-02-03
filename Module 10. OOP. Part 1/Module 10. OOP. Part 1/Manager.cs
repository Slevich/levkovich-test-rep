using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Module_10.OOP.Part_1
{
    //Класс наследует функционал класса Consultant и расширяет его при помощи интерфейса IManager.
    class Manager : Consultant, IManager
    {
        #region Constructor
        //Пустой конструктор, так как он не используется.
        public Manager(string ClientID, string Department, string Surname, string FirstName, string MiddleName, string PhoneNumber, string PassportSeriesNumber)
                      : base(ClientID, Department, Surname, FirstName, MiddleName, PhoneNumber, PassportSeriesNumber)
        {

        }
        #endregion

        #region Methods
        /// <summary>
        /// Метод меняет поле клиента на новое значение, добавляя при этом новую запись
        /// в историю изменений. При этом, изменяемое поле выбирается на основе номера поля,
        /// выбранного в окне самой программы (ItemIndex).
        /// </summary>
        /// <param name="client"></param>
        /// <param name="newValue"></param>
        /// <param name="fieldNumber"></param>
        /// <param name="recordAuthor"></param>
        /// <returns>Экземпляр клиента с измененным полем.</returns>
        public Client ChangeClientInfoFields(Client client, string newValue, int fieldNumber, string recordAuthor)
        {
            Client changedClient;

            if (CheckClientInfoField(newValue, fieldNumber))
            {
                switch ((InfoFields)fieldNumber)
                {
                    case InfoFields.Department:
                        AddRecord(client.ClientID, client.Department, newValue, "Департамент", recordAuthor, false);
                        FileManager.ChangeFileText(FileManager.ClientsBasePath, newValue, client.Department, client.ClientID);
                        changedClient = client;
                        changedClient.Department = newValue;
                        return changedClient;

                    case InfoFields.Surname:
                        AddRecord(client.ClientID, client.Surname, newValue, "Фамилия", recordAuthor, false);
                        FileManager.ChangeFileText(FileManager.ClientsBasePath, newValue, client.Surname, client.ClientID);
                        changedClient = client;
                        changedClient.Surname = newValue;
                        return changedClient;

                    case InfoFields.FirstName:
                        AddRecord(client.ClientID, client.FirstName, newValue, "Имя", recordAuthor, false);
                        FileManager.ChangeFileText(FileManager.ClientsBasePath, newValue, client.FirstName, client.ClientID);
                        changedClient = client;
                        changedClient.FirstName = newValue;
                        return changedClient;

                    case InfoFields.MiddleName:
                        AddRecord(client.ClientID, client.MiddleName, newValue, "Отчество", recordAuthor, false);
                        FileManager.ChangeFileText(FileManager.ClientsBasePath, newValue, client.MiddleName, client.ClientID);
                        changedClient = client;
                        changedClient.MiddleName = newValue;
                        return changedClient;

                    case InfoFields.PhoneNumber:
                        AddRecord(client.ClientID, client.PhoneNumber, newValue, "Номер телефона", recordAuthor, false);
                        FileManager.ChangeFileText(FileManager.ClientsBasePath, newValue, client.PhoneNumber, client.ClientID);
                        changedClient = client;
                        changedClient.PhoneNumber = newValue;
                        return changedClient;

                    case InfoFields.PassportSeriesNumber:
                        AddRecord(client.ClientID, client.PassportSeriesNumber, newValue, "Серия и номер паспорта", recordAuthor, false);
                        FileManager.ChangeFileText(FileManager.ClientsBasePath, newValue, client.PassportSeriesNumber, client.ClientID);
                        changedClient = client;
                        changedClient.PassportSeriesNumber = newValue;
                        return changedClient;
                }
            }
            return null;
        }

        /// <summary>
        /// Метод добавляет нового клиента в файл .json с базой данных клиентов.
        /// При этом, проверяет содержит ли новый номер телефона число.
        /// Если нет - возвращает null.
        /// </summary>
        /// <param name="NoteFields"></param>
        /// <param name="clientsList"></param>
        /// <param name="RecordAuthor"></param>
        /// <returns>Нового клиента (или ноль - если новый номер телефона не содержит число).</returns>
        public Client AddNewClient(List<string> NoteFields, List<Client> clientsList, string RecordAuthor)
        {
            Client newClient;

            if (CheckClientInfoFields(NoteFields))
            {
                int lastID = Convert.ToInt32(clientsList[clientsList.Count - 1].ClientID);
                newClient = new Client((++lastID).ToString(), NoteFields[0], NoteFields[1], NoteFields[2], NoteFields[3], NoteFields[4], NoteFields[5]);
                AddRecord(newClient.ClientID, string.Empty, string.Empty, "Новый клиент добавлен.", RecordAuthor, true);
                string newClientText = JsonConvert.SerializeObject(newClient);
                FileManager.AddNewText(newClientText, FileManager.ClientsBasePath);
            }
            else
            {
                newClient = null;
            }

            return newClient;
        }

        /// <summary>
        /// Метод ищет клиента в листе по его ID.
        /// После чего удаляет текст о нем из файла.
        /// Добавляет запись об этом.
        /// Удаляет клиента из листа клиентов.
        /// Возвращает обновленный лист клиентов.
        /// </summary>
        /// <param name="ClientsList">Лист клиентов.</param>
        /// <param name="ClientID">ID клиента, которого стоит удалить.</param>
        /// <param name="RecordAuthor">Автор записи.</param>
        /// <returns></returns>
        public List<Client> DeleteClient(List<Client> ClientsList, string ClientID, string RecordAuthor)
        {
            Client deletedClient = ListSorter.GetSingleByID(ClientID, ClientsList);
            FileManager.DeleteText(deletedClient.ClientID, FileManager.ClientsBasePath);
            AddRecord(deletedClient.ClientID, string.Empty, string.Empty, "Клиент удален.", RecordAuthor, true);
            ClientsList.Remove(deletedClient);
            return ClientsList;
        }

        /* Метод проверяет поля при добавлении нового клиента.
         * Для полей с буквами проверяет не пусто ли поле и не
         * введены ли там цифры вместо букв.
         * Для полей с цифрами проверяет не пусто ли полей и 
         * содержит ли поле какое нибудь число.
         */ 
        private bool CheckClientInfoFields(List<string> FieldsList)
        {
            int wrongFieldsCount = 0;

            for (int i = 0; i < FieldsList.Count; i++)
            {
                if (i <= 3)
                {
                    if (FieldsList[i] == string.Empty || CheckStringIsContainNumber(FieldsList[i])) wrongFieldsCount++;
                }
                else
                {
                    if (FieldsList[i] == string.Empty || CheckStringIsContainNumber(FieldsList[i]) == false) wrongFieldsCount++;
                }
            }

            if (wrongFieldsCount > 0) return false;
            else return true;
        }
        #endregion
    }
}
