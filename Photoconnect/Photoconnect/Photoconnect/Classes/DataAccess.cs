using Photoconnect.Interfaces;
using SQLite;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Photoconnect.Classes
{
    public class DataAccess : IDisposable
    {
        private SQLiteConnection connection;


        public DataAccess()
        {
            var config = DependencyService.Get<IConfig>();
            connection = new SQLiteConnection(System.IO.Path.Combine(config.DiretorioDB, "Photoconnect.db2"));

            connection.CreateTable<Session>();
        }

        public void Inserir<T>(T model)
        {
            connection.Insert(model);
        }

        public void Atualizar<T>(T model)
        {
            connection.Update(model);
        }

        public void Delete<T>(T model)
        {
            connection.Delete(model);
        }

        public T Find<T>(int id) where T : new()
        {
            return connection.Table<T>().FirstOrDefault(Model => Model.GetHashCode() == id);
        }

        public List<T> GetList<T>() where T : new()
        {
            return connection.Table<T>().ToList();
        }

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}
