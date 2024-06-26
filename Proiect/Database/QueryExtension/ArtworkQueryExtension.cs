using Core.Dto;
using Core.Entities;
using Core.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.QueryExtension
{
    public static class ArtworkQueryExtension
    {

        public static IQueryable<Artwork> Where(this IQueryable<Artwork> query, SortingDto filter)
        {

            var _filterName = (string)((ArtworkSortingDto)filter).GetCriterion()[2];
            var _filterYear = (int?)((ArtworkSortingDto)filter).GetCriterion()[3];
            var _filterArtist = (int?)((ArtworkSortingDto)filter).GetCriterion()[3];

            if (_filterName != string.Empty)
                query = query.Where(e => e.Title == _filterName);
            if (_filterYear.HasValue)
                query = query.Where(e => e.Year == _filterYear);
            if (_filterArtist.HasValue)
                query = query.Where(e => e.ArtistId == _filterArtist);

            return query;
        }

        public static IQueryable<Artwork> Sort(this IQueryable<Artwork> query, SortingDto sortingCriterion)
        {
            var criterion = (ArtworkSortingCriterion)sortingCriterion.GetCriterion()[0];
            var order = (SortingOrder)sortingCriterion.GetCriterion()[1];


            switch (criterion)
            {
                case ArtworkSortingCriterion.None:
                    {
                        if (order == SortingOrder.Ascending)
                            return query.OrderBy(x => x.Id);
                        else
                            return query.OrderByDescending(e => e.Id);
                    }

                case ArtworkSortingCriterion.ByTitle:
                    {
                        if (order == SortingOrder.Ascending)
                            return query.OrderBy(e => e.Title);
                        else
                            return query.OrderByDescending(e => e.Title);
                    }

                case ArtworkSortingCriterion.ByArtistId:
                    {
                        if (order == SortingOrder.Ascending)
                            return query.OrderBy(e => e.ArtistId);
                        else
                            return query.OrderByDescending(e => e.ArtistId);
                    }

                default: return query;
            }
        }
    }
}
