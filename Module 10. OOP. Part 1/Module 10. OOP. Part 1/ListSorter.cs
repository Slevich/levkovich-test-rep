using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_10.OOP.Part_1
{
    static class ListSorter
    {
        #region Методы
        /// <summary>
        /// Метод сортирует лист экземпляров Client по определенному ключу типа string.
        /// </summary>
        /// <param name="ClientsList">Лист клиентов, который сортируем.</param>
        /// <param name="KeySelector">Функция с классом и критерием сортировки.</param>
        static public void SortListByStringValue(List<Client> ClientsList, Func<Client,string> KeySelector, bool IsSorted)
        {
            int i = 0;

            if (IsSorted)
            {
                foreach (Client client in ClientsList.OrderByDescending(KeySelector))
                {
                    ClientsList[i] = client;
                    i++;
                }
            }
            else
            {
                foreach (Client client in ClientsList.OrderBy(KeySelector))
                {
                    ClientsList[i] = client;
                    i++;
                }
            }
        }

        /// <summary>
        /// Метод сортирует лист экземпляров Client по определенному ключу типа int.
        /// Актуально, когда поле содержит число без пробелов и других символов.
        /// </summary>
        /// <param name="ClientsList">Лист клиентов, который сортируем.</param>
        /// <param name="KeySelector">Функция с классом и критерием сортировки.</param>
        static public void SortListByIntValue(List<Client> ClientsList, Func<Client, int> KeySelector, bool IsSorted)
        {
            int i = 0;

            if (IsSorted)
            {
                foreach (Client client in ClientsList.OrderByDescending(KeySelector))
                {
                    ClientsList[i] = client;
                    i++;
                }
            }
            else
            {
                foreach (Client client in ClientsList.OrderBy(KeySelector))
                {
                    ClientsList[i] = client;
                    i++;
                }
            }
        }

        /// <summary>
        /// Метод формирует лист клиентов по запросу в виде названия департамента.
        /// </summary>
        /// <param name="ResponseText">Текст с названием департамента.</param>
        /// <param name="ClientsList">Лист клиентов, в котором просходит поиск.</param>
        /// <returns>Лист клиентов департамента.</returns>
        static public List<Client> GetListByResponse(string ResponseText, List<Client> ClientsList)
        {
            List<Client> departmentClients = ClientsList.Where(client => client.Department == ResponseText).ToList();
            return departmentClients;
        }

        /// <summary>
        /// Метод выполняет поиск конкретного клиента по его ID
        /// и возвращает его.
        /// </summary>
        /// <param name="ClientID">ID искомого клиента.</param>
        /// <param name="ClientsList">Лист клиентов, в котором происходит поиск.</param>
        /// <returns>Клиент с указанным ID.</returns>
        static public Client GetSingleByID(string ClientID, List<Client> ClientsList)
        {
            Client findingClient = ClientsList.Where(client => client.ClientID == ClientID).Single();
            return findingClient;
        }
        #endregion
    }
}
