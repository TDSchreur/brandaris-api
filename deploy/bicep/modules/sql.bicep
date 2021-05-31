param location string = resourceGroup().location
param server_name string = uniqueString('sql', resourceGroup().id)
param database_name string = 'ApeDB'
param administratorLogin string

@secure()
param administratorLoginPassword string

resource server 'Microsoft.Sql/servers@2019-06-01-preview' = {
  name: server_name
  location: location
  properties: {
    administratorLogin: administratorLogin
    administratorLoginPassword: administratorLoginPassword
  }
}

resource sqlDB 'Microsoft.Sql/servers/databases@2020-08-01-preview' = {
  name: '${server.name}/${database_name}'
  location: location
  properties: {
    collation: 'SQL_Latin1_General_CP1_CI_AS'
  }
  sku: {
    name: 'Basic'
    tier: 'Basic'
  }
}

output sqlserver_fullyQualifiedDomainName string = server.properties.fullyQualifiedDomainName
output sqldatabase_name string = database_name
