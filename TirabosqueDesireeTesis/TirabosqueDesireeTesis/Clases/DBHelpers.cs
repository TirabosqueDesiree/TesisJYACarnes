using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TirabosqueDesireeTesis.Models;

namespace TirabosqueDesireeTesis.Clases
{
    public class DBHelpers : IDisposable
    {
        private static CarniceriaContext db = new CarniceriaContext();






        public void Dispose()
        {
            db.Dispose();
        }

        public static int GetEstado(string descripcion, CarniceriaContext db)
        {
            var estado = db.Estadoes.Where(e => e.Descripcion == descripcion).FirstOrDefault();
            if (estado == null)
            {
                estado = new Estado { Descripcion = descripcion, };
                db.Estadoes.Add(estado);
                db.SaveChanges();
            }
            return estado.IdEstado;
        }
    }
}