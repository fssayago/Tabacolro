using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Database;
namespace EccobankRecolector.Conexiones
{
  public  class Constantes
    {
        public static FirebaseClient firebase = new FirebaseClient("https://tabaco-vrosario-default-rtdb.firebaseio.com/");
        public const string WebapyFirebase = "AIzaSyC0hIdIEFgQTa0Ukq5PGOIWcBKQu-XzVt4";
        public const string GoogleMapsApiKey = "AIzaSyBV2RVWt4nSdLk51m2qBoaQ-Z04BeumwyU";
                
        // public const string GoogleMapsApiKey = "AIzaSyBELEJVvNMlg5uWbEfxNLb3USeWAdt5fpk";
        // public const string GoogleMapsApiKey = "AIzaSyCnIerckxbiRVSxTTOHqiv0MgQi1VroRmQ";
        // public const string GoogleMapsApiKey = "AIzaSyDvbryLP_205Z835TGcs0dtKRLSxyvqJYs";

    }
}
