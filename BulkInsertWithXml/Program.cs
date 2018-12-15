using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace BulkInsertWithXml
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bulk Insert Via XML");
            BulkInsertor bk = new BulkInsertor();
            bk.InsertAssetGeoFence();
        }


    }

    class BulkInsertor {


        public void InsertAssetGeoFence()
        {
            List<GeoBoundaryEvent> events = new List<GeoBoundaryEvent>();
            events.Add(new GeoBoundaryEvent() { AssetType = "Plant",AssetDeviceSerialNumber="DSN1" });
            events.Add(new GeoBoundaryEvent() { AssetType = "Paver", AssetDeviceSerialNumber = "DSN1" });
            events.Add(new GeoBoundaryEvent() { AssetType = "Paver", AssetDeviceSerialNumber = "DSN2" });

            //XElement xmlElements = new XElement("GeoBoundaryEvents", events.Select(i => new XElement("GeoBoundaryEvent", i.AssetGeofenceID)));

            XmlSerializer serializer = new XmlSerializer(typeof(List<GeoBoundaryEvent>),new XmlRootAttribute("GeoBoundaryEvents"));

            using (StringWriter textWriter = new StringWriter())
            {
                serializer.Serialize(textWriter, events);
                Console.WriteLine(textWriter.ToString());
            }

            System.Console.ReadLine();
        }
    }

   // [XmlRoot("GeoBoundaryEvent")]
    public class GeoBoundaryEvent
    {
     //   [XmlElement]
        public string AssetType { get; set; }

        //[XmlElement]
        public string AssetDeviceSerialNumber { get; set; }
     //   public DateTime AssetLocationTimestamp { get; set; }
        //public DateTime AlertTimestamp { get; set; }
        //public string GeoBoundaryAssetType { get; set; }
        //public string GeoBoundaryAssetDeviceSerialNumber { get; set; }
        //public string AlertType { get; set; }
        //public string GeoFenceName { get; set; }
        //public string GeoFenceId { get; set; }
        //public long AssetGeofenceID { get; set; }
        //public int fk_EventTypeID { get; set; }

    }
}
