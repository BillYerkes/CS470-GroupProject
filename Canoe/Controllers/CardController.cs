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

        public async Task<IActionResult> CardList(string v_strFilter = "", int v_intPageNumber = 1)
        {
            try
            {
                IQueryable<Cards> l_rsCardList;
                if (v_strFilter == null)
                    v_strFilter = "";

                if (v_strFilter == "" ) 
                    l_rsCardList = from m in _context.GetCardList.FromSql("Call GetCardList()") select m;
                else
                    l_rsCardList = from m in _context.GetCardList.FromSql("Call GetCardListFiltered({0})", v_strFilter) select m;

                return View(await PaginatedList<Cards>.CreateAsync(l_rsCardList, v_intPageNumber, m_intPageSize, v_strFilter));
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
                    IQueryable<Cards> l_rsCardList;

                    l_rsCardList = from m in _context.GetCardList.FromSql("Call GetSetCardList({0})", v_intSetID) select m;

                    return View(await PaginatedList<Cards>.CreateAsync(l_rsCardList, v_intPageNumber, m_intPageSize, v_intSetID));
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

        public async Task<IActionResult> CardDetail(int v_intCardID, int v_intPageNumber = 1)
        {
            try
            {
                IQueryable<CardViewComplete> l_rsCardView;

                l_rsCardView = from m in _context.GetCardViews.FromSql("Call GetCardView({0})", v_intCardID) select m;

                IQueryable<Classes> l_rsClasses;
                IQueryable<Colors> l_rsColors;
                IQueryable<Tribes> l_rsTribes;

                string l_strClasses;
                string l_strColors;
                string l_strTribes;

                l_rsClasses = from m in _context.classes.FromSql("Call GetCardClasses({0})", v_intCardID) select m;
                l_rsColors = from m in _context.colors.FromSql("Call GetCardColors({0})", v_intCardID) select m;
                l_rsTribes = from m in _context.tribes.FromSql("Call GetCardTribes({0})", v_intCardID) select m;

                l_strClasses = "";
                l_strColors = "";
                l_strTribes = "";

                foreach (var c in l_rsClasses)
                {
                    l_strClasses = l_strClasses + c.Name + " ";
                }

                foreach (var c in l_rsColors)
                {
                    l_strColors = l_strColors + c.Name + " ";
                }

                foreach (var c in l_rsTribes)
                {
                    l_strTribes = l_strTribes + c.Name + " ";
                }

                foreach (var v in l_rsCardView)
                {
                    v.Classes = l_strClasses;
                    v.Colors = l_strColors;
                    v.Tribes = l_strTribes;
                }


                return View(await PaginatedList<CardViewComplete>.CreateAsync(l_rsCardView, 1, 1));

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