﻿namespace MyMovieApp.Models
    {
    public class Address
        {
        public int AddressId { get; set; }
        public string DetaildedAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string? PostalCode { get; set; }

        }
    }
