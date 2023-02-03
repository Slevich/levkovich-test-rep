using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_10.OOP.Part_1
{
    //Класс является базовым для Consultant и Manager. Содержит базовые поля, свойства, конструктор и метод.
    class Client
    {
        #region Fields
        //Поля класса.
        private string clientID;
        private string department;
        private string surname;
        private string firstName;
        private string middleName;
        private string phoneNumber;
        private string passportSeriesNumber;
        #endregion

        #region Properties
        //Свойства класса для получения доступа к полям.
        public string ClientID { get { return clientID; }
                                 set { clientID = value; } }
        public string Department { get { return department; }
                                   set { department = value; } }
        public string Surname { get { return surname; } 
                                set { surname = value; } }
        public string FirstName { get { return firstName; }
                                  set { firstName = value; } }
        public string MiddleName { get { return middleName; }
                                   set { middleName = value; } }
        public string PhoneNumber { get { return phoneNumber; }
                                    set { phoneNumber = value; } }
        public string PassportSeriesNumber { get {return passportSeriesNumber;}
                                             set { passportSeriesNumber = value; } }
        #endregion

        #region Enum
        //Перечисление полей класса Client.
        public enum InfoFields
        {
            ClientID = 0,
            Department = 1,
            Surname = 2,
            FirstName = 3,
            MiddleName = 4,
            PhoneNumber = 5,
            PassportSeriesNumber = 6
        }
        #endregion

        #region Constructor
        //Конструктор класса Client.
        public Client(string ClientID, string Department, string Surname, string FirstName, string MiddleName, string PhoneNumber, string PassportSeriesNumber)
        {
            clientID = ClientID;
            department = Department;
            surname = Surname;
            firstName = FirstName;
            middleName = MiddleName;
            phoneNumber = PhoneNumber;
            passportSeriesNumber = PassportSeriesNumber;
        }
        #endregion
    }
}
