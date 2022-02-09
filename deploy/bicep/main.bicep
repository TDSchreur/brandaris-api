@secure()
param sql_administratorLoginPassword string
param sql_administratorLogin string

@secure()
param fe_clientsecret string

@minValue(1)
@maxValue(5)
param serviceplan_capacity int
param serviceplan_sku string
param serviceplan_tier string
param project_name string

var api_name = '${project_name}-api'
var frontend_name = '${project_name}-frontend'
var insights_name = '${project_name}-insights'
param serviceplan_name = '${project_name}-plan'
var location = resourceGroup().location

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

module plan 'modules/plan.bicep' = {
  name: 'plan-deployment'
  params: {
    serviceplan_name: serviceplan_name
    serviceplan_sku: serviceplan_sku
    serviceplan_tier: serviceplan_tier
    serviceplan_capacity: serviceplan_capacity
  }
}

module api './modules/api.bicep' = {
  name: 'web-deployment'
  params: {
    insights_instrumentationkey: insights.outputs.instrumentationKey
    serviceplanId: plan.outputs.serviceplanId
    name: api_name
    sqlserver_fullyQualifiedDomainName: sql.outputs.sqlserver_fullyQualifiedDomainName
    sqlserver_database_name: sql.outputs.sqldatabase_name
    sqlserver_username: sql_administratorLogin
    sqlserver_password: sql_administratorLoginPassword
    location: location
  }
}

module frontend 'modules/frontend.bicep' = {
  name: 'frontend-deployment'
  params: {
    insights_instrumentationkey: insights.outputs.instrumentationKey
    serviceplanId: plan.outputs.serviceplanId
    name: frontend_name
    location: location
    clientsecret: fe_clientsecret
  }
}
