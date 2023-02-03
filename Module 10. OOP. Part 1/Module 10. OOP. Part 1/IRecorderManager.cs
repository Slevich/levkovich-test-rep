using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_10.OOP.Part_1
{
    //Это интерфейс, который содержит в себе методы работы с записями о изменениях, которые реализуются в классе Consultant.
    interface IRecorderManager
    {
        #region Methods
        //Метод добавляет новую запись об изменении данных.
        void AddRecord(string ClientID, string OldData, string ChangedData, string ChangesType, string RecordAuthor, bool IsNewClient);
        //Метод добавляет новую запись с изменениями.
        List<Recorder> GetRecords();
        #endregion
    }
}
