variable "sql_admin_user" {
  description = "SQL Admin Username"
  type        = string
  default     = "sqladmin"
}

variable "sql_admin_password" {
  description = "SQL Admin Password"
  type        = string
  sensitive   = true
}
variable "subscription_id" {
  description = "70cabe69-f7c3-46a0-be65-ea3a0934ee91"
  type        = string
  sensitive   = true
}