# Azure Function APIM Testing

Integrating Azure API Management .NET SDK into Azure Functions

## AZ AD APP

- Create a new AD app with APIM permissions set.
```
az ad sp create-for-rbac -n "APIM SDK Access" --role "API Management Service Contributor" --scopes /subscriptions/$(az account show --query id -o tsv)
```
Take note of values, they will be needed in `local.settings.json`

```
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "RESOURCE_GROUP": "",
    "SERVICE_NAME": "",
    "SUBSCRIPTION_ID": "",
    "TENANT_ID": "",
    "CLIENT_ID": "",
    "CLIENT_SECRET": ""
  }
}
```
