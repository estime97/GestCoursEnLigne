using BlazorHero.CleanArchitecture.Application.Features.Requisitions.Commands;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.SuiviRequisitions;

using Microsoft.AspNetCore.Components;

using MudBlazor;

using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Client.Pages.Content
{
    public partial class UpdateStatut
    {
        [CascadingParameter] IMudDialogInstance MudDialog { get; set; }
        [Inject] public ISuiviRequisitionManager suiviRequisitionManager { get; set; }
        [Parameter] public UpdateRequisition.Command Command { get; set; } = new();

        private async Task SubmitAsync()
        {
            var response = await suiviRequisitionManager.UpdateRequisition(Command);
            if (response.Succeeded)
            {
                _snackBar.Add(response.Messages[0], Severity.Success);
                MudDialog.Close();
            }
            else
            {
                foreach (string message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }
    }
}
