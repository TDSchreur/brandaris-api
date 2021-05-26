@minLength(3)
@maxLength(6)
param namePrefix string = 'tds'

param storage_sku string = 'Standard_LRS'

@minValue(1)
@maxValue(5)
param serviceplan_capacity int
param serviceplan_name string
param serviceplan_sku string

param api_name string

var location = resourceGroup().location
var storageAccountName = '${namePrefix}${uniqueString(resourceGroup().id)}'
var ai_name = 'ai-${api_name}'

module stg './modules/storage.bicep' = {
  name: 'storageDeploy'
  params: {
    storageAccountName: storageAccountName
    location: location
    storageSku: storage_sku
  }
}

module insights './modules/insights.bicep' = {
  name: 'aiDeploy'
  params: {
    insights_name: ai_name
  }
}

module web './modules/web.bicep' = {
  name: 'webDeploy'
  params: {
    insights_instrumentationkey: insights.outputs.instrumentationKey
    serviceplan_name: serviceplan_name
    serviceplan_sku: serviceplan_sku
    serviceplan_capacity: serviceplan_capacity
    api_name: api_name
  }
}

output storageId string = stg.outputs.storageId
