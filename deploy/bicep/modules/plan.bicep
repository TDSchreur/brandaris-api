@allowed([
  'B1'
  'D1'
  'S1'
])
param serviceplan_sku string
@minValue(1)
@maxValue(5)
param serviceplan_capacity int
param serviceplan_tier string
param serviceplan_name string

param location string = resourceGroup().location

resource serviceplan 'Microsoft.Web/serverfarms@2020-12-01' = {
  name: serviceplan_name
  location: location
  sku: {
    name: serviceplan_sku
    tier: serviceplan_tier
    capacity: serviceplan_capacity
  }
  kind: 'web'
  properties: {
    reserved: false
  }
}

output serviceplanId string = serviceplan.id
