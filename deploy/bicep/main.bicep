param sql_administratorLogin string
param sql_administratorLoginPassword string

@minValue(1)
@maxValue(5)
param serviceplan_capacity int
param serviceplan_name string
param serviceplan_sku string
param serviceplan_tier string

param api_name string

var location = resourceGroup().location
var insights_name = 'insights-${api_name}'

module sql './modules/sql.bicep' = {
  name: 'sql-deployment'
  params: {
    administratorLogin: sql_administratorLogin
    administratorLoginPassword: sql_administratorLoginPassword
  }
}

module insights './modules/insights.bicep' = {
  name: 'insights-deployment'
  params: {
    insights_name: insights_name
  }
}

module web './modules/web.bicep' = {
  name: 'web-deployment'
  params: {
    insights_instrumentationkey: insights.outputs.instrumentationKey
    serviceplan_name: serviceplan_name
    serviceplan_sku: serviceplan_sku
    serviceplan_tier: serviceplan_tier
    serviceplan_capacity: serviceplan_capacity
    api_name: api_name
    sqlserver_fullyQualifiedDomainName: sql.outputs.sqlserver_fullyQualifiedDomainName
    sqlserver_database_name: sql.outputs.sqldatabase_name
    sqlserver_username: sql_administratorLogin
    sqlserver_password: sql_administratorLoginPassword
  }
}
