variable "location" {
  description = "The Azure location where all resources in this example should be created"
  default     = "westeurope"
}

variable "resource_group_name" {
  description = "The name of the resource group"
  default     = "rg_terraform"
}

variable "users" {
  type = list(object({
    name  = string
    email = string
  }))
  default = [
    {
      name  = "Dennis"
      email = "t.d.schreur@dnb.nl"
    }
  ]
}
