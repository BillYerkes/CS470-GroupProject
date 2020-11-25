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
    public class LookUpTablesController : Controller
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
        public LookUpTablesController(MvcCanoeContext context, UserManager<ApplicationUser> userManager,
                                ILogger<LookUpTablesController> logger)
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

        public async Task<IActionResult> ClassList(int v_intPageNumber = 1)
        {
            try
            {
                IQueryable<Classes> l_rsClasses;

                l_rsClasses = from m in _context.classes.FromSql("Call GetClasses()") select m;

                return View(await PaginatedList<Classes>.CreateAsync(l_rsClasses, v_intPageNumber, m_intPageSize));
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

        public async Task<IActionResult> ColorList(int v_intPageNumber = 1)
        {
            try
            {
                IQueryable<Colors> l_rsColors;

                l_rsColors = from m in _context.colors.FromSql("Call GetColors()") select m;

                return View(await PaginatedList<Colors>.CreateAsync(l_rsColors, v_intPageNumber, m_intPageSize));
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

        public async Task<IActionResult> DeckFormatList(int v_intPageNumber = 1)
        {
            try
            {
                IQueryable<DeckFormats> l_rsDeckFormats;

                l_rsDeckFormats = from m in _context.deckformats.FromSql("Call GetDeckFormats()") select m;

                return View(await PaginatedList<DeckFormats>.CreateAsync(l_rsDeckFormats, v_intPageNumber, m_intPageSize));
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

        public async Task<IActionResult> MatchTypeList(int v_intPageNumber = 1)
        {
            try
            {
                IQueryable<MatchTypes> l_rsMatchTypes;

                l_rsMatchTypes = from m in _context.matchtypes.FromSql("Call GetMatchTypes()") select m;

                return View(await PaginatedList<MatchTypes>.CreateAsync(l_rsMatchTypes, v_intPageNumber, m_intPageSize));
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

        public async Task<IActionResult> RarityList(int v_intPageNumber = 1)
        {
            try
            {
                IQueryable<Rarity> l_rsRarity;

                l_rsRarity = from m in _context.rarity.FromSql("Call GetRarities()") select m;

                return View(await PaginatedList<Rarity>.CreateAsync(l_rsRarity, v_intPageNumber, m_intPageSize));
            }
            catch (Exception ex)
            {
            _logger.LogError($"Something went wrong: {ex}");
            return StatusCode(500, "Internal server error");
            }
        }

        //**********************************************************************************************************
        // RarityEdit   Edit the Name of the Movie
        //
        // Inputs:
        // string v_strTitle             Name of the Network so we can reset the title to the page
        // int v_intPageNumber           current page in the list so we can come back to the same spot
        // int v_intServiceCD            The service CD value so we can create the same filter
        // int v_intID = 0               ID of record
        // string v_strDescription = ""  Name of Movie
        // string v_strAction = ""       Prep - setting up the view or Update - processing the change
        //**********************************************************************************************************
        [HttpPost]
        public async Task<IActionResult> RarityEdit( int v_intPageNumber, 
                                                     int v_intID = 0, 
                                                     string v_strName = "",
                                                     string v_strAction = "")
        {
            try
            {
                //create the object for the edit view
                var l_clsRarityEdit = new RarityEdit();

                l_clsRarityEdit.ID = v_intID;
                l_clsRarityEdit.pageNumber = v_intPageNumber;


                if (v_strAction == "Prep")
                {
                    //need and id to edit
                    if (v_intID == 0)
                    {
                        return NotFound();
                    }

                    //check to see if the id exist in the db
                    var l_clsRarity = await _context.rarity.FindAsync(v_intID);
                    if (l_clsRarity == null)
                    {
                        return NotFound();
                    }

                    //Update field with value from database
                    l_clsRarityEdit.Name = l_clsRarity.Name;

                    return View(l_clsRarityEdit);
                }
                else if (v_strAction == "Update")
                {
                    try
                    {
                        if (ModelState.IsValid)
                        {
                            var l_clsRarity = new Rarity();
                            l_clsRarity.ID = v_intID;
                            l_clsRarity.Name = v_strName;

                            _context.Update(l_clsRarity);
                            await _context.SaveChangesAsync();

                            return RedirectToAction("RarityList");
                        }

                        return View(l_clsRarityEdit);
                    }

                    catch (DbUpdateConcurrencyException e)
                    {
                        ModelState.AddModelError("", e.InnerException.Message);
                        return View(l_clsRarityEdit);
                    }
                    catch (DbUpdateException e)
                    {
                        if ((uint)e.HResult == 0x80131500) //duplicate record
                        {
                            ModelState.AddModelError("", "The rarity: " + l_clsRarityEdit.Name + ", is already in the system.");
                        }
                        else
                        {
                            ModelState.AddModelError("", e.InnerException.Message);
                        }
                        return View(l_clsRarityEdit);
                    }
                }
                else
                {
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
        // RarityEdit   Edit the Name of the Movie
        //
        // Inputs:
        // string v_strTitle             Name of the Network so we can reset the title to the page
        // int v_intPageNumber           current page in the list so we can come back to the same spot
        // int v_intServiceCD            The service CD value so we can create the same filter
        // int v_intID = 0               ID of record
        // string v_strDescription = ""  Name of Movie
        // string v_strAction = ""       Prep - setting up the view or Update - processing the change
        //**********************************************************************************************************
        [HttpPost]
        public async Task<IActionResult> RarityCreate(int v_intPageNumber,
                                                     int v_intID = 0,
                                                     string v_strName = "",
                                                     string v_strAction = "")
        {
            try
            {
                //create the object for the edit view
                var l_clsEdit = new RarityEdit();

                l_clsEdit.pageNumber = v_intPageNumber;

                if (v_strAction == "Prep")
                {
                    return View(l_clsEdit);
                }
                else if (v_strAction == "Update")
                {
                    try
                    {
                        if (ModelState.IsValid)
                        {
                            var l_clsUpdate = new Rarity();
                            l_clsUpdate.Name = v_strName;

                            _context.Update(l_clsUpdate);
                            await _context.SaveChangesAsync();

                            return RedirectToAction("RarityList");
                        }

                        return View(l_clsEdit);
                    }

                    catch (DbUpdateConcurrencyException e)
                    {
                        ModelState.AddModelError("", e.InnerException.Message);
                        return View(l_clsEdit);
                    }
                    catch (DbUpdateException e)
                    {
                        if ((uint)e.HResult == 0x80131500) //duplicate record
                        {
                            ModelState.AddModelError("", "The rarity: " + l_clsEdit.Name + ", is already in the system.");
                        }
                        else
                        {
                            ModelState.AddModelError("", e.InnerException.Message);
                        }
                        return View(l_clsEdit);
                    }
                }
                else
                {
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

        public async Task<IActionResult> TribeList(int v_intPageNumber = 1)
        {
            try
            {
                IQueryable<Tribes> l_rsTribes;

                l_rsTribes = from m in _context.tribes.FromSql("Call GetTribes()") select m;

                return View(await PaginatedList<Tribes>.CreateAsync(l_rsTribes, v_intPageNumber, m_intPageSize));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}