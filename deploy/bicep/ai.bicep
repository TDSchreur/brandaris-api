param ai_name string

resource ai 'microsoft.insights/components@2015-05-01' = {
  name: ai_name
  location: resourceGroup().location
  kind: 'web'
  properties: {
    Application_Type: 'web'
  }
}

output instrumentationKey string = ai.properties.InstrumentationKey
