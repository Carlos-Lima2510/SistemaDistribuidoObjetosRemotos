using Conversor;
using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Windows.Forms;
using System.Configuration;

namespace Cliente
{
    public partial class Form1 : Form
    {
        private IConversor conversor;
        public Form1()
        {
            InitializeComponent();
            InicializarRemoting();
        }

        private void InicializarRemoting()
        {
            try
            {
                if (ChannelServices.RegisteredChannels.Length == 0)
                {
                    TcpChannel channel = new TcpChannel();
                    ChannelServices.RegisterChannel(channel, false);
                }

                conversor = (IConversor)Activator.GetObject(
                    typeof(IConversor),
                    $"tcp://{ConfigurationManager.AppSettings["host"]}:{Int32.Parse(ConfigurationManager.AppSettings["port"])}/ConversorService");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar: " + ex.Message);
            }
        }

        private void btnConvertir_Click(object sender, EventArgs e)
        {
            try
            {
                if (conversor == null)
                {
                    MessageBox.Show("No hay conexión con el servidor remoto. Intente reiniciar la aplicación.",
                                    "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bool tieneEuros = !string.IsNullOrWhiteSpace(txtEuros.Text);
                bool tieneDolares = !string.IsNullOrWhiteSpace(txtDolares.Text);

                if (tieneEuros && tieneDolares)
                {
                    MessageBox.Show("Por favor, rellene solo uno de los campos (Euros o Dólares).",
                                    "Entrada inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!tieneEuros && !tieneDolares)
                {
                    MessageBox.Show("Debe introducir una cantidad en Euros o en Dólares.",
                                    "Falta información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                double cantidad;
                double resultado;

                if (tieneEuros)
                {
                    if (!double.TryParse(txtEuros.Text, out cantidad))
                    {
                        MessageBox.Show("El valor de Euros no es válido.", "Error de formato",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    resultado = conversor.ConvertEurToUsd(cantidad);
                    txtDolares.Text = resultado.ToString("F2"); 
                }
                else
                {
                    if (!double.TryParse(txtDolares.Text, out cantidad))
                    {
                        MessageBox.Show("El valor de Dólares no es válido.", "Error de formato",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    resultado = conversor.ConvertUsdToEur(cantidad);
                    txtEuros.Text = resultado.ToString("F2");
                }
            }
            catch (RemotingException)
            {
                MessageBox.Show("Error de comunicación con el servidor. Asegúrese de que esté en ejecución.",
                                "Fallo de red", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FormatException)
            {
                MessageBox.Show("Por favor, ingrese un número válido.",
                                "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al conectar con el servicio:\n{ex.Message}",
                                "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
