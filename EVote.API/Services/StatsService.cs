using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using EVote.API.Context;
using EVote.API.Extensions;
using EVote.API.Models;
using EVote.API.Models.Data;
using EVote.API.Models.Stats;
using EVote.API.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EVote.API.Services
{
    public class StatsService
    {
        public async Task<List<ChartStatsModel>> VoterActivity(string connection)
        {
            var context = new ElectionContext(connection);
            if (context == null)
                throw new Exception();

            var query = from voterActivity in context.VoterActivities
                        join location in context.Locations
                            on voterActivity.LocationId equals location.LocationId into votedlocationgroup
                        from votedLocation in votedlocationgroup.DefaultIfEmpty()
                        where voterActivity.StatusId == 11 || voterActivity.StatusId == 12
                        select new
                        {
                            votedLocation.LocationName,
                            voterActivity.StatusId
                        };

            return await query
                         .GroupBy(g => g.LocationName)
                         .Select(v => new ChartStatsModel
                         {
                             CategoryName = v.Key,
                             Total = v.Count()
                         })
                         .ToListAsync();
        }

        public async Task<List<ChartStatsModel>> ElectionActivity(string connection)
        {
            var context = new ElectionContext(connection);
            if (context == null)
                throw new Exception();

            var query = from voterActivity in context.VoterActivities
                        join status in context.Statuses
                            on voterActivity.StatusId equals status.StatusId into votedstatusgroup
                        from votedStatus in votedstatusgroup.DefaultIfEmpty()
                        where voterActivity.StatusId > 1
                        select new
                        {
                            votedStatus.StatusDescription,
                            voterActivity.StatusId
                        };

            return await query
                         .GroupBy(g => g.StatusDescription)
                         .Select(v => new ChartStatsModel
                         {
                             CategoryName = v.Key,
                             Total = v.Count()
                         })
                         .ToListAsync();
        }

        public async Task<VoterCountsModel> VoterCounts(string connection)
        {
            var context = new ElectionContext(connection);
            if (context == null)
                throw new Exception();

            VoterCountsModel voterCounts = new VoterCountsModel();

            voterCounts.TotalVoters = await context.Voters.CountAsync();

            voterCounts.ActiveVoters = await context.VoterActivities
                .Where(v => v.StatusId == 11 || v.StatusId == 12)
                .CountAsync();

            return voterCounts;
        }

        public async Task<NumStatsModel> MaxBarCode(string connection)
        {
            var context = new ElectionContext(connection);
            if (context == null)
                throw new Exception();

            NumStatsModel barcode = new NumStatsModel();
            barcode.Value = await context.VoterActivities.MaxAsync(b => b.BarCode);

            return barcode;
        }
    }
}
