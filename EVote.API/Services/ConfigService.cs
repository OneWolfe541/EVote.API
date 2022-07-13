using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using EVote.API.Context;
using EVote.API.Models;
using EVote.API.ViewModels;

namespace EVote.API.Services
{
    public class ConfigService
    {
        public async Task<string> ElectionName(ElectionContext context)
        {
            return await context.Configs.Where(c => c.ConfigSetting == "ElectionName").Select(c => c.ConfigValue).FirstOrDefaultAsync();
        }

        public async Task<string> ElectionDate(ElectionContext context)
        {
            return await context.Configs.Where(c => c.ConfigSetting == "ElectionDate").Select(c => c.ConfigValue).FirstOrDefaultAsync();
        }

        public async Task<string> OrganizationName(ElectionContext context)
        {
            return await context.Configs.Where(c => c.ConfigSetting == "OrganizationName").Select(c => c.ConfigValue).FirstOrDefaultAsync();
        }

        public async Task<string> ElectionDateLong(ElectionContext context)
        {
            return await context.Configs.Where(c => c.ConfigSetting == "ElectionDateLong").Select(c => c.ConfigValue).FirstOrDefaultAsync();
        }
    }
}
