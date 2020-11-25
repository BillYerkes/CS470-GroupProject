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
    public class CardController : Controller
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
        public CardController(MvcCanoeContext context, UserManager<ApplicationUser> userManager,
                                ILogger<CardController> logger)
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

        public async Task<IActionResult> CardList(int v_intPageNumber = 1)
        {
            try
            {
                IQueryable<Cards> l_rsCardList;

                l_rsCardList = from m in _context.GetCardList.FromSql("Call GetCardList()") select m;

                return View(await PaginatedList<Cards>.CreateAsync(l_rsCardList, v_intPageNumber, m_intPageSize));
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

        public async Task<IActionResult> CardSetList(int v_intSetID, int v_intPageNumber = 1)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {// user needs to be logged in
                    IQueryable<Cards> l_rsCardList;

                    l_rsCardList = from m in _context.GetCardList.FromSql("Call GetSetCardList({0})", v_intSetID) select m;

                    return View(await PaginatedList<Cards>.CreateAsync(l_rsCardList, v_intPageNumber, m_intPageSize, v_intSetID));
                }
                else
                { //should not get here but just in case
                    return NotFound();
                }
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

        public async Task<IActionResult> CardDetail(int v_intPageNumber = 1)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {// user needs to be logged in
                    IQueryable<CardView> l_rsCardView;

                    l_rsCardView = from m in _context.GetCardViews.FromSql("Call GetCardView({0})", 6) select m;
                    
                    return View(await PaginatedList<CardView>.CreateAsync(l_rsCardView, v_intPageNumber, m_intPageSize));
                }
                else
                { //should not get here but just in case
                    return NotFound();
                }
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

        public async Task<IActionResult> SetList(int v_intPageNumber = 1)
        {
            try
            {
                IQueryable<Sets> l_rsSets;

                l_rsSets = from m in _context.sets.FromSql("Call GetSets()") select m;

                return View(await PaginatedList<Sets>.CreateAsync(l_rsSets, v_intPageNumber, m_intPageSize));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}