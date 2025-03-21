using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infraestructura.Abstract;
using Infraestructura.Models.Pagos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Linq;

namespace Server.Pages.Pages.Pagos
{
    public partial class PgCorteServicio
    {
        // Variable para controlar la expansión del panel de formulario
        private bool expande = false; 
        protected async Task GetClientes()
        {
            var result = await _Rest.GetAsync<List<PgClienteDto>>("PgCliente/GetAll");
            if (result.State == State.Success)
            {
                _clientes = result.Data;
                return;
            }
            else
            {
                _MessageShow("Ocurrió un error: " + result.Message, State.Warning);
            }
        }
        // Modelo para el corte de servicio
        public PgCorteServicioDto _Corte = new PgCorteServicioDto();
        public List<PgClienteDto> _clientes = new List<PgClienteDto>();
        // Lista de cortes
        public List<PgCorteServicioDto> _cortes = new List<PgCorteServicioDto>();

        // Obtener la lista de cortes
protected async Task GetCortes()
{
    var result = await _Rest.GetAsync<List<PgCorteServicioDto>>("PgCorteServicio/GetAll");
    if (result.State == State.Success)
    {
        _cortes = result.Data;

        // Asignar el nombre del cliente a cada corte
        foreach (var corte in _cortes)
        {
            var cliente = _clientes.FirstOrDefault(c => c.IdpgCliente == corte.IdpgCliente);
            if (cliente != null)
            {
                corte.NombreCliente = cliente.Nombre; // Asignar el nombre
            }
        }

        return;
    }
    else
    {
        _MessageShow("Ocurrió un error: " + result.Message, State.Warning);
    }
}


        // Guardar un nuevo corte
        protected async Task SaveCorte(PgCorteServicioDto corte)
        {
            try
            {
                _Loading.Show();
                var response = await _Rest.PostAsync<int?>("PgCorteServicio", new { _PgCorteServicio = corte });
                _Loading.Hide();
                _MessageShow(response.Message, response.State);
                if (response.State != State.Success)
                {
                    response.Errors.ForEach(x => _MessageShow(x, State.Warning));
                    return;
                }
            }
            catch (Exception e)
            {
                _Loading.Hide();
                _MessageShow(e.Message, State.Error);
            }
        }

        // Actualizar un corte existente
        protected async Task UpdateCorte(PgCorteServicioDto corte)
        {
            try
            {
                _Loading.Show();
                var response = await _Rest.PutAsync<int>("PgCorteServicio", corte, corte.IdpgCorte);
                _Loading.Hide();
                _MessageShow(response.Message, response.State);
            }
            catch (Exception e)
            {
                _Loading.Hide();
                _MessageShow(e.Message, State.Error);
            }
        }

        // Eliminar un corte
        protected async Task EliminarCorte(int idCorte)
        {
            await _MessageConfirm("¿Está seguro de eliminar el registro...?", async () =>
            {
                var response = await _Rest.DeleteAsync<int>("PgCorteServicio", idCorte);
                if (!response.Succeeded)
                {
                    _MessageShow(response.Message, State.Error);
                }
                else
                {
                    _MessageShow(response.Message, response.State);
                    await GetCortes();
                    StateHasChanged();
                }
            });
        }

        // Validar y guardar/actualizar el formulario
        private async Task OnValidCorte(EditContext context)
        {
            if (_Corte.IdpgCorte > 0)
            {
                await UpdateCorte(_Corte);
            }
            else
            {
                await SaveCorte(_Corte);
            }
            _Corte = new PgCorteServicioDto();
            await GetCortes();
            ToggleExpand();
            StateHasChanged();
        }

        // Preparar el formulario para editar un corte
        protected void FormEditarCorte(PgCorteServicioDto corte)
        {
            _Corte = corte;
            ToggleExpand();
        }

        // Reiniciar el formulario
        private void ResetCorte() => _Corte = new PgCorteServicioDto();

        // Controlar la expansión del panel
        private void ToggleExpand() => expande = !expande;

        protected override async Task OnInitializedAsync()
        {
            await GetClientes();
            await GetCortes();
           
        }
    }
}
