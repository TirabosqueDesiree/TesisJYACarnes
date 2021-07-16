using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace TirabosqueDesireeTesis.Models
{
    public class CarniceriaContext: DbContext
    {
        public CarniceriaContext()
         : base("DefaultConnection")
        {

        }
        //metodo para no permitir el borrado en cascada
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public System.Data.Entity.DbSet<TirabosqueDesireeTesis.Models.Provincia> Provincias { get; set; }

        public System.Data.Entity.DbSet<TirabosqueDesireeTesis.Models.Localidad> Localidads { get; set; }

        public System.Data.Entity.DbSet<TirabosqueDesireeTesis.Models.Usuario> Usuarios { get; set; }

        public System.Data.Entity.DbSet<TirabosqueDesireeTesis.Models.Categoria> Categorias { get; set; }

        public System.Data.Entity.DbSet<TirabosqueDesireeTesis.Models.Marca> Marcas { get; set; }

        public System.Data.Entity.DbSet<TirabosqueDesireeTesis.Models.Producto> Productoes { get; set; }

        public DbSet<Estado> Estadoes { get; set; }

        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<DetallePedido> DetallePedidos { get; set; }
        public DbSet<PedidoDetalleTemp> PedidoDetalleTemps { get; set; }
        public DbSet<Mensaje> Mensajes { get; set; }
    }
   
}