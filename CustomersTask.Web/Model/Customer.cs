﻿namespace CustomersTask.Web.Model
{
    public class Customer
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
    }
}
