param location string = resourceGroup().location

param name string

param insights_instrumentationkey string
param serviceplanId string

@secure()
param clientsecret string

var name_unique = '${name}-biceps'

resource api 'Microsoft.Web/sites@2021-02-01' = {
  name: name_unique
  location: location
  kind: 'web'
  properties: {
    enabled: true
    httpsOnly: true
    clientAffinityEnabled: false
    hostNameSslStates: [
      {
        name: '${name_unique}.azurewebsites.net'
        sslState: 'Disabled'
        hostType: 'Standard'
      }
      {
        name: '${name_unique}.scm.azurewebsites.net'
        sslState: 'Disabled'
        hostType: 'Repository'
      }
    ]
    serverFarmId: serviceplanId
    siteConfig: {
      netFrameworkVersion: 'v6.0'
      minTlsVersion: '1.2'
      alwaysOn: true
      appSettings: [
        {
          'name': 'APPINSIGHTS_INSTRUMENTATIONKEY'
          'value': insights_instrumentationkey
        }
        {
          'name': 'AzureAd:Domain'
          'value': 'dennistdschreur.onmicrosoft.com'
        }
        {
          'name': 'AzureAd:ClientId'
          'value': 'cb79e4b9-ff8b-4659-ae61-0a94e2f1e631'
        }
        {
          'name': 'AzureAd:TenantId'
          'value': 'ae86fed2-d115-4a00-b6ed-68ff87b986f7'
        }
        {
          'name': 'AzureAd:ClientSecret'
          'value': clientsecret
        }
      ]
    }
  }
}

resource diagnosticSettings 'Microsoft.Web/sites/config@2021-02-01' = {
  parent: api
  name: 'logs'
  properties: {
    applicationLogs: {
      fileSystem: {
        level: 'Information'
      }
    }
  }
}

resource webConfig 'Microsoft.Web/sites/config@2020-06-01' = {
  parent: api
  name: 'web'
  properties: {
    netFrameworkVersion: 'v6.0'
    phpVersion: 'off'
    requestTracingEnabled: false
    remoteDebuggingEnabled: false
    httpLoggingEnabled: false
    logsDirectorySizeLimit: 35
    detailedErrorLoggingEnabled: false
    publishingUsername: name
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
    healthCheckPath: '/health/readiness'
  }
}
