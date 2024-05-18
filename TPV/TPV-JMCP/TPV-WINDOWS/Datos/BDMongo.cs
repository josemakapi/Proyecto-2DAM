using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;

namespace TPV_WINDOWS.Datos
{
    public class BDMongo
    {
        private IMongoDatabase? _dbTPV;
        private MongoClient? _clienteDB;
        private string _connectionString;
        public string ConnectionString { get => _connectionString; set => _connectionString = value; }
        private string _host;
        public string Host { get => _host; set => _host = value; }

        private int _port;
        public int Port { get => _port; set => _port = value; }
        private string _username;
        public string Username { get => _username; set => _username = value; }
        private string _password;
        public string Password { get => _password; set => _password = value; }

        public BDMongo(string host, int port, string username, string password)
        {
            this._host = host;
            this._port = port;
            this._username = username;
            this._password = password;
            this._connectionString = $"mongodb://{this._username}:{this._password}@{this._host}:{this._port}/?connectTimeoutMS=5000&socketTimeoutMS=5000&maxIdleTimeMS=5000";
        }

        public bool ConectarBD()
        {
            try
            {
                this._clienteDB = new MongoClient(this._connectionString);
                this._dbTPV = this._clienteDB.GetDatabase("TPV-JMCP-TIENDA00");
                //dbTPV.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool PersistirObjeto<T>(T objeto)
        {
            try
            {
                IMongoCollection<T> collection = this._dbTPV!.GetCollection<T>(typeof(T).Name);
                collection.InsertOne(objeto);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<T> LeerObjetos<T>()
        {
            try
            {
                IMongoCollection<T> collection = this._dbTPV!.GetCollection<T>(typeof(T).Name);
                return collection.Find(new BsonDocument()).ToList();
            }
            catch (Exception)
            {
                return new List<T>();
            }
        }

        public List<T> LeerObjetos<T>(string propiedad, string valor)
        {
            try
            {
                IMongoCollection<T> collection = this._dbTPV!.GetCollection<T>(typeof(T).Name);
                FilterDefinition<T> filter = Builders<T>.Filter.Eq(propiedad, valor);
                return collection.Find(filter).ToList();
            }
            catch (Exception)
            {
                return new List<T>();
            }
        }

        public bool EliminarObjeto<T>(T objeto, string propiedad)
        {
            try
            {
                IMongoCollection<T> collection = this._dbTPV!.GetCollection<T>(typeof(T).Name);
                FilterDefinition<T> filter = Builders<T>.Filter.Eq(propiedad, objeto!.GetType().GetProperty(propiedad)?.GetValue(objeto));
                collection.DeleteOne(filter);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ActualizarObjeto<T>(T objeto, string propiedad)
        {
            try
            {
                IMongoCollection<T> collection = this._dbTPV!.GetCollection<T>(typeof(T).Name);
                FilterDefinition<T> filter = Builders<T>.Filter.Eq(propiedad, objeto!.GetType().GetProperty(propiedad)?.GetValue(objeto));
                collection.ReplaceOne(filter, objeto);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


    }
}


