using Microsoft.AspNetCore.Mvc;
using LAB6.Data;
using LAB6.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using LAB6.Services;
using RestSharp;

namespace LAB6.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/addresses")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class AddressController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ISalesforceAuthService _salesforceAuthService;

        public AddressController(ApplicationDbContext context, ISalesforceAuthService salesforceAuthService)
        {
            _context = context;
            _salesforceAuthService = salesforceAuthService;
        }

        // Version 1 - Get by ID
        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Address>> GetAddressByIdV1(int id)
        {
            var address = await _context.Addresses.FindAsync(id);
            if (address == null) return NotFound();
            return address;
        }


        // Version 2 - Get by ID, with additional info or relationships (for example)
        [HttpGet("{id}")]
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<Address>> GetAddressByIdV2(int id)
        {
            var address = await _context.Addresses
                .Include(a => a.Branches)  // Optional: include related branches if needed
                .Include(a => a.Customers) // Optional: include related customers if needed
                .FirstOrDefaultAsync(a => a.AddressId == id);

            if (address == null) return NotFound();
            return address;
        }

        // POST: api/addresses/sendAddresses (Sending address data to Salesforce)
        [HttpPost("sendAddresses")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult> SendAddresses()
        {
            var addresses = await _context.Addresses.ToListAsync();

            var salesforceAddresses = addresses.Select(a => new
            {
                Name = a.Line1,  
                Address_Line2__c = a.Line2,  
                City__c = a.TownCity,
                Postal_Code__c = a.ZipPostcode,  
                State_Province_County__c = a.StateProvinceCounty, 
                Country__c = a.Country, 
            }).ToList();

            var authResult = await _salesforceAuthService.AuthenticateAsync("avdeevn1142-tqhw@force.com", "#q4ZgJ7%BPGjM$%", "HoEYTbZaxTMqHuLQPfqVlr7lM");

            if (!authResult.IsSuccess)
            {
                return Unauthorized($"Authentication failed with Salesforce. Error: {authResult.Result}");
            }

            string accessToken = authResult.AuthToken;  
            string instanceUrl = authResult.InstanceUrl; 

            string salesforceApiUrl = $"{instanceUrl}/services/data/v57.0/sobjects/Address/";

            var client = new RestClient(salesforceApiUrl);

            foreach (var address in salesforceAddresses)
            {
                var body = JsonConvert.SerializeObject(address); 

                var request = new RestRequest(Method.POST);
                request.AddHeader("Authorization", "Bearer " + accessToken);  
                request.AddHeader("Content-Type", "application/json"); 
                request.AddJsonBody(body);  

                var response = await client.ExecuteAsync(request);

                if (!response.IsSuccessful)
                {
                     return BadRequest($"Error sending data to Salesforce: {response.Content}");
                }
            }


            return Ok("Data successfully sent to Salesforce.");
        }

    }
}
