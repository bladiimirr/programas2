﻿
using SQLite;
using System.Xml.Linq;

namespace AgendaPersonal.Modelos;

public class Contacto
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? Telefono { get; set; }
    public string? Correo { get; set; }

    public string? Direccion { get; set; }


    public bool Activo { get; set; }
}
