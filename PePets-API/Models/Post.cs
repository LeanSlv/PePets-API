using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PePets_API.Models
{
    public class Post
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public List<string> Images { get; set; }
        public int Cost { get; set; }
        public string PhoneNumber { get; set; }
        //public PetDescription PetDescription { get; set; }
        public string UserId { get; set; }
        //public User User { get; set; }
        public string UserFavoritesId { get; set; }
        //public User UserFavorites { get; set; }
        public string Location { get; set; }
        public int NumberOfLikes { get; set; }
        public int Views { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}
