using Core.Dto;
using Core.Entities;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.QueryExtension
{
    public static class ArtistQueryExtension
    {

        public static IQueryable<Artist> Where(this IQueryable<Artist> query, SortingDto filter)
        {

            var _filterName = (string)((ArtistSortingDto)filter).GetCriterion()[2];
            var _filterNationality = (string)((ArtistSortingDto)filter).GetCriterion()[3];
            
            if (_filterName != string.Empty)
                query = query.Where(e => e.Name == _filterName);
            if(_filterNationality!= string.Empty)
                query = query.Where(e => e.Nationality == _filterNationality);

            return query;
        }

        public static IQueryable<Artist> Sort(this IQueryable<Artist> query, SortingDto sortingCriterion)
        {
            var criterion= (ArtistSortingCriterion)sortingCriterion.GetCriterion()[0];
            var order= (SortingOrder)sortingCriterion.GetCriterion()[1];


            switch (criterion)
            {
                case ArtistSortingCriterion.None:
                    {
                        if (order == SortingOrder.Ascending)
                            return query.OrderBy(e => e.Id);
                        else
                            return query.OrderByDescending(e => e.Id);
                    }

                case ArtistSortingCriterion.ByName:
                    {
                        if (order == SortingOrder.Ascending)
                            return query.OrderBy(e => e.Name);
                        else
                            return query.OrderByDescending(e => e.Name);
                    }

                case ArtistSortingCriterion.ByNationality:
                    {
                        if (order == SortingOrder.Ascending)
                            return query.OrderBy(e => e.Nationality);
                        else
                            return query.OrderByDescending(e => e.Nationality);
                    }

                default: return query;
            }
        }
    }
}
