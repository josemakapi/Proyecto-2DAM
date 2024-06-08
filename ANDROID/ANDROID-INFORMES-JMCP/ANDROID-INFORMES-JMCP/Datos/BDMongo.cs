using System.Data;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using ANDROID_INFORMES_JMCP.Modelo;



namespace ANDROID_INFORMES_JMCP.Datos
{
    /// <summary>
    /// Clase que gestiona la BD del proyecto. Se instancia de forma estática dentro de la clase SharedObjects para que sea accesible desde toda al App.
    /// </summary>
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

        //public bool CompruebaConexionCloudTest()
        //{
        //    const string connectionUri = "mongodb+srv://josemankapi:EAIJwykKmFJZe9mz@cluster0.zxd0ycl.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";
        //    var settings = MongoClientSettings.FromConnectionString(connectionUri);
        //    settings.ServerApi = new ServerApi(ServerApiVersion.V1);
        //    var client = new MongoClient(settings);
        //    try
        //    {
        //        var result = client.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
        //        Console.WriteLine("Pinged your deployment. You successfully connected to MongoDB!");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //        return false;
        //    }
        //    return true;
        //}

        public bool ConectarBD(string dbName)
        {
            try
            {
                this._clienteDB = new MongoClient(this._connectionString);
                this._dbTPV = this._clienteDB.GetDatabase(dbName);
                return IsBDConnected();
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsBDConnected()
        {
            try
            {
                var result = _dbTPV!.RunCommand<BsonDocument>(new BsonDocument("ping", 1));
                return result.Contains("ok") && result["ok"].ToDouble() == 1.0;
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


        public List<T> LeerObjetosTipo<T>()
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



        public List<T> BuscarObjeto<T>(T objeto, string nombreCampoClave)
        {
            try
            {
                IMongoCollection<T> collection = this._dbTPV!.GetCollection<T>(typeof(T).Name);
                FilterDefinition<T> filter = Builders<T>.Filter.Eq(nombreCampoClave, objeto!.GetType().GetProperty(nombreCampoClave)?.GetValue(objeto));
                return collection.Find(filter).ToList();
            }
            catch (Exception)
            {
                return new List<T>();
            }
        }

        public List<T> BuscarObjetosString<T>(string propiedad, string valor)
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

        public List<T> BuscarObjetosStringAndString<T>(string propiedad1, string valor1, string propiedad2, string valor2)
        {
            try
            {
                IMongoCollection<T> collection = this._dbTPV!.GetCollection<T>(typeof(T).Name);
                FilterDefinition<T> filter = Builders<T>.Filter.And(
                    Builders<T>.Filter.Eq(propiedad1, valor1),
                    Builders<T>.Filter.Eq(propiedad2, valor2)
                );
                return collection.Find(filter).ToList();
            }
            catch (Exception)
            {
                return new List<T>();
            }
        }

        public List<T> BuscarObjetosStringAndInt<T>(string propiedad1, string valor1, string propiedad2, int valor2)
        {
            try
            {
                IMongoCollection<T> collection = this._dbTPV!.GetCollection<T>(typeof(T).Name);
                FilterDefinition<T> filter = Builders<T>.Filter.And(
                    Builders<T>.Filter.Eq(propiedad1, valor1),
                    Builders<T>.Filter.Eq(propiedad2, valor2)
                );
                return collection.Find(filter).ToList();
            }
            catch (Exception)
            {
                return new List<T>();
            }
        }

        public List<T> BuscarObjetosBool<T>(string propiedad, bool valor)
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

        public List<T> BuscarObjetosInt<T>(string propiedad, int valor)
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

        public List<T> BuscarObjetosIntAndInt<T>(string propiedad1, int valor1, string propiedad2, int valor2)
        {
            try
            {
                IMongoCollection<T> collection = this._dbTPV!.GetCollection<T>(typeof(T).Name);
                FilterDefinition<T> filter = Builders<T>.Filter.And(
                    Builders<T>.Filter.Eq(propiedad1, valor1),
                    Builders<T>.Filter.Eq(propiedad2, valor2)
                );
                return collection.Find(filter).ToList();
            }
            catch (Exception)
            {
                return new List<T>();
            }
        }

        public List<T> LeerObjetosTest<T>(List<T> objetos, string propiedad)
        {
            try
            {
                IMongoCollection<T> collection = this._dbTPV!.GetCollection<T>(typeof(T).Name);
                List<T> listaObjetos = new List<T>();
                foreach (T objeto in objetos)
                {
                    FilterDefinition<T> filter = Builders<T>.Filter.Eq(propiedad, objeto!.GetType().GetProperty(propiedad)?.GetValue(objeto));
                    listaObjetos.Add(collection.Find(filter).FirstOrDefault());
                }
                return listaObjetos;
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

        public bool ActualizarObjeto<T>(T objeto)
        {
            try
            {
                IMongoCollection<T> collection = this._dbTPV!.GetCollection<T>(typeof(T).Name);

                // Usa reflexión para obtener la propiedad Id
                var idProperty = typeof(T).GetProperty("Id");
                if (idProperty == null)
                {
                    throw new ArgumentException($"El objeto de tipo '{typeof(T).Name}' no tiene una propiedad 'Id'.");
                }

                // Obtiene el valor de la propiedad Id
                var idValue = idProperty.GetValue(objeto);
                if (idValue == null)
                {
                    throw new ArgumentException($"El valor de la propiedad 'Id' no puede ser nulo.");
                }

                // Crea un filtro para encontrar el documento con el Id especificado
                var filter = Builders<T>.Filter.Eq("Id", idValue);

                // Realiza la actualización del documento
                var result = collection.ReplaceOne(filter, objeto);

                // Verifica si la operación fue exitosa
                return result.IsAcknowledged && result.ModifiedCount > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }


        //public bool ActualizarObjeto<T>(T objeto, string propiedad)
        //{
        //    try
        //    {
        //        IMongoCollection<T> collection = this._dbTPV!.GetCollection<T>(typeof(T).Name);
        //        FilterDefinition<T> filter = Builders<T>.Filter.Eq(propiedad, objeto!.GetType().GetProperty(propiedad)?.GetValue(objeto));
        //        collection.ReplaceOne(filter, objeto);
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
        public int SelectMAXInt(string nombreClase, string nombreMiembroPrivado)
        {
            try
            {
                IMongoCollection<BsonDocument> collection = this._dbTPV!.GetCollection<BsonDocument>(nombreClase);
                var sort = Builders<BsonDocument>.Sort.Descending(nombreMiembroPrivado);
                var document = collection.Find(new BsonDocument()).Project(Builders<BsonDocument>.Projection.Include(nombreMiembroPrivado)).Sort(sort).FirstOrDefault();
                if (document != null)
                {
                    return document.GetValue(nombreMiembroPrivado).AsInt32;
                }
            }
            catch (Exception)
            {
                return -1;
            }
            return 0;
        }
        public string SelectMAXTicketT(int _ejercicio, int _numTienda)
        {
            try
            {
                IMongoCollection<Ticket> collection = this._dbTPV!.GetCollection<Ticket>(typeof(Ticket).Name);
                var filterBuilder = Builders<Ticket>.Filter;
                //var filter = filterBuilder.Eq("_ejercicio", _ejercicio) & filterBuilder.Eq("_tienda", _numTienda) & filterBuilder.Regex("_numTicket", new BsonRegularExpression("^T\\d+$"));
                var filter = filterBuilder.Eq("_ejercicio", _ejercicio) & filterBuilder.Eq("_tienda", _numTienda);
                var sort = Builders<Ticket>.Sort.Descending("_numTicket");
                var document = collection.Find(filter).Project(Builders<Ticket>.Projection.Include("_numTicket")).Sort(sort).FirstOrDefault();
                if (document != null)
                {
                    return document.GetElement("_numTicket").Value.AsString;
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
            return string.Empty;
        }

        public bool DesconectarBD()
        {
            try
            {
                this._clienteDB = null;
            }
            catch (Exception) { return false; }
            return true;
        }
        public int ContarObjetos<T>()
        {
            try
            {
                IMongoCollection<T> collection = this._dbTPV!.GetCollection<T>(typeof(T).Name);
                return (int)collection.CountDocuments(new BsonDocument());
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
