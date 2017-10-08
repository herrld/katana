using authtesting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver.Linq;
using MongoDB.Driver;

namespace authtesting
{
    public static class MongoConfig
    {
        public async static void Seed()
        {
            var patients = PatientDb.Open();
            if (!patients.AsQueryable<Patient>().Any(p => p.Name.Equals("Scott")))
            {
                var data = new List<Patient>()
                {
                    new Patient
                    {
                        Name = "Scott",
                        Ailments = new List<Ailment>() { new Ailment { Name="Crazy"} },
                        Medications = new List<Medication> { new Medication { Name = "asprin" } }
                    },
                    new Patient
                    {
                        Name = "Joy",
                        Ailments = new List<Ailment>() { new Ailment { Name = "Crazy" } },
                        Medications = new List<Medication>() { new Medication { Name ="asprin"} }
                    }

                };
                patients.InsertMany(data);
            }
        }
    }
}