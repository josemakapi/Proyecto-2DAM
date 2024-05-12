using Microsoft.VisualBasic;
using Orient.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using TPV_WINDOWS.Modelo;

namespace TPV_WINDOWS.Controlador
{
    /// <summary>
    /// Si esta TPV está configurada como Master,
    /// Este proceso residente atiende las peticiones de número de ticket del proceso TPVBase tanto local como de otras TPV's
    /// </summary>
    public class TPVMasterOLD
    {
        private bool _isRunning;
        private Thread? _hilo;
        private readonly object _bloqueoInstancia = new object();
        private TcpListener? _tcpListener;
        private SemaphoreSlim bloqueo = new SemaphoreSlim(1, 1);
        public bool IsRunning { get { return _isRunning; }}

        public TPVMaster()
        {
            _isRunning = false;
            Iniciar();
        }

        public bool Iniciar()
        {
            if (!_isRunning)
            {      
                try
                {
                    this.Listener();
                } catch (Exception)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public void Parar()
        {
            if (_isRunning)
            {  
                _isRunning = false;
                MessageBox.Show("Cerrando procesos master");
            }
        }

        private int Listener()
        {
            this._isRunning = true;
            int estado = 1;
            _tcpListener = new TcpListener(IPAddress.Any, 12700);
            _tcpListener!.Start();
            MessageBox.Show("A la escucha de llamadas");
            while (_isRunning)
            {
                TcpClient? _cliente = null;
                try
                {
                    _cliente = _tcpListener.AcceptTcpClient();
                    //_cliente = await LlamaCliente();
                    MessageBox.Show("Cliente conectado");
                    //En la siguiente línea logramos que que el hilo principal siga aceptando nuevas conexiones mientras se procesan las solicitudes de los clientes en hilos separados.
                    Thread hiloCliente = new Thread(async() => await ProcesarCliente(_cliente)); 
                    hiloCliente.Start();
                }
                catch (InvalidOperationException) { return -2; }
                catch (Exception)
                {
                    return -1;
                }
                finally
                {
                    _cliente!.Close();
                }
            }
            _tcpListener!.Stop();
            estado = 0;
            return estado;
        }

        private async Task<TcpClient> LlamaCliente()
        {
            TcpClient? cliente = null;
            try
            {
                cliente = await _tcpListener!.AcceptTcpClientAsync();
                return cliente!;
            }
            catch (Exception) { return cliente!; }
            
        }

        private async Task<bool> ProcesarCliente(TcpClient cliente)
        {
            try
            {
                NetworkStream stream = cliente.GetStream();
                await bloqueo.WaitAsync();
                // Aquí buscaremos el último nº de ticket
                try //Zona crítica, lo que controlaremos con un semáforo
                {
                    string lastTicketNum = "Aquí debería ir el número de ticket";
                    byte[] buffer = Encoding.UTF8.GetBytes(lastTicketNum);
                    await stream.WriteAsync(buffer, 0, buffer.Length);
                }
                finally
                {
                    bloqueo.Release();
                }
                
                stream.Close();
                cliente.Close();
                return true;
            }
            catch (Exception){return false;}
        }
    }
}
