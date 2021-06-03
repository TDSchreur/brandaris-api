variable "location" {
  description = "The Azure location where all resources in this example should be created"
}

variable "resource_group_name" {
  description = "The name of the resource group"
}

variable "users" {
  type = list(object({
    name  = string
    email = string
  }))
}
