using System;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Canoe.Data;
using Canoe.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Canoe.Controllers
{
    public class PlayerController : Controller
    {

        private MvcCanoeContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;
        private int m_intPageSize = 10;

        //**********************************************************************************************************
        // LookUpTablesController
        //
        //
        // Inputs:   None
        //**********************************************************************************************************
        public PlayerController(MvcCanoeContext context, UserManager<ApplicationUser> userManager,
                                ILogger<PlayerController> logger)
        {
            try
            {
                _context = context;
                _userManager = userManager;
                _logger = logger;
            }
            catch
            {

            }
        }

        //**********************************************************************************************************
        // RarityList     List of All Rarities
        //
        // Inputs:   
        // int v_intPageNumber = 1              Used for pagination
        //**********************************************************************************************************

        public async Task<IActionResult> PlayerList(int v_intPageNumber = 1)
        {
            try
            {
                IQueryable<Player> l_rsPlayerList;

                l_rsPlayerList = from m in _context.GetPlayerList.FromSql("Call GetPlayerList()") select m;

                return View(await PaginatedList<Player>.CreateAsync(l_rsPlayerList, v_intPageNumber, m_intPageSize));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        //**********************************************************************************************************
        // RarityList     List of All Rarities
        //
        // Inputs:   
        // int v_intPageNumber = 1              Used for pagination
        //**********************************************************************************************************

        public async Task<IActionResult> PlayerCollectionList(int v_intPlayerID, int v_intPageNumber = 1)
        {
            try
            {
                IQueryable<PlayerCollection> l_rsPlayerCollectionList;

                l_rsPlayerCollectionList = from m in _context.GetPlayerCollection.FromSql("Call GetPlayerCollection({0})", v_intPlayerID) select m;

                return View(await PaginatedList<PlayerCollection>.CreateAsync(l_rsPlayerCollectionList, v_intPageNumber, m_intPageSize, v_intPlayerID));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}