using Aplicacion.DTOs;
using Aplicacion.DTOs.Pagos;
using Aplicacion.Interfaces.Repositories;
using Aplicacion.Wrappers;
using Microsoft.EntityFrameworkCore;
using Persistencia.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistencia.Repository.Custom
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AplicationDbContext _context;

        public ClienteRepository(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<List<PagoPendienteDto>>> ObtenerPagosPendientes()
        {
            var hoy = DateTime.Today;
            // Definimos el primer día del mes actual
            var currentMonth = new DateTime(hoy.Year, hoy.Month, 1);

            // Obtenemos todos los clientes junto con sus pagos (se asume la propiedad de navegación PgPagos)
            var clientes = await _context.PgCliente
                .Include(c => c.PgPagos)
                .ToListAsync();

            var resultados = new List<PagoPendienteDto>();

            foreach (var cliente in clientes)
            {
                // El primer mes a cobrar es el mes siguiente al inicio del servicio
                var startMonth = new DateTime(cliente.FechaInicio.Year, cliente.FechaInicio.Month, 1).AddMonths(1);

                // Generamos la serie de meses desde startMonth hasta el mes actual (inclusive)
                for (var mes = startMonth; mes <= currentMonth; mes = mes.AddMonths(1))
                {
                    // Buscar si existe un pago para este cliente en el mes (comparando año y mes)
                    var pagoExistente = cliente.PgPagos?.FirstOrDefault(p =>
                        new DateTime(p.MesPago.Year, p.MesPago.Month, 1) == mes);

                    // Si no existe registro o el registro existe y su estado es "Pendiente"
                    if (pagoExistente == null || (pagoExistente != null && pagoExistente.EstadoPago == "Pendiente"))
                    {
                        // Si existe pago pendiente, usamos su monto; de lo contrario, asignamos monto 0
                        var monto = pagoExistente?.Monto ?? 0;

                        resultados.Add(new PagoPendienteDto
                        {
                            ClienteId = cliente.IdpgCliente,
                            NombreCliente = cliente.Nombre,
                            TelefonoCliente = cliente.Telefono,
                            MesDeuda = mes.ToString("yyyy-MM"),
                            Monto = monto,
                            EstadoPago = "Pendiente"
                        });
                    }
                }
            }

            // Ordenar los resultados por ClienteId y MesDeuda
            resultados = resultados.OrderBy(r => r.ClienteId).ThenBy(r => r.MesDeuda).ToList();

            return new Response<List<PagoPendienteDto>>(resultados)
            {
                Message = resultados.Count > 0 ? $"Query successful, {resultados.Count} pending payments found." : "No pending payments found.",
                Succeeded = resultados.Count > 0
            };
        }
    }
}
