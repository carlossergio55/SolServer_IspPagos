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
    public partial class PgCliente
    {
        // Variable para controlar la expansión del panel de formulario
        private bool expande = false;

        // Modelo para el cliente
        public PgClienteDto _Cliente = new PgClienteDto();

        // Lista de clientes
        public List<PgClienteDto> _clientes = new List<PgClienteDto>();

        // Obtener la lista de clientes
        protected async Task GetClientes()
        {
            var result = await _Rest.GetAsync<List<PgClienteDto>>("PgCliente/GetAll");
            if (result.State == State.Success)
            {
                _clientes = result.Data.OrderByDescending(c => c.IdpgCliente).ToList();
                return;
            }
            else
            {
                _MessageShow("Ocurrió un error: " + result.Message, State.Warning);
            }
        }


        // Guardar un nuevo cliente
        protected async Task SaveCliente(PgClienteDto cliente)
        {
            try
            {
                _Loading.Show();
                var response = await _Rest.PostAsync<int?>("PgCliente", new { _PgCliente = cliente });
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

        // Actualizar un cliente existente
        protected async Task UpdateCliente(PgClienteDto cliente)
        {
            try
            {
                _Loading.Show();
                var response = await _Rest.PutAsync<int>("PgCliente", cliente, cliente.IdpgCliente);
                _Loading.Hide();
                _MessageShow(response.Message, response.State);
            }
            catch (Exception e)
            {
                _Loading.Hide();
                _MessageShow(e.Message, State.Error);
            }
        }

        // Eliminar un cliente
        protected async Task EliminarCliente(int idCliente)
        {
            await _MessageConfirm("¿Está seguro de eliminar el registro...?", async () =>
            {
                var response = await _Rest.DeleteAsync<int>("PgCliente", idCliente);
                if (!response.Succeeded)
                {
                    _MessageShow(response.Message, State.Error);
                }
                else
                {
                    _MessageShow(response.Message, response.State);
                    await GetClientes();
                    StateHasChanged();
                }
            });
        }

        // Validar y guardar/actualizar el formulario
        private async Task OnValidCliente(EditContext context)
        {
            if (_Cliente.IdpgCliente > 0)
            {
                await UpdateCliente(_Cliente);
            }
            else
            {
                await SaveCliente(_Cliente);
            }
            _Cliente = new PgClienteDto();
            await GetClientes();
            ToggleExpand();
            StateHasChanged();
        }

        // Preparar el formulario para editar un cliente
        protected void FormEditarCliente(PgClienteDto cliente)
        {
            _Cliente = cliente;
            ToggleExpand();
        }

        // Reiniciar el formulario
        private void ResetCliente() => _Cliente = new PgClienteDto();

        // Controlar la expansión del panel
        private void ToggleExpand() => expande = !expande;

        protected override async Task OnInitializedAsync()
        {
            await GetClientes();
        }
    }
}
