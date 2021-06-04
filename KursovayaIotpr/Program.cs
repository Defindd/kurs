using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace KursovayaIotpr
{
    [Serializable]
    public class Client
    {
        public string ClientName;
        public string ContactName;
        public string Segment;
        public string Adres;
        public string Status;
        public string ContactNumber;

    }
    class Program
    {
         public static XmlSerializer formatter = new XmlSerializer(typeof(List<Client>));
        public static List<Client> Clients = new List<Client>();
        public static void ShowClients()
        {
            Console.Clear();
            if (Clients.Count != 0)
            {
                Console.WriteLine("{0,2}|{1,15}|{2,15}|{3,15}|{4,15}|{5,15}|{6,20}|","№", "Название","Адрес","Сегмент","Статус клиента","контактное лицо","контактный номер");
                for (int i = 0; i < Clients.Count; i++)
                {
                    Console.WriteLine("{0,2}|{1,15}|{2,15}|{3,15}|{4,15}|{5,15}|{6,20}|", i + 1, Clients[i].ClientName, Clients[i].Adres, Clients[i].Segment, Clients[i].Status,
                        Clients[i].ContactName, Clients[i].ContactNumber);
                }
                Console.WriteLine();
                Console.WriteLine("Нажмите любую кнопку для продолжения");
            }
            else Console.WriteLine("Клиенты отсутствуют, нажмите любую клавишу для возвращения в меню");
            Console.ReadKey();
            Menu();
        }
        public static void DeleteClient()
        {
            Console.Clear();
            Console.WriteLine("Введите название компании для удаления");
            var companyToDelete = Console.ReadLine();
            int index = -1;
            for(int i = 0; i < Clients.Count; i++)
            {
                if (Clients[i].ClientName == companyToDelete) index = i;
            }
            if (index != -1)
            {
                Clients.RemoveAt(index);

                Console.Clear();
                Console.WriteLine("Клиент успешно удален, для возвращения в меню нажмите любую кнопку");
                Console.ReadKey();
                Menu();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Клиент не найден, проверьте правильность написания и повторите попытку. Для возвращения в меню нажмите любую кнопку");
                Console.ReadKey();
                Menu();
            }
        }
        public static void AddClient()
        {
            var client = new Client();
            Console.Clear();
            Console.Write("Введите название клиента:");
            client.ClientName = Console.ReadLine();
            Console.Clear();
            Console.Write("Введите адрес клиента:");
            client.Adres = Console.ReadLine();
            Console.Clear();
            Console.Write("Введите сегмент клиента:");
            client.Segment = Console.ReadLine();
            Console.Clear();
            Console.Write("Введите статус клиента:");
            client.Status = Console.ReadLine();
            Console.Clear();
            Console.Write("Введите контактное лицо клиента:");
            client.ContactName = Console.ReadLine();
            Console.Clear();
            Console.Write("Введите номер контактного лица:");
            client.ContactNumber = Console.ReadLine();
            Console.Clear();
            bool fl = false;
            for(int i = 0; i < Clients.Count; i++)
            {
                if (Clients[i].ClientName == client.ClientName) fl = true;
            }
            if (!fl)
            {
                Console.WriteLine("Клииент успешно добавлен, для возвращения в меню нажмите любую кнопку");
                Console.ReadKey();
                Clients.Add(client);
                Menu();
            }
            else
            {
                Console.WriteLine("Клиент с таким названием уже существует, для возвращения в меню нажмите любую кнопку");
                Console.ReadKey();
                Menu();
            }
        }
        public static void Menu()
        {
            Console.Clear();
            Console.WriteLine("1.Показать клиентов");
            Console.WriteLine("2.Добавить клиента");
            Console.WriteLine("3.Удалить клиента");
            Console.WriteLine("4.Выход");
            Console.WriteLine();
            Console.Write("Выберите пункт меню:");
        
            var input = Console.ReadLine();
            
            
                switch (input[0])
                {
                    case '1': ShowClients();
                        break;
                    case '2':AddClient();
                        break;
                case '3':DeleteClient();break;
                                        
                    case '4':break;
                }
              
            
        }
        static void Main(string[] args)
        {
            
            using (FileStream fs = new FileStream("clients.xml", FileMode.OpenOrCreate))
            {
                if(fs.Length!=0)
                Clients=formatter.Deserialize(fs) as List<Client>;

            }
            Menu();
            using (FileStream fs = new FileStream("clients.xml", FileMode.Create))
            {
                formatter.Serialize(fs, Clients);

            }

        }
    }
}
