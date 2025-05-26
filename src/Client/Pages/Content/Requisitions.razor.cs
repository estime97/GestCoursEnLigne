using BlazorHero.CleanArchitecture.Application.Features.Requisitions;
using BlazorHero.CleanArchitecture.Application.Requests;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.SuiviRequisitions;
using BlazorHero.CleanArchitecture.Shared.Constants.Permission;
using BlazorHero.CleanArchitecture.Shared.Constants.Storage;
using BlazorHero.CleanArchitecture.Shared.Wrapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

using MudBlazor;

using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Client.Pages.Content
{
    public partial class Requisitions
    {
        [Inject] public ISuiviRequisitionManager suiviRequisitionManager { get; set; }
        private IEnumerable<RequisitionResponse> Items;
        private MudTable<RequisitionResponse> Table;
        private int TotalItems = 0;
        private string SearchString { get; set; } = "";
        private ClaimsPrincipal _authenticationStateProviderUser;
        private bool _canImportRequisitions;
        private bool _canUpdateRequisitions;
        private bool _canViewRequisitions;
        protected async override Task OnInitializedAsync()
        {
            _authenticationStateProviderUser = await _stateProvider.GetAuthenticationStateProviderUserAsync();
            var passwordIsExpired = await _localStorage.GetItemAsync<bool>(StorageConstants.Local.PassWordExpired);
            if (passwordIsExpired)
            {
                _navigationManager.NavigateTo("/change-password/");
            }
            _canImportRequisitions = (await _authorizationService.AuthorizeAsync(_authenticationStateProviderUser, Permissions.Requisitions.Import)).Succeeded && !passwordIsExpired;
            _canUpdateRequisitions = (await _authorizationService.AuthorizeAsync(_authenticationStateProviderUser, Permissions.Requisitions.Update)).Succeeded && !passwordIsExpired;
            _canViewRequisitions = (await _authorizationService.AuthorizeAsync(_authenticationStateProviderUser, Permissions.Requisitions.View)).Succeeded && !passwordIsExpired;
        }
        private async Task<TableData<RequisitionResponse>> ServerReload(TableState state, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(SearchString))
            {
                state.Page = 0;
            }
            PaginatedResult<RequisitionResponse> response = await suiviRequisitionManager.GetAllRequisitions(new PaginateRequest()
            {
                PageNumber = state.Page + 1,
                PageSize = state.PageSize,
                SearchString = SearchString,
            });
            if (response.Succeeded)
            {
                TotalItems = response.TotalCount;
                Items = response.Data;
            }
            else
            {
                foreach (string message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
            return new TableData<RequisitionResponse> { TotalItems = TotalItems, Items = Items };
        }
        private void RefreshData(string searchText)
        {
            SearchString = searchText;
            Table.ReloadServerData();
            StateHasChanged();
        }
        private async Task Import()
        {
            var dialog = await _dialogService.ShowAsync<Importation>("",
                options: new DialogOptions()
                {
                    CloseOnEscapeKey = true,
                    MaxWidth = MaxWidth.ExtraSmall,
                    BackdropClick = false,
                    CloseButton = true
                });
            DialogResult result = await dialog.Result;
            if (!result.Canceled)
            {
                RefreshData("");
            }
        }
        private async Task Update(RequisitionResponse requisition)
        {
            var dialog = await _dialogService.ShowAsync<UpdateStatut>("", parameters: new DialogParameters()
            {
                {nameof(UpdateStatut.Requisition), requisition},
            },
            options: new DialogOptions()
            {
                CloseOnEscapeKey = true,
                MaxWidth = MaxWidth.Medium,
                BackdropClick = false,
                CloseButton = true
            });
            DialogResult result = await dialog.Result;
            if (!result.Canceled)
            {
                RefreshData("");
            }
        }
        private async Task View(RequisitionResponse response)
        {
            var dialog = await _dialogService.ShowAsync<RequisitionDetails>("", parameters: new DialogParameters()
            {
                {nameof(RequisitionDetails.Requisition), response},
            },
            options: new DialogOptions()
            {
                CloseOnEscapeKey = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true,
                BackdropClick = true
            });
            await dialog.Result;
        }
    }
}
