using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_10.OOP.Part_1
{
    //Это интерфейс, который содержит в себе методы менеджера, которые реализуются в классе Manager.
    interface IManager
    {
        #region Methods
        //Метод изменяет какое-либо из полей клиента и возвращает измененный экземпляр класса.
        Client ChangeClientInfoFields(Client Client, string NewValue, int FieldNumber, string RecordAuthor);
        //Метод добавляет нового клиента на основе ввода данных из полей в WPF. Также возвращает нового клиента.
        Client AddNewClient(List<string> NoteFields, List<Client> clientsList, string RecordAuthor);
        #endregion
    }
}
