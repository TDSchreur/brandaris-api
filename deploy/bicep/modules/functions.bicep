param insights_instrumentationkey string
param storage_account_name string
param serviceplan_name string
param functionapp_name string
param location string = resourceGroup().location
param openId_issuer string
param allowed_audiences array
param clientid string

var storagename = uniqueString(storage_account_name, resourceGroup().id)
var functionappname = uniqueString(functionapp_name, resourceGroup().id)

resource storageAccount 'Microsoft.Storage/storageAccounts@2019-06-01' = {
  name: storagename
  location: location
  kind: 'StorageV2'
  sku: {
    name: 'Standard_LRS'
    tier: 'Standard'
  }
}

resource hostingPlan 'Microsoft.Web/serverfarms@2020-10-01' = {
  name: serviceplan_name
  location: location
  sku: {
    name: 'Y1'
    tier: 'Dynamic'
  }
}

resource functionApp 'Microsoft.Web/sites@2020-06-01' = {
  name: functionappname
  location: location
  kind: 'functionapp'
  properties: {
    httpsOnly: true
    serverFarmId: hostingPlan.id
    clientAffinityEnabled: true
    siteConfig: {
      appSettings: [
        {
          'name': 'APPINSIGHTS_INSTRUMENTATIONKEY'
          'value': insights_instrumentationkey
        }
        {
          name: 'AzureWebJobsStorage'
          value: 'DefaultEndpointsProtocol=https;AccountName=${storageAccount.name};EndpointSuffix=${environment().suffixes.storage};AccountKey=${listKeys(storageAccount.id, storageAccount.apiVersion).keys[0].value}'
        }
        {
          'name': 'FUNCTIONS_EXTENSION_VERSION'
          'value': '~3'
        }
        {
          'name': 'FUNCTIONS_WORKER_RUNTIME'
          'value': 'dotnet-isolated'
        }
        {
          name: 'WEBSITE_CONTENTAZUREFILECONNECTIONSTRING'
          value: 'DefaultEndpointsProtocol=https;AccountName=${storageAccount.name};EndpointSuffix=${environment().suffixes.storage};AccountKey=${listKeys(storageAccount.id, storageAccount.apiVersion).keys[0].value}'
        }
      ]
    }
  }
}

resource authsettings 'Microsoft.Web/sites/config@2020-12-01' = {
  parent: functionApp
  name: 'authsettingsV2'
  properties: {
    platform: {
      enabled: true
    }
    globalValidation: {
      requireAuthentication: true
      unauthenticatedClientAction: 'Return401'
    }
    identityProviders: {
      azureActiveDirectory: {
        enabled: true
        registration: {
          openIdIssuer: openId_issuer
          clientId: clientid
        }
        validation: {
          allowedAudiences: allowed_audiences
        }
        isAutoProvisioned: false
      }
    }
    login: {
      tokenStore: {
        enabled: false
      }
    }
    httpSettings: {
      requireHttps: true
    }
  }
}
