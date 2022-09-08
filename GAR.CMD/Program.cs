using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GAR.BL.Model;
using GAR.BL.Controler;
using System.Data.Entity.Core.Objects;

namespace GAR.CMD
{
    class Program
    {
        static void Main(string[] args)
        {
            //91:02:006006:55
            //91:04:002014:625
            //91:02:002007:43
            //земля 91:02:001006:45
            //апартаменты 91:02:001011:2101
            //машиноместа 91:02:001002:12092

            int i = 0;

            string command = "";

            while(command != "END")
            {
                Console.WriteLine("Введите кадастровый номер или END для выхода: ");
                command = Console.ReadLine();
                Console.WriteLine();

                if (command == "END") break;

                List<string> list = DBControler.PathToOject(command);

                List<string> adress = DBControler.AdressToObject(DBControler.PathToAdress(list));

                foreach (var item in adress)
                {
                    Console.WriteLine(++i + ") " + item);
                    Console.WriteLine();
                }
                if (adress.Count == 0) Console.WriteLine("Кадастровый номер отсутствует");
                Console.WriteLine("-------------------------------------------------------");
                i = 0;
            }

        }

    }
}
