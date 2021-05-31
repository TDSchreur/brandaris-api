param insights_name string
param location string = resourceGroup().location

resource insights 'microsoft.insights/components@2015-05-01' = {
  name: insights_name
  location: location
  kind: 'web'
  properties: {
    Application_Type: 'web'
  }
}

output instrumentationKey string = insights.properties.InstrumentationKey
