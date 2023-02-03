using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Module_10.OOP.Part_1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields
        //Получаем экземпляры классов с пустыми конструкторами.
        Consultant consultant = new Consultant(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
        Manager manager = new Manager(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
        //Лист экземпляров класса Client.
        List<Client> clientsList = new List<Client>();
        List<Client> departmentClientsList = new List<Client>();
        //Экземпляр класса Client, хранящий клиента, выбранного в общем списке ListView.
        Client selectedClient;
        //Лист экземпляров класса Recorder с историей записей.
        List<Recorder> recordsList = new List<Recorder>();
        //Лист строк, содержащий пользователей для ComboBox.
        List<string> users = new List<string>() { "Non authorized", "Consultant", "Manager" };
        //Лист строк, содержащий варианты департиментов для ComboBox.
        List<string> departments = new List<string>() { "Все", "Маркетинг", "Финансирование", "Цифровые продукты" };
        //Index выбранного клиента в списке.
        int clientNumber;
        GridViewColumnHeader currentColumnHeader;
        bool isSorted;
        List<TextBox> inputFieldsTextBoxes = new List<TextBox>();
        #endregion

        #region Methods
        /*При инициализации окна, вызываем методы по созданию файлов, если они не существуют.
        *После чего вызываем метод, который получает список клиентов и историю записей и передает в ListView.
        *Далее в ComboBox добавляем наших юзеров. По дефолту выставляем Non authorized.
        *Делаем неактивными кнопки. Скрываем поле TextBox.
        */
        public MainWindow()
        {
            InitializeComponent();
            FileManager.CreateFilesIfDoesntExist(FileManager.ClientsBasePath);
            FileManager.CreateFilesIfDoesntExist(FileManager.RecordsBasePath);
            ReloadListViews();
            UsersComboBox.ItemsSource = users;
            UsersComboBox.SelectedItem = users[0];
            DepartmentComboBox.ItemsSource = string.Empty;
            DepartmentComboBox.IsEnabled = false;
            ChooseDepartmentComboBox.ItemsSource = new List<string>() { "Маркетинг", "Финансирование", "Цифровые продукты" };
            ChooseDepartmentComboBox.SelectedItem = "Маркетинг";
            NewClientDepartmentComboBox.ItemsSource = new List<string>() { "Маркетинг", "Финансирование", "Цифровые продукты" };
            NewClientDepartmentComboBox.SelectedItem = "Маркетинг";
            currentColumnHeader = IDHeader;
            EditButton.IsEnabled = false;
            AddButton.IsEnabled = false;
            DeleteButton.IsEnabled = false;
            ChangeTextBox.Visibility = Visibility.Hidden;
            inputFieldsTextBoxes.Add(SurnameTextBox);
            inputFieldsTextBoxes.Add(FirstNameTextBox);
            inputFieldsTextBoxes.Add(MiddleNameTextBox);
            inputFieldsTextBoxes.Add(PhoneNumberTextBox);
            inputFieldsTextBoxes.Add(PassportDataTextBox);
        }

        #region ListViewSelectionChangedEvents
        /*При выборе клиента из списка в ListView, если номер клиента больше ноля (то есть в списке есть клиенты).
         * После чего в выбранного клиента передаем клиента с нужным индексом.
         * В ItemSource передаем результат метода по получению информации о конкретном клиенте (лист строк его полей).
         */
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DepartmentComboBox.SelectedItem.ToString() == "Все")
            {
                clientNumber = clientsList.IndexOf((Client)ListView.SelectedItem);
                if (clientNumber >= 0)
                {
                    selectedClient = clientsList[clientNumber];
                    SelectedClientList.ItemsSource = consultant.GetSingleClientInfo(selectedClient);

                    if (UsersComboBox.SelectedItem.ToString() == "Manager") DeleteButton.IsEnabled = true;
                    else DeleteButton.IsEnabled = false;
                }
            }
            else if (DepartmentComboBox.SelectedItem.ToString() != "Все")
            {
                clientNumber = departmentClientsList.IndexOf((Client)ListView.SelectedItem);
                if (clientNumber >= 0)
                {
                    selectedClient = departmentClientsList[clientNumber];
                    SelectedClientList.ItemsSource = consultant.GetSingleClientInfo(selectedClient);

                    if (UsersComboBox.SelectedItem.ToString() == "Manager") DeleteButton.IsEnabled = true;
                    else DeleteButton.IsEnabled = false;
                }
            }
        }

        /*При выборе отдельного поля выбранного клиента в зависимости от пользователя в UsersComboBox,
         * активируются или дезактивируются кнопки. У Consultant'a кнопка Edit активна только, когда
         * выбран номер телефона клиента. У Manager'а кнопка активна при выборе любого поля клиента.
         * У неавторизованного пользователя, кнопка не активна вовсе.
         */
        private void SelectedClientList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UsersComboBox.SelectedItem.ToString() == "Consultant" && SelectedClientList.SelectedItem != null)
            {
                if (SelectedClientList.SelectedIndex == (int)Client.InfoFields.PhoneNumber) EditButton.IsEnabled = true;
                else EditButton.IsEnabled = false;
            }
            else if (UsersComboBox.SelectedItem.ToString() == "Manager" && SelectedClientList.SelectedItem != null && SelectedClientList.SelectedIndex != (int)Client.InfoFields.ClientID)
            {
                EditButton.IsEnabled = true;
            }
            else
            {
                EditButton.IsEnabled = false;
            }
        }

        /*При смене пользователя, либо обнуляем ListView, либо вызываем методы,
         *которые обновляет списки клиентов или истории изменений.
        */
        private void UsersComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedClientList.ItemsSource = string.Empty;

            if (UsersComboBox.SelectedItem.ToString() == "Non authorized")
            {
                ListView.ItemsSource = string.Empty;
                RecordsList.ItemsSource = string.Empty;
                DepartmentComboBox.ItemsSource = string.Empty;
                SelectedClientList.ItemsSource = string.Empty;
                EditButton.IsEnabled = false;
                AddButton.IsEnabled = false;
                DeleteButton.IsEnabled = false;
                DepartmentComboBox.IsEnabled = false;
            }
            else
            {
                clientsList = consultant.ReadClientsInfo();
                ReloadListViews();
                DepartmentComboBox.ItemsSource = departments;
                DepartmentComboBox.SelectedItem = departments[0];
                currentColumnHeader.Background = Brushes.White;
                currentColumnHeader = IDHeader;
                currentColumnHeader.Background = Brushes.LightGray;
                DepartmentComboBox.IsEnabled = true;
                if (UsersComboBox.SelectedItem.ToString() == "Manager")
                {
                    AddButton.IsEnabled = true;
                }
                else if (UsersComboBox.SelectedItem.ToString() == "Consultant")
                {
                    AddButton.IsEnabled = false;
                    DeleteButton.IsEnabled = false;
                }
            }
        }

        //Ивент, который вызывается при смене значения в ComboBox'е с департаментами.
        private void DepartmentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DepartmentComboBox.SelectedItem != null && DepartmentComboBox.SelectedItem.ToString() == "Все")
            {
                ListView.ItemsSource = clientsList;
                SelectedClientList.ItemsSource = string.Empty;
            }
            else if (DepartmentComboBox.SelectedItem != null && DepartmentComboBox.SelectedItem.ToString() != "Все")
            {
                departmentClientsList = ListSorter.GetListByResponse(DepartmentComboBox.SelectedItem.ToString(), clientsList);
                SelectedClientList.ItemsSource = string.Empty;
                ListView.ItemsSource = departmentClientsList;
            }
        }
        #endregion

        #region OnButtonClickEvents
        /*При нажатии кнопки изменить, скрывается кнопка изменить,
         * Становятся видны кнопка принять и поле ввода
        */
        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            EditButton.Visibility = Visibility.Hidden;
            ApplyButton.Visibility = Visibility.Visible;
            SelectedClientList.IsEnabled = false;
            ListView.IsEnabled = false;

            if (SelectedClientList.SelectedIndex == (int)Client.InfoFields.Department)
            {
                ChooseDepartmentComboBox.Visibility = Visibility.Visible;
            }
            else
            {
                ChangeTextBox.Visibility = Visibility.Visible;
            }
        }

        /*При нажатии кнопки принять, в зависимости от выбранного пользователя,
         * вызывается метод изменения только номера телефона (Консультант) или
         * любого из полей (Менеджер).
         * После чего показываем сообщение, если поле поменялось (в случае с номером телефона).
         * После чего обновляем листы и передаем их в ItemSource ListView.
        */
        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedFieldIndex = SelectedClientList.SelectedIndex;
            Client changedClient = null;
            string newValue = string.Empty;

            if (selectedFieldIndex == (int)Client.InfoFields.Department)
            {
                newValue = ChooseDepartmentComboBox.SelectedItem.ToString();
            }
            else
            {
                newValue = ChangeTextBox.Text;
            }

            if (UsersComboBox.SelectedItem.ToString() == "Consultant")
            {
                changedClient = consultant.ChangeClientInfoField(selectedClient, 
                                                                 newValue, 
                                                                 selectedClient.PhoneNumber, 
                                                                 selectedFieldIndex, 
                                                                 UsersComboBox.SelectedItem.ToString());
            }
            else if (UsersComboBox.SelectedItem.ToString() == "Manager")
            {
                changedClient = manager.ChangeClientInfoFields(selectedClient, 
                                                               newValue, 
                                                               selectedFieldIndex, 
                                                               UsersComboBox.SelectedItem.ToString());
            }

            if (changedClient != null)
            {
                MessageBox.Show($"Client's information succesfully changed!");
                selectedClient = changedClient;
                SelectedClientList.ItemsSource = consultant.GetSingleClientInfo(selectedClient);

            }
            else
            {
                MessageBox.Show($"Failure to change client's data :(");
            }

            if (DepartmentComboBox.SelectedItem.ToString() != "Все")
            {
                departmentClientsList = ListSorter.GetListByResponse(DepartmentComboBox.SelectedItem.ToString(), clientsList);
                ListView.ItemsSource = departmentClientsList;
            }

            ListView.Items.Refresh();
            recordsList = consultant.GetRecords();
            RecordsList.ItemsSource = recordsList;
            ChangeTextBox.Text = string.Empty;
            ChangeTextBox.Visibility = Visibility.Hidden;
            ChooseDepartmentComboBox.Visibility = Visibility.Hidden;
            ChooseDepartmentComboBox.SelectedItem = "Маркетинг";
            ApplyButton.Visibility = Visibility.Hidden;
            EditButton.Visibility = Visibility.Visible;
            SelectedClientList.IsEnabled = true;
            ListView.IsEnabled = true;
        }

        //При нажатии кнопки закрыть, скрывает окно ввода данных нового клиента.
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            NewClientCanvas.Visibility = Visibility.Hidden;
            NewClientDepartmentComboBox.SelectedItem = "Маркетинг";
            ChangeInterfaceAvailability(true);
        }
        //При нажатии кнопки добавить, показывает окно ввода данных нового клиента.
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NewClientCanvas.Visibility = Visibility.Visible;
            ChangeInterfaceAvailability(false);
        }

        /*При нажатии кнопки принять в окне ввода данных нового клиента,
         * передаем текст всех TextBox'ов в лист строк.
         * После чего, вызываем метод по добавлению нового клиента.
         * Если клиент добавился, добавляем его в лист клиентов, обновляем листы,
         * перезагружаем ItemSource'ы.
         * Если клиента равен null - показываем сообщение об ошибке.
        */
        private void ApplyNewClientButton_Click(object sender, RoutedEventArgs e)
        {
            List<string> inputFieldsTexts = new List<string>();
            inputFieldsTexts.Add(NewClientDepartmentComboBox.SelectedItem.ToString());

            for (int i = 0; i < inputFieldsTextBoxes.Count; i++)
            {
                inputFieldsTexts.Add(inputFieldsTextBoxes[i].Text);
            }

            Client newClient = manager.AddNewClient(inputFieldsTexts, clientsList, UsersComboBox.SelectedItem.ToString());

            if (newClient != null)
            {
                clientsList.Add(newClient);
                if (DepartmentComboBox.SelectedItem.ToString() == newClient.Department)
                {
                    departmentClientsList.Add(newClient);
                    ListView.Items.Refresh();
                }
                else
                {
                    ReloadListViews();
                }
                MessageBox.Show("New client successfully added!");
            }
            else
            {
                MessageBox.Show("Failed to add a new client :(");
            }

            for (int i = 0; i < inputFieldsTextBoxes.Count; i++)
            {
                inputFieldsTextBoxes[i].Text = string.Empty;
            }

            NewClientCanvas.Visibility = Visibility.Hidden;
            NewClientDepartmentComboBox.SelectedItem = "Маркетинг";
            ChangeInterfaceAvailability(true);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ListView.SelectedItem != null)
            {
                clientsList = manager.DeleteClient(clientsList, selectedClient.ClientID, UsersComboBox.SelectedItem.ToString());

                if (DepartmentComboBox.SelectedItem.ToString() == "Все")
                {
                    ListView.Items.Refresh();
                }
                else
                {
                    departmentClientsList = ListSorter.GetListByResponse(DepartmentComboBox.SelectedItem.ToString(), clientsList);
                    ListView.Items.Refresh();
                }
                SelectedClientList.ItemsSource = string.Empty;
                DeleteButton.IsEnabled = false;
            }
        }

        #endregion

        #region OnHeaderClickEvents
        /*  Следующие методы вызываются при нажатии на заголовки в таблице с клиентами.
         * При нажатии на тот или иной заголовок, происходит сортировка листа с клиентами
         * в новый лист по отдельному департаменту. При этом, сортировка проходит по строке для
         * всех полей, кроме ID. Там сортировка идет по численному значению.
         */
        private void IDColumnHeaderClick(object sender, RoutedEventArgs e)
        {
            if (UsersComboBox.SelectedItem.ToString() != "Non authorized" && DepartmentComboBox.SelectedItem != null)
            {
                if (DepartmentComboBox.SelectedItem.ToString() == "Все")
                {
                    isSorted = CheckClickHeader(sender);
                    ListSorter.SortListByIntValue(clientsList, sortedClient => Convert.ToInt32(sortedClient.ClientID), isSorted);
                    ListView.Items.Refresh();
                }
                else if (DepartmentComboBox.SelectedItem.ToString() != "Все")
                {
                    isSorted = CheckClickHeader(sender);
                    ListSorter.SortListByIntValue(departmentClientsList, sortedClient => Convert.ToInt32(sortedClient.ClientID), isSorted);
                    ListView.Items.Refresh();
                }
            }
        }

        private void DepartmentHeaderClick(object sender, RoutedEventArgs e)
        {
            if (UsersComboBox.SelectedItem.ToString() != "Non authorized" && DepartmentComboBox.SelectedItem != null)
            {
                if (DepartmentComboBox.SelectedItem.ToString() == "Все")
                {
                    isSorted = CheckClickHeader(sender);
                    ListSorter.SortListByStringValue(clientsList, sortedClient => sortedClient.Department, isSorted);
                    ListView.Items.Refresh();
                }
                else if (DepartmentComboBox.SelectedItem.ToString() != "Все")
                {
                    MessageBox.Show("Notice: Список уже отсортирован по департаменту!");
                }
            }
        }

        private void SurnameHeaderClick(object sender, RoutedEventArgs e)
        {
            if (UsersComboBox.SelectedItem.ToString() != "Non authorized" && DepartmentComboBox.SelectedItem != null)
            {
                if (DepartmentComboBox.SelectedItem.ToString() == "Все")
                {
                    isSorted = CheckClickHeader(sender);
                    ListSorter.SortListByStringValue(clientsList, sortedClient => sortedClient.Surname, isSorted);
                    ListView.Items.Refresh();
                }
                else if (DepartmentComboBox.SelectedItem.ToString() != "Все")
                {
                    isSorted = CheckClickHeader(sender);
                    ListSorter.SortListByStringValue(departmentClientsList, sortedClient => sortedClient.Surname, isSorted);
                    ListView.Items.Refresh();
                }
            }
        }

        private void FirstNameHeaderClick(object sender, RoutedEventArgs e)
        {
            if (UsersComboBox.SelectedItem.ToString() != "Non authorized" && DepartmentComboBox.SelectedItem != null)
            {
                if (DepartmentComboBox.SelectedItem.ToString() == "Все")
                {
                    isSorted = CheckClickHeader(sender);
                    ListSorter.SortListByStringValue(clientsList, sortedClient => sortedClient.FirstName, isSorted);
                    ListView.Items.Refresh();
                }
                else if (DepartmentComboBox.SelectedItem.ToString() != "Все")
                {
                    isSorted = CheckClickHeader(sender);
                    ListSorter.SortListByStringValue(departmentClientsList, sortedClient => sortedClient.FirstName, isSorted);
                    ListView.Items.Refresh();
                }
            }
        }

        private void MiddleNameHeaderClick(object sender, RoutedEventArgs e)
        {
            if (UsersComboBox.SelectedItem.ToString() != "Non authorized" && DepartmentComboBox.SelectedItem != null)
            {
                if (DepartmentComboBox.SelectedItem.ToString() == "Все")
                {
                    isSorted = CheckClickHeader(sender);
                    ListSorter.SortListByStringValue(clientsList, sortedClient => sortedClient.MiddleName, isSorted);
                    ListView.Items.Refresh();
                }
                else if (DepartmentComboBox.SelectedItem.ToString() != "Все")
                {
                    isSorted = CheckClickHeader(sender);
                    ListSorter.SortListByStringValue(departmentClientsList, sortedClient => sortedClient.MiddleName, isSorted);
                    ListView.Items.Refresh();
                }
            }
        }

        private void PhoneNumberHeaderClick(object sender, RoutedEventArgs e)
        {
            if (UsersComboBox.SelectedItem.ToString() != "Non authorized" && DepartmentComboBox.SelectedItem != null)
            {
                if (DepartmentComboBox.SelectedItem.ToString() == "Все")
                {
                    isSorted = CheckClickHeader(sender);
                    ListSorter.SortListByStringValue(clientsList, sortedClient => sortedClient.PhoneNumber, isSorted);
                    ListView.Items.Refresh();
                }
                else if (DepartmentComboBox.SelectedItem.ToString() != "Все")
                {
                    isSorted = CheckClickHeader(sender);
                    ListSorter.SortListByStringValue(departmentClientsList, sortedClient => sortedClient.PhoneNumber, isSorted);
                    ListView.Items.Refresh();
                }
            }
        }

        private void PassportDataHeaderClick(object sender, RoutedEventArgs e)
        {
            if (UsersComboBox.SelectedItem.ToString() != "Non authorized")
            {
                MessageBox.Show("Warning: Сортировка по данным паспорта невозможна!");
            }
        }
        #endregion

        #region Custom Methods
        //Метод читает клиентов и историю записей, передает их в листы. Обновляем ItemSource'ы.
        private void ReloadListViews()
        {
            clientsList = consultant.ReadClientsInfo();
            ListView.ItemsSource = clientsList;
            recordsList = consultant.GetRecords();
            RecordsList.ItemsSource = recordsList;
        }

        private bool CheckClickHeader(object Sender)
        {
            GridViewColumnHeader columnHeader = (GridViewColumnHeader)Sender;

            if (columnHeader != currentColumnHeader)
            {
                currentColumnHeader.Background = Brushes.White;
                currentColumnHeader = columnHeader;
                currentColumnHeader.Background = Brushes.LightGray;
                return false;
            }
            else
            {
                return !isSorted;
            }
        }

        private void ChangeInterfaceAvailability(bool IsEnabled)
        {
            ListView.IsEnabled = IsEnabled;
            SelectedClientList.IsEnabled = IsEnabled;
            UsersComboBox.IsEnabled = IsEnabled;
            DepartmentComboBox.IsEnabled = IsEnabled;
            AddButton.IsEnabled = IsEnabled;
            EditButton.IsEnabled = IsEnabled;
        }
        #endregion

        #endregion
    }
}
