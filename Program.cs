using System;
using Geolocation;
namespace emergency_console_app;

class Program
{
    static void Main(string[] args)
    {
        
        
        Coordinate coordinate = new Coordinate();
        string closestCenter = "";
        double distance;
        double lat = 0;
        double lon = 0;
        

        getClosestCenter(coordinate, out closestCenter, out distance);
        getLatAndLon(out lat, out lon);
        coordinate.Latitude = lat;
        coordinate.Longitude = lon;

    }

    static Coordinate getClosestCenter(Coordinate incidentPlace, out string closestCenter, out double distance){
        string[,] centers = {
            {"Volcan Ajusco", "19.205556", "-99.258056"},
            {"Volvan Tláloc", "19.108889", "-99.030278"},
            {"Volcan Pelado","19.150000","-99.216667"},
            {"Cerro Cilcuayo","19.101944","-99.989444"},
            {"Cerro el Charco","19.289444","-99.300000"},
            {"Volcan Cuautzin","19.154444","-99.105556"},
            {"Volcan Chichinautzin","19.090000","-99.135000"},
            {"Volcan Acopiaxco","19.116389","-99.162222"},
            {"Volcan Otlayucan","19.048333","-99.060000"},
            {"Volcan Tezoyo","19.096389","-99.225278"},
            {"Cerro Ayaqueme","19.168889","-98.453333"},
            {"Volcan Guadalupe (El Borrego)","19.322778","-99.998889"},
            {"Cerro del Chiquihuite","19.531389","-99.130000"},
            {"Volcan Tehutli","19.224722","-99.030833"},
            {"Volcan Xaltepec","19.316667","-99.030556"},
            {"Cerro de la Estrella","19.342500","-99.088611"},
            {"Cerro de Chapultepec","19.422778","-99.174444"}
        };
        double minDistance = 1000000; //km
        Coordinate center = new Coordinate();
        string closeCenter = "";
        for (int i = 0; i <= centers.Rank;i++){
            center.Latitude = Convert.ToDouble(centers[i,1]);
            center.Longitude = Convert.ToDouble(centers[i,2]);
            if(GeoCalculator.GetDistance(center, incidentPlace) < minDistance){
                minDistance = GeoCalculator.GetDistance(center,incidentPlace);
                closeCenter = centers[i,0];

            }
        }
        closestCenter = closeCenter;
        distance = minDistance;

        return center;
    }

    static void getLatAndLon(out double latitud, out double longitud){
        System.Console.Write("Ingresa la latitud del lugar de emergencia: ");
        latitud = Convert.ToDouble(Console.ReadLine());
        System.Console.Write("Ingresa la longitud del lugar de emergencia: ");
        longitud = Convert.ToDouble(Console.ReadLine());
    }
}
