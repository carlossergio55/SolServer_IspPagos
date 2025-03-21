using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infraestructura.Abstract;
using Infraestructura.Models.Pagos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using MudBlazor;
using System.Linq;

namespace Server.Pages.Pages.Pagos
{
    public partial class PgPago
    {
        // Control de la expansión del panel
        private bool expande = false;
        [Inject]
        private IDialogService _dialogService { get; set; }
        // Modelo para el pago
        public PgPagoDto _Pago = new PgPagoDto();
        public List<PgClienteDto> _clientes = new List<PgClienteDto>();
        private string searchClientName = string.Empty;

        // Obtener la lista de clientes
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
        // Lista de pagos
        public List<PgPagoDto> _pagos = new List<PgPagoDto>();

        // Obtener la lista de pagos
        protected async Task GetPagos()
        {
            var result = await _Rest.GetAsync<List<PgPagoDto>>("PgPago/GetAll");
            if (result.State == State.Success)
            {
                // Filtrar por EstadoPago igual a "Pendiente" y ordenar por IdpgPago en orden descendente
                _pagos = result.Data
                    .Where(p => p.EstadoPago == "Pendiente")
                    .OrderByDescending(p => p.IdpgPago)
                    .ToList();
                return;
            }
            else
            {
                _MessageShow("Ocurrió un error: " + result.Message, State.Warning);
            }
        }



        // Guardar un nuevo pago
        protected async Task SavePago(PgPagoDto pago)
        {
            try
            {
                _Loading.Show();
                var response = await _Rest.PostAsync<int?>("PgPago", new { _PgPago = pago });
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

        // Actualizar un pago existente
        protected async Task UpdatePago(PgPagoDto pago)
        {
            try
            {
                _Loading.Show();
                var response = await _Rest.PutAsync<int>("PgPago", pago, pago.IdpgPago);
                _Loading.Hide();
                _MessageShow(response.Message, response.State);
            }
            catch (Exception e)
            {
                _Loading.Hide();
                _MessageShow(e.Message, State.Error);
            }
        }
        private async Task CambiarEstadoPago(PgPagoDto pago)
        {
            if (pago.EstadoPago != "Pendiente")
            {
                _MessageShow("El pago ya se encuentra confirmado.", State.Warning);
                return;
            }

            var dialog = _dialogService.Show<ConfirmarPagoDialog>("Confirmar Pago");
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                pago.Tipopago = result.Data.ToString();
                pago.EstadoPago = "Pagado";
                pago.FechaPago = DateTime.Now; // Opcional: registrar fecha de pago

                await UpdatePago(pago);
                _MessageShow("Pago confirmado correctamente", State.Success);
                await GetPagos();
            }
        }



        // Eliminar un pago
        protected async Task EliminarPago(int idPago)
        {
            await _MessageConfirm("¿Está seguro de eliminar el registro...?", async () =>
            {
                var response = await _Rest.DeleteAsync<int>("PgPago", idPago);
                if (!response.Succeeded)
                {
                    _MessageShow(response.Message, State.Error);
                }
                else
                {
                    _MessageShow(response.Message, response.State);
                    await GetPagos();
                    StateHasChanged();
                }
            });
        }

        // Validar y guardar/actualizar el formulario
        private async Task OnValidPago(EditContext context)
        {
            if (_Pago.IdpgPago > 0)
            {
                await UpdatePago(_Pago);
            }
            else
            {
                await SavePago(_Pago);
            }
            _Pago = new PgPagoDto();
            await GetPagos();
            ToggleExpand();
            StateHasChanged();
        }

        // Preparar el formulario para editar un pago
        protected void FormEditarPago(PgPagoDto pago)
        {
            _Pago = pago;
            ToggleExpand();
        }

        // Reiniciar el formulario
        private void ResetPago() => _Pago = new PgPagoDto();

        // Controlar la expansión del panel
        private void ToggleExpand() => expande = !expande;

        protected override async Task OnInitializedAsync()
        {
            await GetPagos();
            await GetClientes();
        }

    }
}
