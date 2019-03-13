
using UnityEngine;

namespace Assets.FYP
{
    // TODO Add relevant fields as they become required such as description and type
    public class Poi: MonoBehaviour
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string Puid { get; set; }
        public string Buid { get; set; }
        public int Floor { get; set; }
        public string Name { get; set; }


        public void Init(double lat, double lon, string puid, string buid, int floor, string name=null)
        {
            Lat = lat;
            Lon = lon;
            Puid = puid;
            Buid = buid;
            Floor = floor;
            Name = name;
        }

        public void FromAnyplace(AnyplacePoi poi)
        {
            Lat = double.Parse(poi.coordinates_lat);
            Lon = double.Parse(poi.coordinates_lon);
            Puid = poi.puid;
            Buid = poi.buid;
            Floor = int.Parse(poi.floor_number);
            Name = poi.name;
        }

    }
}
