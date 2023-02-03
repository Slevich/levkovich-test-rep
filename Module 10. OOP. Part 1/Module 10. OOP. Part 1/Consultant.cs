using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Module_10.OOP.Part_1
{
    //Класс наследуется от Client и двух интерфесов с описанием методов.
    class Consultant : Client, IRecorderManager, IConsultant
    {
        #region Constructor
        //Пустой конструктор к сожалению необходим, но я его не использую далее в коде.
        public Consultant(string ClientID, string Department, string Surname, string FirstName, string MiddleName, string PhoneNumber, string PassportSeriesNumber)
                         : base (ClientID, Department, Surname, FirstName, MiddleName, PhoneNumber, PassportSeriesNumber)
        {
            
        }
        #endregion

        #region Methods
        //Метод, проверяющий содержит ли строка некое число.
        protected bool CheckStringIsContainNumber(string CheckedString)
        {
            double numericValue;
            bool isNumber = double.TryParse(CheckedString, out numericValue);
            return isNumber;
        }

        /// <summary>
        /// Метод десериализует файл .json с базой данных клиента и передает их в лист клиентов.
        /// При этом, консультант, если поле с паспортными данными не пустое, видит звездочки.
        /// </summary>
        /// <returns>Лист клиентов из файла с базой данных клиентов.</returns>
        public List<Client> ReadClientsInfo()
        {
            List<Client> clientsInfoList = JsonConvert.DeserializeObject<List<Client>>(FileManager.GetFileText(FileManager.ClientsBasePath));
            for (int i = 0; i < clientsInfoList.Count; i++)
            {
                if (CheckStringIsContainNumber(clientsInfoList[i].PassportSeriesNumber)) clientsInfoList[i].PassportSeriesNumber = "******************";
                else clientsInfoList[i].PassportSeriesNumber = string.Empty;
            }
            return clientsInfoList;
        }

        /// <summary>
        /// Метод трансформирует экземпляр класса Client, всего его поля в лист строк.
        /// </summary>
        /// <param name="client"></param>
        /// <returns>Лист строк, представляющий собой все поля конкретного клиента.</returns>
        public List<string> GetSingleClientInfo(Client client)
        {
            List<string> clientInfoList = new List<string>() {client.ClientID,
                                                              client.Department,
                                                              client.Surname,
                                                              client.FirstName,
                                                              client.MiddleName,
                                                              client.PhoneNumber,
                                                              client.PassportSeriesNumber};
            return clientInfoList;
        }

        /// <summary>
        /// Метод проверяет, содержит ли новый номер телефона клиента какое то число.
        /// Если да - добавляет запись об этом в историю изменений,
        /// изменяет текст файла с базой данных клиентов,
        /// меняет значение поля с номером телефона.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="changedPhoneNumber"></param>
        /// <param name="oldPhoneNumber"></param>
        /// <param name="fieldNumber"></param>
        /// <param name="ChangesAuthor"></param>
        /// <returns>Клиента с измененным (или нет) номером телефона.</returns>
        public Client ChangeClientInfoField(Client client, string changedPhoneNumber, string oldPhoneNumber, int fieldNumber, string ChangesAuthor)
        {
            Client changedClient;

            if (CheckClientInfoField(changedPhoneNumber, fieldNumber))
            {
                changedClient = client;
                AddRecord(client.ClientID, client.PhoneNumber, changedPhoneNumber, "Номер телефона", ChangesAuthor, false);
                FileManager.ChangeFileText(FileManager.ClientsBasePath, changedPhoneNumber, oldPhoneNumber, changedClient.ClientID);
                changedClient.PhoneNumber = changedPhoneNumber;
                return changedClient;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Метод добавляет новую запись в файл с записями об истории изменений.
        /// При этом, записи отличаются в записимости от того, это изменение
        /// имеющегося клиента или добавление нового.
        /// </summary>
        /// <param name="ClientID"></param>
        /// <param name="OldData"></param>
        /// <param name="ChangedData"></param>
        /// <param name="DataType"></param>
        /// <param name="RecordAuthor"></param>
        /// <param name="IsNewClient"></param>
        public void AddRecord(string ClientID, string OldData, string ChangedData, string DataType, string RecordAuthor, bool IsNewClient)
        {
            string changesType = string.Empty;

            if(IsNewClient)
            {
                changesType = "Запись о клиенте изменена.";
            }
            else
            {
                changesType = $"{OldData} изменено на {ChangedData}";
            }    

            Recorder record = new Recorder(ClientID, DateTime.Now.ToString("f"), DataType, changesType, RecordAuthor);
            string recordText = JsonConvert.SerializeObject(record);
            FileManager.AddNewText(recordText, FileManager.RecordsBasePath);
        }

        /// <summary>
        /// Метод десериализует .json-файл с информацией об истории изменений,
        /// после чего передает в лист типа Recorder.
        /// </summary>
        /// <returns>Лист с историей изменения данных клиентов.</returns>
        public List<Recorder> GetRecords()
        {
            List<Recorder> recordsList = JsonConvert.DeserializeObject<List<Recorder>>(FileManager.GetFileText(FileManager.RecordsBasePath));
            return recordsList;
        }

        /* Метод проверяет поле ввода, передаваемое в качестве параметра.
         * При этом, если поле строковое - проверяет пусто оно и не содержит ли одни цифры.
         * Если поле числовое (номер паспорта, телефон), проверяет не пусто ли оно и содержит ли цифры.
         * Если соответствует условиям - возвращает false.
         */
        protected bool CheckClientInfoField(string InputInfoField, int fieldIndex)
        {
            if (fieldIndex >= (int)InfoFields.Department && fieldIndex <= (int)InfoFields.MiddleName)
            {
                if (InputInfoField == string.Empty || CheckStringIsContainNumber(InputInfoField)) return false;
            }
            else
            {
                if (InputInfoField == string.Empty || CheckStringIsContainNumber(InputInfoField) == false) return false;
            }
            return true;
        }
        #endregion
    }
}
