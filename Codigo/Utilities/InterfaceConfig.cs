using System;
using System.Configuration;

namespace AnnarComMICROSESV60.Utilities
{
    static internal class InterfaceConfig
    {

        static internal string diasatras;
        static internal string nombreEquipo;

        static internal string equipoCodigoCaratula;
        static internal string recativoCodigoCaratula;
        static internal string ValidaResultado;
        static internal string imprimirQueriesDBLog;
        static internal int intentosReconexionDB;

        static internal string rutalog;
        static internal string logActivo;
        static internal string nombreLog;

        static internal string StrCadenaConeccion;

        static internal string generarGraficas;

        //bandera para controlar procesos en el dashboard
        static internal bool banderaTerminal = false;
        static internal bool banderaConfig = false;

        static internal string portName;
        static internal int baudRate;
        static internal int dataBits;
        static internal string parity;
        static internal string dataMode;
        static internal string stopBits;
        static internal string clearOnOpen;
        static internal string clearWithDTR;

        static internal void InitializeConfig()
        {


            diasatras = ConfigurationManager.AppSettings["DiasAtras"];
            nombreEquipo = ConfigurationManager.AppSettings["nombreEquipo"];

            equipoCodigoCaratula = ConfigurationManager.AppSettings["equipoCodigoCaratula"].ToString();
            recativoCodigoCaratula = ConfigurationManager.AppSettings["recativoCodigoCaratula"].ToString();

            ValidaResultado = ConfigurationManager.AppSettings["ValidaResultado"].ToString();

            imprimirQueriesDBLog = ConfigurationManager.AppSettings["imprimirQueriesDBLog"];
            intentosReconexionDB = Convert.ToInt32(ConfigurationManager.AppSettings["intentosReconexionDB"]);

            rutalog = ConfigurationManager.AppSettings["Rutalog"];
            logActivo = ConfigurationManager.AppSettings["logActivo"];
            nombreLog = ConfigurationManager.AppSettings["nombreLog"];

            StrCadenaConeccion = ConfigurationManager.AppSettings["StrCadenaConeccion"].ToString();

            generarGraficas = ConfigurationManager.AppSettings["generarGraficas"];


            portName = ConfigurationManager.AppSettings["portName"];
            baudRate = Convert.ToInt32(ConfigurationManager.AppSettings["baudRate"]);
            dataBits = Convert.ToInt32(ConfigurationManager.AppSettings["dataBits"]);
            parity = ConfigurationManager.AppSettings["parity"];
            dataMode = ConfigurationManager.AppSettings["dataMode"];
            stopBits = ConfigurationManager.AppSettings["stopBits"];
            clearOnOpen = ConfigurationManager.AppSettings["clearOnOpen"];
            clearWithDTR = ConfigurationManager.AppSettings["clearWithDTR"];

        }
    }
}
