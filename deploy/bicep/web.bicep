param serviceplan_name string

@allowed([
  'B1'
  'D1'
  'S1'
])
param serviceplan_sku string

@minValue(1)
@maxValue(5)
param serviceplan_capacity int

param api_name string

param ai_instrumentationkey string

var api_name_unique = '${api_name}-biceps'

resource serviceplan 'Microsoft.Web/serverfarms@2018-02-01' = {
  name: serviceplan_name
  location: resourceGroup().location
  sku: {
    name: serviceplan_sku
    capacity: serviceplan_capacity
  }
  kind: 'app'
  properties: {
    perSiteScaling: false
    isXenon: false
  }
}

resource api 'Microsoft.Web/sites@2018-11-01' = {
  name: api_name_unique
  location: resourceGroup().location
  kind: 'api'
  properties: {
    enabled: true
    hostNameSslStates: [
      {
        name: '${api_name_unique}.azurewebsites.net'
        sslState: 'Disabled'
        hostType: 'Standard'
      }
      {
        name: '${api_name_unique}.scm.azurewebsites.net'
        sslState: 'Disabled'
        hostType: 'Repository'
      }
    ]
    serverFarmId: serviceplan.id
    siteConfig: {
      appSettings: [
        {
          name: 'APPINSIGHTS_INSTRUMENTATIONKEY'
          value: ai_instrumentationkey
        }
      ]
    }
  }
}

resource functionAppConfig 'Microsoft.Web/sites/config@2020-06-01' = {
  parent: api
  name: 'web'
  properties: {
    netFrameworkVersion: 'v4.0'
    phpVersion: 'off'
    requestTracingEnabled: false
    remoteDebuggingEnabled: false
    httpLoggingEnabled: false
    logsDirectorySizeLimit: 35
    detailedErrorLoggingEnabled: false
    publishingUsername: api_name
    use32BitWorkerProcess: false
    webSocketsEnabled: false
    alwaysOn: true
    managedPipelineMode: 'Integrated'
    loadBalancing: 'LeastRequests'
    autoHealEnabled: false
    localMySqlEnabled: false
    ipSecurityRestrictions: [
      {
        ipAddress: 'Any'
        action: 'Allow'
        priority: 1
        name: 'Allow all'
        description: 'Allow all access'
      }
    ]
    scmIpSecurityRestrictions: [
      {
        ipAddress: 'Any'
        action: 'Allow'
        priority: 1
        name: 'Allow all'
        description: 'Allow all access'
      }
    ]
    scmIpSecurityRestrictionsUseMain: false
    http20Enabled: true
    minTlsVersion: '1.2'
  }
}
