using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CINE_PRIME.Models;

public partial class ListaPendiente
{
    public Guid Id { get; set; }
    public string UserId { get; set; }     // FK -> AspNetUsers.Id
    public int MediaId { get; set; }
    public string MediaType { get; set; }
    public DateTime FechaAgregado { get; set; } = DateTime.Now;

    // propiedad de navegación
    public virtual ApplicationUser Usuario { get; set; }



}
