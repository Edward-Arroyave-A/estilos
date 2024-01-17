using AnnarComMICROSESV60.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace AnnarComMICROSESV60.Forms
{
    public partial class Config : Form
    {
        public string conexion = InterfaceConfig.StrCadenaConeccion;
        private string pathConfig;
        private string condicional;

        public Config()
        {
            InitializeComponent();

            #region Cargar datos
            //Conexión
            string[] files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.config");
            pathConfig = files[0];
            var datosConexion = conexion.Split(';');

            txtDireccionIP.Text = datosConexion[0].Split('=')[1].ToString();
            txtPuerto.Text = datosConexion[1].Split('=')[1].ToString();
            txtNombreDB.Text = datosConexion[2].Split('=')[1].ToString();
            txtUsuario.Text = datosConexion[3].Split('=')[1].ToString();
            txtPass.Text = datosConexion[4].Split('=')[1].ToString();
            txtIntentosReCo.Text = InterfaceConfig.intentosReconexionDB.ToString();

            //Parametrización
            txtNombreEquipo.Text = InterfaceConfig.nombreEquipo.ToString();
            //txtIntervalo.Text = InterfaceConfig.intervalo.ToString();
            txtNombreLog.Text = InterfaceConfig.nombreLog.ToString();

            List<String> valores1 = new List<String> { "SI", "NO" };
            List<String> valores2 = new List<String> { "SI", "NO" };
            //cbActivarLog.DataSource = valores1;
            //cbImpQuerys.DataSource = valores2;
            cbActivarLog.Checked = InterfaceConfig.logActivo == "S" ? true : false;
            cbImpQuerys.Checked = InterfaceConfig.imprimirQueriesDBLog == "S" ? true : false;

            //Rutas
            //txtRutaArchivos.Text = InterfaceConfig.rutaArchivos.ToString();
            //txtRutaArchivosOK.Text = InterfaceConfig.rutaArchivosOK.ToString();
            //txtRutaArchivosERROR.Text = InterfaceConfig.rutaArchivosError.ToString();
            txtRutaLog.Text = InterfaceConfig.rutalog.ToString();
            #endregion

            #region Proceso para cargar la primera ventana
            //Color
            btnConexion.ForeColor = Color.FromArgb(64, 81, 252);
            //panelConexion.BackColor = Color.FromArgb(64, 81, 252);

            btnParametrizacion.ForeColor = Color.Gray;
            //panelParametrizacion.BackColor = Color.Gray;

            btnRuta.ForeColor = Color.Gray;
            //panelRuta.BackColor = Color.Gray;

            //Comportamiento
            panelConexion2.Visible = true;
            panelConexion2.Dock = DockStyle.Fill;

            //panelParametrizacion2.Visible = false;
            panelRuta2.Visible = false;
            #endregion
        }


        private void Config_Load(object sender, EventArgs e)
        {

            btnConexion.BackColor = Color.FromArgb(64, 81, 252);
            btnConexion.ForeColor = Color.White;

                RedondearBordesSuperior(btnParametrizacion, 5);
            RedondearBordesSuperior(btnConexion, 5);
            RedondearBordesSuperior(btnRuta, 5);
        }
        private void RedondearBordesSuperior(Control control, int radio)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radio * 2, radio * 2, 180, 90); // Esquina superior izquierda
            path.AddArc(control.Width - radio * 2, 0, radio * 2, radio * 2, 270, 90); // Esquina superior derecha
            path.AddArc(control.Width - radio * 2, control.Height - radio * 2, radio * 2, radio * 2, 0, 90); // Esquina inferior derecha
            path.AddArc(0, control.Height - radio * 2, radio * 2, radio * 2, 90, 90); // Esquina inferior izquierda
            control.Region = new Region(path);
        }
 

        private void btnParametrizacion_Click(object sender, EventArgs e)
        {
            //Color
            btnParametrizacion.BackColor = Color.SteelBlue;
            btnParametrizacion.ForeColor = Color.White;

            //panelParametrizacion.BackColor = Color.FromArgb(64, 81, 252);


            btnConexion.BackColor = Color.White;
            btnConexion.ForeColor = Color.Gray;
            //panelConexion.BackColor = Color.Gray;

            btnRuta.BackColor = Color.White;
            btnRuta.ForeColor = Color.Gray;
            //panelRuta.BackColor = Color.Gray;


            //Comportamiento
            panelParametrizacion2.Visible = true;
            panelParametrizacion2.Dock = DockStyle.Fill;

            panelConexion2.Visible = false;
            panelRuta2.Visible = false;
        }

        private void btnRuta_Click(object sender, EventArgs e)
        {
            //Color
            btnRuta.ForeColor = Color.White;
            btnRuta.BackColor = Color.SteelBlue;
            //panelRuta.BackColor = Color.FromArgb(64, 81, 252);

            btnConexion.BackColor = Color.White;
            btnConexion.ForeColor = Color.Gray;
            //panelConexion.BackColor = Color.Gray;

            btnParametrizacion.BackColor = Color.White;
            btnParametrizacion.ForeColor = Color.Gray;
            //panelParametrizacion.BackColor = Color.Gray;

            //Comportamiento
            panelRuta2.Visible = true;
            panelRuta2.Dock = DockStyle.Fill;

            panelConexion2.Visible = false;
            panelParametrizacion2.Visible = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Proceso conexión
            if (panelConexion2.Visible)
            {
                #region Conexión
                if (!string.IsNullOrEmpty(txtDireccionIP.Text) &&
                !string.IsNullOrEmpty(txtPuerto.Text) &&
                !string.IsNullOrEmpty(txtNombreDB.Text) &&
                !string.IsNullOrEmpty(txtUsuario.Text) &&
                !string.IsNullOrEmpty(txtPass.Text) &&
                !string.IsNullOrEmpty(txtIntentosReCo.Text)
                )
                {
                    try
                    {
                        var cadenaNueva = $@"Server={txtDireccionIP.Text}; Port={txtPuerto.Text}; Database={txtNombreDB.Text}; User Id={txtUsuario.Text}; Password={txtPass.Text};";
                        UpdateConfigKey("StrCadenaConeccion", cadenaNueva, 1);
                        UpdateConfigKey("intentosReconexionDB", txtIntentosReCo.Text, 1);

                        DialogResult result;
                        using (var msFomr = new FormMessageBox("Conexion guardada correctamente. ", "OK", MessageBoxButtons.OK, MessageBoxIcon.None))
                            result = msFomr.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        DialogResult result;
                        using (var msFomr = new FormMessageBox($"No se puedo guardar correctamente. {ex}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error))
                            result = msFomr.ShowDialog();
                    }
                }
                else
                {
                    DialogResult result;
                    using (var msFomr = new FormMessageBox($"No puede enviar campos vacios. ", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information))
                        result = msFomr.ShowDialog();
                }
                #endregion
            }

            //Proceso parametrización
            if (panelParametrizacion2.Visible)
            {
                #region Parametrización
                if (!string.IsNullOrEmpty(txtNombreEquipo.Text) &&
                !string.IsNullOrEmpty(txtNombreLog.Text)
                )
                {
                    try
                    {
                        UpdateConfigKey("nombreEquipo", txtNombreEquipo.Text, 2);
                        
                        UpdateConfigKey("nombreLog", txtNombreLog.Text, 2);
                        UpdateConfigKey("logActivo", cbActivarLog.Checked == true ? "S" : "N", 2);
                        UpdateConfigKey("imprimirQueriesDBLog", cbImpQuerys.Checked == true ? "S" : "N", 2);

                        DialogResult result;
                        using (var msFomr = new FormMessageBox("Parametros guardados correctamente. ", "OK", MessageBoxButtons.OK, MessageBoxIcon.None))
                            result = msFomr.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        DialogResult result;
                        using (var msFomr = new FormMessageBox($"No se puedo guardar correctamente. {ex}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error))
                            result = msFomr.ShowDialog();
                    }
                }
                else
                {
                    DialogResult result;
                    using (var msFomr = new FormMessageBox($"No puede enviar campos vacios. ", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information))
                        result = msFomr.ShowDialog();
                }
                #endregion
            }

            //Proceso ruta
            if (panelRuta2.Visible)
            {
                #region Rutas
                if (
                !string.IsNullOrEmpty(txtRutaLog.Text)
                )
                {
                    try
                    {
                        UpdateConfigKey("Rutalog", txtRutaLog.Text, 2);

                        DialogResult result;
                        using (var msFomr = new FormMessageBox("Parametros guardados correctamente. ", "OK", MessageBoxButtons.OK, MessageBoxIcon.None))
                            result = msFomr.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        DialogResult result;
                        using (var msFomr = new FormMessageBox($"No se puedo guardar correctamente. {ex}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error))
                            result = msFomr.ShowDialog();
                    }
                }
                else
                {
                    DialogResult result;
                    using (var msFomr = new FormMessageBox($"No puede enviar campos vacios. ", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information))
                        result = msFomr.ShowDialog();
                }
                #endregion
            }
        }

        public void UpdateConfigKey(string strKey, string newValue, int seccion) //Actuliza el valor de app.config por llave
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(pathConfig);

            if (!ConfigKeyExists(strKey, seccion))
            {
                throw new ArgumentNullException("Key", "<" + strKey + "> not find in the configuration.");
            }

            if (seccion == 1)
            {
                XmlNode connectionStrings = xmlDoc.SelectSingleNode("configuration/appSettings");

                foreach (XmlNode childNode in connectionStrings)
                {
                    condicional = childNode.NodeType.ToString();

                    if (condicional == "Comment")
                    {
                        continue;
                    }
                    else if (childNode.Attributes["key"].Value == strKey)
                    {
                        childNode.Attributes["value"].Value = newValue;
                    }
                }
            }
            else
            {
                XmlNode appSettingsNode = xmlDoc.SelectSingleNode("configuration/appSettings");

                foreach (XmlNode childNode in appSettingsNode)
                {
                    condicional = childNode.NodeType.ToString();

                    if (condicional == "Comment")
                    {
                        continue;
                    }
                    else if (childNode.Attributes["key"].Value == strKey)
                    {
                        childNode.Attributes["value"].Value = newValue;
                    }
                }
            }

            xmlDoc.Save(pathConfig);
            xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

            if (seccion == 1)
            {
                ConfigurationManager.RefreshSection("appSettings");
            }
            else
            {
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        public bool ConfigKeyExists(string strKey, int seccion) //Verifica que exista la llave
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(pathConfig);

            if (seccion == 1)
            {
                XmlNode connectionStringsNode = xmlDoc.SelectSingleNode("configuration/appSettings");

                foreach (XmlNode childNode in connectionStringsNode)
                {
                    condicional = childNode.NodeType.ToString();

                    if (condicional == "Comment")
                    {
                        continue;
                    }
                    else if (childNode.Attributes["key"].Value == strKey)
                    {
                        return true;
                    }
                }
            }
            else
            {
                XmlNode appSettingsNode = xmlDoc.SelectSingleNode("configuration/appSettings");

                foreach (XmlNode childNode in appSettingsNode)
                {
                    condicional = childNode.NodeType.ToString();

                    if (condicional == "Comment")
                    {
                        continue;
                    }
                    else if (childNode.Attributes["key"].Value == strKey)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        //private void InitializeComponent()
        //{
        //    this.SuspendLayout();
        //    // 
        //    // Config
        //    // 
        //    this.ClientSize = new System.Drawing.Size(284, 261);
        //    this.Name = "Config";
        //    this.Load += new System.EventHandler(this.Config_Load_1);
        //    this.ResumeLayout(false);

        //}

        private void Config_Load_1(object sender, EventArgs e)
        {

        }

        private void lblIntervalo_Click(object sender, EventArgs e)
        {

        }

        private void panelIntervalo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelContenedor_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelParametrizacion2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtNombreEquipo_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnConexion_Click(object sender, EventArgs e)
        {

            //Color
            btnConexion.BackColor = Color.SteelBlue;
            btnConexion.ForeColor = Color.White;

            //panelConexion.BackColor = Color.FromArgb(64, 81, 252);

            btnParametrizacion.BackColor = Color.White;
            btnParametrizacion.ForeColor = Color.Gray;

            //panelParametrizacion.BackColor = Color.Gray;

            btnRuta.BackColor = Color.White;
            btnRuta.ForeColor = Color.Gray;
            //panelRuta.BackColor = Color.Gray;

            //Comportamiento
            panelConexion2.Visible = true;
            panelConexion2.Dock = DockStyle.Fill;

            panelParametrizacion2.Visible = false;
            panelRuta2.Visible = false;
        }
    }
}
