using System;
using System.Collections.Generic;
using System.Text;
using CheckIn.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CheckIn.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set;}
        public DbSet<EmpCheckIn> EmpCheckIn { get; set;}
    }
}
