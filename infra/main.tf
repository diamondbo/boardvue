provider "azurerm" {
  features {
  }
  subscription_id = var.subscription_id
}
resource "azurerm_resource_group" "main" {
    name = "myapp-rg"
    location = "eastus"
}
resource "azurerm_app_service_plan" "main" {
    name = "myapp-plan"
    location = azurerm_resource_group.main.location
    resource_group_name = azurerm_resource_group.main.name
    kind = "Linux"
    sku {
      tier = "Free"
      size = "F1"
    }
    reserved = true
}
resource "azurerm_linux_web_app" "api" {
  name = "myapp-backend-api"
  location = azurerm_resource_group.main.location
  resource_group_name = azurerm_resource_group.main.name
  service_plan_id = azurerm_app_service_plan.main.id
  site_config {
    application_stack {
      dotnet_version = "9.0"
    }
  }
  app_settings = {
    "WEBSITES_ENABLE_APP_SERVICE_STORAGE" = "false"
  }
  connection_string {
    name = "myappdb"
    type = "SQLAzure"
    value = "Server=tcp:myappdb.database.windows.net,1433;Initial Catalog=myappdb;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
  }
}

resource "azurerm_mssql_server" "sql" {
  name = "myappdbserver"
  location = azurerm_resource_group.main.location
  resource_group_name = azurerm_resource_group.main.name
  version = "12.0"
  administrator_login = var.sql_admin_user
  administrator_login_password = var.sql_admin_password

}
resource "azurerm_mssql_database" "db" {
  name = "myappdb"
  server_id = azurerm_mssql_server.sql.id
  sku_name = "S1"
  zone_redundant = false
  collation = "SQL_Latin1_General_CP1_CI_AS"
}

resource "azurerm_mssql_firewall_rule" "allow_azure_services" {
  name = "AllowAzureServices"
  server_id = azurerm_mssql_server.sql.id
  start_ip_address = "0.0.0.0"
  end_ip_address = "0.0.0.0"
}