﻿using MyMovieApp.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMovieApp.ViewModel
{
    public class MovieViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public string? ImageLink { get; set; }
        public string? TrailerLink { get; set; }
        public decimal Price { get; set; }
        public TimeSpan Runtime { get; set; }
        public string Synopsis { get; set; }
        public int Likes { get; set; }
        public int DisLikes { get; set; }
        public string? Format { get; set; }
        public int CertificateId { get; set; }
        public Certificate Certificate { get; set; }
        public ICollection<LanguageViewModel>? Languages { get; set; }
        public ICollection<GenreViewModel>? Genres { get; set; }
        public ICollection<CastViewModel>? Casts { get; set; }
        public ICollection<CinemaViewModel>? Cinamas { get; set; }
        }

    public class LanguageViewModel
    {
        public int LanguageId { get; set; }
        public string Name { get; set; }
    }

    public class GenreViewModel
    {
        public int GenreId { get; set; }
        public string Name { get; set; }
    }

    public class CastViewModel
        {
        public int CastId { get; set; }
        public string CastName { get; set; }
        public string ImageUrl { get; set; }
        }
    public class CinemaViewModel
        {
        public int CinemaId { get; set; }
        public string Name { get; set; }
        public int AddressId { get; set; }
        public string DetaildedAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string? PostalCode { get; set; }

        public ICollection<ShowDetailModel>? ShowDetails { get; set; }
        }
        public class ShowDetailModel
        {
           public DateTime ShowTime { get; set; }
           public int ScreenNumber { get; set; }
        }

    public class HomeMovieViewModel
    {
       public  List<MovieViewModel> Movies { get; set; }
       public  List<LanguageViewModel> Languages { get;set; }
       public List<GenreViewModel> Genres { get;set; }
    }

    public class PaginatedMovieViewModel
    {
        public List<MovieViewModel> Movies { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
    }
}
