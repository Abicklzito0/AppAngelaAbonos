using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppAngelaAbonos.Models
{
    [Table("Documento")]
    public class Documento
    {
        [Column("Id"), PrimaryKey, AutoIncrement]
        public int Id { get { return id; } set { id = value; } }
        [Column(("IdTipoDocumento"))]
        public int IdTipoDocumento { get { return idTipoDocumento; } set { idTipoDocumento = value; } }
        [Column(("IdDocumento"))]
        public int IdDocumento { get { return idDocumento; } set { idDocumento = value; } }
        [Column(("IdCliente"))]
        public int IdCliente { get { return idCliente; } set { idCliente = value; } }
        [Column(("Estado"))]
        public int Estado { get { return estado; } set { estado = value; } }
       
        [Column(("Total"))]
        public double Total { get { return total; } set { total = value; } }
        [Column("Descripcion")]
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
        [Ignore]
        public Cliente Cliente { get { return cliente; } set { cliente = value; } }

        private Cliente cliente;
        private int id;
        private int estado;
        private int idTipoDocumento;
        private int idDocumento;
        private int idCliente;
        private double total;
        private string descripcion;
        

    

    }
}
