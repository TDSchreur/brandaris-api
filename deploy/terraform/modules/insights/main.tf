resource "azurerm_application_insights" "insights" {
  name                = "insights_terraform"
  location            = var.location
  resource_group_name = var.resource_group_name
  application_type    = "web"
}

resource "azurerm_monitor_action_group" "alert_group" {
  name                = "Brandaris alert group"
  resource_group_name = var.resource_group_name
  short_name          = "B-monitor"

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

resource "azurerm_monitor_scheduled_query_rules_alert" "alert_warning" {
  name                = "Warning with EventId 4001"
  location            = var.location
  resource_group_name = var.resource_group_name

  action {
    action_group = [
      azurerm_monitor_action_group.alert_group.id
    ]
    email_subject = "Brandaris warning op production"
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

resource "azurerm_monitor_scheduled_query_rules_alert" "alert_error" {
  name                = "Error"
  location            = var.location
  resource_group_name = var.resource_group_name

  action {
    action_group = [
      azurerm_monitor_action_group.alert_group.id
    ]
    email_subject = "Brandaris error op ${var.environment}"
  }
  data_source_id = azurerm_application_insights.insights.id
  description    = "Warning with EventId 4001"
  enabled        = true
  query          = <<-QUERY
  traces
  | where severityLevel == 3
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
