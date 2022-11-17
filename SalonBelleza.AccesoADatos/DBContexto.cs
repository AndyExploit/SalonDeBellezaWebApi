using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//***********************************************************
//Microsoft.EntityFrameworkCora es un using para utilizar la Clase DbContext, la cual nos ayuda para mapear las Entidades(Clases).
using Microsoft.EntityFrameworkCore;
using SalonBelleza.EntidadesDeNegocio;

namespace SalonBelleza.AccesoADatos
{
    public class DBContexto : DbContext
    {
        //Los DbSet sirven para Mapeasr las Entidades del proyecto.
        public DbSet<Cita> Cita { get; set; }

        public DbSet<Cliente> Cliente { get; set; }

        public DbSet<DetalleCita> DetalleCita { get; set; }

        public DbSet<Rol> Rol { get; set; }

        public DbSet<Servicio> Servicio { get; set; }

        public DbSet<Usuario> Usuario { get; set; }


        //Este Metodo nos sirve para Configurar nuestro Contexto en el EntityFramework.
        // ********





        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //String de conexion Saul
            //optionsBuilder.UseSqlServer(@"Data Source=LAPTOP-85EB5NKE\SQLEXPRESS;Initial Catalog=SalonBelleza;Integrated Security=True;");

            ////String de conexion Hector
            //optionsBuilder.UseSqlServer(@"Data Source=LAPTOP-RMHJKBUU;Initial Catalog=SalonBelleza;Integrated Security=True;");

            //String de conexion deysi
           //optionsBuilder.UseSqlServer(@"Data Source=LAPTOP-HDM3K5RA\SQLEXPRESS;Initial Catalog=salonbelleza;Integrated Security=True;");

            //String de conexion Andy!
            //optionsBuilder.UseSqlServer(@"Data Source=DELL-LATITUDE\SQLEXPRESS;Initial Catalog=SalonBelleza;Integrated Security=True;");

            //String de conexion Eri
            //optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-G0IU72C\SQLEXPRESS;Initial Catalog=SalonBelleza;Integrated Security=True;");

            //String de conexion a la base remota
            optionsBuilder.UseSqlServer(@"workstation id=SalonBellezadb.mssql.somee.com;packet size=4096;user id=AndromedaCorp_SQLLogin_1;pwd=c4sd8dn7h7;
                                                data source=SalonBellezadb.mssql.somee.com;persist security info=False;initial catalog=SalonBellezadb");

        }




    }
}
