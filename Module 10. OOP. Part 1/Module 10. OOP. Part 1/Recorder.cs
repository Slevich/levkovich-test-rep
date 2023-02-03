using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_10.OOP.Part_1
{
    //Класс Recorder содержит базовый функционал (поля, свойства, конструктор) для добавление записей об изменениях.
    public class Recorder
    {
        #region Fields
        //Собственно поля класса (ID клиента бал добавлен, чтобы понять у какого клиента произошли изменения).
        private string recordClientID;
        private string recordDateTime;
        private string recordChangedData;
        private string recordChangesType;
        private string recordAuthor;
        #endregion

        #region Properties
        //Свойства на чтение.
        public string RecordClientID { get { return recordClientID; } }
        public string RecordDateTime { get { return recordDateTime; } }
        public string RecordChangedData { get { return recordChangedData; } }
        public string RecordChangesType { get { return recordChangesType; } }
        public string RecordAuthor { get { return recordAuthor; } }
        #endregion

        #region Constructor
        //Конструктор класса.
        public Recorder (string RecordClientID, string RecordDateTime, string RecordChangedData, string RecordChangesType, string RecordAuthor)
        {
            recordClientID = RecordClientID;
            recordDateTime = RecordDateTime;
            recordChangedData = RecordChangedData;
            recordChangesType = RecordChangesType;
            recordAuthor = RecordAuthor;
        }
        #endregion
    }
}
