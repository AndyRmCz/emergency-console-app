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
        double lat;
        double lon;
        string type ="";
        string desc = "";
        
        getEmergencyInfo(out type, out desc, out lat, out lon);
        coordinate.Latitude = lat;
        coordinate.Longitude = lon;

        Console.Clear();
        getClosestCenter(coordinate, out closestCenter, out distance);
        String tiempoLlegada = Convert.ToString(calcTime(distance));
        String horaLlegada = (DateTime.Now.AddMinutes(calcTime(distance)).ToShortTimeString());
        Console.Clear();

        System.Console.WriteLine("Ha solicitado un servidio de " + type + " que será atendido desde el centro " + closestCenter);
        System.Console.WriteLine("La descripción de su emergencia es:");
        System.Console.WriteLine(desc);
        System.Console.WriteLine();
        System.Console.WriteLine("Se encuentra a una distancia aporoximada de: " + distance + "km a " + tiempoLlegada + " minutos del centro");
        System.Console.WriteLine("La hora aporoximada de llegada será: " + horaLlegada);
        

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
        Coordinate center = new Coordinate();
        double minDistance = 1000000; //km
        string closeCenter = "";
        for(int i = 0; i < 17; i++){
            center.Latitude = Convert.ToDouble(centers[i,1]);
            center.Longitude = Convert.ToDouble(centers[i,2]);

            if(GeoCalculator.GetDistance(center, incidentPlace) < minDistance){
                minDistance = GeoCalculator.GetDistance(center,incidentPlace,1,DistanceUnit.Kilometers);
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

    static void getEmergencyInfo(out string type, out string desc, out double lat, out double lon){
        bool continuar = true;
        string intType = "";
        double intLon = 0;
        double intLat = 0;
        
        
        do{
            Console.WriteLine("¿De que servicio requiere?");
            Console.WriteLine("1.- Policia");
            Console.WriteLine("2.- Bomberos");
            Console.WriteLine("3.- Ambulancia");
            Console.WriteLine("0.- Salir");
            char op = Char.ToUpper(GetKeyPress("Eliga una opcion: ", new Char[] { '1', '2', '3', '0' } ));
            if (op == '0'){
                continuar = false;
            }
            else if (op == '1'){
            //Ejecuta metodo para recolectar datos de entrada
                // Console.Clear();
                intType = "Policia";
                break;
            }
            else if (op == '2'){
                intType = "Bomberos";
                break;
            }
            else if (op == '3'){
                intType = "Ambulancia";
                break;
            }
        }while(continuar);
        type = intType;
        getLatAndLon(out intLat, out intLon);
        lat = intLat;
        lon = intLon;
        System.Console.WriteLine("Ingrese una breve descripcion de su emergencia: ");
        desc = Console.ReadLine()!;
    }

    static double calcTime(double distance){
        const int velocity = 120;
        double time = (distance/velocity);
        return time*60;
    }

    private static Char GetKeyPress(String msg, Char[] validChars)
   {
      ConsoleKeyInfo keyPressed;
      bool valid = false;

      Console.WriteLine();
      do {
         Console.Write(msg);
         keyPressed = Console.ReadKey();
         Console.WriteLine();
         if (Array.Exists(validChars, ch => ch.Equals(Char.ToUpper(keyPressed.KeyChar))))
            valid = true;
      } while (! valid);
      return keyPressed.KeyChar;
   }
}
