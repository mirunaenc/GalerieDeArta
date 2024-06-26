using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enums
{
    public enum ArtistSortingCriterion
    {
        None = 0,
        ByName = 1,
        ByNationality = 2    
    }
    public enum ArtworkSortingCriterion
    {
        None = 0,
        ByTitle=1,
        ByYear =2,
        ByArtistId=3
    }
}
