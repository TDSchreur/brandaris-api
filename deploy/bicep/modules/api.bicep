param location string = resourceGroup().location

param name string

param insights_instrumentationkey string

param serviceplanId string

param sqlserver_fullyQualifiedDomainName string
param sqlserver_database_name string
param sqlserver_username string
param sqlserver_password string

resource symbolicname 'Microsoft.ManagedIdentity/userAssignedIdentities@2018-11-30' = {
  name: 'api_identity'
  location: location
}

resource api 'Microsoft.Web/sites@2018-11-01' = {
  name: name
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
        name: '${name}.azurewebsites.net'
        sslState: 'Disabled'
        hostType: 'Standard'
      }
      {
        name: '${name}.scm.azurewebsites.net'
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
          'name': 'Authentication:AppIdUri'
          'value': 'api://brandaris-api'
        }

        {
          'name': 'Authentication:ClientId'
          'value': 'c0a8de33-97fe-45a0-8c63-fe54a39cd842'
        }

        {
          'name': 'Authentication:TenantId'
          'value': 'ae86fed2-d115-4a00-b6ed-68ff87b986f7'
        }
      ]
    }
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
