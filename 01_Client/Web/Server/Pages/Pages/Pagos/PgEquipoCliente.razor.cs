using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infraestructura.Abstract;
using Infraestructura.Models.Pagos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using Microsoft.JSInterop;
namespace Server.Pages.Pages.Pagos
{
    public partial class PgEquipoCliente
    {
        // Control de la expansión del panel de formulario
        private bool expande = false;

        // Modelo para el equipo de cliente
        public PgEquipoClienteDto _Equipo = new PgEquipoClienteDto();

        // Lista de equipos
        public List<PgEquipoClienteDto> _equipos = new List<PgEquipoClienteDto>();
        public List<PgClienteDto> _clientes = new List<PgClienteDto>();
        private string searchClientName = string.Empty;
        [Inject] public IJSRuntime JS { get; set; } // Asegúrate de inyectar IJSRuntime

        private async Task OpenIpAiros(PgEquipoClienteDto equipo)
        {
            if (string.IsNullOrEmpty(equipo.IpAiros)) return;

            string auth = "";
            if (!string.IsNullOrEmpty(equipo.UsuarioAiros))
            {
                string usuario = Uri.EscapeDataString(equipo.UsuarioAiros);
                string contraseña = Uri.EscapeDataString(equipo.ContraseñaAiros ?? "");
                auth = $"{usuario}:{contraseña}@";
            }

            string url = $"http://{auth}{equipo.IpAiros}";
            await JS.InvokeVoidAsync("window.open", url, "_blank");
        }

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
        // Obtener la lista de equipos
        protected async Task GetEquipos()
        {
            var result = await _Rest.GetAsync<List<PgEquipoClienteDto>>("PgEquipoCliente/GetAll");
            if (result.State == State.Success)
            {
                _equipos = result.Data;
                return;
            }
            else
            {
                _MessageShow("Ocurrió un error: " + result.Message, State.Warning);
            }
        }

        // Guardar un nuevo equipo
        protected async Task SaveEquipo(PgEquipoClienteDto equipo)
        {
            try
            {
                _Loading.Show();
                var response = await _Rest.PostAsync<int?>("PgEquipoCliente", new { _PgEquipoCliente = equipo });
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

        // Actualizar un equipo existente
        protected async Task UpdateEquipo(PgEquipoClienteDto equipo)
        {
            try
            {
                _Loading.Show();
                var response = await _Rest.PutAsync<int>("PgEquipoCliente", equipo, equipo.IdpgEquipo);
                _Loading.Hide();
                _MessageShow(response.Message, response.State);
            }
            catch (Exception e)
            {
                _Loading.Hide();
                _MessageShow(e.Message, State.Error);
            }
        }

        // Eliminar un equipo
        protected async Task EliminarEquipo(int idEquipo)
        {
            await _MessageConfirm("¿Está seguro de eliminar el registro...?", async () =>
            {
                var response = await _Rest.DeleteAsync<int>("PgEquipoCliente", idEquipo);
                if (!response.Succeeded)
                {
                    _MessageShow(response.Message, State.Error);
                }
                else
                {
                    _MessageShow(response.Message, response.State);
                    await GetEquipos();
                    StateHasChanged();
                }
            });
        }

        // Validar y guardar/actualizar el formulario
        private async Task OnValidEquipo(EditContext context)
        {
            if (_Equipo.IdpgEquipo > 0)
            {
                await UpdateEquipo(_Equipo);
            }
            else
            {
                await SaveEquipo(_Equipo);
            }
            _Equipo = new PgEquipoClienteDto();
            await GetEquipos();
            ToggleExpand();
            StateHasChanged();
        }

        // Preparar el formulario para editar un equipo
        protected void FormEditarEquipo(PgEquipoClienteDto equipo)
        {
            _Equipo = equipo;
            ToggleExpand();
        }

        // Reiniciar el formulario
        private void ResetEquipo() => _Equipo = new PgEquipoClienteDto();

        // Controlar la expansión del panel
        private void ToggleExpand() => expande = !expande;

        protected override async Task OnInitializedAsync()
        {
            await GetEquipos();
            await GetClientes();
        }
    }
}
