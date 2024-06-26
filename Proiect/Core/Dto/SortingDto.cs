using Core.Enums;
using System.Reflection.Metadata.Ecma335;

namespace Core.Dto
{
    public class SortingDto
    {
        public virtual List<object> GetCriterion() { return null; }
    }
    public class ArtistSortingDto:SortingDto
    {
        public ArtistSortingCriterion Property { get; set; } = ArtistSortingCriterion.None;
        public SortingOrder SortingOrder { get; set; } =  SortingOrder.None;
        public string FilterName { get; set; } 
        public string FilterNationality { get; set; } 
        public override List<object> GetCriterion()
        {
            return [Property,SortingOrder,FilterName,FilterNationality];
        }
    }
    public class ArtworkSortingDto: SortingDto
    {
        public ArtworkSortingCriterion Property { get; set; } = ArtworkSortingCriterion.None;
        public SortingOrder SortingOrder { get; set; } = SortingOrder.None;
        public string FilterName {  get; set; } 
        public int? FilterYear { get; set; } 
        public int? FilterArtist {  get; set; } 
        public override List<object> GetCriterion()
        {
            return [Property, SortingOrder,FilterName,FilterYear,FilterArtist];
        }
    }
}
