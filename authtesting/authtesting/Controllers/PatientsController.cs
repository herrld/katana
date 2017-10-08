using authtesting.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace authtesting.Controllers
{
    public class PatientsController : ApiController
    {
        IMongoCollection<Patient> patients;

        public PatientsController()
        {
            this.patients = PatientDb.Open();
        }

        public IEnumerable<Patient> Get()
        {
            return this.patients.AsQueryable();
        }

        public HttpResponseMessage Get(string id)
        {
            var patient = this.patients.AsQueryable().First(p => p.Id.Equals(id));
            return Request.CreateResponse(patient);
        }

        [Route("api/patients/{id}/medications")]
        public HttpResponseMessage GetMedications(string id)
        {
            var patient = this.patients.AsQueryable().First(p => p.Id.Equals(id));
            return Request.CreateResponse(patient.Medications);
        }

    }
}
