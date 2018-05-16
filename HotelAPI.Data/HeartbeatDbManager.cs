using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelAPI.Data;
using HotelAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace HotelAPI.Infrastructure
{
    public class HeartbeatDbManager
    {
        private readonly HotelContext _context;


        public HeartbeatDbManager(HotelContext context)
        {
            _context = context;

        }
        public bool CheckIfDatabaseIsOnline()
        {
            try
            {
                _context.Database.OpenConnection();
            }

            catch (Exception)
            {
                return false;
            }

            finally
            {
                _context.Database.CloseConnection();
            }

            return true;
        }


    }
}
