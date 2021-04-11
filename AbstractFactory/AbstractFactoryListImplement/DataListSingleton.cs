using System;
using System.Collections.Generic;
using AbstractFactoryListImplement.Models;

namespace AbstractFactoryListImplement
{
    class DataListSingleton
    {
        private static DataListSingleton instance;
        public List<Component> Components { get; set; }
        public List<Order> Orders { get; set; }
        public List<Aircraft> Aircraft { get; set; }

        public List<Storage> Storages { get; set; }
        private DataListSingleton()
        {
            Components = new List<Component>();
            Orders = new List<Order>();
            Aircraft = new List<Aircraft>();
        }
        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }
            return instance;
        }
    }
}
