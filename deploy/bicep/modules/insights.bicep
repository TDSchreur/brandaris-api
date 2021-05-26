param insights_name string

resource insights 'microsoft.insights/components@2015-05-01' = {
  name: insights_name
  location: resourceGroup().location
  kind: 'web'
  properties: {
    Application_Type: 'web'
  }
}

output instrumentationKey string = insights.properties.InstrumentationKey
