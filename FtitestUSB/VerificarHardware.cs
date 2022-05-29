using FTD2XX_NET;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace FtitestUSB
{
    public class VerificarHardware
    {

        private bool conectadoArduino { set; get; }
        private bool conectadoFTDI { set; get; }

        public VerificarHardware()
        {
            conectadoArduino = false;
            conectadoFTDI = false;
        }

        public bool configurarHardware()
        {


            bool result = false;
            verificarArduino();

            verificarFTDI();


            if (conectadoArduino)
            {
                String tipoMultitest = "Arduino";
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                xmlDoc.SelectSingleNode("//appSettings/add[@key='Tipomultitest']").Attributes["value"].Value = tipoMultitest;
                xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                ConfigurationManager.RefreshSection("appSettings");
                result = true;
            }
            else
            {
                if (ConfigurationManager.AppSettings["Tipomultitest"] == "Arduino")
                {
                    var xmlDoc = new XmlDocument();
                    xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                    xmlDoc.SelectSingleNode("//appSettings/add[@key='Tipomultitest']").Attributes["value"].Value = "No";
                    xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                    ConfigurationManager.RefreshSection("appSettings");

                }
            }

            if (conectadoFTDI)
            {
                String tipoMultitest = "FTDI";
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                xmlDoc.SelectSingleNode("//appSettings/add[@key='Tipomultitest']").Attributes["value"].Value = tipoMultitest;
                xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                ConfigurationManager.RefreshSection("appSettings");
                result = true;
            }
            else
            {
                if (ConfigurationManager.AppSettings["Tipomultitest"] == "FTDI")
                {
                    var xmlDoc = new XmlDocument();
                    xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                    xmlDoc.SelectSingleNode("//appSettings/add[@key='Tipomultitest']").Attributes["value"].Value = "No";
                    xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                    ConfigurationManager.RefreshSection("appSettings");
                }
            }

            if (!conectadoArduino && !conectadoFTDI)
            {
                
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                xmlDoc.SelectSingleNode("//appSettings/add[@key='Tipomultitest']").Attributes["value"].Value = "No";
                xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                ConfigurationManager.RefreshSection("appSettings");
            }


            return result;
        }


        private void verificarArduino()
        {
            bool res = false;

            ManagementObjectCollection collection;
            using (var searcher = new ManagementObjectSearcher("root\\CIMV2",
          @"SELECT * FROM Win32_PnPEntity where DeviceID Like ""USB%"""))
                collection = searcher.Get();

            String arduinoID = "USB\\VID_1A86&PID_7523";


            String aux = null;

            foreach (var device in collection)
            {
                String temp = device.GetPropertyValue("DeviceID").ToString().Substring(0, 21);

                if (temp == arduinoID)
                {
                    res = true;
                    aux = temp;
                    conectadoArduino = true;
                }
                
            }


        }

        private bool verificarFTDI()
        {
            try
            {
                FTDI myFtdiDevice = new FTDI();

                bool conectado = false;
                UInt32 ftdiDeviceCount = 0;

                FTDI.FT_STATUS ftStatus = myFtdiDevice.GetNumberOfDevices(ref ftdiDeviceCount);

                ///lista de dispositivos ftdi
                FTDI.FT_DEVICE_INFO_NODE[] ftdiDeviceList = new FTDI.FT_DEVICE_INFO_NODE[ftdiDeviceCount];

                //asigna la lista de dispositivos
                ftStatus = myFtdiDevice.GetDeviceList(ftdiDeviceList);
                if (ftdiDeviceList.Length > 0)
                {
                    conectado = true;
                    conectadoFTDI = true;
                }

                return conectado;
            }
            catch (Exception)
            {

                throw;
            }
           

        }
    }
}
