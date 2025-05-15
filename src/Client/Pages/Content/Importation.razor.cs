using BlazorHero.CleanArchitecture.Application.Enums;
using BlazorHero.CleanArchitecture.Application.Features.Requisitions.Commands;
using BlazorHero.CleanArchitecture.Application.Requests;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.SuiviRequisitions;
using BlazorHero.CleanArchitecture.Shared.Constants.Application;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

using MudBlazor;

using System;
using System.IO;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Client.Pages.Content
{
    public partial class Importation
    {
        [CascadingParameter] IMudDialogInstance MudDialog { get; set; }
        [Inject] public ISuiviRequisitionManager suiviRequisitionManager { get; set; }
        public ImportRequisitions.Command Command { get; set; } = new();
        private IBrowserFile _file;
        public UploadRequest UploadRequest { get; set; } = new();
        private async Task UploadFile(InputFileChangeEventArgs e)
        {
            try
            {
                _file = e.File;
                if (_file != null)
                {
                    var buffer = new byte[_file.Size];
                    var extension = Path.GetExtension(_file.Name);
                    await _file.OpenReadStream(_file.Size).ReadExactlyAsync(buffer);
                    UploadRequest = new UploadRequest
                    {
                        Data = buffer,
                        FileName = _file.Name,
                        UploadType = UploadType.Document,
                        Extension = extension
                    };
                }
            }
            catch (Exception ex)
            {
                _snackBar.Add(ex.Message, Severity.Error);
            }
        }
        private async Task Submit()
        {
            var response = await suiviRequisitionManager.ImportRequisitions(new ImportRequisitions.Command()
            {
                UploadRequest = UploadRequest
            });

            if (response.Succeeded)
            {
                _snackBar.Add(response.Messages[0], Severity.Success);
                MudDialog.Close();
            }
            else
            {
                _snackBar.Add(response.Messages[0], Severity.Error);
            }
        }
        private async Task ExportToExcel()
        {
            var base64 = await suiviRequisitionManager.ExportToExcelAsync();
            await _jsRuntime.InvokeVoidAsync("Download", new
            {
                ByteArray = base64,
                FileName = $"{nameof(Requisitions).ToLower()}_{DateTime.Now:ddMMyyyyHHmmss}.xlsx",
                MimeType = ApplicationConstants.MimeTypes.OpenXml
            });
            _snackBar.Add("Fichier excel exporté", Severity.Success);
        }
    }
}
