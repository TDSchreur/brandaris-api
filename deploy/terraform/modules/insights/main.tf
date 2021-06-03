resource "azurerm_application_insights" "insights" {
  name                = "insights_terraform"
  location            = var.location
  resource_group_name = var.resource_group_name
  application_type    = "web"
}

resource "azurerm_monitor_action_group" "insights" {
  name                = "Becca alert group"
  resource_group_name = var.resource_group_name
  short_name          = "Beccamonitor"

  dynamic "email_receiver" {
    for_each = [for s in var.users : {
      name  = s.name
      email = s.email
    }]

    content {
      name          = email_receiver.value.name
      email_address = email_receiver.value.email
    }
  }
}

resource "azurerm_monitor_scheduled_query_rules_alert" "insights" {
  name                = "Warning with EventId 4001"
  location            = var.location
  resource_group_name = var.resource_group_name

  action {
    action_group = [
      azurerm_monitor_action_group.insights.id
    ]
    email_subject = "Becca warning op production"
  }
  data_source_id = azurerm_application_insights.insights.id
  description    = "Warning with EventId 4001"
  enabled        = true
  query          = <<-QUERY
  traces
  | where severityLevel == 2
  | where customDimensions.EventId == 4001
  | order by timestamp desc
QUERY
  severity       = 3
  frequency      = 5
  time_window    = 5
  trigger {
    operator  = "GreaterThan"
    threshold = 0
  }
}
