using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AnnarComMICROSESV60.Dao;
using AnnarComMICROSESV60.Utilities;
using AnnarComMICROSESV60.Properties;
using System.Drawing.Drawing2D;
using SerialPortTerminal;
using static System.Net.Mime.MediaTypeNames;
using System.IO.Ports;
using System.Runtime;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading;
using Npgsql;
using static AnnarComMICROSESV60.Dao.ConnectionDB;
using System.Configuration;
using System.Windows.Forms.DataVisualization.Charting;
using Application = System.Windows.Forms.Application;
using System.Data;

namespace AnnarComMICROSESV60.Forms
{
    #region Public Enumerations
    public enum DataMode { Text, Hex }
    public enum LogMsgType { Incoming, Outgoing, Normal, Warning, Error };
    #endregion


    public partial class Resultados : Form
    {



        ConnectionDB.ResultadoQuery resultadoQuery = new ConnectionDB.ResultadoQuery();
        ConnectionDB.ResultadoStatement resultadoStatement = new ConnectionDB.ResultadoStatement();
        DbQuery dbQuery = new DbQuery();
        RegistroLog log = new RegistroLog();

        public string nombreLog = InterfaceConfig.nombreLog;
        public bool booleanTimer = false;

        public enum EnumEstados { Ok, Info, Process, Warning, Error }



        #region Local Variables

        public string rutaGuardarImagenHistograma = ConfigurationManager.AppSettings["RutaGuardarImagenHistograma"].ToString();
        public string SobreEscribeResultado = ConfigurationManager.AppSettings["SobreEscribeResultado"].ToString();
        public string utilizacaratulaporequipos = ConfigurationManager.AppSettings["utilizacaratulaporequipos"].ToString();
        public string tiempo = "2";

        //Para metodo decimales
        // public string AdicionarUnidadMedida = ConfigurationManager.AppSettings["AdicionarUnidadMedida"].ToString();

        // The main control for communicating through the RS-232 port
        private SerialPort comport = new SerialPort();

        // Various colors for logging info
        private Color[] LogMsgTypeColor = { Color.Blue, Color.Green, Color.Black, Color.Orange, Color.Red };

        // Temp holder for whether a key was pressed
        private bool KeyHandled = false;

        private Settings settings = Settings.Default;
        public string ERROR = "ERROR";
        public bool existe = false;

        #endregion

        public string[] strValoresLimites = new string[50];

        public string[] ArrPaquete = new string[91];
        public string[] ArrPaqueteResultado = new string[3000];
        public string[] ArrPaqueteETX = new string[3000];
        public string[] ArrPaqueteETB = new string[3000];
        public string vCodPaciente = "";
        public string strExamen = "";
        public string strregExa = "";
        public string StrLineaEstudio = "";
        public string timeractivo = "N";
        public string strLineaResultado = "";
        public string iniciaRecepcion = "N";
        public string strLineaRestante = "";
        public string registraevento = "N";
        public string banderaquery = "N";
        public int Seq = 0;
        int iArr = 0;
        public int consecutive = 0;

        public int cantdigitos;
        public string NroGrupo;
        public string NroTapa;
        public bool tubo_ind;
        public string CharEnviado;
        public int inc;
        public int incr;
        public NpgsqlDataReader reader;
        public NpgsqlDataReader reader2;
        public string EscribeResultado;

        public const char EOT = (char)4;
        public const char US = (char)31;
        public const char RS = (char)30;
        public const char ACK = (char)6;
        public const char NAK = (char)21;
        public const char ENQ = (char)5;
        public const char STX = (char)2;
        public const char ETX = (char)3;
        public const char ETB = (char)23;
        public const char LF = '\n';
        public const char CR = '\r';
        public const char FS = (char)28;
        public const char SUB = (char)26;
        public string nroDocumento = string.Empty;
        public string vCodSede = string.Empty;
        public string tipoEspecie = string.Empty;
        public string tipoRaza = string.Empty;
        public bool valUnidad;

   


        public class T
        {
            public const byte ENQ = 5;
            public const byte ACK = 6;
            public const byte NAK = 21;
            public const byte EOT = 4;
            public const byte ETX = 3;
            public const byte ETB = 23;
            public const byte STX = 2;
            public const byte NEWLINE = 10;
            public static byte[] ACK_BUFF = { ACK };
            public static byte[] ENQ_BUFF = { ENQ };
            public static byte[] NAK_BUFF = { NAK };
            public static byte[] EOT_BUFF = { EOT };
        }


        public Resultados()
        {
            InitializeComponent();

            // Load user settings
            settings.Reload();

            // Restore the users settings
            InitializeControlValues();

            // Enable/disable controls based on the current state
            EnableControls();

            comport.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
            comport.PinChanged += new SerialPinChangedEventHandler(comport_PinChanged);

        }

        private void timerIntervalos_Tick(object sender, EventArgs e)
        {
            if ((timeractivo.Contains("S")) && (iniciaRecepcion.Contains("N")))
            {
                timeractivo = "N";

                if (ArrPaquete[inc] != null)
                {
                    CharEnviado = "ENQ";
                    SendData(ENQ.ToString());
                }
                else
                {
                    timeractivo = "S";
                }
            }
        }

        private void Terminal_Load(object sender, EventArgs e)
        {
            tmrCheckComPorts.Interval = Convert.ToInt32(settings.VelocidadBuffer);


            EsconderSubmenu();

         


            MensajesFlowLP($"Interfaz iniciada", EnumEstados.Ok);


            RedondearBordes(pnlSubMenu, 35);
            RedondearBordes(pictureBox1, 20);
            RedondearBordes(pictureBox2, 20);
            RedondearBordes(pictureBox3, 20);
            RedondearBordes(pictureBox4, 20);
            RedondearBordes(pictureBox5, 20);
            RedondearBordes(pictureBox6, 20);


           
                VariablesGlobal.Resultados = true;
             

        }

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // If the com port has been closed, do nothing
            if (!comport.IsOpen) return;

            // This method will be called when there is data waiting in the port's buffer

            // Determain which mode (string or binary) the user is in
            if (CurrentDataMode == DataMode.Text)
            {
                // Read all the data waiting in the buffer
                string data = comport.ReadExisting();

                // Display the text to the user in the terminal
                Log(LogMsgType.Incoming, data);
            }
            else
            {
                // Obtain the number of bytes waiting in the port's buffer
                int bytes = comport.BytesToRead;

                // Create a byte array buffer to hold the incoming data
                byte[] buffer = new byte[bytes];


                // Read the data from the port and store it in our buffer
                comport.Read(buffer, 0, bytes);

                // Show the user the incoming data in hex format
                Log(LogMsgType.Incoming, ByteArrayToHexString(buffer));
            }
        }

        private void mostrarSubmenu(Panel panel)
        {
            //Alternar la visibilidad de los submenus
            if (panel.Visible == false)
            {
                EsconderSubmenu();
                panel.Visible = true;
                flpContenedorResul.Location = new Point(15, 247);
            }
            else
            {
                panel.Visible = false;
                flpContenedorResul.Location = new Point(15, 100);

            }
        }

        private void EsconderSubmenu()
        {
            if (pnlSubMenu.Visible == true) pnlSubMenu.Visible = false;
            flpContenedorResul.Location = new Point(15, 73);

        }


        //Metodo para mostrar los mensajes en el FlowLayoutPanel
        public void MensajesFlowLP(string msg, EnumEstados estado)
        {
            Button nuevoButton = new Button();
            string fechaActual = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            //nuevoButton.Dock = DockStyle.Top;
            nuevoButton.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            nuevoButton.ImageAlign = ContentAlignment.MiddleLeft;
            nuevoButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            nuevoButton.Width = 225;
            nuevoButton.Height = 10;
            nuevoButton.FlatStyle = FlatStyle.Flat;
            nuevoButton.FlatAppearance.BorderColor = Color.White;
            nuevoButton.FlatAppearance.BorderSize = 0;
            nuevoButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            nuevoButton.FlatAppearance.MouseOverBackColor = Color.Transparent;

            switch (estado)
            {
                case EnumEstados.Ok:
                    nuevoButton.Image = Resources.OkM;
                    nuevoButton.AutoSize = true;
                    nuevoButton.Text = $" {fechaActual}: " + msg;
                    nuevoButton.ForeColor = Color.Black;
                    nuevoButton.TextAlign = ContentAlignment.MiddleLeft; // Alinea el texto a la izquierda
                    break;

                case EnumEstados.Info:
                    nuevoButton.Image = Resources.Null;
                    nuevoButton.AutoSize = true;
                    nuevoButton.Text = msg;
                    nuevoButton.ForeColor = Color.Black;
                    nuevoButton.TextAlign = ContentAlignment.MiddleLeft; // Alinea el texto a la izquierda
                    break;

                case EnumEstados.Process:
                    nuevoButton.Image = Resources.InterpretandoM;
                    nuevoButton.AutoSize = true;
                    nuevoButton.Text = $" {fechaActual}: " + msg;
                    nuevoButton.ForeColor = Color.Black;
                    break;

                case EnumEstados.Warning:
                    nuevoButton.Image = Resources.EsperandoM;
                    nuevoButton.AutoSize = true;
                    nuevoButton.Text = $" {fechaActual}: " + msg;
                    nuevoButton.ForeColor = Color.Black;
                    break;

                case EnumEstados.Error:
                    nuevoButton.Image = Resources.ErrorM;
                    nuevoButton.AutoSize = true;
                    nuevoButton.Text = $" {fechaActual}: " + msg;
                    nuevoButton.ForeColor = Color.Black;
                    break;

                default:
                    break;
            }

            flpContenedorResul.Invoke(new EventHandler(delegate
            {
                flpContenedorResul.Controls.Add(nuevoButton);

                //Hacer scroll hacia abajo para mostrar el contenido más reciente
                flpContenedorResul.AutoScrollPosition = new Point(0, flpContenedorResul.VerticalScroll.Maximum);
            }));
        }

        private void flpContenedorResul_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnOpenPort_Click(object sender, EventArgs e)
        {
            SaveSettings();

         
                bool error = false;

                // If the port is open, close it.
                if (comport.IsOpen)
                {


                    comport.Close();
                }
                else
                {
                    // Set the port's settings
                    comport.BaudRate = int.Parse(cmbBaudRate.Text);
                    comport.DataBits = int.Parse(cmbDataBits.Text);
                    comport.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cmbStopBits.Text);
                    comport.Parity = (Parity)Enum.Parse(typeof(Parity), cmbParity.Text);
                    comport.PortName = cmbPortName.Text;



                    try
                    {
                        // Open the port
                        comport.Open();
                    }
                    catch (UnauthorizedAccessException) { error = true; }
                    catch (IOException) { error = true; }
                    catch (ArgumentException) { error = true; }




                    if (error)
                    {
                        DialogResult result;
                        using (var msFomr = new FormMessageBox("No se puede abrir el puerto.", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Error))
                            result = msFomr.ShowDialog();
                       
                    }

                    else
                    {
                        // Show the initial pin states
                        UpdatePinState();
                        chkDTR.Checked = comport.DtrEnable;
                        chkRTS.Checked = comport.RtsEnable;
                    }
                }

                // Change the state of the form's controls
                EnableControls();

                // If the port is open, send focus to the send data box
                if (comport.IsOpen)
                {
                     VariablesGlobal.Conectar = true;

                if (chkClearOnOpen.Checked) ClearTerminal();
                    log.RegistraEnLog("Interfaz Conectada", nombreLog);
                    timerIntervalos.Interval = Convert.ToInt32(tiempo) * 1000;
                    //Timer1.Enabled = true;
                    timeractivo = "S";
                }
                else
                {
                    VariablesGlobal.Conectar = false;

                    log.RegistraEnLog("Interfaz Desconectada", nombreLog);
                     timerIntervalos.Enabled = false;
                }
         


        }
        private void ClearTerminal()
        {
            //Control parent = btn.Parent;

            //if (parent is Panel)
            //{
            //    Panel panel = (Panel)parent;
            //    int btnIndex = panel.Controls.IndexOf(btn);
            //    for (int i = btnIndex; i >= 0; i--)
            //    {
            //        Control control = panel.Controls[i];
            //        if (control is Button)
            //        {
            //            break;
            //        }
            //        panel.Controls.Remove(control);
            //    }
            //}

        }
        private void EnableControls()
        {
            pnlSubMenu.Enabled = !comport.IsOpen;

            if (comport.IsOpen)
            {
                btnOpenPort.BackgroundImage = Resources.ConectarDesconectar_2x;
                //btnOpenPort.BackgroundImage = Resources.Conectar_2x;
            }
            else
            {
                btnOpenPort.BackgroundImage = Resources.Conectar_2x;
                //btnOpenPort.BackgroundImage = Resources.ConectarDesconectar_2x;
            }
        }

        private void pnlSubMenu_Paint(object sender, PaintEventArgs e)
        {

        }
        private void RedondearBordes(Control control, int radio)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radio * 2, radio * 2, 180, 90); // Esquina superior izquierda
            path.AddArc(control.Width - radio * 2, 0, radio * 2, radio * 2, 270, 90); // Esquina superior derecha
            path.AddArc(control.Width - radio * 2, control.Height - radio * 2, radio * 2, radio * 2, 0, 90); // Esquina inferior derecha
            path.AddArc(0, control.Height - radio * 2, radio * 2, radio * 2, 90, 90); // Esquina inferior izquierda
            control.Region = new Region(path);
        }

        private void btnPuerto_Click(object sender, EventArgs e)
        {
            mostrarSubmenu(pnlSubMenu);
        }

        void comport_PinChanged(object sender, SerialPinChangedEventArgs e)
        {
            // Show the state of the pins
            UpdatePinState();
        }

        private void UpdatePinState()
        {
            this.Invoke(new ThreadStart(() =>
            {
                // Show the state of the pins
                chkCD.Checked = comport.CDHolding;
                chkCTS.Checked = comport.CtsHolding;
                chkDSR.Checked = comport.DsrHolding;
            }));
        }


        #region Local Methods

        /// <summary> Save the user's settings. </summary>
        private void SaveSettings()
        {
            InterfaceConfig.baudRate = int.Parse(cmbBaudRate.Text);
            InterfaceConfig.dataBits = int.Parse(cmbDataBits.Text);
            InterfaceConfig.dataMode = CurrentDataMode.ToString();
            InterfaceConfig.portName = cmbPortName.Text;

            settings.BaudRate = int.Parse(cmbBaudRate.Text);
            settings.DataBits = int.Parse(cmbDataBits.Text);
            settings.DataMode = CurrentDataMode;
            settings.Parity = (Parity)Enum.Parse(typeof(Parity), cmbParity.Text);
            settings.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cmbStopBits.Text);
            settings.PortName = cmbPortName.Text;
            settings.ClearOnOpen = chkClearOnOpen.Checked;
            settings.ClearWithDTR = chkClearWithDTR.Checked;

            settings.Save();
        }
        private void RefreshComPortList()
        {
            // Determain if the list of com port names has changed since last checked
            string selected = RefreshComPortList(cmbPortName.Items.Cast<string>(), cmbPortName.SelectedItem as string, comport.IsOpen);

            // If there was an update, then update the control showing the user the list of port names
            if (!String.IsNullOrEmpty(selected))
            {
                cmbPortName.Items.Clear();
                cmbPortName.Items.AddRange(OrderedPortNames());
                cmbPortName.SelectedItem = selected;
            }
        }
        private string[] OrderedPortNames()
        {
            // Just a placeholder for a successful parsing of a string to an integer
            int num;

            // Order the serial port names in numberic order (if possible)
            return SerialPort.GetPortNames().OrderBy(a => a.Length > 3 && int.TryParse(a.Substring(3), out num) ? num : 0).ToArray();
        }
        private string RefreshComPortList(IEnumerable<string> PreviousPortNames, string CurrentSelection, bool PortOpen)
        {
            // Create a new return report to populate
            string selected = null;

            // Retrieve the list of ports currently mounted by the operating system (sorted by name)
            string[] ports = SerialPort.GetPortNames();

            // First determain if there was a change (any additions or removals)
            bool updated = PreviousPortNames.Except(ports).Count() > 0 || ports.Except(PreviousPortNames).Count() > 0;

            // If there was a change, then select an appropriate default port
            if (updated)
            {
                // Use the correctly ordered set of port names
                ports = OrderedPortNames();

                // Find newest port if one or more were added
                string newest = SerialPort.GetPortNames().Except(PreviousPortNames).OrderBy(a => a).LastOrDefault();

                // If the port was already open... (see logic notes and reasoning in Notes.txt)
                if (PortOpen)
                {
                    if (ports.Contains(CurrentSelection)) selected = CurrentSelection;
                    else if (!String.IsNullOrEmpty(newest)) selected = newest;
                    else selected = ports.LastOrDefault();
                }
                else
                {
                    if (!String.IsNullOrEmpty(newest)) selected = newest;
                    else if (ports.Contains(CurrentSelection)) selected = CurrentSelection;
                    else selected = ports.LastOrDefault();
                }
            }

            // If there was a change to the port list, return the recommended default selection
            return selected;
        }

        /// <summary> Populate the form's controls with default settings. </summary>
        private void InitializeControlValues()
        {
            cmbParity.Items.Clear(); cmbParity.Items.AddRange(Enum.GetNames(typeof(Parity)));
            cmbStopBits.Items.Clear(); cmbStopBits.Items.AddRange(Enum.GetNames(typeof(StopBits)));

            cmbParity.Text = settings.Parity.ToString();
            cmbStopBits.Text = settings.StopBits.ToString();
            cmbDataBits.Text = settings.DataBits.ToString();
            cmbParity.Text = settings.Parity.ToString();
            cmbBaudRate.Text = settings.BaudRate.ToString();
            CurrentDataMode = settings.DataMode;

            RefreshComPortList();

            chkClearOnOpen.Checked = settings.ClearOnOpen;
            chkClearWithDTR.Checked = settings.ClearWithDTR;

            // If it is still avalible, select the last com port used
            if (cmbPortName.Items.Contains(settings.PortName)) cmbPortName.Text = settings.PortName;
            else if (cmbPortName.Items.Count > 0) cmbPortName.SelectedIndex = cmbPortName.Items.Count - 1;
            else
            {
                MessageBox.Show(this, "There are no COM Ports detected on this computer.\nPlease install a COM Port and restart this app.", "No COM Ports Installed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        /// <summary> Enable/disable controls based on the app's current state. </summary>

        /// <summary> Send the user's data currently entered in the 'send' box.</summary>
        private void SendData(string strlinea)
        {
            if (CurrentDataMode == DataMode.Text)
            {
                // Send the user's text straight out the port
                comport.Write(strlinea);

                // Show in the terminal window the user's text
                Log(LogMsgType.Outgoing, strlinea + "\n");
            }
            else
            {
                try
                {
                    // Convert the user's string of hex digits (ex: B4 CA E2) to a byte array
                    byte[] data = HexStringToByteArray(strlinea);

                    // Send the binary data out the port
                    comport.Write(data, 0, data.Length);

                    // Show the hex digits on in the terminal window
                    Log(LogMsgType.Outgoing, ByteArrayToHexString(data) + "\n");
                }
                catch (FormatException)
                {
                    // Inform the user if the hex string was not properly formatted
                    Log(LogMsgType.Error, "Not properly formatted hex string: " + "\n");
                }
            }
            // txtSendData.SelectAll();
        }

        /// <summary> Log data to the terminal window. </summary>
        /// <param name="msgtype"> The type of message to be written. </param>
        /// <param name="msg"> The string containing the message to be shown. </param>
        /// 

        private void Log(LogMsgType msgtype, string msg)
        {
            string tipomsg = "";

            if (msgtype.ToString() == "Outgoing") { tipomsg = "Enviado"; }
            if (msgtype.ToString() == "Incoming") { tipomsg = "Recibido"; }

            if (CharEnviado == null)
            {
                CharEnviado = "";
            }

            if ((msgtype.ToString() == "Incoming") && (CharEnviado.Contains("ENQ")) && (msg.Contains(EOT)))
            {
                SendData(ACK.ToString());
            }

            if ((msgtype.ToString().Contains("Incoming")) && (CharEnviado.Contains("ENQ")) && (msg == ACK.ToString()) && (iniciaRecepcion.Contains("N")))
            {
                enviapaquete();
            }

            if ((msgtype.ToString().Contains("Incoming")) && (msg.Contains(ENQ.ToString())))
            {
                log.RegistraEnLog(" Inicio Recepcion de resultados --> ", InterfaceConfig.nombreLog);

                timeractivo = "N";
                incr = 0;
                iniciaRecepcion = "S";
                CharEnviado = "ACK";
                strLineaResultado = "";
                SendData(ACK.ToString());
            }

            if (msgtype.ToString() == "Incoming")
            {
                ///AQUI GUARDAR PAQUETE RECIBIDO
                strLineaResultado = strLineaResultado + msg.ToString();
                SendData(ACK.ToString());

            }

            if ((msgtype.ToString() == "Incoming") && (msg.Contains(EOT)))
            {

                timeractivo = "S";
                CharEnviado = "";
                iniciaRecepcion = "N";
                incr = 0;

                strLineaResultado = strLineaRestante + strLineaResultado;

                ArrPaqueteETX = strLineaResultado.Split(ETX);

                strLineaRestante = ArrPaqueteETX[ArrPaqueteETX.Length - 1];

                try
                {
                    strLineaResultado = "";
                    for (var x = 0; x <= ArrPaqueteETX.Length - 2; x++)
                    {

                        strLineaResultado = strLineaResultado + ArrPaqueteETX[x].Split(CR)[1].Trim();

                    }
                }
                catch (FormatException)
                {

                    log.RegistraEnLog(" Error cargando paquete ArrPaqueteETX ", InterfaceConfig.nombreLog);
                }


                // timeractivo = "S";
                CharEnviado = "";
                // iniciaRecepcion = "N";
                incr = 0;
                try
                {

                    for (var x = 0; x <= ArrPaqueteResultado.Length - 1; x++)
                    {

                        ArrPaqueteResultado[x] = null;

                    }
                }
                catch (FormatException)
                {

                    log.RegistraEnLog(" Error limpiando Arreglo  ArrPaqueteResultado[x] ", InterfaceConfig.nombreLog);
                }
                // strLineaResultado = strLineaResultado.Replace(STX.ToString(), "");
                //strLineaResultado = strLineaResultado.Replace(ENQ.ToString(), "");

                strLineaResultado = strLineaResultado.Replace(ETB.ToString(), "");
                // strLineaResultado = strLineaResultado.Replace(EOT.ToString(), "");

                ArrPaqueteResultado = strLineaResultado.Split(STX);

                for (var x = 0; x <= ArrPaqueteResultado.Length - 1; x++)
                {

                    log.RegistraEnLog(" Paquete Recibido " + Convert.ToString(x) + " --> " + ArrPaqueteResultado[x], InterfaceConfig.nombreLog);

                }
                ProcesarResultadosVet(ArrPaqueteResultado);
                strLineaResultado = "";





                for (var x = 0; x <= ArrPaqueteETX.Length - 1; x++)
                {

                    ArrPaqueteETX[x] = null;

                }
            }

            //rtfTerminal.Invoke(new EventHandler(delegate
            //{
            //    log.RegistraEnLog(tipomsg + " --> " + msg, "Interfaz_Tramas_" + InterfaceConfig.nombreEquipo);
            //    rtfTerminal.SelectedText = string.Empty;
            //    rtfTerminal.Clear();
            //    rtfTerminal.SelectionFont = new Font(rtfTerminal.SelectionFont, FontStyle.Bold);
            //    rtfTerminal.SelectionColor = LogMsgTypeColor[(int)msgtype];
            //    rtfTerminal.AppendText(msg);
            //    rtfTerminal.AppendText("Esperando Resultados...");
            //    rtfTerminal.ScrollToCaret();
            //}));
        }

        /// <summary> Convert a string of hex digits (ex: E4 CA B2) to a byte array. </summary>
        /// <param name="s"> The string containing the hex digits (with or without spaces). </param>
        /// <returns> Returns an array of bytes. </returns>
        private byte[] HexStringToByteArray(string s)
        {
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }

        /// <summary> Converts an array of bytes into a formatted string of hex digits (ex: E4 CA B2)</summary>
        /// <param name="data"> The array of bytes to be translated into a string of hex digits. </param>
        /// <returns> Returns a well formatted string of hex digits with spacing. </returns>
        private string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3, ' '));
            return sb.ToString().ToUpper();
        }
        #endregion

        #region Local Properties
        private DataMode CurrentDataMode
        {
            get
            {
                if (rbHex.Checked) return DataMode.Hex;
                else return DataMode.Text;
            }
            set
            {
                if (value == DataMode.Text) rbText.Checked = true;
                else rbHex.Checked = true;
            }
        }
        #endregion

     
        private void lnkAbout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Show the user the about dialog
            
        }

        public string GetCheckSum(string frame)
        {
            string checksum = "00";

            int byteVal = 0;
            int sumOfChars = 0;
            bool complete = false;


            for (int idx = 0; idx < frame.Length; idx++)
            {
                byteVal = frame[idx];

                switch (byteVal)
                {
                    case T.STX:
                        sumOfChars = 0;
                        break;
                    case T.ETX:
                    case T.ETB:
                        sumOfChars += byteVal;
                        complete = true;
                        break;
                    default:
                        sumOfChars += byteVal;
                        break;
                }

                if (complete)
                {
                    break;
                }
            }

            if (sumOfChars > 0)
            {

                checksum = Convert.ToString(sumOfChars % 256, 16).ToUpper();
            }
            return Convert.ToString((checksum.Length == 1) ? "0" + checksum : checksum);
        }

        private void enviapaquete() //enviapaquete no implementa persistencia, pero queda la codificacion para hacerlo si amerita
        {
            if (ArrPaquete[inc] != null)
            {
                byte[] hexData = System.Text.Encoding.ASCII.GetBytes(STX + ArrPaquete[inc] + '\r' + ETX);

                //' After calling the function GetCheckSum the variable will 
                //' contain &H30 using your test data
                string checkSum = GetCheckSum(STX + ArrPaquete[inc] + '\r' + ETX);

                SendData(STX + ArrPaquete[inc] + '\r' + ETX + checkSum + "\r" + "\n");
                iniciaRecepcion = "N";

                inc = inc + 1;
            }
            else
            {
                CharEnviado = "EOT";
                SendData(EOT.ToString());

                iniciaRecepcion = "N";

                int ancho = 0;
                ancho = strregExa.Length;

                if (ancho > 0)
                {
                    strregExa = strregExa.Substring(1, ancho - 1);
                    ancho = strExamen.Length;
                    strExamen = strExamen.Substring(0, ancho - 1);

                    log.RegistraEnLog("Cambia Estado a Enviado paciente_examenes Paciente: " + vCodPaciente, "Interfaz_" + InterfaceConfig.nombreEquipo);
                    log.RegistraEnLog("Cambia estado en paciente_examenes" + vCodPaciente, "Interfaz_" + InterfaceConfig.nombreEquipo);

                    string strCambiaEstado = string.Format("update paciente_examenes set enviado_interfaz= 'S' where paciente_cod = '{0}' and examen in({1}) and reg_exa  in({2})", vCodPaciente, strExamen, strregExa);

                    //Por si se trabaja desde la persistencia
                    //resultadoStatement = dbQuery.UpdatePaquete(vCodPaciente, strExamen, strregExa);
                    //if (resultadoStatement.Resultado.Equals(ERROR)) return;

                    NpgsqlConnection cnn3 = new NpgsqlConnection(InterfaceConfig.StrCadenaConeccion);
                    NpgsqlCommand comsql = new NpgsqlCommand(strCambiaEstado, cnn3);

                    log.RegistraEnLog("Sentencia " + strCambiaEstado, "Interfaz_" + InterfaceConfig.nombreEquipo);
                    log.RegistraEnLog("Examenes " + strExamen + "  " + strregExa, "Interfaz_" + InterfaceConfig.nombreEquipo);
                    cnn3.Open();
                    cnn3.Close();

                    vCodPaciente = "";
                    strregExa = "";
                    strExamen = "";
                    log.RegistraEnLog("Fin Cambia estado en paciente_examenes" + vCodPaciente, "Interfaz_" + InterfaceConfig.nombreEquipo);

                    for (var xx = 0; xx <= ArrPaquete.Length - 1; xx++)
                    {
                        ArrPaquete[xx] = null;
                    }

                }
                else
                {
                    log.RegistraEnLog("Sin Registros", "Interfaz_" + InterfaceConfig.nombreEquipo);
                }
                timeractivo = "S";
                inc = 0;

                for (var xx = 0; xx <= ArrPaquete.Length - 1; xx++)
                {
                    ArrPaquete[xx] = null;
                }

            }
        }


        public string ProcesarResultadosVet(string[] PaqueteResultado)
        {
            var arrayTubos = new List<string>();
            //int index = 0;

            log.RegistraEnLog("Paquete recibido: " + Convert.ToString(PaqueteResultado.Length), InterfaceConfig.nombreLog);
            //log.RegistraEnLog("TipoResultado configurado: " + tipoResultado, nombreLog);
            consecutive = 0;

            string strOrdenResultado = null;
            string strFechaToma = null;
            string strResultadoCuantitativo = "";
            string strResultadoCualitativo = "";

            int num = 0; //contador
            string[] parte1 = new string[3];

            for (var x = 0; x <= PaqueteResultado.Length - 1; x++)
            {

                if (!string.IsNullOrEmpty(PaqueteResultado[x]))
                {
                    string strlinea = PaqueteResultado[x];
                    string[] arrLinea = strlinea.Split('|');
                    string strencabezado;

                    log.RegistraEnLog("Linea " + Convert.ToString(x) + " -->" + PaqueteResultado[x], InterfaceConfig.nombreLog);

                    try
                    {
                        strencabezado = strlinea.Substring(1, 1);
                    }
                    catch (Exception) //contenia un ex sin usar
                    {
                        strencabezado = "";
                    }

                    if (strencabezado == "H")
                    {
                        Seq = 1;
                        iArr = 0;
                        strFechaToma = DateTime.Now.ToString("yyyyMMddHHmmss");
                    }


                    if (strencabezado == "O")
                    {
                        strOrdenResultado = arrLinea[2].ToString();
                        log.RegistraEnLog("Nro Tubo: " + strOrdenResultado, InterfaceConfig.nombreLog);
                        banderaquery = "N";



                    }

                    if (strencabezado == "P")
                    {
                        strResultadoCuantitativo = "";
                        strResultadoCualitativo = "";

                    }

                    if (strencabezado == "R")
                    {
                        try
                        {
                            banderaquery = "N";
                            string strVariable = "";
                            var arrVariable = arrLinea[2].Split('^');
                            strVariable = arrVariable[3];
                            string strResultado = arrLinea[3];
                            //string strTipoResultado = arrVariable[5]; //DOSE(NUMERICO) ó INTR(INTERPRETADO CUALITATIVO)

                            ///SOP-6627

                            log.RegistraEnLog($"Registrar resultados --> variable[{strVariable}], resultado[{strResultado}]", InterfaceConfig.nombreLog);
                            //  se comento  RegistraResultados(strOrdenResultado, strVariable, strResultado, "");
                            registraevento = "S";
                        }
                        catch (Exception ex)
                        {
                            log.RegistraEnLog("Error en trama Segmento R : " + ex.Message, InterfaceConfig.nombreLog);
                        }
                    }


                    if (strencabezado == "C")
                    {
                        if (InterfaceConfig.generarGraficas.Equals("S"))
                        {
                            if (existe)
                            {
                                string[] arrLinea0 = strlinea.Split('|');
                                string[] posicion = arrLinea0[3].Split('^');

                                string analito = posicion[1];

                                string Examenhomologado = null;
                                string vImagen = null;

                                try
                                {
                                    if (posicion[0].Equals("curve") && analito.Equals("PLT"))
                                    {
                                        num = num + 1;

                                        parte1[num] = posicion[4];

                                        if (num == 2)
                                        {
                                            resultadoQuery = dbQuery.HomologacionHistogramas(analito, strOrdenResultado);

                                            DataTable dtHomologacionI = resultadoQuery.Tabla;
                                            if (dtHomologacionI.Rows.Count > 0)
                                            {
                                                foreach (DataRow drHomologacionI in dtHomologacionI.Rows)
                                                {
                                                    Examenhomologado = drHomologacionI["examen_cod"].ToString();
                                                    vImagen = drHomologacionI["resultado"].ToString();
                                                }
                                            }

                                            string resTotal = parte1[1] + parte1[2];
                                            num = 0;

                                            //se comento     generarImagen(strOrdenResultado, resTotal, analito, Examenhomologado, vImagen);
                                        }
                                    }
                                    else if (posicion[0].Equals("curve") && posicion[1].Equals("RBC"))
                                    {
                                        num = num + 1;

                                        parte1[num] = posicion[4];

                                        if (num == 2)
                                        {
                                            resultadoQuery = dbQuery.HomologacionHistogramas(analito, strOrdenResultado);

                                            DataTable dtHomologacionI = resultadoQuery.Tabla;

                                            if (dtHomologacionI.Rows.Count > 0)
                                            {
                                                foreach (DataRow drHomologacionI in dtHomologacionI.Rows)
                                                {
                                                    Examenhomologado = drHomologacionI["examen_cod"].ToString();
                                                    vImagen = drHomologacionI["resultado"].ToString();
                                                }
                                            }

                                            string resTotal = parte1[1] + parte1[2];
                                            num = 0;

                                            // se comento   generarImagen(strOrdenResultado, resTotal, analito, Examenhomologado, vImagen);
                                        }
                                    }
                                    else if (posicion[0].Equals("curve") && posicion[1].Equals("WBC"))
                                    {
                                        num = num + 1;

                                        parte1[num] = posicion[4];

                                        if (num == 2)
                                        {
                                            resultadoQuery = dbQuery.HomologacionHistogramas(analito, strOrdenResultado);

                                            DataTable dtHomologacionI = resultadoQuery.Tabla;

                                            if (dtHomologacionI.Rows.Count > 0)
                                            {
                                                foreach (DataRow drHomologacionI in dtHomologacionI.Rows)
                                                {
                                                    Examenhomologado = drHomologacionI["examen_cod"].ToString();
                                                    vImagen = drHomologacionI["resultado"].ToString();
                                                }
                                            }

                                            string resTotal = parte1[1] + parte1[2];
                                            num = 0;

                                            // se comento    generarImagen(strOrdenResultado, resTotal, analito, Examenhomologado, vImagen);
                                        }
                                    }
                                    else
                                    {

                                    }
                                }
                                catch (Exception ex)
                                {
                                    log.RegistraEnLog("Error en trama Segmento C : " + ex.Message, InterfaceConfig.nombreLog);
                                }
                            }

                        }
                        else
                        {
                            log.RegistraEnLog("No esta habilitada la configuración para generar graficas. ", InterfaceConfig.nombreLog);
                        }
                    }

                }
            }

            //INSTANT C# NOTE: Inserted the following 'return' since all code paths must return a value in C#:
            return null;
        }

        private void tmrCheckComPorts_Tick(object sender, EventArgs e)
        {
            // checks to see if COM ports have been added or removed
            // since it is quite common now with USB-to-Serial adapters
            RefreshComPortList();
        }

        private void Resultados_Shown(object sender, EventArgs e)
        {
            Log(LogMsgType.Normal, String.Format("Interfaz Iniciada {0}\n", DateTime.Now));
        }
    }



}

