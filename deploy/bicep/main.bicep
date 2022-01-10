@secure()
param sql_administratorLoginPassword string
param sql_administratorLogin string

@minValue(1)
@maxValue(5)
param serviceplan_capacity int
param serviceplan_name string
param serviceplan_sku string
param serviceplan_tier string

param api_name string

// param storage_account_name string
// param functionapp_name string
// param function_serviceplan_name string
// param functionapp_openid_issuer string
// param functionapp_allowed_audiences array
// param functionapp_clientid string

var location = resourceGroup().location
var insights_name = 'insights-${api_name}'

module sql './modules/sql.bicep' = {
  name: 'sql-deployment'
  params: {
    administratorLogin: sql_administratorLogin
    administratorLoginPassword: sql_administratorLoginPassword
    location: location
  }
}

module insights './modules/insights.bicep' = {
  name: 'insights-deployment'
  params: {
    insights_name: insights_name
    location: location
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
    location: location
  }
}

// module function './modules/functions.bicep' = {
//   name: 'function-deployment'
//   params: {
//     insights_instrumentationkey: insights.outputs.instrumentationKey
//     location: location
//     storage_account_name: storage_account_name
//     functionapp_name: functionapp_name
//     serviceplan_name: function_serviceplan_name
//     openId_issuer: functionapp_openid_issuer
//     allowed_audiences: functionapp_allowed_audiences
//     clientid: functionapp_clientid
//   }
// }
