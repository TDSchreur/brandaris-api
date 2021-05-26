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

module stg './storage.bicep' = {
  name: 'storageDeploy'
  params: {
    storageAccountName: storageAccountName
    location: location
    storageSku: storage_sku
  }
}

module ai './ai.bicep' = {
  name: 'aiDeploy'
  params: {
    ai_name: ai_name
  }
}

module web './web.bicep' = {
  name: 'webDeploy'
  params: {
    ai_instrumentationkey: ai.outputs.instrumentationKey
    serviceplan_name: serviceplan_name
    serviceplan_sku: serviceplan_sku
    serviceplan_capacity: serviceplan_capacity
    api_name: api_name
  }
}

output storageId string = stg.outputs.storageId
