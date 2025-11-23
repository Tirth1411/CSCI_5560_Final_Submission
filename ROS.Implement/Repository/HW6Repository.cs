#region backup message error



using Microsoft.EntityFrameworkCore;
using ROS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ROS.Implement.Repository
{
    public interface IHW6Repository
    {
        Task<IEnumerable<Shipment>> getShipment();
        Task<string> InsertShipment(Shipment shipment);
        Task<string> IncreaseSupplierStatusBy10Percent();
        Task<List<Supplier>> GetAllSuppliers();
        Task<List<Supplier>> GetSuppliersByPartNumber(string partNumber);
    }

    public class HW6Repository : IHW6Repository
    {
        private readonly HW6DbContext _hw6DbContext;

        public HW6Repository(HW6DbContext dbContext)
        {
            _hw6DbContext = dbContext;
        }

        public async Task<IEnumerable<Shipment>> getShipment()
        {
            try
            {
                return await _hw6DbContext.Shipment.ToListAsync();
            }
            catch (Exception ex)
            {
                // Optional: Log the exception
                Console.WriteLine($"Error retrieving shipments: {ex.Message}");

                return Enumerable.Empty<Shipment>(); // Returns an empty list if there's an error
            }
        }



        public async Task<string> InsertShipment(Shipment shipment)
        {
            try
            {
                // Check if a record with the same (Sno, Pno) exists
                var existingShipment = await _hw6DbContext.Shipment
                    .FirstOrDefaultAsync(s => s.Sno == shipment.Sno && s.Pno == shipment.Pno);

                // If such a record exists, return a specific message
                if (existingShipment != null)
                {
                    return "Duplicate entry: A shipment with the same (Sno, Pno) already exists.";
                }

                // If no such record exists, proceed to add the new shipment
                await _hw6DbContext.Shipment.AddAsync(shipment);
                await _hw6DbContext.SaveChangesAsync();
                return "Insertion successful.";
            }
            catch (Exception ex)
            {
                // Return the exception message in case of an error
                return $"Error: {ex.Message}";
            }
        }


        public async Task<string> IncreaseSupplierStatusBy10Percent()
        {
            try
            {
                // Get all suppliers
                var suppliers = await _hw6DbContext.Supplier.ToListAsync();

                suppliers.ForEach(supplier => supplier.Status = (int)(supplier.Status * 1.1));

                // Save changes to the database
                await _hw6DbContext.SaveChangesAsync();

                return "Status of each supplier increased by 10%.";
            }
            catch (Exception ex)
            {
                // Return an error message in case of an exception
                return $"Error: {ex.Message}";
            }
        }

        public async Task<List<Supplier>> GetAllSuppliers()
        {
            try
            {
                return await _hw6DbContext.Supplier.ToListAsync();
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                return new List<Supplier>(); // Return an empty list in case of an error
            }
        }


        public async Task<List<Supplier>> GetSuppliersByPartNumber(string partNumber)
        {
            try
            {
                return await _hw6DbContext.Shipment
                    .Where(shipment => shipment.Pno == partNumber)
                    .Select(shipment => shipment.Supplier)
                    .Distinct()
                    .ToListAsync();
            }
            catch (Exception)
            {
                return new List<Supplier>(); // Return an empty list if an error occurs
            }
        }

    }
}
#endregion



