param serviceplan_name string
param location string = resourceGroup().location

@allowed([
  'B1'
  'D1'
  'S1'
])
param serviceplan_sku string

param serviceplan_tier string

@minValue(1)
@maxValue(5)
param serviceplan_capacity int

param api_name string

param insights_instrumentationkey string

param sqlserver_fullyQualifiedDomainName string
param sqlserver_database_name string
param sqlserver_username string
param sqlserver_password string

var api_name_unique = '${api_name}-biceps'

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

resource symbolicname 'Microsoft.ManagedIdentity/userAssignedIdentities@2018-11-30' = {
  name: 'api_identity'
  location: location
}

resource api 'Microsoft.Web/sites@2018-11-01' = {
  name: api_name_unique
  location: location
  kind: 'api'
  identity: {
    type: 'UserAssigned'
    userAssignedIdentities: {
      '${symbolicname.id}': {}
    }
  }

  properties: {
    enabled: true
    httpsOnly: true
    clientAffinityEnabled: false
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
  }
}

resource siteconfig 'Microsoft.Web/sites/config@2020-06-01' = {
  parent: api
  name: 'appsettings'
  properties: {
    'APPINSIGHTS_INSTRUMENTATIONKEY': insights_instrumentationkey
    'Authentication:AppIdUri': 'api://brandaris-api'
    'Authentication:ClientId': 'c0a8de33-97fe-45a0-8c63-fe54a39cd842'
    'Authentication:TenantId': 'ae86fed2-d115-4a00-b6ed-68ff87b986f7'
  }
}

resource connectionStrings 'Microsoft.Web/sites/config@2020-06-01' = {
  parent: api
  name: 'connectionstrings'
  properties: {
    Default: {
      type: 'SQLAzure'
      value: 'Data Source=tcp:${sqlserver_fullyQualifiedDomainName} ,1433;Initial Catalog=${sqlserver_database_name};User Id=${sqlserver_username};Password=${sqlserver_password}'
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
    healthCheckPath: '/health/readiness'
  }
}
