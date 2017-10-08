using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace authtesting.Models
{
    public static class PatientDb
    {
        public static IMongoCollection<Patient> Open()
        {
            var client = new MongoClient("mongodb://localhost");
            var db = client.GetDatabase("patientDb");
            return db.GetCollection<Patient>("Patients");
        }
    }
}