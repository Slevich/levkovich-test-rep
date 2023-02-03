using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_10.OOP.Part_1
{
    //Это интерфейс, который содержит в себе методы консультанта, которые реализуются в классе Manager.
    interface IConsultant
    {
        #region Methods
        //Метод получает список всех клиентов из файла.
        List<Client> ReadClientsInfo();
        //Метод получает лист строк полей отдельного клиента.
        List<string> GetSingleClientInfo(Client client);
        //Метод изменяет поле с номером телефона клиента и возвращает измененный экземпляр.
        Client ChangeClientInfoField(Client client, string changedPhoneNumber, string oldPhoneNumber, int fieldNumber, string ChangesAuthor);
        #endregion
    }
}
