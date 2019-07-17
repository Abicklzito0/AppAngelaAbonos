using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppAngelaAbonos.Models
{
    [Table("Cliente")]
    public class Cliente
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get { return id; } set { id = value; } }
        public string Nombre { get { return nombre; } set { nombre = value; } }
        public string Direccion { get { return direccion; } set { direccion = value; } }
        public string Telefono { get { return telefono; } set { telefono = value; } }
        [Ignore]
        public double Deuda { get { return deuda; } set { deuda = value; } }
        private int id;
        private string nombre;
        private string direccion;
        private string telefono;
        private double deuda;


    }
}
