using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locations.Shared
{

    public class SearchLocation
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int SearchId { get; set; } 
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? Category { get; set; }
        [ForeignKey("Place")]
        public virtual Place? places { get; set; }
    }
    public class Place
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int PlaceIdentifier { get; set; }

        [NotMapped]
        public virtual List<string>? html_attributions { get; set; }
        public string? html_attr { get; set; }
        [ForeignKey("Result")]
        public virtual List<Result>? results { get; set; }
        public string? status { get; set; }
        public string? error_message { get; set; }
        public string? info_messages { get; set; }
        public string? next_page_token { get; set; }
    }

    public class Result
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int? resultID { get; set; }
        public string? business_status { get; set; }
        [ForeignKey("Geometry")]
        public virtual Geometry? geometry { get; set; }
        public string? icon { get; set; }
        public string? icon_background_color { get; set; }
        public string? icon_mask_base_uri { get; set; }
        public string? name { get; set; }
        [ForeignKey("OpeningHours")]
        public virtual Opening_Hours? opening_hours { get; set; }
        [ForeignKey("Photo")]
        public virtual List<Photo>? photos { get; set; }
        public string? place_id { get; set; }
        [ForeignKey("PlusCode")]
        public virtual Plus_Code? plus_code { get; set; }
        public int? price_level { get; set; }
        public float? rating { get; set; }
        public string? reference { get; set; }
        public string? scope { get; set; }
        [NotMapped]
        public virtual List<string>? types { get; set; }

        public string? categories { get; set; }
        public int? user_ratings_total { get; set; }
        public string? vicinity { get; set; }
    }

    public class Geometry
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int GeometryId { get; set; }

        [ForeignKey("Location")]
        public virtual Location? location { get; set; }

        [ForeignKey("ViewPort")]
        public virtual Viewport? viewport { get; set; }
    }

    public class Location
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int LocationId { get; set; }
        public float? lat { get; set; }
        public float? lng { get; set; }
    }

    public class Viewport
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int ViewPortId { get; set; }
        [ForeignKey("Northeast")]
        public virtual Northeast? northeast { get; set; }
        [ForeignKey("Southwest")]
        public virtual Southwest? southwest { get; set; }
    }

    public class Northeast
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int NortheastId { get; set; }
        public float? lat { get; set; }
        public float? lng { get; set; }
    }

    public class Southwest
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int SouthwestId { get; set; }
        public float? lat { get; set; }
        public float? lng { get; set; }
    }

    public class Opening_Hours
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int OpeningHoursId { get; set; }
        public bool? open_now { get; set; }
    }

    public class Plus_Code
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int PlusCodeId { get; set; }
        public string? compound_code { get; set; }
        public string? global_code { get; set; }
    }

    public class Photo
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int PhotoId { get; set; }
        public int? height { get; set; }
        [NotMapped]
        public virtual List<string>? html_attributions { get; set; }
        public string? photo_reference { get; set; }
        public int? width { get; set; }
    }


}
