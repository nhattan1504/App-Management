using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManagementApp.Models;
using Microsoft.EntityFrameworkCore;

namespace UploadFileInCKEditorInASPNetCore.Models
{
    public class UploadFileInCKEditorInASPNetCoreContext : DbContext
    {
        public UploadFileInCKEditorInASPNetCoreContext (DbContextOptions<UploadFileInCKEditorInASPNetCoreContext> options)
            : base(options)
        {
        }

        public DbSet<Posts> Post { get; set; }
    }
}
