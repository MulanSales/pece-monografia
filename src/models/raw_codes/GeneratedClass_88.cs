@using BTCPayServer.Client
@using BTCPayServer.Controllers
@using BTCPayServer.Security.GreenField
@model ManageController.AddApiKeyViewModel

@{
ViewData.SetActivePageAndTitle(ManageNavPages.APIKeys, "Generate API Key");
}

@section PageHeadContent {
<style>
.remove-btn { font-size: 1.5rem; border-radius: 0; }
.remove-btn:hover { background-color: #CCCCCC; }
</style>
}

@section PageFootContent {
<partial name="_ValidationScriptsPartial"/>
<script>
delegate('click', `form button[value*=':']`, function (e) {
const { form, value } = e.target
const [action] = form.getAttribute('action').split('#')
const anchor = value.replace(/:.*/, '')
form.setAttribute('action', `${action}#${anchor}`)
})
</script>
}

<h2 class="mb-3">@ViewData["PageTitle"]</h2>

<p>Generate a new api key to use BTCPay through its API.</p>

<form method="post" asp-action="AddApiKey">
<div asp-validation-summary="All" class="text-danger"></div>

<div class="form-group">
<label asp-for="Label" class="form-label"></label>
<input asp-for="Label" class="form-control"/>
<span asp-validation-for="Label" class="text-danger"></span>
</div>

<h5 class="mt-4 mb-3">Permissions</h5>
<div class="list-group mb-4">
@for (int i = 0; i < Model.PermissionValues.Count; i++)
{
@if (Model.PermissionValues[i].Forbidden)
{
<input type="hidden" asp-for="PermissionValues[i].Value" value="false" />
}
else
{
<div class="list-group-item py-3">
<input type="hidden" asp-for="PermissionValues[i].Permission" />
@if (Policies.IsStorePolicy(Model.PermissionValues[i].Permission))
{
<input type="hidden" asp-for="PermissionValues[i].StoreMode" value="@Model.PermissionValues[i].StoreMode" />
@if (Model.PermissionValues[i].StoreMode == ManageController.AddApiKeyViewModel.ApiKeyStoreMode.AllStores)
{
<div class="form-check">
<input id="@Model.PermissionValues[i].Permission" type="checkbox" asp-for="PermissionValues[i].Value" class="form-check-input ms-n4"/>
<label for="@Model.PermissionValues[i].Permission" class="h5 form-check-label me-2 mb-1">
<span class="me-lg-1">@Model.PermissionValues[i].Title</span>
<small class="text-muted text-break d-block my-2 d-lg-inline-block my-lg-0">@Model.PermissionValues[i].Permission</small>
</label>
<div>
<span asp-validation-for="PermissionValues[i].Value" class="text-danger"></span>
<div class="text-muted">@Model.PermissionValues[i].Description</div>
@if (Model.Stores.Any())
{
<button type="submit" class="btn btn-link p-0" name="command" value="@($"{Model.PermissionValues[i].Permission}:change-store-mode")">Select specific stores</button>
}
</div>
</div>
}
else
{
<h5 class="mb-1" id="@Model.PermissionValues[i].Permission">
<span class="me-lg-1">@Model.PermissionValues[i].Title</span>
<small class="text-muted text-break d-block my-2 d-lg-inline-block my-lg-0">@Model.PermissionValues[i].Permission</small>
</h5>
<div class="text-muted">@Model.PermissionValues[i].Description</div>
<button type="submit" class="btn btn-link p-0" name="command" value="@($"{Model.PermissionValues[i].Permission}:change-store-mode")">Give permission to all stores instead</button>

@if (!Model.Stores.Any())
{
<p class="text-warning mt-2 mb-0">
You currently have no stores configured.
</p>
}
@for (var index = 0; index < Model.PermissionValues[i].SpecificStores.Count; index++)
{
<div class="input-group my-3">
@if (Model.PermissionValues[i].SpecificStores[index] == null)
{
<select asp-for="PermissionValues[i].SpecificStores[index]" class="form-select w-auto flex-grow-0" asp-items="@(new SelectList(Model.Stores.Where(data => !Model.PermissionValues[i].SpecificStores.Contains(data.Id)), nameof(StoreData.Id), nameof(StoreData.StoreName)))"></select>
}
else
{
var store = Model.Stores.SingleOrDefault(data => data.Id == Model.PermissionValues[i].SpecificStores[index]);
<select asp-for="PermissionValues[i].SpecificStores[index]" class="form-select w-auto flex-grow-0" asp-items="@(new SelectList(new[] {store}, nameof(StoreData.Id), nameof(StoreData.StoreName), store.Id))"></select>
}
<span asp-validation-for="PermissionValues[i].SpecificStores[index]" class="text-danger"></span>
<button type="submit" title="Remove Store Permission" name="command" value="@($"{Model.PermissionValues[i].Permission}:remove-store:{index}")" class="btn btn-danger">
Remove
</button>
</div>
}
@if (Model.PermissionValues[i].SpecificStores.Count < Model.Stores.Length)
{
<div class="mt-3 mb-2">
<button type="submit" name="command" value="@($"{Model.PermissionValues[i].Permission}:add-store")" class="btn btn-secondary">Add another store</button>
</div>
}
}
}
else
{
<div class="form-check">
<input id="@Model.PermissionValues[i].Permission" type="checkbox" asp-for="PermissionValues[i].Value" class="form-check-input ms-n4" />
<label for="@Model.PermissionValues[i].Permission" class="h5 form-check-label me-2 mb-1">
<span class="me-lg-1">@Model.PermissionValues[i].Title</span>
<small class="text-muted text-break d-block my-2 d-lg-inline-block my-lg-0">@Model.PermissionValues[i].Permission</small>
</label>
<div>
<span asp-validation-for="PermissionValues[i].Value" class="text-danger"></span>
<span class="text-muted">@Model.PermissionValues[i].Description</span>
</div>
</div>
}
</div>
}
}
</div>

<button type="submit" class="btn btn-primary" id="Generate">Generate API Key</button>
</form>
